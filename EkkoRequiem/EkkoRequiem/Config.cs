using System.ComponentModel;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace EkkoRequiem
{
    class Config
    {
        public static Menu Settings = new Menu("Code Gease: Ekko RequieM", "menu", true);
        public static Menu orbwalkMenu;



        public static void Initialize()
        {

            //Orbwalk
            {
                orbwalkMenu = new Menu("Orbwalk", "Orbwalk");
                Settings.AddSubMenu(orbwalkMenu);
            }

            //Combo
            {
                var menuCombo = new Menu("Combo", "Combo");
                menuCombo.AddItem(new MenuItem("comboQ", "Use Q").SetValue(true));
                menuCombo.AddItem(new MenuItem("comboW", "Use W").SetValue(true));
                menuCombo.AddItem(new MenuItem("comboE", "Use E").SetValue(true));
                menuCombo.AddItem(new MenuItem("comboR", "Use R").SetValue(true));
                menuCombo.AddItem(new MenuItem("comboRCount", "Cast R if x in Ult").SetValue(new Slider(1, 1, 5)));
                menuCombo.AddItem(new MenuItem("comboRPercent", "Effective Ult Casting HP%").SetValue(new Slider(60, 10, 99)));
                Settings.AddSubMenu(menuCombo);
            }
            //Harrass
            {
                var harrasMenu = new Menu("Harass", "Harass");
                harrasMenu.AddItem(new MenuItem("harassQ", "Harass Q")).SetValue(true);
                harrasMenu.AddItem(new MenuItem("HarassMana", "Minumum Harass Mana%")).SetValue(new Slider(30, 1, 99));
                Settings.AddSubMenu(harrasMenu);
            }
            //KillSteal
            {
                var KillSteal = new Menu("KillSteal", "Kill Steal");
                KillSteal.AddItem(new MenuItem("KillStealQ", "KillSteal Q")).SetValue(true);
                KillSteal.AddItem(new MenuItem("KillStealE", "KillSteal E")).SetValue(true);
                KillSteal.AddItem(new MenuItem("KillStealR", "KillSteal R")).SetValue(true);
                Settings.AddSubMenu(KillSteal);
            }

            //Lane Clear
            {
                var menuLaneClear = new Menu("LaneClear", "Lane Clear");
                menuLaneClear.AddItem(new MenuItem("laneClearQ", "Q").SetValue(true));
                menuLaneClear.AddItem(new MenuItem("laneClearE", "E").SetValue(true));
                menuLaneClear.AddItem(new MenuItem("clearMana", "Minimum Mana %").SetValue(new Slider(30, 1, 99)));
                Settings.AddSubMenu(menuLaneClear);
            }

            //Jungle Clear
            {
                var menuJungleClear = new Menu("Jungle", "Jungle");
                menuJungleClear.AddItem(new MenuItem("JungleQ", "Q").SetValue(true));
                menuJungleClear.AddItem(new MenuItem("JungleW", "W").SetValue(true));
                menuJungleClear.AddItem(new MenuItem("JungleE", "E").SetValue(true));
                menuJungleClear.AddItem(new MenuItem("clearManaJ", "Minimum Mana %").SetValue(new Slider(30, 1, 99)));
                Settings.AddSubMenu(menuJungleClear);
            }


            //Drawing
            {
                var menuDraw = new Menu("draw", "Drawing");
                menuDraw.AddItem(new MenuItem("drawQ", "Draw Q").SetValue(false));
                menuDraw.AddItem(new MenuItem("drawW", "Draw W").SetValue(false));
                menuDraw.AddItem(new MenuItem("drawE", "Draw E").SetValue(false));
                menuDraw.AddItem(new MenuItem("drawRPos", "Draw R").SetValue(false));
                Settings.AddSubMenu(menuDraw);
            }





            //todo: add more R Options !


            Settings.AddToMainMenu();

        }
        /*
        public static void Initialize()
        {
            //Combo
            {
                var menuCombo = new Menu("menuCombo", "Combo");
                menuCombo.Add(new MenuBool("comboQ", "Use Q", true));
                menuCombo.Add(new MenuBool("comboW", "Use W", true));
                menuCombo.Add(new MenuBool("comboE", "Use E", true));
                menuCombo.Add(new MenuSlider("comboRHit", "Cast R if enemies >=", 0, 1, 5));
            //todo: add more R Options !
                Settings.Add(menuCombo);
            }
            //Harass
            {
                var menuHarass = new Menu("menuHarass", "Harass");
                menuHarass.Add(new MenuBool("harassQ", "Q", true));
                menuHarass.Add(new MenuBool("harassW", "W", true));
                menuHarass.Add(new MenuBool("harassE", "E", true));
                menuHarass.Add(new MenuSlider("harassMana", "Mana % >=", 30, 1, 99));
                Settings.Add(menuHarass);
            }
            //Lane Clear
            {
                var menuLaneClear = new Menu("menuLaneClear", "Lane Clear");
                menuLaneClear.Add(new MenuBool("laneClearQ", "Q", true));
                menuLaneClear.Add(new MenuSlider("clearMana", "Mana % >=", 30, 1, 99));
                Settings.Add(menuLaneClear);
            }

            {
                var menuJungleClear = new Menu("menuJungleClear", "Jungle Clear");
                menuJungleClear.Add(new MenuBool("JungleClearQ", "Q", true));
                menuJungleClear.Add(new MenuBool("JungleClearW", "W", true));
                menuJungleClear.Add(new MenuBool("JungleClearE", "E", true));
                menuJungleClear.Add(new MenuSlider("clearMana", "Mana % >=", 30, 1, 99));
                Settings.Add(menuJungleClear);
            }
            //Anti-Gapcloser
            {
                var menuGapCloser = new Menu("menuGapCloser", "Anti-Gapcloser");
                menuGapCloser.Add(new MenuBool("gapcloseE", "Dodge With E", true));
                Settings.Add(menuGapCloser);
            }
            //Ultimate
            {
                var menuUlt = new Menu("menuUlt", "Ult Settings");
                menuUlt.Add(new MenuBool("ks", "Killsteal with R", true));
                menuUlt.Add(new MenuSlider("ultMinRange", "Min. Range to Ult", 550, 550, 5000));
                menuUlt.Add(new MenuSlider("ultMaxRange", "Max. Range to Ult", 1000, 600, 5000));
                Settings.Add(menuUlt);
            }
            //Drawing
            {
                var menuDrawing = new Menu("menuDrawing", "Drawing");
                menuDrawing.Add(new MenuBool("drawQ", "Draw Q Range", true));
                menuDrawing.Add(new MenuBool("drawW", "Draw W Range", false));
                menuDrawing.Add(new MenuBool("drawRMin", "Draw Min. R Range", true));
                menuDrawing.Add(new MenuBool("drawRMax", "Draw Max. R Range", true));
                menuDrawing.Add(new MenuBool("drawRPos", "Draw Max. R Range", true));
               
                Settings.Add(menuDrawing);
            }
            //Finish
            {
                Settings.Add(new MenuSeparator("Info", "Made by XcxooxL"));
                Settings.Attach();
                
            }
        }
         * */

        public static bool ComboQ { get { return Settings.Item("comboQ").GetValue<bool>() && SpellManager.Q.IsReady(); } }
        public static bool ComboW { get { return Settings.Item("comboW").GetValue<bool>() && SpellManager.W.IsReady(); } }
        public static bool ComboE { get
        {
            return Settings.Item("comboE").GetValue<bool>() &&
                   (SpellManager.E.IsReady() || ObjectManager.Player.HasBuff("ekkoeattackbuff"));
        } }
        public static bool ComboR { get { return Settings.Item("comboR").GetValue<bool>() && SpellManager.R.IsReady(); } }
        public static double ComboRPercent { get { return Settings.Item("comboRPercent").GetValue<Slider>().Value / 100; } }
        public static int ComboRCount { get { return Settings.Item("comboRCount").GetValue<Slider>().Value; } }


        public static bool HarassQ { get { return Settings.Item("harassQ").GetValue<bool>() && SpellManager.Q.IsReady(); } }
        public static int HarassMana { get { return Settings.Item("HarassMana").GetValue<Slider>().Value; } }

        public static bool LaneClearQ { get { return Settings.Item("laneClearQ").GetValue<bool>() && SpellManager.Q.IsReady(); } }
        public static bool LaneClearE { get { return Settings.Item("laneClearE").GetValue<bool>() && SpellManager.E.IsReady(); } }
        public static int ClearMana { get { return Settings.Item("clearMana").GetValue<Slider>().Value; } }


        public static bool JungleQ { get { return Settings.Item("JungleQ").GetValue<bool>() && SpellManager.Q.IsReady(); } }
        public static bool JungleW { get { return Settings.Item("JungleW").GetValue<bool>() && SpellManager.W.IsReady(); } }
        public static bool JungleE { get { return Settings.Item("JungleE").GetValue<bool>() && SpellManager.E.IsReady(); } }
        public static int JungleMana { get { return Settings.Item("clearManaJ").GetValue<Slider>().Value; } }



        public static bool KillStealQ { get { return Settings.Item("KillStealQ").GetValue<bool>() && SpellManager.Q.IsReady(); } }
        public static bool KillStealE { get { return Settings.Item("KillStealE").GetValue<bool>(); } }
        public static bool KillStealR { get { return Settings.Item("KillStealR").GetValue<bool>() && SpellManager.R.IsReady(); } }





        public static bool DrawQ { get { return Settings.Item("drawQ").GetValue<bool>(); } }
        public static bool DrawW { get { return Settings.Item("drawW").GetValue<bool>(); } }
        public static bool DrawE { get { return Settings.Item("drawE").GetValue<bool>(); } }
        public static bool DrawRPos { get { return Settings.Item("drawRPos").GetValue<bool>(); } }


        /*
        public static bool ComboW { get { return Settings["menuCombo"]["comboW"].GetValue<MenuBool>().Value; } }
    public static bool ComboE { get { return Settings["menuCombo"]["comboE"].GetValue<MenuBool>().Value; } }
    public static bool ComboR { get { return Settings["menuCombo"]["comboR"].GetValue<MenuBool>().Value; } }
    public static int ComboRHit { get { return Settings["menuCombo"]["comboRHit"].GetValue<MenuSlider>().Value; } }

    public static bool HarassQ { get { return Settings["menuHarass"]["harassQ"].GetValue<MenuBool>().Value; } }
    public static bool HarassW { get { return Settings["menuHarass"]["harassW"].GetValue<MenuBool>().Value; } }
    public static int HarassMana { get { return Settings["menuHarass"]["harassMana"].GetValue<MenuSlider>().Value; } }

    public static MenuKeyBind ToggleAuto { get { return Settings["menuHarass"]["menuAuto"]["toggleAuto"].GetValue<MenuKeyBind>(); } }
    public static bool ShouldAuto(string championName)
    {
        return Settings["menuHarass"]["menuAuto"]["autoChamps"]["auto" + championName].GetValue<MenuBool>().Value;
    }
    public static bool AutoQ { get { return Settings["menuHarass"]["menuAuto"]["autoQ"].GetValue<MenuBool>().Value; } }
    public static bool AutoW { get { return Settings["menuHarass"]["menuAuto"]["autoW"].GetValue<MenuBool>().Value; } }
    public static int AutoMana { get { return Settings["menuHarass"]["menuAuto"]["autoMana"].GetValue<MenuSlider>().Value; } }
    public static bool AutoTurret { get { return Settings["menuHarass"]["menuAuto"]["autoTurret"].GetValue<MenuBool>().Value; } }

    public static bool LastHitQ { get { return Settings["menuLastHit"]["lastHitQ"].GetValue<MenuBool>().Value; } }

    public static bool LaneClearQ { get { return Settings["menuLaneClear"]["laneClearQ"].GetValue<MenuBool>().Value; } }
    public static int ClearMana { get { return Settings["menuLaneClear"]["clearMana"].GetValue<MenuSlider>().Value; } }

    public static bool GapcloseE { get { return Settings["menuGapcloser"]["gapcloseE"].GetValue<MenuBool>().Value; } }

    public static MenuKeyBind UltLowest { get { return Settings["menuUlt"]["ultLowest"].GetValue<MenuKeyBind>(); } }
    public static bool Killsteal { get { return Settings["menuUlt"]["ks"].GetValue<MenuBool>().Value; } }
    public static int UltMinRange { get { return Settings["menuUlt"]["ultMinRange"].GetValue<MenuSlider>().Value; } }
    public static int UltMaxRange { get { return Settings["menuUlt"]["ultMaxRange"].GetValue<MenuSlider>().Value; } }

    public static bool DrawQ { get { return Settings["menuDrawing"]["drawQ"].GetValue<MenuBool>().Value; } }
    public static bool DrawW { get { return Settings["menuDrawing"]["drawW"].GetValue<MenuBool>().Value; } }
    public static bool DrawRMin { get { return Settings["menuDrawing"]["drawRMin"].GetValue<MenuBool>().Value; } }
    public static bool DrawRMax { get { return Settings["menuDrawing"]["drawRMax"].GetValue<MenuBool>().Value; } }

    public static bool drawRPos { get { return Settings["menuDrawing"]["drawRPos"].GetValue<MenuBool>().Value; } }
*/
    }
}