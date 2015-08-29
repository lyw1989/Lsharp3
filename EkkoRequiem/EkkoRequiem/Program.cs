using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace EkkoRequiem
{
    class Program
    {
        static Obj_AI_Hero Player = ObjectManager.Player;
        private static Obj_GeneralParticleEmitter EkkoUlt;
        private static Orbwalking.Orbwalker orbwalker;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Load_OnLoad;
        }

        static void Load_OnLoad(EventArgs args)
        {
            if (Player.ChampionName != "Ekko")
                return;
            SpellManager.Initialize();
            Config.Initialize();

            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;

            orbwalker = new Orbwalking.Orbwalker(Config.orbwalkMenu);

            foreach (var ult in ObjectManager.Get<Obj_GeneralParticleEmitter>())
            {
                if (ult.Name.Equals("Ekko_Base_R_TrailEnd.troy"))
                    EkkoUlt = ult;
            }

        }



        public static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var particle = sender as Obj_GeneralParticleEmitter;
            if (EkkoUlt != null && particle != null)
            {
                if (particle.Name.Equals("Ekko_Base_R_TrailEnd.troy"))
                {
                    EkkoUlt = null;
                }
            }
        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var particle = sender as Obj_GeneralParticleEmitter;
            if (EkkoUlt == null && particle != null)
            {
                if (particle.Name.Equals("Ekko_Base_R_TrailEnd.troy"))
                {
                    EkkoUlt = particle;
                }
            }
        }


        static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling() || args == null)
                return;

            Killsteal();

            switch (orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    var units = ObjectManager.Get<Obj_AI_Base>().Where(monster => monster.Team != Player.Team && monster.Distance(Player) < 1000 && !monster.Name.Contains("trinket") && !monster.Name.Contains("ward")).OrderBy(unit => unit.Distance(Player)).ToList();
                    var target = units.FirstOrDefault();
                    if (target != null && target.Team == GameObjectTeam.Neutral) Jungle(units);
                    else LaneClear(units);
                    break;
            }
        }

        private static void Jungle(List<Obj_AI_Base> units)
        {
            if (Player.ManaPercent < Config.JungleMana)
                return;
            var jungle = units.Where(unit => unit.Team == GameObjectTeam.Neutral).ToList();

            if (Config.JungleQ) SpellManager.Q.Cast(SpellManager.Q.GetLineFarmLocation(jungle).Position);
            if (Config.JungleW) SpellManager.W.Cast(SpellManager.W.GetCircularFarmLocation(jungle).Position);
            if (Config.JungleE) SpellManager.CastE(jungle.OrderBy(unit => unit.Health).FirstOrDefault());
        }

        static void Combo()
        {
            var enemys =
                ObjectManager.Get<Obj_AI_Hero>()
                    .Where(
                        enemy =>
                            enemy.IsEnemy && enemy.Distance(Player) <= 800 && enemy.IsValidTarget())
                    .OrderBy(enemy => CalcDamage(enemy) / enemy.Health).ToList();

            if(!enemys.Any())return;

            var t = enemys.Last();


            //finding the best target to cast on by finding out which enemy the combo will be most effective on

            if (t == null) return;

            if (Config.ComboQ && t.Distance(Player) < 800) SpellManager.Q.Cast(t);

            if (Config.ComboW) SpellManager.W.Cast(t);

            if (Config.ComboE) SpellManager.CastE(t);

            if (EkkoUlt == null || !Config.ComboR)
                return;

            var targets =
                ObjectManager.Get<Obj_AI_Hero>()
                    .Where(enemy => enemy.IsEnemy && enemy.Distance(EkkoUlt.Position) < SpellManager.R.Range).ToList();

            if(!targets.Any())return;
            if (targets.Count(enemy => ((enemy.Health - Rdamage()) / enemy.MaxHealth <= Config.ComboRPercent)) >= Config.ComboRCount) SpellManager.R.Cast();

        }


        public static double CalcDamage(Obj_AI_Hero enemy)
        {
            double damage = 0;
            if (enemy.GetBuffCount("EkkoStacks") == 2)
                damage += 10 + 10 * Player.Level + 0.7 * Player.TotalMagicalDamage;
            if (SpellManager.Q.IsReady())
                damage += 45 + SpellManager.Q.Level * 15 + Player.TotalMagicalDamage * 0.1;
            if (SpellManager.E.IsReady() || Player.HasBuff("ekkoeattackbuff"))
                damage += 20 + SpellManager.E.Level * 30 + Player.TotalMagicalDamage() * 0.2;
            if (EkkoUlt != null)
                if (enemy.Distance(EkkoUlt.Position) <= SpellManager.R.Range)
                    damage += Rdamage();

            return Player.CalcDamage(enemy, Damage.DamageType.Magical, damage);
        }


        private static double Rdamage()
        {
            return 50 + SpellManager.R.Level * 150 + 1.3 * Player.TotalMagicalDamage;
        }

        static void Harass()
        {
            if (Player.ManaPercent < Config.HarassMana)
                return;

            var t = TargetSelector.GetTarget(SpellManager.Q.Range, TargetSelector.DamageType.Magical);
            if (t == null) return;

            if (Config.HarassQ) SpellManager.Q.Cast(t);

        }

        /*


        static void LastHit()
        {
            if (SpellManager.Q.IsReady() && Config.LastHitQ)
            {
                var minion =
                    ObjectManager.Get<Obj_AI_Base>()
                        .Where(unit => unit.IsMinion && unit.IsEnemy && unit.Distance(Player) < SpellManager.Q.Range).OrderBy(unit => unit.Health).FirstOrDefault();
                
                if (minion!=null)
                    SpellManager.Q.Cast(minion);
                
            }
        }
         * */

        static void LaneClear(List<Obj_AI_Base> units)
        {
            if (Player.ManaPercent < Config.ClearMana)
                return;
            var laneclear = units.Where(unit => unit.Team != GameObjectTeam.Neutral).ToList();
            if (Config.LaneClearQ) SpellManager.Q.Cast(SpellManager.Q.GetLineFarmLocation(laneclear).Position);
            if (Config.LaneClearE) SpellManager.CastE(laneclear.OrderBy(unit => unit.Health).FirstOrDefault());
        }



        //todo:Add KillSteal

        static void Killsteal()
        {
            if (Config.KillStealR && EkkoUlt != null)
            {
                if (EkkoUlt != null)
                    foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
                    {
                        if (hero.IsEnemy && hero.Distance(EkkoUlt.Position) < SpellManager.R.Range && hero.Health <= Rdamage() && hero.IsValidTarget() && !hero.IsDead)
                            SpellManager.R.Cast();
                    }
            }
            if (Config.KillStealQ)
            {
                foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
                {
                    if (hero.IsEnemy && hero.Distance(Player) < 800 && hero.Health <= Player.GetDamageSpell(hero, SpellSlot.Q).CalculatedDamage)
                        SpellManager.Q.Cast();
                }
            }

            if (Config.KillStealE)
            {
                foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
                {
                    var damage = 20 + 30 * SpellManager.E.Level + 0.2 * Player.TotalMagicalDamage();
                    if (hero.GetBuffCount("EkkoStacks") == 2)
                        damage += 10 + 10 * Player.Level + 0.7 * Player.TotalMagicalDamage;

                    if (hero.IsEnemy && hero.Distance(Player) <= 700 && hero.Health <= Player.CalcDamage(hero, Damage.DamageType.Magical, damage))
                        SpellManager.CastE(hero);
                }
            }
        }
        static void Drawing_OnDraw(EventArgs args)
        {
            if (Config.DrawQ)
                Drawing.DrawCircle(Player.Position, SpellManager.Q.Range, Color.Yellow);
            if (Config.DrawW)
                Drawing.DrawCircle(Player.Position, SpellManager.W.Range, Color.Yellow);

            if (Config.DrawRPos)
            {
                if (EkkoUlt != null)
                    Drawing.DrawCircle(EkkoUlt.Position, 400, Color.Crimson);
            }
        }
    }
}