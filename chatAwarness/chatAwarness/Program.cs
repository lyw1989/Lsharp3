using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace chatAwarness
{

    class Program
    {
        private static List<String> lanesList = new List<string>() { "top", "mid", "jungle", "bot" };

        private static List<String> general = new List<string>() {"gank","ss" };

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            Game.OnChat += Game_OnChat;
        }

        static void Game_OnChat(GameChatEventArgs args)
        {
            if(!args.Sender.IsMe)return;
            String[] words = args.Message.Split(' ');
            foreach (var word in words)
            {
                String msg = args.Message;
                if (lanesList.Contains(word.Replace("!","")))
                {
                    switch (word.ToLower())
                    {
                        case "!top":
                            Game.SendPing(PingCategory.AssistMe, SummonersRift.TopLane.Contest_Zone.Points.FirstOrDefault());
                            msg = args.Message.Replace("!top", "top");
                            args.Process = false;
                            Game.Say(msg);
                            break;
                        case "!mid":
                            Game.SendPing(PingCategory.AssistMe, SummonersRift.MidLane.Contest_Zone.Points.FirstOrDefault());
                            msg = args.Message.Replace("!mid", "mid");
                            args.Process = false;
                            Game.Say(msg);
                            break;
                        case "!bot":
                            Game.SendPing(PingCategory.AssistMe, SummonersRift.TopLane.Contest_Zone.Points.FirstOrDefault());
                            msg = args.Message.Replace("!bot", "bot");
                            args.Process = false;
                            Game.Say(msg);
                            break;
                    }
                }

                if (general.Contains(word.Replace("!","")))
                {
                    switch (word.ToLower())
                    {
                        case "!gank":
                            foreach (var ally in ObjectManager.Get<Obj_AI_Hero>())
                            {
                                if (!ally.IsMe && ally.IsAlly)
                                    Game.SendPing(PingCategory.AssistMe, ally.Position);
                            }
                            msg = args.Message.Replace("!gank", "gank");
                            Game.Say(msg);
                            args.Process = false;
                            break;

                        case "!ss":
                            msg = args.Message.Replace("!ss", "ss");
                            Game.Say(msg);
                            foreach (var ally in ObjectManager.Get<Obj_AI_Hero>())
                            {
                                if (!ally.IsMe && ally.IsAlly)
                                    Game.SendPing(PingCategory.Fallback, ally.Position);
                            }
                            args.Process = false;
                            break;
                    }
                }
            }
        }
    }
}
