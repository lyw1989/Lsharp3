using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace AnnouncerPack
{
    class Program
    {
        static List<KillData> Data = new List<KillData>(); 

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                Data.Add(new KillData(hero));
            }

            Game.OnUpdate += Game_OnUpdate;
        }

        static void Game_OnUpdate(EventArgs args)
        {
            onKill();
        }

        private static void onKill()
        {
            foreach (var unit in Data)
            {
                var champ = unit.hero;
                foreach (var KillData in Data)
                {
                    if (champ.Name.Equals(KillData.hero.Name))
                    {
                        if (champ.ChampionsKilled != KillData.getKills())
                        {
                            KillData.addKill();
                            
                        }
                    }
                }
            }
        }
    }
}
