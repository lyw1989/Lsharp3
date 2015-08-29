using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace RickRoll
{
    internal class Program
    {
        public static Menu Config;

        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Rick_Roll[] Troll;
        private static List<Obj_AI_Hero> enemys;
        private static int myKills = 0;
        private static int lastMessage = 0;




        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
           
            Config = new Menu("Rick Roll", "Rick Roll", true);
            Config.AddItem(new MenuItem("Rick", "Rick Roll").SetValue(true));
            Config.AddItem(new MenuItem("myKills", "Only My Kills?").SetValue(true));
            Config.AddItem(new MenuItem("percent", "chance to rickRoll").SetValue(new Slider(100)));
            Config.AddItem(new MenuItem("delay", "delay to shout").SetValue(new Slider(1500,0,3000)));
            

            Config.AddToMainMenu();
            Troll = new Rick_Roll[ObjectManager.Get<Obj_AI_Hero>().Count(enemy => enemy.IsEnemy)];
           

            int count = 0;
            enemys = new List<Obj_AI_Hero>();
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (hero.IsEnemy)
                {
                    Troll[count++] = new Rick_Roll(hero);
                    enemys.Add(hero);
                }
            }
            Game.OnUpdate += Game_OnUpdate;
        }




        private static void Game_OnUpdate(EventArgs args)
        {

            if (Config.Item("myKills").GetValue<bool>())
            {
                if (Player.ChampionsKilled == myKills)
                {
                    return;
                }
                myKills = Player.ChampionsKilled;
            }

            int chance = new Random().Next(1, 100);
            if (chance > Config.Item("percent").GetValue<Slider>().Value)
                return;

            if (!Config.Item("Rick").GetValue<bool>())
                return;
            
            foreach (var enemy in enemys)
            {
                if (enemy.IsEnemy)
                {
                    foreach (Rick_Roll Rick in Troll)
                    {
                        if (enemy.Name.Equals(Rick.Enemy))
                        {
                            if (enemy.Deaths > Rick.deaths)
                            {
                                Utility.DelayAction.Add(Config.Item("delay").GetValue<Slider>().Value, sayRoll);
                                Rick.deaths = enemy.Deaths;
                            }
                        }
                    }
                }
            }
        }

        private static void sayRoll()
        {
            if(lastMessage + 1500 > Environment.TickCount)return;
            var random = new Random().Next(0, 11);
            switch (random)
            {
                case 0:
                    Game.Say("/all Never gonna make you cry");
                    break;
                case 1:
                    Game.Say("/all We're no strangers to love You know the rules and so do I");
                    break;
                case 2:
                    Game.Say("/all A full commitment's what I'm thinking of You wouldn't get this from any other guy");
                    break;
                case 3:
                    Game.Say("/all I just wanna tell you how I'm feeling Gotta make you understand");
                    break;
                case 4:
                    Game.Say("/all We know the game and we're gonna play it");
                    break;
                case 5:
                    Game.Say("/all Never gonna give you up, Never gonna let you down");
                    break;
                case 6:
                    Game.Say("/all Never gonna run around and desert you");
                    break;
                case 7:
                    Game.Say("/all We've known each other for so long Your heart's been aching, but you're too shy to say it");
                    break;
                case 8:
                    Game.Say("/all Inside, we both know what's been going on");
                    break;
                case 9:
                    Game.Say("/all Never gonna say goodbye");
                    break;
                case 10:
                    Game.Say("/all Never gonna tell a lie and hurt you");
                    break;
                case 11:
                    Game.Say("/all And if you ask me how I'm feeling Don't tell me you're too blind to see");
                    break;

            }
            lastMessage = Environment.TickCount;
        }
    }



}
