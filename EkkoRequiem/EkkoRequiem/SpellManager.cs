using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace EkkoRequiem
{
    class SpellManager
    {
        private static Obj_AI_Hero Player = ObjectManager.Player;

        private static Spell _Q, _W, _E, _R;

        public static Spell Q { get { return _Q; } }
        public static Spell W { get { return _W; } }
        public static Spell E { get { return _E; } }
        public static Spell R { get { return _R; } }

        public static void Initialize()
        {
            Console.WriteLine("start");
            //TODO:UPDATE VALUES OF PREDICTION
            _Q = new Spell(SpellSlot.Q, 1000);

            Q.SetSkillshot(0.25f, 60f, 1650f, false, SkillshotType.SkillshotLine);

            _W = new Spell(SpellSlot.W, 1600);

            W.SetSkillshot(0.5f, 500f, 1000f, false, SkillshotType.SkillshotCircle);

            _E = new Spell(SpellSlot.E, 700);

            _R = new Spell(SpellSlot.R, 375);
        }




        public static void CastR(Obj_AI_Hero target, Spell spell, Obj_GeneralParticleEmitter ekkoUlt)
        {

            //            if (count < Config.ComboRHit)return;

            //todo: add logic for if ult is effective:
            //todo: 1. will save my life
            //todo: 2. will get me closer for a kill

        }

        public static void CastE(Obj_AI_Base target)
        {
            if (E.IsReady() && Player.Distance(target) < 700) //fix the range
                E.Cast(target.ServerPosition);
            else
            {
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
            }
        }


    }
}