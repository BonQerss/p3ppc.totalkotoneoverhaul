using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using p3ppc.totalkotoneoverhaul.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;

namespace p3ppc.totalkotoneoverhaul.Configuration
{
    public class Config : Configurable<Config>
    {
        /*
            User Properties:
                - Please put all of your configurable properties here.
    
            By default, configuration saves as "Config.json" in mod user config folder.    
            Need more config files/classes? See Configuration.cs
    
            Available Attributes:
            - Category
            - DisplayName
            - Description
            - DefaultValue

            // Technically Supported but not Useful
            - Browsable
            - Localizable

            The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
        */

        [Display(Order = 1)]
        [DisplayName("Title Screen")]
        [Description("Replaces the title screen background and assets.")]
        [Category("Title Screen")]
        [DefaultValue(true)]
        public bool FEMCTitleScreen { get; set; } = true;

        [Display(Order = 2)]
        [DisplayName("Analysis")]
        [Description("Recolors the analysis screen.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool ILoveJesus { get; set; } = true;

        [Display(Order = 3)]
        [DisplayName("AOA Prompt")]
        [Description("Recolors the All Out Attack prompt.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool AOA { get; set; } = true;

        [Display(Order = 4)]
        [DisplayName("Time Limit")]
        [Description("Recolors the Full Moon mission timer.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool Timer { get; set; } = true;

        [Display(Order = 5)]
        [DisplayName("Player Advantage")]
        [Description("Recolors the Player Advantage icon and animation.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool Advantage { get; set; } = true;

        [Display(Order = 6)]
        [DisplayName("One More")]
        [Description("Recolors the One More animation.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool OneMore { get; set; } = true;

        [Display(Order = 7)]
        [DisplayName("Fusion Spells")]
        [Category("Tartarus Recoloring")]
        [Description("Recolors Fusion Spell assets.")]
        [DefaultValue(true)]
        public bool FusionSpells { get; set; } = true;

        [Display(Order = 8)]
        [DisplayName("Persona Summoning Smoke")]
        [Description("Recolors Persona summoning smoke.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool FSMOKE { get; set; } = true;

        [Display(Order = 9)]
        [DisplayName("AOA Background")]
        [Description("Recolors All Out Attack backgrounds.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool AOABackground { get; set; } = true;

        [Display(Order = 10)]
        [DisplayName("Map Screen")]
        [Description("Recolors map screen assets.")]
        [Category("Overworld Recoloring")]
        [DefaultValue(true)]
        public bool MapScreen { get; set; } = true;

        [Display(Order = 11)]
        [DisplayName("Shuffle Time")]
        [Description("Recolors Shuffle Time assets.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool ShuffleTime { get; set; } = true;

        [Display(Order = 12)]
        [DisplayName("Tarot Cards")]
        [Description("Recolors Tarot cards.")]
        [Category("Overworld Recoloring")]
        [DefaultValue(true)]
        public bool Tarot { get; set; } = true;

        [Display(Order = 13)]
        [DisplayName("Mini Map")]
        [Description("Recolors Tartarus mini map assets.")]
        [Category("Tartarus Recoloring")]
        [DefaultValue(true)]
        public bool MiniMap { get; set; } = true;


        [Display(Order = 14)]
        [DisplayName("Title Screen Color")]
        [Description("Sets the title screen color.")]
        [Category("Title Screen Colors")]
        [DefaultValue("#B23146")]
        public string TitleScreenColor { get; set; } = "#B23146";

        [Display(Order = 15)]
        [DisplayName("Analysis Primary Color")]
        [Description("Sets the analysis primary color.")]
        [Category("Analysis Colors")]
        [DefaultValue("#FDDAE9")]
        public string AnalysisPrimaryColor { get; set; } = "#FDDAE9";

        [Display(Order = 16)]
        [DisplayName("Analysis Hover Color")]
        [Description("Sets the analysis hover color.")]
        [Category("Analysis Colors")]
        [DefaultValue("#FFEEF6")]
        public string AnalysisHoverColor { get; set; } = "#FFEEF6";

        [Display(Order = 17)]
        [DisplayName("Analysis Selection Color")]
        [Description("Sets the analysis selection color.")]
        [Category("Analysis Colors")]
        [DefaultValue("#FF759A")]
        public string AnalysisSelectionColor { get; set; } = "#FF759A";

        [Display(Order = 18)]
        [DisplayName("AOA Prompt Color")]
        [Description("Sets the AOA prompt color.")]
        [Category("AOA Prompt Colors")]
        [DefaultValue("#FDE8F1")]
        public string AOAPromptColor { get; set; } = "#FDE8F1";

        [Display(Order = 19)]
        [DisplayName("Time Limit Color")]
        [Description("Sets the time limit color.")]
        [Category("Time Limit Colors")]
        [DefaultValue("#FFC1DC")]
        public string TimerColor { get; set; } = "#FFC1DC";

        [Display(Order = 20)]
        [DisplayName("Player Advantage Glow Color")]
        [Description("Sets the player advantage glow color.")]
        [Category("Player Advantage Colors")]
        [DefaultValue("#FF759A")]
        public string AdvantageGlowColor { get; set; } = "#FF759A";

        [Display(Order = 21)]
        [DisplayName("Player Advantage Diamond Color")]
        [Description("Sets the player advantage diamond color.")]
        [Category("Player Advantage Colors")]
        [DefaultValue("#DE1F5B")]
        public string AdvantageDiamondColor { get; set; } = "#DE1F5B";

        [Display(Order = 22)]
        [DisplayName("Player Advantage Text Outline Color")]
        [Description("Sets the player advantage text and outline color.")]
        [Category("Player Advantage Colors")]
        [DefaultValue("#FFC1DC")]
        public string AdvantageTextColor { get; set; } = "#FFC1DC";

        [Display(Order = 23)]
        [DisplayName("One More Diamond Color")]
        [Description("Sets the one more diamond color.")]
        [Category("One More Colors")]
        [DefaultValue("#FFC1DC")]
        public string OneMoreDiamondColor { get; set; } = "#FFC1DC";

        [Display(Order = 24)]
        [DisplayName("One More Lightning Color")]
        [Description("Sets the one more lightning color.")]
        [Category("One More Colors")]
        [DefaultValue("#DE1F5B")]
        public string OneMoreLightningColor { get; set; } = "#DE1F5B";

        [Display(Order = 25)]
        [DisplayName("Map Palette 1")]
        [Description("Sets map palette slot 1.")]
        [Category("Map Screen Colors")]
        [DefaultValue("#FFDCDC")]
        public string MapColor1 { get; set; } = "#FFDCDC";

        [Display(Order = 26)]
        [DisplayName("Map Palette 2")]
        [Description("Sets map palette slot 2.")]
        [Category("Map Screen Colors")]
        [DefaultValue("#431414")]
        public string MapColor2 { get; set; } = "#431414";

        [Display(Order = 27)]
        [DisplayName("Map Palette 3")]
        [Description("Sets map palette slot 3.")]
        [Category("Map Screen Colors")]
        [DefaultValue("#F6B464")]
        public string MapColor3 { get; set; } = "#F6B464";

        [Display(Order = 28)]
        [DisplayName("Map Palette 4")]
        [Description("Sets map palette slot 4.")]
        [Category("Map Screen Colors")]
        [DefaultValue("#552B2B")]
        public string MapColor4 { get; set; } = "#552B2B";

        [Display(Order = 29)]
        [DisplayName("Map Palette 5")]
        [Description("Sets map palette slot 5.")]
        [Category("Map Screen Colors")]
        [DefaultValue("#FFFFFF")]
        public string MapColor5 { get; set; } = "#FFFFFF";

        [Display(Order = 30)]
        [DisplayName("Map Palette 6")]
        [Description("Sets map palette slot 6.")]
        [Category("Map Screen Colors")]
        [DefaultValue("#421212")]
        public string MapColor6 { get; set; } = "#421212";


        [Display(Order = 31)]
        [DisplayName("Police Station")]
        [Description("EXPERIMENTAL: patches confirmed Police Station entries in the shared facility table DAT_1407BB3B0. Restart the game after disabling an already-applied patch.")]
        [Category("Experimental Police Station")]
        [DefaultValue(false)]
        public bool Police { get; set; } = false;

        [Display(Order = 32)]
        [DisplayName("Aohige Pharmacy")]
        [Description("EXPERIMENTAL: patches the confirmed Aohige Pharmacy indices in DAT_1407BB3B0. Restart the game after disabling an already-applied patch.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue(false)]
        public bool AohigePharmacy { get; set; } = false;

        [Display(Order = 33)]
        [DisplayName("Antique Shop")]
        [Description("EXPERIMENTAL: patches confirmed Antique Shop DAT_1407BB3B0 entries plus its two traced local comparison-color immediates. Restart after disabling an already-applied patch.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue(false)]
        public bool AntiqueShop { get; set; } = false;

        [Display(Order = 34)]
        [DisplayName("Police Lookup 0x00")]
        [Description("DAT_1407BB3B0[0x00] @ 0x1407BB3B0. Confirmed Police Station facility lookup. Legacy property name PoliceColor01 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#F3DCE2")]
        public string PoliceColor01 { get; set; } = "#F3DCE2";

        [Display(Order = 35)]
        [DisplayName("Police Lookup 0x01")]
        [Description("DAT_1407BB3B0[0x01] @ 0x1407BB3B4. Confirmed Police Station facility lookup. Legacy property name PoliceColor02 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#C003A9")]
        public string PoliceColor02 { get; set; } = "#C003A9";

        [Display(Order = 36)]
        [DisplayName("Police Lookup 0x02")]
        [Description("DAT_1407BB3B0[0x02] @ 0x1407BB3B8. Confirmed Police Station facility lookup. Legacy property name PoliceColor03 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#8C0404")]
        public string PoliceColor03 { get; set; } = "#8C0404";

        [Display(Order = 37)]
        [DisplayName("Police Lookup 0x03")]
        [Description("DAT_1407BB3B0[0x03] @ 0x1407BB3BC. Confirmed Police Station facility lookup. Legacy property name PoliceColor04 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#421212")]
        public string PoliceColor04 { get; set; } = "#421212";

        [Display(Order = 38)]
        [DisplayName("Police Lookup 0x04")]
        [Description("DAT_1407BB3B0[0x04] @ 0x1407BB3C0. Confirmed Police Station facility lookup. Legacy property name PoliceColor05 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#9E7492")]
        public string PoliceColor05 { get; set; } = "#9E7492";

        [Display(Order = 39)]
        [DisplayName("Police Lookup 0x05")]
        [Description("DAT_1407BB3B0[0x05] @ 0x1407BB3C4. Confirmed Police Station facility lookup. Legacy property name PoliceColor06 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FFFFFF")]
        public string PoliceColor06 { get; set; } = "#FFFFFF";

        [Display(Order = 40)]
        [DisplayName("Police Lookup 0x06")]
        [Description("DAT_1407BB3B0[0x06] @ 0x1407BB3C8. Confirmed Police Station facility lookup. Legacy property name PoliceColor07 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#000000")]
        public string PoliceColor07 { get; set; } = "#000000";

        [Display(Order = 41)]
        [DisplayName("Police Lookup 0x07")]
        [Description("DAT_1407BB3B0[0x07] @ 0x1407BB3CC. Confirmed Police Station facility lookup. Legacy property name PoliceColor08 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#9B107B")]
        public string PoliceColor08 { get; set; } = "#9B107B";

        [Display(Order = 42)]
        [DisplayName("Police Lookup 0x08")]
        [Description("DAT_1407BB3B0[0x08] @ 0x1407BB3D0. Confirmed Police Station facility lookup. Legacy property name PoliceColor09 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FC8FE0")]
        public string PoliceColor09 { get; set; } = "#FC8FE0";

        [Display(Order = 43)]
        [DisplayName("Police Lookup 0x09")]
        [Description("DAT_1407BB3B0[0x09] @ 0x1407BB3D4. Confirmed Police Station facility lookup. Legacy property name PoliceColor10 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FFF470")]
        public string PoliceColor10 { get; set; } = "#FFF470";

        [Display(Order = 44)]
        [DisplayName("Police Lookup 0x0A")]
        [Description("DAT_1407BB3B0[0x0A] @ 0x1407BB3D8. Confirmed Police Station facility lookup. Legacy property name PoliceColor11 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#5E1E1E")]
        public string PoliceColor11 { get; set; } = "#5E1E1E";

        [Display(Order = 45)]
        [DisplayName("Police Lookup 0x0B")]
        [Description("DAT_1407BB3B0[0x0B] @ 0x1407BB3DC. Confirmed Police Station facility lookup. Legacy property name PoliceColor12 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FF64CB")]
        public string PoliceColor12 { get; set; } = "#FF64CB";

        [Display(Order = 46)]
        [DisplayName("Police Lookup 0x0C")]
        [Description("DAT_1407BB3B0[0x0C] @ 0x1407BB3E0. Confirmed Police Station facility lookup. Legacy property name PoliceColor13 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#F885E8")]
        public string PoliceColor13 { get; set; } = "#F885E8";

        [Display(Order = 47)]
        [DisplayName("Police Lookup 0x0D")]
        [Description("DAT_1407BB3B0[0x0D] @ 0x1407BB3E4. Confirmed Police Station facility lookup. Legacy property name PoliceColor14 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FFA9B3")]
        public string PoliceColor14 { get; set; } = "#FFA9B3";

        [Display(Order = 48)]
        [DisplayName("Police Lookup 0x0E")]
        [Description("DAT_1407BB3B0[0x0E] @ 0x1407BB3E8. Confirmed Police Station facility lookup. Legacy property name PoliceColor15 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FF939F")]
        public string PoliceColor15 { get; set; } = "#FF939F";

        [Display(Order = 49)]
        [DisplayName("Police Lookup 0x0F")]
        [Description("DAT_1407BB3B0[0x0F] @ 0x1407BB3EC. Confirmed Police Station facility lookup. Legacy property name PoliceColor16 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#D914B2")]
        public string PoliceColor16 { get; set; } = "#D914B2";

        [Display(Order = 50)]
        [DisplayName("Police Lookup 0x10")]
        [Description("DAT_1407BB3B0[0x10] @ 0x1407BB3F0. Confirmed Police Station facility lookup. Legacy property name PoliceColor17 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#FC8FE0")]
        public string PoliceColor17 { get; set; } = "#FC8FE0";

        [Display(Order = 51)]
        [DisplayName("Police Lookup 0x11")]
        [Description("DAT_1407BB3B0[0x11] @ 0x1407BB3F4. Confirmed Police Station facility lookup. Legacy property name PoliceColor18 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#ED00C5")]
        public string PoliceColor18 { get; set; } = "#ED00C5";

        [Display(Order = 52)]
        [DisplayName("Police Lookup 0x12")]
        [Description("DAT_1407BB3B0[0x12] @ 0x1407BB3F8. Confirmed Police Station facility lookup. Legacy property name PoliceColor19 is retained for preset compatibility.")]
        [Category("Experimental Police Station")]
        [DefaultValue("#F028C0")]
        public string PoliceColor19 { get; set; } = "#F028C0";

        [Display(Order = 53)]
        [DisplayName("Enable Expanded UI Color Overrides")]
        [Description("Enables normalized Kotone UI colors, including the shared Camp UI palettes.")]
        [Category("Experimental")]
        [DefaultValue(true)]
        public bool ExperimentalGlobalFemcPalette { get; set; } = true;

        

        [Display(Order = 54)]
        [DisplayName("Battle Affinity Primary")]
        [Description("Sets the battle affinity primary color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#A66794")]
        public string FemcBattleAffinityPrimary { get; set; } = "#A66794";

        [Display(Order = 55)]
        [DisplayName("Battle Affinity Secondary")]
        [Description("Sets the battle affinity secondary color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#C04D7C")]
        public string FemcBattleAffinitySecondary { get; set; } = "#C04D7C";

        [Display(Order = 56)]
        [DisplayName("Battle Affinity Dark Neutral A")]
        [Description("Sets the battle affinity dark neutral a color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#232425")]
        public string FemcBattleAffinityDarkNeutralA { get; set; } = "#232425";

        [Display(Order = 57)]
        [DisplayName("Battle Affinity White")]
        [Description("Sets the battle affinity white color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#FFFFFF")]
        public string FemcBattleAffinityWhite { get; set; } = "#FFFFFF";

        [Display(Order = 58)]
        [DisplayName("Battle Affinity Dark Neutral B")]
        [Description("Sets the battle affinity dark neutral b color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#242424")]
        public string FemcBattleAffinityDarkNeutralB { get; set; } = "#242424";

        [Display(Order = 59)]
        [DisplayName("Battle Affinity Warm Highlight")]
        [Description("Sets the battle affinity warm highlight color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#FFB571")]
        public string FemcBattleAffinityWarmHighlight { get; set; } = "#FFB571";

        [Display(Order = 60)]
        [DisplayName("Battle Affinity Purple Highlight")]
        [Description("Sets the battle affinity purple highlight color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#8E78F6")]
        public string FemcBattleAffinityPurpleHighlight { get; set; } = "#8E78F6";

        [Display(Order = 61)]
        [DisplayName("Battle Affinity Deep Teal")]
        [Description("Sets the battle affinity deep teal color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#00415A")]
        public string FemcBattleAffinityDeepTeal { get; set; } = "#00415A";

        [Display(Order = 62)]
        [DisplayName("Battle Affinity Gender Accent")]
        [Description("Sets the battle affinity gender accent color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#FF2459")]
        public string FemcBattleAffinityGenderAccent { get; set; } = "#FF2459";

        [Display(Order = 63)]
        [DisplayName("Battle Affinity Dark Gender Accent")]
        [Description("Sets the battle affinity dark gender accent color.")]
        [Category("Experimental Battle Panel Affinity Colors")]
        [DefaultValue("#5F0023")]
        public string FemcBattleAffinityDarkGenderAccent { get; set; } = "#5F0023";

        [Display(Order = 64)]
        [DisplayName("Battle Menu Primary Accent")]
        [Description("Sets the battle menu primary accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#FF3296")]
        public string FemcBattleMenuPrimaryAccent { get; set; } = "#FF3296";

        [Display(Order = 65)]
        [DisplayName("Battle Menu Neutral Gray A")]
        [Description("Sets the battle menu neutral gray a color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#9BA0A2")]
        public string FemcBattleMenuNeutralGrayA { get; set; } = "#9BA0A2";

        [Display(Order = 66)]
        [DisplayName("Battle Menu Dark Gray A")]
        [Description("Sets the battle menu dark gray a color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#3A3A3A")]
        public string FemcBattleMenuDarkGrayA { get; set; } = "#3A3A3A";

        [Display(Order = 67)]
        [DisplayName("Battle Menu Dark Gray B")]
        [Description("Sets the battle menu dark gray b color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#2D2D2D")]
        public string FemcBattleMenuDarkGrayB { get; set; } = "#2D2D2D";

        [Display(Order = 68)]
        [DisplayName("Battle Menu Dark Gray C")]
        [Description("Sets the battle menu dark gray c color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#424242")]
        public string FemcBattleMenuDarkGrayC { get; set; } = "#424242";

        [Display(Order = 69)]
        [DisplayName("Battle Menu White")]
        [Description("Sets the battle menu white color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#FFFFFF")]
        public string FemcBattleMenuWhite { get; set; } = "#FFFFFF";

        [Display(Order = 70)]
        [DisplayName("Battle Menu Alert Red Accent")]
        [Description("Sets the battle menu alert and red accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#C10D0C")]
        public string FemcBattleMenuAlertRed { get; set; } = "#C10D0C";

        [Display(Order = 71)]
        [DisplayName("Battle Menu Selected Dark Fill")]
        [Description("Sets the battle menu selected dark fill color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#2D2D2D")]
        public string FemcBattleMenuSelectedDarkFill { get; set; } = "#2D2D2D";

        [Display(Order = 72)]
        [DisplayName("Battle Menu Shared Light Blue")]
        [Description("Sets the battle menu shared light blue color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#6DB3FF")]
        public string FemcBattleMenuSharedLightBlue { get; set; } = "#6DB3FF";

        [Display(Order = 73)]
        [DisplayName("Battle Menu Shared Mid Blue")]
        [Description("Sets the battle menu shared mid blue color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#4486CD")]
        public string FemcBattleMenuSharedMidBlue { get; set; } = "#4486CD";

        [Display(Order = 74)]
        [DisplayName("Battle Menu Mid Gray")]
        [Description("Sets the battle menu mid gray color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#6C6C6C")]
        public string FemcBattleMenuMidGray { get; set; } = "#6C6C6C";

        [Display(Order = 75)]
        [DisplayName("Battle Help Box Background")]
        [Description("Sets the battle help box background color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#313F4D")]
        public string FemcBattleHelpBoxBackground { get; set; } = "#313F4D";

        [Display(Order = 76)]
        [DisplayName("Battle Description Text")]
        [Description("Compatibility setting only. Battle-description text is intentionally left at the vanilla colour so theme changes cannot reduce readability.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#DEDEE2")]
        public string FemcBattleDescriptionText { get; set; } = "#DEDEE2";

        [Display(Order = 77)]
        [DisplayName("Battle Submenu Light Accent")]
        [Description("Sets the battle submenu light accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#FAD2E6")]
        public string FemcBattleSubmenuLightAccent { get; set; } = "#FAD2E6";

        [Display(Order = 78)]
        [DisplayName("Battle Menu Blue Gray Accent")]
        [Description("Sets the battle menu blue gray accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#9FB0C3")]
        public string FemcBattleMenuBlueGrayAccent { get; set; } = "#9FB0C3";

        [Display(Order = 79)]
        [DisplayName("Battle Submenu Header Icon Accent")]
        [Description("Sets the battle submenu header and icon accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#5F0045")]
        public string FemcBattleSubmenuHeaderAccent { get; set; } = "#5F0045";

        [Display(Order = 80)]
        [DisplayName("Battle Tactics Light Accent")]
        [Description("Sets the battle tactics light accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#FF81D5")]
        public string FemcBattleTacticsLightAccent { get; set; } = "#FF81D5";

        [Display(Order = 81)]
        [DisplayName("Battle Tactics Mid Accent")]
        [Description("Sets the battle tactics mid accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#CD4493")]
        public string FemcBattleTacticsMidAccent { get; set; } = "#CD4493";

        [Display(Order = 82)]
        [DisplayName("Battle Special Alert Accent")]
        [Description("Sets the battle special and alert accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#A607FF")]
        public string FemcBattleSpecialAccent { get; set; } = "#A607FF";

        [Display(Order = 83)]
        [DisplayName("Battle Muted Light Accent")]
        [Description("Sets the battle muted light accent color.")]
        [Category("Experimental Battle Menu Tactics Colors")]
        [DefaultValue("#D1D1D1")]
        public string FemcBattleMutedLightAccent { get; set; } = "#D1D1D1";

        [Display(Order = 84)]
        [DisplayName("Battle Results Primary Dark Accent")]
        [Description("Sets the battle results primary dark accent color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#6B010A")]
        public string FemcBattleResultsPrimaryDark { get; set; } = "#6B010A";

        [Display(Order = 85)]
        [DisplayName("Battle Results Pale Accent")]
        [Description("Sets the battle results pale accent color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#FFE5EC")]
        public string FemcBattleResultsPaleAccent { get; set; } = "#FFE5EC";

        [Display(Order = 86)]
        [DisplayName("Battle Results Reward Highlight")]
        [Description("Sets the battle results reward highlight color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#FFEB97")]
        public string FemcBattleResultsRewardHighlight { get; set; } = "#FFEB97";

        [Display(Order = 87)]
        [DisplayName("Battle Results White")]
        [Description("Sets the battle results white color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#FFFFFF")]
        public string FemcBattleResultsWhite { get; set; } = "#FFFFFF";

        [Display(Order = 88)]
        [DisplayName("Battle Results Blue Accent A")]
        [Description("Sets the battle results blue accent a color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#2494FF")]
        public string FemcBattleResultsBlueAccentA { get; set; } = "#2494FF";

        [Display(Order = 89)]
        [DisplayName("Battle Results Blue Accent B")]
        [Description("Sets the battle results blue accent b color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#2494FF")]
        public string FemcBattleResultsBlueAccentB { get; set; } = "#2494FF";

        [Display(Order = 90)]
        [DisplayName("Battle Results Blue Accent C")]
        [Description("Sets the battle results blue accent c color.")]
        [Category("Experimental Battle Results Colors")]
        [DefaultValue("#2494FF")]
        public string FemcBattleResultsBlueAccentC { get; set; } = "#2494FF";

        [Display(Order = 91)]
        [DisplayName("Shuffle Time Dark Base")]
        [Description("Sets the Shuffle Time dark base color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#69172A")]
        public string FemcShuffleDarkBase { get; set; } = "#69172A";

        [Display(Order = 92)]
        [DisplayName("Shuffle Time Glow A")]
        [Description("Sets the Shuffle Time glow a color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FE5FFF")]
        public string FemcShuffleGlowA { get; set; } = "#FE5FFF";

        [Display(Order = 93)]
        [DisplayName("Shuffle Time Mid Base")]
        [Description("Sets the Shuffle Time mid base color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#B63530")]
        public string FemcShuffleMidBase { get; set; } = "#B63530";

        [Display(Order = 94)]
        [DisplayName("Shuffle Time Glow B")]
        [Description("Sets the Shuffle Time glow b color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FF3D58")]
        public string FemcShuffleGlowB { get; set; } = "#FF3D58";

        [Display(Order = 95)]
        [DisplayName("Shuffle Time Primary Accent")]
        [Description("Sets the Shuffle Time primary accent color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FF4F64")]
        public string FemcShufflePrimaryAccent { get; set; } = "#FF4F64";

        [Display(Order = 96)]
        [DisplayName("Shuffle Time Translucent Secondary")]
        [Description("Sets the Shuffle Time translucent secondary color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#838D00")]
        public string FemcShuffleTranslucentSecondary { get; set; } = "#838D00";

        [Display(Order = 97)]
        [DisplayName("Shuffle Time White A")]
        [Description("Sets the Shuffle Time white a color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FFFFFF")]
        public string FemcShuffleWhiteA { get; set; } = "#FFFFFF";

        [Display(Order = 98)]
        [DisplayName("Shuffle Time Deep Accent")]
        [Description("Sets the Shuffle Time deep accent color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#BD003D")]
        public string FemcShuffleDeepAccent { get; set; } = "#BD003D";

        [Display(Order = 99)]
        [DisplayName("Shuffle Time Black")]
        [Description("Sets the Shuffle Time black color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#000000")]
        public string FemcShuffleBlack { get; set; } = "#000000";

        [Display(Order = 100)]
        [DisplayName("Shuffle Time Dark Secondary")]
        [Description("Sets the Shuffle Time dark secondary color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#290963")]
        public string FemcShuffleDarkSecondary { get; set; } = "#290963";

        [Display(Order = 101)]
        [DisplayName("Shuffle Time Highlight")]
        [Description("Sets the Shuffle Time highlight color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FF7672")]
        public string FemcShuffleHighlight { get; set; } = "#FF7672";

        [Display(Order = 102)]
        [DisplayName("Shuffle Time White B")]
        [Description("Sets the Shuffle Time white b color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FFFFFF")]
        public string FemcShuffleWhiteB { get; set; } = "#FFFFFF";

        [Display(Order = 103)]
        [DisplayName("Shuffle Time Strong Accent")]
        [Description("Sets the Shuffle Time strong accent color.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FF003C")]
        public string FemcShuffleStrongAccent { get; set; } = "#FF003C";

        [Display(Order = 104)]
        [DisplayName("Camp Root Palette 00")]
        [Description("Kotone Camp root table slot 0x00 @ 0x1405CE3FC. Direct consumers confirmed in FUN_14013B370, RenderPersonaDetails and FUN_1402ADBC0.")]
        [Category("Camp Command Menu")]
        [DefaultValue("#FFC1DC")]
        public string FemcCampRootPrimaryLight { get; set; } = "#FFC1DC";

        [Display(Order = 105)]
        [DisplayName("Camp Root Palette 01")]
        [Description("Kotone Camp root table slot 0x01 @ 0x1405CE400. Direct consumers confirmed in FUN_14013B370 and RenderPersonaDetails.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#FDDAE9")]
        public string FemcCampRootPaleAccent { get; set; } = "#FDDAE9";

        [Display(Order = 106)]
        [DisplayName("Camp Root Palette 02")]
        [Description("Kotone Camp root table slot 0x02 @ 0x1405CE404. Direct consumers confirmed in FUN_14013B370, RenderPersonaDetails and FUN_1402ADBC0.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#6B010A")]
        public string FemcCampRootDarkAccent { get; set; } = "#6B010A";

        [Display(Order = 107)]
        [DisplayName("Menu Background")]
        [Description("Sets the actual large Command Menu background. This is Kotone iFlash palette index 44, used by root preset 1 quad 2. Makoto uses #2494FF and Kotone uses #FF759A.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FF759A")]
        public string FemcCampRootMidAccent { get; set; } = "#FF759A";

        [Display(Order = 204)]
        [DisplayName("MAIN Header Tint")]
        [Description("Sets Kotone Camp root palette slot 0x03, used by the cached MAIN sprite in FUN_14014B350. This is separate from the large Menu Background.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#FF759A")]
        public string FemcCampRootMainHeaderTint { get; set; } = "#FF759A";

        [Display(Order = 108)]
        [DisplayName("Camp Root Palette 04")]
        [Description("Kotone Camp root table slot 0x04 @ 0x1405CE40C. No direct consumer was established in the current decomp; this option changes only the table slot.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#FFFFFF")]
        public string FemcCampRootWhite { get; set; } = "#FFFFFF";

        [Display(Order = 109)]
        [DisplayName("Camp Root Palette 05")]
        [Description("Kotone Camp root table slot 0x05 @ 0x1405CE410. No direct consumer was established in the current decomp; this option changes only the table slot.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#FF68C6")]
        public string FemcCampRootPinkPurpleAccent { get; set; } = "#FF68C6";

        [Display(Order = 110)]
        [DisplayName("Camp Root Palette 06")]
        [Description("Kotone Camp root table slot 0x06 @ 0x1405CE414. Direct consumers confirmed in FUN_14013B370.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#DE1F5B")]
        public string FemcCampRootStrongAccent { get; set; } = "#DE1F5B";

        [Display(Order = 111)]
        [DisplayName("Camp Root Palette 07")]
        [Description("Kotone Camp root table slot 0x07 @ 0x1405CE418. No direct table consumer was established. FUN_14013B370 also contains independent local color literals, which remain separate until their exact instruction sites are verified.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#4E6D8B")]
        public string FemcCampRootBlueGrayNeutral { get; set; } = "#4E6D8B";

        [Display(Order = 112)]
        [DisplayName("Camp Root Palette 08")]
        [Description("Kotone Camp root table slot 0x08 @ 0x1405CE41C. No direct table consumer was established. Related local literals in FUN_14013B370 remain unpatched pending exact instruction-site verification.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#F5FFA4")]
        public string FemcCampRootLimeHighlight { get; set; } = "#F5FFA4";

        [Display(Order = 113)]
        [DisplayName("Camp Root Palette 09")]
        [Description("Kotone Camp root table slot 0x09 @ 0x1405CE420. No direct table consumer was established. Related local literals in FUN_14013B370 remain unpatched pending exact instruction-site verification.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#9AF5FF")]
        public string FemcCampRootCyanHighlight { get; set; } = "#9AF5FF";

        [Display(Order = 114)]
        [DisplayName("Camp Root Palette 0A")]
        [Description("Kotone Camp root table slot 0x0A @ 0x1405CE424. No direct table consumer was established. Related local literals in FUN_14013B370 remain unpatched pending exact instruction-site verification.")]
        [Category("Camp Command Menu Root Palette")]
        [DefaultValue("#FFA4D1")]
        public string FemcCampRootPinkHighlight { get; set; } = "#FFA4D1";

        [Display(Order = 115)]
        [DisplayName("Party Panel Status Marker")]
        [Description("Independent FeMC #DE1F5E inline RGB used by the shared party panel path. It is not loaded from either Camp color bank.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#DE1F5E")]
        public string FemcPartyPanelCharacterStatusAccent { get; set; } = "#DE1F5E";

        [Display(Order = 116)]
        [DisplayName("Camp Root Status Marker")]
        [Description("Independent FeMC #DE1F5E inline RGB in FUN_14013B370. This is a separate original source from the shared party-panel status marker.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#DE1F5E")]
        public string FemcCampPartyStatusMarkerAccent { get; set; } = "#DE1F5E";

        [Display(Order = 117)]
        [DisplayName("Party Panel Character Name")]
        [Description("Compatibility setting only. This is text, so the mod now leaves the original game immediate untouched.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#6B010A")]
        public string FemcPartyPanelCharacterNameColor { get; set; } = "#6B010A";

        [Display(Order = 118)]
        [DisplayName("Skill Text Normal")]
        [Description("Compatibility setting only. Normal/fallback skill text is intentionally left at the original game colour.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#6B010A")]
        public string FemcCampSkillTextNormalColor { get; set; } = "#6B010A";

        [Display(Order = 119)]
        [DisplayName("Skill Text Selected")]
        [Description("Compatibility setting only. Selected skill text is intentionally left at the original game colour.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#FFC1DC")]
        public string FemcCampSkillTextSelectedColor { get; set; } = "#FFC1DC";

        [Display(Order = 120)]
        [DisplayName("Skill Text New")]
        [Description("Compatibility setting only. New-skill text is intentionally left at the original game colour.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#CBFF9C")]
        public string FemcCampSkillTextNewColor { get; set; } = "#CBFF9C";

        [Display(Order = 121)]
        [DisplayName("Status Panel Accent")]
        [Description("Sets the Kotone #FF759A status-panel tint across the confirmed cmpstatus implementations, including the live 0x14B303F80 path and the state-machine override. Makoto uses #2494FF/#64003C from the native status table.")]
        [Category("Camp Command Menu Independent Hardcoded Copies")]
        [DefaultValue("#FF759A")]
        public string FemcCampStatusRendererAccent { get; set; } = "#FF759A";

        [Display(Order = 123)]
        [DisplayName("Social Link Bust up Tint")]
        [Description("One logical option for the two duplicate FeMC #6B010A bust-up tint immediates in cmpcommu FUN_14011ECA0 and FUN_14011F150. These two sites intentionally stay linked because they implement the same UI role.")]
        [Category("Social Link Independent Hardcoded Copies")]
        [DefaultValue("#6B010A")]
        public string FemcSocialLinkBustupTint { get; set; } = "#6B010A";

        [Display(Order = 124)]
        [DisplayName("Camp / Persona Transition Primitive Accent")]
        [Description("Sets the Kotone #970060 transition primitive used by the Camp foreground transition path. The same logical color is applied to its initial and expanded primitive forms.")]
        [Category("Camp Command Menu Foreground Colors")]
        [DefaultValue("#970060")]
        public string FemcPersonaTransitionPrimitiveAccent { get; set; } = "#970060";

[Display(Order = 125)]
        [DisplayName("Primary Shared UI Accent")]
        [Description("Native Kotone shared gender palette index 0x00 @ 0x1405CE364: Primary shared accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu")]
        [DefaultValue("#DE1F5B")]
        public string FemcPrimaryUiAccent { get; set; } = "#DE1F5B";

        [Display(Order = 126)]
        [DisplayName("Normal Unselected Box Background")]
        [Description("Native Kotone shared gender palette index 0x01 @ 0x1405CE368: Normal/unselected box background. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FFC1DC")]
        public string FemcNormalSkillBoxBackground { get; set; } = "#FFC1DC";

        [Display(Order = 127)]
        [DisplayName("Selected Active Box Background")]
        [Description("Native Kotone shared gender palette index 0x02 @ 0x1405CE36C: Selected/active dark background. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#6B010A")]
        public string FemcSelectedSkillBoxBackground { get; set; } = "#6B010A";

        [Display(Order = 128)]
        [DisplayName("Menu Transition Accent")]
        [Description("Sets the Camp transition overlay and shared accent color.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FF759A")]
        public string FemcSecondaryPanelFrameAccent { get; set; } = "#FF759A";

        [Display(Order = 129)]
        [DisplayName("Primary Light Text Stat Text")]
        [Description("Compatibility setting only. Shared light/stat text stays at vanilla #FFFFFF and is no longer themed.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FFFFFF")]
        public string FemcPrimaryLightText { get; set; } = "#FFFFFF";

        [Display(Order = 130)]
        [DisplayName("Attention Filled Marker Highlight")]
        [Description("Native Kotone shared gender palette index 0x05 @ 0x1405CE378: Attention/filled marker highlight. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FFDE00")]
        public string FemcAttentionFilledMarkerHighlight { get; set; } = "#FFDE00";

        [Display(Order = 131)]
        [DisplayName("Persona Name Text")]
        [Description("Compatibility setting only. Persona-name text stays at vanilla Kotone #FDDAE9 and is no longer themed.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FDDAE9")]
        public string FemcPersonaNameText { get; set; } = "#FDDAE9";

        [Display(Order = 132)]
        [DisplayName("Equipment Stat Decrease Indicator")]
        [Description("Native Kotone shared gender palette index 0x07 @ 0x1405CE380: Equipment stat decrease indicator. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#77D1FF")]
        public string FemcEquipmentStatDecreaseIndicator { get; set; } = "#77D1FF";

        [Display(Order = 133)]
        [DisplayName("Skill Transition Persona Gauge Accent")]
        [Description("Native Kotone shared gender palette index 0x08 @ 0x1405CE384: Skill transition / Persona gauge accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#CBFF9C")]
        public string FemcSkillTransitionPersonaGaugeAccent { get; set; } = "#CBFF9C";

        [Display(Order = 134)]
        [DisplayName("New Skill Background")]
        [Description("Native Kotone shared gender palette index 0x09 @ 0x1405CE388: New/fusion skill background. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#649016")]
        public string FemcNewSkillBackground { get; set; } = "#649016";

        [Display(Order = 135)]
        [DisplayName("Persona Status Detail Panel Accent")]
        [Description("Native Kotone shared gender palette index 0x0A @ 0x1405CE38C: Persona/Status detail panel accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FDDAE9")]
        public string FemcPersonaStatusDetailPanelAccent { get; set; } = "#FDDAE9";

        [Display(Order = 136)]
        [DisplayName("Calendar Saturday Current Selected")]
        [Description("Native Kotone shared gender palette index 0x0B @ 0x1405CE390: Calendar Saturday current/selected. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#8FC2E2")]
        public string FemcCalendarSaturdaySelected { get; set; } = "#8FC2E2";

        [Display(Order = 137)]
        [DisplayName("Calendar Saturday Normal Unselected")]
        [Description("Native Kotone shared gender palette index 0x0C @ 0x1405CE394: Calendar Saturday normal/unselected. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#007BD2")]
        public string FemcCalendarSaturdayNormal { get; set; } = "#007BD2";

        [Display(Order = 138)]
        [DisplayName("Calendar Sunday Special Current")]
        [Description("Native Kotone shared gender palette index 0x0D @ 0x1405CE398: Calendar Sunday/special current. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#ED939F")]
        public string FemcCalendarSundaySpecialSelected { get; set; } = "#ED939F";

        [Display(Order = 139)]
        [DisplayName("Calendar Sunday Special Normal")]
        [Description("Native Kotone shared gender palette index 0x0E @ 0x1405CE39C: Calendar Sunday/special normal. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#910010")]
        public string FemcCalendarSundaySpecialNormal { get; set; } = "#910010";

        [Display(Order = 140)]
        [DisplayName("Disabled Neutral Indexed State")]
        [Description("Native Kotone shared gender palette index 0x0F @ 0x1405CE3A0: Disabled/neutral indexed state. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#787878")]
        public string FemcDisabledNeutralIndexedState { get; set; } = "#787878";

        [Display(Order = 141)]
        [DisplayName("Calendar Frame Decorative Accent")]
        [Description("Native Kotone shared gender palette index 0x10 @ 0x1405CE3A4: Calendar/decorative pale accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FDE8F1")]
        public string FemcCalendarFrameDecorativeAccent { get; set; } = "#FDE8F1";

        [Display(Order = 142)]
        [DisplayName("Indexed Pale List Menu Accent")]
        [Description("Native Kotone shared gender palette index 0x11 @ 0x1405CE3A8: Pale indexed list/menu accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FFEEF6")]
        public string FemcIndexedPaleListMenuAccent { get; set; } = "#FFEEF6";

        [Display(Order = 143)]
        [DisplayName("Reverse Social Link Frame")]
        [Description("Native Kotone shared gender palette index 0x12 @ 0x1405CE3AC: Reverse Social Link frame. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#620000")]
        public string FemcReverseSocialLinkFrame { get; set; } = "#620000";

        [Display(Order = 144)]
        [DisplayName("Reverse Social Link Interior Rank Accent")]
        [Description("Native Kotone shared gender palette index 0x13 @ 0x1405CE3B0: Reverse Social Link interior/rank. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FF3737")]
        public string FemcReverseSocialLinkInteriorAccent { get; set; } = "#FF3737";

        [Display(Order = 145)]
        [DisplayName("Broken Social Link Interior")]
        [Description("Native Kotone shared gender palette index 0x14 @ 0x1405CE3B4: Broken Social Link interior. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#960067")]
        public string FemcBrokenSocialLinkInterior { get; set; } = "#960067";

        [Display(Order = 146)]
        [DisplayName("Broken Social Link Accent")]
        [Description("Native Kotone shared gender palette index 0x15 @ 0x1405CE3B8: Broken Social Link accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FFADFF")]
        public string FemcBrokenSocialLinkAccent { get; set; } = "#FFADFF";

        [Display(Order = 147)]
        [DisplayName("Social Link Effect Pulse A")]
        [Description("Native Kotone shared gender palette index 0x16 @ 0x1405CE3BC: Social Link effect pulse A. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#F4000A")]
        public string FemcSocialLinkEffectPulseA { get; set; } = "#F4000A";

        [Display(Order = 148)]
        [DisplayName("Social Link Effect Pulse B")]
        [Description("Native Kotone shared gender palette index 0x17 @ 0x1405CE3C0: Social Link effect pulse B. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#F400BA")]
        public string FemcSocialLinkEffectPulseB { get; set; } = "#F400BA";

        [Display(Order = 149)]
        [DisplayName("Persona Progress Gauge Accent")]
        [Description("Native Kotone shared gender palette index 0x18 @ 0x1405CE3C4: Persona progress/gauge accent. All consumers of this same bank index intentionally share this option.")]
        [Category("Camp Command Menu Shared Gender Palette")]
        [DefaultValue("#FF7335")]
        public string FemcPersonaProgressGaugeAccent { get; set; } = "#FF7335";


        [Display(Order = 150)]
        [DisplayName("Persona Dark Model Accent")]
        [Description("Sets the Persona model and frame tint.")]
        [Category("Experimental Hardcoded Gender Colors")]
        [DefaultValue("#64003C")]
        public string FemcPersonaDarkModelAccent { get; set; } = "#64003C";

        [Display(Order = 151)]
        [DisplayName("Dialogue Prompt / Advance Accent")]
        [Description("Route-specific dialogue prompt accent from FUN_140244F80. Kotone vanilla is #F4589E.")]
        [Category("Dialogue UI Colors")]
        [DefaultValue("#F4589E")]
        public string FemcDialoguePromptAccent { get; set; } = "#F4589E";

        [Display(Order = 152)]
        [DisplayName("Dialogue Choice Highlight Bar")]
        [Description("Route-specific selected-row bar in FUN_140244F80. Makoto uses #428AFF and Kotone uses #EC447F.")]
        [Category("Dialogue UI Colors")]
        [DefaultValue("#EC447F")]
        public string FemcDialogueWindowAccent { get; set; } = "#EC447F";

        [Display(Order = 222)]
        [DisplayName("Dialogue Choice Text Color")]
        [Description("Compatibility setting only. FUN_140244F80 uses this route-specific value for dialogue choice text, so the mod now leaves the original game immediate untouched.")]
        [Category("Dialogue UI Colors")]
        [DefaultValue("#EC447F")]
        public string FemcDialogueChoiceTextColor { get; set; } = "#EC447F";

        [Display(Order = 153)]
        [DisplayName("Field NPC Name Color")]
        [Description("Compatibility setting only. Field NPC-name text is intentionally left at the original game colour.")]
        [Category("Experimental Hardcoded Gender Colors")]
        [DefaultValue("#461E32")]
        public string FemcFieldNpcNameColor { get; set; } = "#461E32";

        [Display(Order = 154)]
        [DisplayName("Shuffle Time Result Accent")]
        [Description("Sets the route-specific Shuffle Time result accent. Kotone vanilla is #FFAD9B.")]
        [Category("Shuffle Time Colors")]
        [DefaultValue("#FFAD9B")]
        public string FemcBattleShuffleResultAccent { get; set; } = "#FFAD9B";

        [Display(Order = 155)]
        [DisplayName("Field Cursor Effect 1")]
        [Description("Sets field cursor effect type 1.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#FF9BB1")]
        public string FemcFieldCursorEffectType1 { get; set; } = "#FF9BB1";

        [Display(Order = 156)]
        [DisplayName("Field Cursor Effect 3")]
        [Description("Sets field cursor effect type 3.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#FFCC00")]
        public string FemcFieldCursorEffectType3 { get; set; } = "#FFCC00";

        [Display(Order = 157)]
        [DisplayName("Field Cursor Effect 2")]
        [Description("Sets field cursor effect type 2.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#BAFF00")]
        public string FemcFieldCursorEffectType2 { get; set; } = "#BAFF00";

        [Display(Order = 158)]
        [DisplayName("Field Cursor Effect 4")]
        [Description("Sets field cursor effect type 4.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#B165FF")]
        public string FemcFieldCursorEffectType4 { get; set; } = "#B165FF";

        [Display(Order = 159)]
        [DisplayName("Field Cursor Effect 5")]
        [Description("Sets field cursor effect type 5.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#FE6000")]
        public string FemcFieldCursorEffectType5 { get; set; } = "#FE6000";

        [Display(Order = 160)]
        [DisplayName("Field Cursor Flash")]
        [Description("Sets the field cursor flash color.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#FF759A")]
        public string FemcFieldCursorFlashParticle { get; set; } = "#FF759A";

        [Display(Order = 161)]
        [DisplayName("Field Cursor Ring")]
        [Description("Sets the field cursor ring color.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#CA48B4")]
        public string FemcFieldCursorRingCenter { get; set; } = "#CA48B4";

        [Display(Order = 162)]
        [DisplayName("NPC Name Effect")]
        [Description("Sets the field NPC name effect color.")]
        [Category("Experimental Field Cursor Colors")]
        [DefaultValue("#FF9BB1")]
        public string FemcFieldNpcNameEffect { get; set; } = "#FF9BB1";

        [Display(Order = 163)]
        [DisplayName("Day Change Transition Accent")]
        [Description("Sets the animated day-change transition accent.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#A990FE")]
        public string FemcDateTransitionAccent { get; set; } = "#A990FE";

        [Display(Order = 164)]
        [DisplayName("Day Change Text")]
        [Description("Compatibility setting only. Day-change/calendar text is intentionally left at vanilla Kotone #EAE4FF.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#EAE4FF")]
        public string FemcDateTextColor { get; set; } = "#EAE4FF";

        [Display(Order = 165)]
        [DisplayName("Day Change Main Accent")]
        [Description("Sets the main accent used by the day-change/calendar transition UI.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#747474")]
        public string FemcDateMainAccent { get; set; } = "#747474";

        [Display(Order = 166)]
        [DisplayName("Calendar Normal Day")]
        [Description("Sets normal date state A.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#625C88")]
        public string FemcDateNormalDayA { get; set; } = "#625C88";

        [Display(Order = 167)]
        [DisplayName("Calendar Selected Day")]
        [Description("Sets selected date state A.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#FFFFFF")]
        public string FemcDateSelectedDayA { get; set; } = "#FFFFFF";

        [Display(Order = 168)]
        [DisplayName("Calendar Special Marker")]
        [Description("Sets the date special marker color.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#5790AA")]
        public string FemcDateSpecialMarker { get; set; } = "#5790AA";

        [Display(Order = 169)]
        [DisplayName("Calendar Sunday Special Day")]
        [Description("Sets Sunday and special state A.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#81665C")]
        public string FemcDateSundaySpecialA { get; set; } = "#81665C";

        [Display(Order = 170)]
        [DisplayName("Calendar Detail Accent")]
        [Description("Sets the date detail accent color.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#2A2A2A")]
        public string FemcDateDetailAccent { get; set; } = "#2A2A2A";

        [Display(Order = 171)]
        [DisplayName("Calendar Sunday Special Detail")]
        [Description("Sets Sunday and special state B.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#A8928B")]
        public string FemcDateSundaySpecialB { get; set; } = "#A8928B";

        [Display(Order = 172)]
        [DisplayName("Calendar Normal Day Detail")]
        [Description("Sets normal date state B.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#8F8BAA")]
        public string FemcDateNormalDayB { get; set; } = "#8F8BAA";

        [Display(Order = 173)]
        [DisplayName("Calendar Weekday Detail")]
        [Description("Sets the weekday detail color.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#7F7F7F")]
        public string FemcDateWeekdayDetail { get; set; } = "#7F7F7F";

        [Display(Order = 174)]
        [DisplayName("Calendar Selected Day Detail")]
        [Description("Sets selected date state B.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#D1A28D")]
        public string FemcDateSelectedDayB { get; set; } = "#D1A28D";

        [Display(Order = 175)]
        [DisplayName("Calendar Current Day")]
        [Description("Sets the current day accent color.")]
        [Category("Experimental Day Change Calendar Colors")]
        [DefaultValue("#AFA5E9")]
        public string FemcDateCurrentDay { get; set; } = "#AFA5E9";

        [Display(Order = 176)]
        [DisplayName("Field HUD Color 0")]
        [Description("Sets field HUD color slot 0.")]
        [Category("Experimental Field HUD Colors")]
        [DefaultValue("#CA2043")]
        public string FemcFieldHudColor0 { get; set; } = "#CA2043";

        [Display(Order = 177)]
        [DisplayName("Field HUD Color 1")]
        [Description("Sets field HUD color slot 1.")]
        [Category("Experimental Field HUD Colors")]
        [DefaultValue("#FFC1DC")]
        public string FemcFieldHudColor1 { get; set; } = "#FFC1DC";

        [Display(Order = 178)]
        [DisplayName("Field HUD Color 2")]
        [Description("Sets field HUD color slot 2.")]
        [Category("Experimental Field HUD Colors")]
        [DefaultValue("#6B010A")]
        public string FemcFieldHudColor2 { get; set; } = "#6B010A";

        [Display(Order = 179)]
        [DisplayName("Social Link Rank Color 0")]
        [Description("Sets Social Link rank effect slot 0.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FFF0F0")]
        public string FemcSocialRankColor0 { get; set; } = "#FFF0F0";

        [Display(Order = 180)]
        [DisplayName("Social Link Rank Color 1")]
        [Description("Sets Social Link rank effect slot 1.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FF5C80")]
        public string FemcSocialRankColor1 { get; set; } = "#FF5C80";

        [Display(Order = 181)]
        [DisplayName("Social Link Rank Color 2")]
        [Description("Sets Social Link rank effect slot 2.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FF5C80")]
        public string FemcSocialRankColor2 { get; set; } = "#FF5C80";

        [Display(Order = 182)]
        [DisplayName("Social Link Rank Color 3")]
        [Description("Sets Social Link rank effect slot 3.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FF77FD")]
        public string FemcSocialRankColor3 { get; set; } = "#FF77FD";

        [Display(Order = 183)]
        [DisplayName("Social Link Rank Color 4")]
        [Description("Sets Social Link rank effect slot 4.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FF5C80")]
        public string FemcSocialRankColor4 { get; set; } = "#FF5C80";

        [Display(Order = 184)]
        [DisplayName("Social Link Rank Color 5")]
        [Description("Sets Social Link rank effect slot 5.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#B82E4B")]
        public string FemcSocialRankColor5 { get; set; } = "#B82E4B";

        [Display(Order = 185)]
        [DisplayName("Social Link Rank Color 6")]
        [Description("Sets Social Link rank effect slot 6.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FF84A8")]
        public string FemcSocialRankColor6 { get; set; } = "#FF84A8";

        [Display(Order = 186)]
        [DisplayName("Social Link Rank Color 7")]
        [Description("Sets Social Link rank effect slot 7.")]
        [Category("Experimental Social Link Rank Colors")]
        [DefaultValue("#FF6994")]
        public string FemcSocialRankColor7 { get; set; } = "#FF6994";

        [Display(Order = 187)]
        [DisplayName("Aohige Lookup 0x2C")]
        [Description("DAT_1407BB3B0[0x2C] @ 0x1407BB460. Confirmed Aohige Pharmacy lookup. Vanilla #5ABEFF; normalized default #FFC1DC.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FFC1DC")]
        public string PharmacyColor2C { get; set; } = "#FFC1DC";

        [Display(Order = 188)]
        [DisplayName("Aohige Lookup 0x2D")]
        [Description("DAT_1407BB3B0[0x2D] @ 0x1407BB464. Confirmed Aohige Pharmacy lookup. Vanilla #449CFC; normalized default #FF759A.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FF759A")]
        public string PharmacyColor2D { get; set; } = "#FF759A";

        [Display(Order = 189)]
        [DisplayName("Aohige Lookup 0x2E")]
        [Description("DAT_1407BB3B0[0x2E] @ 0x1407BB468. Confirmed Aohige Pharmacy lookup. Vanilla #123941; normalized default #6B010A.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#6B010A")]
        public string PharmacyColor2E { get; set; } = "#6B010A";

        [Display(Order = 190)]
        [DisplayName("Aohige Lookup 0x2F")]
        [Description("DAT_1407BB3B0[0x2F] @ 0x1407BB46C. Confirmed Aohige Pharmacy lookup. Vanilla #DCF3FF; normalized default #FFEEF6.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FFEEF6")]
        public string PharmacyColor2F { get; set; } = "#FFEEF6";

        [Display(Order = 191)]
        [DisplayName("Aohige Lookup 0x30")]
        [Description("DAT_1407BB3B0[0x30] @ 0x1407BB470. Confirmed Aohige Pharmacy lookup. Vanilla #FFFFFF; normalized default #FFFFFF.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FFFFFF")]
        public string PharmacyColor30 { get; set; } = "#FFFFFF";

        [Display(Order = 192)]
        [DisplayName("Aohige Lookup 0x31")]
        [Description("DAT_1407BB3B0[0x31] @ 0x1407BB474. Confirmed Aohige Pharmacy lookup. Vanilla #00F2FF; normalized default #FF68C6.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FF68C6")]
        public string PharmacyColor31 { get; set; } = "#FF68C6";

        [Display(Order = 193)]
        [DisplayName("Aohige Lookup 0x32")]
        [Description("DAT_1407BB3B0[0x32] @ 0x1407BB478. Confirmed Aohige Pharmacy lookup. Vanilla #74AEEB; normalized default #FFC1DC.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FFC1DC")]
        public string PharmacyColor32 { get; set; } = "#FFC1DC";

        [Display(Order = 194)]
        [DisplayName("Aohige Lookup 0x35")]
        [Description("DAT_1407BB3B0[0x35] @ 0x1407BB484. Confirmed Aohige Pharmacy lookup. Vanilla #FFF370; normalized default #FFF370.")]
        [Category("Experimental Aohige Pharmacy")]
        [DefaultValue("#FFF370")]
        public string PharmacyColor35 { get; set; } = "#FFF370";

        [Display(Order = 195)]
        [DisplayName("Antique Lookup 0x36")]
        [Description("DAT_1407BB3B0[0x36] @ 0x1407BB488. Confirmed Antique Shop lookup. Vanilla #008FA9; normalized default #DE1F5B.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#DE1F5B")]
        public string AntiqueColor36 { get; set; } = "#DE1F5B";

        [Display(Order = 196)]
        [DisplayName("Antique Lookup 0x37")]
        [Description("DAT_1407BB3B0[0x37] @ 0x1407BB48C. Confirmed Antique Shop lookup. Vanilla #DCF3FF; normalized default #FFEEF6.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#FFEEF6")]
        public string AntiqueColor37 { get; set; } = "#FFEEF6";

        [Display(Order = 197)]
        [DisplayName("Antique Lookup 0x38")]
        [Description("DAT_1407BB3B0[0x38] @ 0x1407BB490. Confirmed Antique Shop lookup. Vanilla #123942; normalized default #6B010A.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#6B010A")]
        public string AntiqueColor38 { get; set; } = "#6B010A";

        [Display(Order = 198)]
        [DisplayName("Antique Lookup 0x3A")]
        [Description("DAT_1407BB3B0[0x3A] @ 0x1407BB498. Confirmed Antique Shop lookup. Vanilla #FFFFFF; normalized default #FFFFFF.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#FFFFFF")]
        public string AntiqueColor3A { get; set; } = "#FFFFFF";

        [Display(Order = 199)]
        [DisplayName("Antique Lookup 0x3B")]
        [Description("DAT_1407BB3B0[0x3B] @ 0x1407BB49C. Confirmed Antique Shop lookup. Vanilla #F58F02; normalized default #F58F02.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#F58F02")]
        public string AntiqueColor3B { get; set; } = "#F58F02";

        [Display(Order = 200)]
        [DisplayName("Antique Lookup 0x3E")]
        [Description("DAT_1407BB3B0[0x3E] @ 0x1407BB4A8. Confirmed Antique Shop lookup. Vanilla #8FD0FC; normalized default #FDDAE9.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#FDDAE9")]
        public string AntiqueColor3E { get; set; } = "#FDDAE9";

        [Display(Order = 201)]
        [DisplayName("Antique Lookup 0x3F")]
        [Description("DAT_1407BB3B0[0x3F] @ 0x1407BB4AC. Confirmed Antique Shop lookup. Vanilla #FFC323; normalized default #FFC323.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#FFC323")]
        public string AntiqueColor3F { get; set; } = "#FFC323";

        [Display(Order = 202)]
        [DisplayName("Antique Stat Decrease")]
        [Description("Independent local stat-decrease immediate in FUN_14017F8F0. Vanilla byte RGB #FFA9B3; not a DAT_1407BB3B0 entry.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#FFA9B3")]
        public string AntiqueComparisonDecrease { get; set; } = "#FFA9B3";

        [Display(Order = 203)]
        [DisplayName("Antique Stat Increase")]
        [Description("Independent local stat-increase immediate in FUN_14017F8F0. Vanilla byte RGB #8CE4FA; not a DAT_1407BB3B0 entry.")]
        [Category("Experimental Antique Shop")]
        [DefaultValue("#8CE4FA")]
        public string AntiqueComparisonIncrease { get; set; } = "#8CE4FA";

        [Display(Order = 206)]
        [DisplayName("iFlash Primary Light")]
        [Description("Kotone iFlash palette index 0. Makoto uses #6DB3FF and Kotone uses #FFC1DC. This route-specific entry is shared by several Camp transition presets.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FFC1DC")]
        public string FemcCampIFlashPrimaryLight { get; set; } = "#FFC1DC";

        [Display(Order = 207)]
        [DisplayName("iFlash Full Screen Transition")]
        [Description("Kotone iFlash palette index 1. Makoto uses #2494FF and Kotone uses #FF759A. Used by full-screen Camp transition geometry.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FF759A")]
        public string FemcCampIFlashFullScreenTransition { get; set; } = "#FF759A";

        [Display(Order = 208)]
        [DisplayName("iFlash Warm Accent")]
        [Description("Kotone iFlash palette index 2. Makoto uses #A9A49B and Kotone uses #FFA49B. No preset-1 root use was found, but the route palette itself differs and is exposed intentionally.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FFA49B")]
        public string FemcCampIFlashWarmAccent { get; set; } = "#FFA49B";

        [Display(Order = 209)]
        [DisplayName("iFlash Command Orange")]
        [Description("Kotone iFlash palette index 6, #FFC449. This is an iFlash transition color and is separate from the hardcoded COMMAND header tint even though vanilla uses the same RGB.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FFC449")]
        public string FemcCampIFlashCommandOrange { get; set; } = "#FFC449";

        [Display(Order = 210)]
        [DisplayName("iFlash Submenu Accent")]
        [Description("Kotone iFlash palette index 7. Makoto uses #2494FF and Kotone uses #FF94FF.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FF94FF")]
        public string FemcCampIFlashSubmenuAccent { get; set; } = "#FF94FF";

        [Display(Order = 211)]
        [DisplayName("iFlash Submenu Light Accent")]
        [Description("Kotone iFlash palette index 8. Makoto uses #6DB3DE and Kotone uses #FFB3DE.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FFB3DE")]
        public string FemcCampIFlashSubmenuLightAccent { get; set; } = "#FFB3DE";

        [Display(Order = 212)]
        [DisplayName("Command Dark Background")]
        [Description("Kotone iFlash palette index 42. Root preset 1 quad 0 draws the large dark base ellipse. Makoto uses #003C5F and Kotone uses #6B010A.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#6B010A")]
        public string FemcCampCommandDarkBackground { get; set; } = "#6B010A";

        [Display(Order = 213)]
        [DisplayName("Command Opening Accent")]
        [Description("Kotone iFlash palette index 43. Root preset 1 quad 1 draws the moving/opening accent ellipse. Makoto uses #2494FF and Kotone uses #FF759A.")]
        [Category("Camp Command Menu iFlash Palette")]
        [DefaultValue("#FF759A")]
        public string FemcCampCommandOpeningAccent { get; set; } = "#FF759A";

        [Display(Order = 214)]
        [DisplayName("COMMAND Header Orange")]
        [Description("Hardcoded #FFC449 tint used by the cached COMMAND foreground sprite in FUN_14014B350. Kept separate from iFlash index 6 because they are independent original color sources.")]
        [Category("Camp Command Menu Foreground Colors")]
        [DefaultValue("#FFC449")]
        public string FemcCampCommandHeaderOrange { get; set; } = "#FFC449";

        [Display(Order = 215)]
        [DisplayName("Camp Foreground Dark Primitive")]
        [Description("Hardcoded Kotone foreground primitive in FUN_14014B350. Makoto uses #0D2736 and Kotone uses #490107. This is separate from the iFlash Command Dark Background.")]
        [Category("Camp Command Menu Foreground Colors")]
        [DefaultValue("#490107")]
        public string FemcCampForegroundDarkPrimitive { get; set; } = "#490107";

        [Display(Order = 217)]
        [DisplayName("Top-Right Date Corner Background")]
        [Description("Persistent field-date route color at DAT_140772C60/C64. Makoto uses #61AFFE and Kotone uses #FA99B4. This is the large route-colored top-right corner/triangle background.")]
        [Category("Field Date HUD Colors")]
        [DefaultValue("#FA99B4")]
        public string FemcFieldDateCornerBackground { get; set; } = "#FA99B4";

        [Display(Order = 218)]
        [DisplayName("Top-Right Date Secondary Light Accent")]
        [Description("Persistent field-date route color at DAT_140772C68/C6C. Makoto uses #DFF3FF and Kotone uses #FDE6EE.")]
        [Category("Field Date HUD Colors")]
        [DefaultValue("#FDE6EE")]
        public string FemcFieldDateSecondaryLightAccent { get; set; } = "#FDE6EE";

        [Display(Order = 219)]
        [DisplayName("Top-Right Date Dark Accent")]
        [Description("Persistent field-date route color at DAT_140772C70/C74. Makoto uses #437FAF and Kotone uses #A12832. This is a route-specific field-date tint and remains independently configurable.")]
        [Category("Field Date HUD Colors")]
        [DefaultValue("#A12832")]
        public string FemcFieldDateDarkAccent { get; set; } = "#A12832";

        [Display(Order = 220)]
        [DisplayName("Top-Right Date Number / Weekday Tint")]
        [Description("Persistent field-date route color at DAT_140772C78/C7C. Makoto uses #256387 and Kotone uses #A12832. This directly tints the route-colored date/day glyph sprites.")]
        [Category("Field Date HUD Colors")]
        [DefaultValue("#A12832")]
        public string FemcFieldDateNumberWeekdayTint { get; set; } = "#A12832";

        [Display(Order = 221)]
        [DisplayName("Top-Right Date Light Accent")]
        [Description("Persistent field-date route color at DAT_140772C88/C8C. Makoto uses #72EDFF and Kotone uses #FFC1DC. This is another route-specific light tint used by the persistent field-date renderer.")]
        [Category("Field Date HUD Colors")]
        [DefaultValue("#FFC1DC")]
        public string FemcFieldDateLightAccent { get; set; } = "#FFC1DC";

        [Display(Order = 216)]
        [DisplayName("Enable Color Discovery Logging")]
        [Description("Logs UI color callers and values for debugging.")]
        [Category("Experimental Color Discovery Logging")]
        [DefaultValue(false)]
        public bool ExperimentalColorLogging { get; set; } = false;

    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}
