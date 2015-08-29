using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;

namespace AnnouncerPack
{
    class KillData
    {
        public Obj_AI_Hero hero;
        List<int> kills = new List<int>();
        private SoundPlayer myPlayer;

        public KillData(Obj_AI_Hero hero)
        {
            this.hero = hero;
        }

        public int getKills()
        {
            return kills.Count();

        }

        public void addKill()
        {
            kills.Add(1);
        }

        public void calcKills()
        {
            var count = getKills();
            kills.Clear();
            switch (count)
            {
                case 0:
                    return;
                case 1:
                    myPlayer = new SoundPlayer("Kill");
                    myPlayer.Play();
                    break;
                case 2:
                    myPlayer = new SoundPlayer("DKill");
                    myPlayer.Play();
                    break;
                case 3:
                    myPlayer = new SoundPlayer("Tkill");
                    myPlayer.Play();
                    break;
                case 4:
                    myPlayer = new SoundPlayer("Qkill");
                    myPlayer.Play();
                    break;
                case 5:
                    myPlayer = new SoundPlayer("Pkill");
                    myPlayer.Play();
                    break;
            }
        }
    }
}
