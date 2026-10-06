using Reloaded.Hooks.Definitions;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Reloaded.Memory.Interfaces;

using p3ppc.totalkotoneoverhaul.Configuration;
using p3ppc.totalkotoneoverhaul.Template;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using PAK.Stream.Emulator;
using PAK.Stream.Emulator.Interfaces;
using static p3ppc.totalkotoneoverhaul.Utils;

namespace p3ppc.totalkotoneoverhaul
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {
        /// <summary>
        /// Provides access to the mod loader API.
        /// </summary>
        private readonly IModLoader _modLoader;

        /// <summary>
        /// Provides access to the Reloaded.Hooks API.
        /// </summary>
        /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
        private readonly IReloadedHooks? _hooks;

        /// <summary>
        /// Provides access to the Reloaded logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Entry point into the mod, instance that created this class.
        /// </summary>
        private readonly IMod _owner;

        /// <summary>
        /// Provides access to this mod's configuration.
        /// </summary>
        private Config _configuration;

        /// <summary>
        /// The configuration of the currently executing mod.
        /// </summary>
        private readonly IModConfig _modConfig;

        private readonly Reloaded.Memory.Memory memory;

        private nuint _battlePanelPalette;
        private nuint _battleMenuPalette;
        private nuint _battleResultsPalette;
        private nuint _shufflePalette;
        private nuint _sharedCampPalette;
        private nuint _campRootPalette;
        private nuint _campIFlashPalette;
        private nuint _fieldCursorPalette;
        private nuint _datePaletteA;
        private nuint _datePaletteB;
        private nuint _fieldDatePalette;
        private nuint _fieldHudPalette;
        private nuint _socialRankPalette;

        
        
        private nuint _facilityColorTable;
        private nuint _antiqueComparisonDecreasePatch;
        private nuint _antiqueComparisonIncreasePatch;

        
        
        private nuint _socialLinkBustupTintPatchA;
        private nuint _socialLinkBustupTintPatchB;
        private nuint _partyPanelCharacterNamePatch;
        private nuint _skillTextNormalPatchA;
        private nuint _skillTextNormalPatchB;
        private nuint _skillTextSelectedPatch;
        private nuint _skillTextNewPatch;
        private readonly List<nuint> _statusAccentPatches = new();
        private nuint _statusStateAccentPatch;
        private readonly List<nuint> _partyPanelStatusPatches = new();
        private nuint _campPartyStatusPatch;

        private nuint _campCommandHeaderStructColorPatch;
        private nuint _campCommandHeaderArgsColorPatch;
        private nuint _campCommandHeaderRegisterColorPatch;
        private nuint _campForegroundDarkPrimitivePatch;

        private nuint _battleEfficacyAccentPatch;
        private readonly List<nuint> _personaDarkEbxPatches = new();
        private readonly List<nuint> _personaDarkEsiPatches = new();
        private readonly List<nuint> _personaDarkEcxPatches = new();
        private nuint _personaInitialTransitionPatch;
        private nuint _personaTransitionPrimitivePatch;
        private nuint _dialoguePromptPatch;
        private nuint _dialogueChoiceTextPatch;
        private nuint _dialogueWindowPatch;
        private nuint _fieldNpcNamePatch;
        private nuint _shuffleResultPatch;

        private IAsmHook? _campTransitionOverlayDrawHook;
        private nuint _campTransitionColorBuffer;

        private readonly ColorLogger _colorLogger;


        public Mod(ModContext context)
        {
               
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;
            memory = Reloaded.Memory.Memory.Instance;
            _campTransitionColorBuffer = (nuint)Marshal.AllocHGlobal(3);
            UpdateCampTransitionColor();

            _modLoader.OnModLoaderInitialized += OnModLoaderInitialized;

            Utils.Initialise(_logger, _configuration, _modLoader);

            _colorLogger = new ColorLogger(_hooks, _configuration);
            var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId); // modDir variable for file emulation

            var criFsController = _modLoader.GetController<ICriFsRedirectorApi>();
            if (criFsController == null || !criFsController.TryGetTarget(out var criFsApi))
            {
                _logger.WriteLine($"Something in CriFS shit its pants! Normal files will not load properly!", System.Drawing.Color.Red);
                return;
            }

            var PakEmulatorController = _modLoader.GetController<IPakEmulator>();
            if (PakEmulatorController == null || !PakEmulatorController.TryGetTarget(out var _PakEmulator))
            {
                _logger.WriteLine($"Something in PAK Emulator shit its pants! Files requiring bin merging will not load properly!", System.Drawing.Color.Red);
                return;
            }

            if (_configuration.FEMCTitleScreen)
            {
                var titleColor = GetColor(_configuration.TitleScreenColor, "Title Screen Color", 0xB2, 0x31, 0x46);
                var flag = _modLoader.GetActiveMods().Any(x => x.Generic.ModId == "p3ppc.kotonecutscenes");
                if (!flag)
                {
                    SigScan("C7 45 ?? 00 01 25 FF", "Femc Title Screen", address =>
                    {
                        memory.SafeWrite((nuint)(address + 3), new byte[] { titleColor.R, titleColor.G, titleColor.B, 0xFF });
                    });

                    SigScan("75 ?? F6 83 ?? ?? ?? ?? 02 74 ?? E8 ?? ?? ?? ??", "Fixing my mistakes", address =>
                    {
                        memory.SafeWrite((nuint)address, new byte[] { 0x90, 0x90 });
                    });

                    SigScan("0F BA F0 07 ?? ?? ?? ?? ?? ?? ??", "Pink Loading Card + Title config", 4, address =>
                    {
                        memory.SafeWrite((nuint)(address + 2), new byte[] { 0xE8 });
                    });

                    SigScan("31 73 18 83 4B 04 02 C7 03 08 00 00 00", "Disable Title Portrait Transition", address =>
                    {
                        memory.SafeWrite((nuint)address, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
                    });

                    SigScan("83 73 18 01 83 4B 04 02 C7 03 06 00 00 00 89 7B 1C", "No Title Portrait Return Transition", address =>
                    {
                        memory.SafeWrite((nuint)address, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
                        memory.SafeWrite((nuint)(address + 10), new byte[] { 0x07 });
                        memory.SafeWrite((nuint)(address + 16), new byte[] { 0x08 });
                    });

                    SigScan("23 C6 89 7B 30 89 43 18 C7 43 38 00 00 80 3F", "Always Initialize Kotone Title Portrait", address =>
                    {
                        memory.SafeWrite((nuint)address, new byte[] { 0x31, 0xC0 });
                    });

                    SigScan("BF 00 00 01 00 89 7B 28 23 C6 89 7B 30", "Force Kotone Title Variant", address =>
                    {
                        memory.SafeWrite((nuint)(address + 3), new byte[] { 0x02 });
                    });

                    criFsApi.AddProbingPath("Title Screen/P5REssentials/CPK");
                }
            }


            RegisterHardcodedGenderColorScans();
            RegisterGenderPaletteScans();
            RegisterCampRendererColorPatches();

            if (_configuration.Timer)
            {
                var timerColor = GetColor(_configuration.TimerColor, "Time Limit Color", 0xFF, 0xC1, 0xDC);

                SigScan("C6 44 24 ?? 5B", "Timer R", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { timerColor.R });
                });

                SigScan("C6 44 24 ?? FF C6 44 24 ?? 5B", "Timer G", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { timerColor.G });
                });

                SigScan("C6 44 24 30 ED", "Timer B", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { timerColor.B });
                });
            }

            if (_configuration.AOA)
            {
                var aoaColor = GetColor(_configuration.AOAPromptColor, "AOA Prompt Color", 0xFD, 0xE8, 0xF1);

                SigScan("C6 44 24 38 FF C6 44 24 30 CF 48 8B CF C6 44 24 28 9C", "AOA Prompt", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, aoaColor.B, 0xC6, 0x44, 0x24, 0x30, aoaColor.G, 0x48, 0x8B, 0xCF, 0xC6, 0x44, 0x24, 0x28, aoaColor.R });
                });

                SigScan("C6 44 24 38 FF C6 44 24 30 CF 44 8D 42 6F C6 44 24 28 9C", "AOA Prompt PT 2", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, aoaColor.B, 0xC6, 0x44, 0x24, 0x30, aoaColor.G, 0x44, 0x8D, 0x42, 0x6F, 0xC6, 0x44, 0x24, 0x28, aoaColor.R });
                });

                criFsApi.AddProbingPath("AOA/P5REssentials/CPK");
            }

            if (_configuration.OneMore)
            {
                var oneMoreDiamond = GetColor(_configuration.OneMoreDiamondColor, "One More Diamond Color", 0xFF, 0xC1, 0xDC);
                var oneMoreLightning = GetColor(_configuration.OneMoreLightningColor, "One More Lightning Color", 0xDE, 0x1F, 0x5B);

                SigScan("41 BE 00 FF 82 66", "One More Diamond", address =>
                {
                    memory.SafeWrite((nuint)(address + 3), new byte[] { oneMoreDiamond.B, oneMoreDiamond.G, oneMoreDiamond.R });
                });

                SigScan("41 0F 44 C0 41 B8 0C 02 00 00", "One More Lightning R", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0x41, 0x8B, 0xC0, 0x90 });
                });

                SigScan("80 E1 19", "One More Lightning G", address =>
                {
                    memory.SafeWrite((nuint)(address + 2), new byte[] { oneMoreLightning.G });
                });

                SigScan("41 B8 FF 00 00 00 66 F7 D8", "One More Lightning B", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0x41, 0xB0, oneMoreLightning.R, 0x80, 0xE2, oneMoreLightning.B });
                });
            }

            if (_configuration.ILoveJesus)
            {
                var analysisPrimary = GetColor(_configuration.AnalysisPrimaryColor, "Analysis Primary Color", 0xFD, 0xDA, 0xE9);
                var analysisHover = GetColor(_configuration.AnalysisHoverColor, "Analysis Hover Color", 0xFF, 0xEE, 0xF6);
                var analysisSelection = GetColor(_configuration.AnalysisSelectionColor, "Analysis Selection Color", 0xFF, 0x75, 0x9A);

                SigScan("40 57 41 57 48 81 EC 98 01 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4", "AnalysisRenderEnemyDetails", address =>
                {
                    int[] redWrites = { 0x1D6, 0x20C, 0x2A1, 0x374, 0x504, 0x5C3, 0x6CD, 0x70C, 0x7E2, 0x912, 0xA55, 0xB9D, 0xBF3, 0xDB1, 0xDEF, 0xE4B, 0xE83, 0xEBD, 0xF5C };
                    int[] greenWrites = { 0x1CE, 0x204, 0x29C, 0x36C, 0x4FC, 0x5BE, 0x6C5, 0x703, 0x7DA, 0x908, 0xA47, 0xB82, 0xBE9, 0xDAC, 0xDEA, 0xE46, 0xE7B, 0xEB8, 0xF54 };
                    int[] blueWrites = { 0x1C4, 0x1FA, 0x294, 0x362, 0x4F2, 0x5B9, 0x6BB, 0x6FE, 0x7D0, 0x900, 0xA42, 0xB7B, 0xBDF, 0xD9D, 0xDE2, 0xE3E, 0xE6A, 0xEAF, 0xF4A };

                    foreach (var offset in redWrites) memory.SafeWrite((nuint)(address + offset + 4), new byte[] { analysisPrimary.R });
                    foreach (var offset in greenWrites) memory.SafeWrite((nuint)(address + offset + 4), new byte[] { analysisPrimary.G });
                    foreach (var offset in blueWrites) memory.SafeWrite((nuint)(address + offset + 4), new byte[] { analysisPrimary.B });

                    memory.SafeWrite((nuint)(address + 0x3F1 + 2), new byte[] { 0xFF, analysisPrimary.B, analysisPrimary.G, analysisPrimary.R });
                });

                SigScanAll("C7 45 ?? 69 DF FF FF", "Analysis Packed Primary", address =>
                {
                    memory.SafeWrite((nuint)(address + 3), new byte[] { analysisPrimary.R, analysisPrimary.G, analysisPrimary.B, 0xFF });
                });

                SigScan("44 88 7C 24 ?? F3 44 0F 11 44 24 ??", "Analysis Primary R Register", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x28, analysisPrimary.R });
                });
                SigScan("44 88 74 24 ?? 48 8B CE 44 88 7C 24 ?? F3 44 0F 11 44 24 ??", "Analysis Primary G Register", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x30, analysisPrimary.G });
                });
                SigScan("40 88 6C 24 ?? BA 0B 00 00 00", "Analysis Primary B Register", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, analysisPrimary.B });
                });
                SigScan("40 88 6C 24 ?? 0F 28 DE", "Analysis Primary A Register", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x40, 0xFF });
                });

                SigScan("C6 44 24 38 E5 0F 57 DB C6 44 24 30 FF 48 8B CB C6 44 24 28 9F 44 8D 42 66", "Analysis Hover Next", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, analysisHover.B, 0x0F, 0x57, 0xDB, 0xC6, 0x44, 0x24, 0x30, analysisHover.G, 0x48, 0x8B, 0xCB, 0xC6, 0x44, 0x24, 0x28, analysisHover.R });
                });
                SigScan("C6 44 24 38 FF C6 44 24 30 BD C6 44 24 28 00 F3 0F 11 4C 24 20", "Analysis Normal Outer Selection", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, analysisSelection.B, 0xC6, 0x44, 0x24, 0x30, analysisSelection.G, 0xC6, 0x44, 0x24, 0x28, analysisSelection.R });
                });
                SigScan("41 81 C8 00 FF 75 00 44 89 64 24 28", "Analysis Inner Rotating Square", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { analysisSelection.B, analysisSelection.G, analysisSelection.R });
                });
                SigScan("C6 44 24 38 FF C6 44 24 30 B5 C6 44 24 28 00", "Analysis Whole Selection", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, analysisSelection.B, 0xC6, 0x44, 0x24, 0x30, analysisSelection.G, 0xC6, 0x44, 0x24, 0x28, analysisSelection.R });
                });
            }

            if (_configuration.Advantage)
            {
                var advantageGlow = GetColor(_configuration.AdvantageGlowColor, "Player Advantage Glow Color", 0xFF, 0x75, 0x9A);
                var advantageDiamond = GetColor(_configuration.AdvantageDiamondColor, "Player Advantage Diamond Color", 0xDE, 0x1F, 0x5B);
                var advantageText = GetColor(_configuration.AdvantageTextColor, "Player Advantage Text Outline Color", 0xFF, 0xC1, 0xDC);

                SigScan("C6 44 24 38 FF C6 44 24 30 19 44 88 74 24 28", "Advantage Center Glow", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, advantageGlow.B, 0xC6, 0x44, 0x24, 0x30, advantageGlow.G, 0xC6, 0x44, 0x24, 0x28, advantageGlow.R });
                });

                SigScan("66 C7 45 48 43 59 C6 45 4A FC", "Advantage Center Diamond", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { advantageDiamond.R, advantageDiamond.G, 0xC6, 0x45, 0x4A, advantageDiamond.B });
                });

                SigScan("66 C7 45 48 66 82 C6 45 4A FF", "Advantage Text", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { advantageText.R, advantageText.G, 0xC6, 0x45, 0x4A, advantageText.B });
                });

                SigScan("C7 45 48 66 82 FF FF", "Advantage Square Outline", address =>
                {
                    memory.SafeWrite((nuint)(address + 3), new byte[] { advantageText.R, advantageText.G, advantageText.B });
                });
            }
            
            if (_configuration.FusionSpells)
            {
                criFsApi.AddProbingPath("FusionSpells/P5REssentials/CPK");
            }

            if (_configuration.AOABackground)
            {
                criFsApi.AddProbingPath("Background/P5REssentials/CPK");
            }

            if (_configuration.FSMOKE)
            {
                _PakEmulator.AddDirectory(Path.Combine(modDir, "Smoke", "FEmulator", "PAK"));
            }

            if (_configuration.MapScreen)
            {
                var mapPalette = new[]
                {
                    GetColor(_configuration.MapColor1, "Map Palette 1", 0xFF, 0xDC, 0xDC),
                    GetColor(_configuration.MapColor2, "Map Palette 2", 0x43, 0x14, 0x14),
                    GetColor(_configuration.MapColor3, "Map Palette 3", 0xFF, 0xC5, 0x49),
                    GetColor(_configuration.MapColor4, "Map Palette 4", 0x55, 0x2B, 0x2B),
                    GetColor(_configuration.MapColor5, "Map Palette 5", 0xFF, 0xFF, 0xFF),
                    GetColor(_configuration.MapColor6, "Map Palette 6", 0x42, 0x12, 0x12),
                };

                _PakEmulator.AddDirectory(Path.Combine(modDir, "Map", "FEmulator", "PAK"));
                SigScan("DC F3 FF FF 14 37 43 FF FB AE 64 FF 2B 4E 55 FF FF FF FF FF 12 39 42 FF", "Map Screen", address =>
                {
                    memory.SafeWrite((nuint)address, BuildPaletteBytes(mapPalette, 0xFF));
                });
            }

            if (_configuration.MiniMap)
            {
                _PakEmulator.AddDirectory(Path.Combine(modDir, "Mini Map", "FEmulator", "PAK"));
            }

            if (_configuration.ShuffleTime)
            {
                criFsApi.AddProbingPath("ShuffleTime/P5REssentials/CPK");
            }

            if (_configuration.Tarot)
            {
                criFsApi.AddProbingPath("Tarot/P5REssentials/CPK");
                _PakEmulator.AddDirectory(Path.Combine(modDir, "Tarot", "FEmulator", "PAK"));
            }


            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic
        }


        private static RgbColor GetColor(string? hex, string settingName, byte fallbackR, byte fallbackG, byte fallbackB)
        {
            var value = (hex ?? string.Empty).Trim();

            if (value.StartsWith("#", StringComparison.Ordinal))
                value = value[1..];

            if (value.Length == 6 &&
                byte.TryParse(value.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var r) &&
                byte.TryParse(value.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var g) &&
                byte.TryParse(value.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var b))
            {
                return new RgbColor(r, g, b);
            }

            LogError($"Invalid {settingName} value '{hex}'. Expected #RRGGBB; using #{fallbackR:X2}{fallbackG:X2}{fallbackB:X2}.");
            return new RgbColor(fallbackR, fallbackG, fallbackB);
        }

        private RgbColor[] GetConfiguredSharedCampPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcPrimaryUiAccent, "Primary Shared UI Accent", 0xDE, 0x1F, 0x5B),
                GetColor(_configuration.FemcNormalSkillBoxBackground, "Normal Unselected Box Background", 0xFF, 0xC1, 0xDC),
                GetColor(_configuration.FemcSelectedSkillBoxBackground, "Selected Active Box Background", 0x6B, 0x01, 0x0A),
                GetColor(_configuration.FemcSecondaryPanelFrameAccent, "Secondary Panel Frame Accent", 0xFF, 0x75, 0x9A),
                new RgbColor(0xFF, 0xFF, 0xFF), // Text: preserve vanilla.
                GetColor(_configuration.FemcAttentionFilledMarkerHighlight, "Attention Filled Marker Highlight", 0xFF, 0xDE, 0x00),
                new RgbColor(0xFD, 0xDA, 0xE9), // Text: preserve vanilla FeMC colour.
                GetColor(_configuration.FemcEquipmentStatDecreaseIndicator, "Equipment Stat Decrease Indicator", 0x77, 0xD1, 0xFF),
                GetColor(_configuration.FemcSkillTransitionPersonaGaugeAccent, "Skill Transition Persona Gauge Accent", 0xCB, 0xFF, 0x9C),
                GetColor(_configuration.FemcNewSkillBackground, "New Skill Background", 0x64, 0x90, 0x16),
                GetColor(_configuration.FemcPersonaStatusDetailPanelAccent, "Persona Status Detail Panel Accent", 0xFD, 0xDA, 0xE9),
                GetColor(_configuration.FemcCalendarSaturdaySelected, "Calendar Saturday Current Selected", 0x8F, 0xC2, 0xE2),
                GetColor(_configuration.FemcCalendarSaturdayNormal, "Calendar Saturday Normal Unselected", 0x00, 0x7B, 0xD2),
                GetColor(_configuration.FemcCalendarSundaySpecialSelected, "Calendar Sunday Special Day Current", 0xED, 0x93, 0x9F),
                GetColor(_configuration.FemcCalendarSundaySpecialNormal, "Calendar Sunday Special Day Normal", 0x91, 0x00, 0x10),
                GetColor(_configuration.FemcDisabledNeutralIndexedState, "Disabled Neutral Indexed State", 0x78, 0x78, 0x78),
                GetColor(_configuration.FemcCalendarFrameDecorativeAccent, "Calendar Frame Decorative Accent", 0xFD, 0xE8, 0xF1),
                GetColor(_configuration.FemcIndexedPaleListMenuAccent, "Indexed Pale List Menu Accent", 0xFF, 0xEE, 0xF6),
                GetColor(_configuration.FemcReverseSocialLinkFrame, "Reverse Social Link Frame", 0x62, 0x00, 0x00),
                GetColor(_configuration.FemcReverseSocialLinkInteriorAccent, "Reverse Social Link Interior Rank Accent", 0xFF, 0x37, 0x37),
                GetColor(_configuration.FemcBrokenSocialLinkInterior, "Broken Social Link Interior", 0x96, 0x00, 0x67),
                GetColor(_configuration.FemcBrokenSocialLinkAccent, "Broken Social Link Accent", 0xFF, 0xAD, 0xFF),
                GetColor(_configuration.FemcSocialLinkEffectPulseA, "Social Link Effect Pulse A", 0xF4, 0x00, 0x0A),
                GetColor(_configuration.FemcSocialLinkEffectPulseB, "Social Link Effect Pulse B", 0xF4, 0x00, 0xBA),
                GetColor(_configuration.FemcPersonaProgressGaugeAccent, "Persona Progress Gauge Accent", 0xFF, 0x73, 0x35)
            };
        }

        private RgbColor[] GetConfiguredBattlePanelPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcBattleAffinityPrimary, "Battle Affinity Primary", 0xA6, 0x67, 0x94),
                GetColor(_configuration.FemcBattleAffinitySecondary, "Battle Affinity Secondary", 0xC0, 0x4D, 0x7C),
                GetColor(_configuration.FemcBattleAffinityDarkNeutralA, "Battle Affinity Dark Neutral A", 0x23, 0x24, 0x25),
                GetColor(_configuration.FemcBattleAffinityWhite, "Battle Affinity White", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcBattleAffinityDarkNeutralB, "Battle Affinity Dark Neutral B", 0x24, 0x24, 0x24),
                GetColor(_configuration.FemcBattleAffinityWarmHighlight, "Battle Affinity Warm Highlight", 0xFF, 0xB5, 0x71),
                GetColor(_configuration.FemcBattleAffinityPurpleHighlight, "Battle Affinity Purple Highlight", 0x8E, 0x78, 0xF6),
                GetColor(_configuration.FemcBattleAffinityDeepTeal, "Battle Affinity Deep Teal", 0x00, 0x41, 0x5A),
                GetColor(_configuration.FemcBattleAffinityGenderAccent, "Battle Affinity Gender Accent", 0xFF, 0x24, 0x59),
                GetColor(_configuration.FemcBattleAffinityDarkGenderAccent, "Battle Affinity Dark Gender Accent", 0x5F, 0x00, 0x23)
            };
        }

        private RgbColor[] GetConfiguredBattleMenuPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcBattleMenuPrimaryAccent, "Battle Menu Primary Accent", 0xFF, 0x32, 0x96),
                GetColor(_configuration.FemcBattleMenuNeutralGrayA, "Battle Menu Neutral Gray A", 0x9B, 0xA0, 0xA2),
                GetColor(_configuration.FemcBattleMenuDarkGrayA, "Battle Menu Dark Gray A", 0x3A, 0x3A, 0x3A),
                GetColor(_configuration.FemcBattleMenuDarkGrayB, "Battle Menu Dark Gray B", 0x2D, 0x2D, 0x2D),
                GetColor(_configuration.FemcBattleMenuDarkGrayC, "Battle Menu Dark Gray C", 0x42, 0x42, 0x42),
                GetColor(_configuration.FemcBattleMenuWhite, "Battle Menu White", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcBattleMenuAlertRed, "Battle Menu Alert Red Accent", 0xC1, 0x0D, 0x0C),
                GetColor(_configuration.FemcBattleMenuSelectedDarkFill, "Battle Menu Selected Dark Fill", 0x2D, 0x2D, 0x2D),
                GetColor(_configuration.FemcBattleMenuSharedLightBlue, "Battle Menu Shared Light Blue", 0x6D, 0xB3, 0xFF),
                GetColor(_configuration.FemcBattleMenuSharedMidBlue, "Battle Menu Shared Mid Blue", 0x44, 0x86, 0xCD),
                GetColor(_configuration.FemcBattleMenuMidGray, "Battle Menu Mid Gray", 0x6C, 0x6C, 0x6C),
                GetColor(_configuration.FemcBattleHelpBoxBackground, "Battle Help Box Background", 0x31, 0x3F, 0x4D),
                new RgbColor(0xDE, 0xDE, 0xE2), // Text: preserve vanilla.
                GetColor(_configuration.FemcBattleSubmenuLightAccent, "Battle Submenu Light Accent", 0xFA, 0xD2, 0xE6),
                GetColor(_configuration.FemcBattleMenuBlueGrayAccent, "Battle Menu Blue Gray Accent", 0x9F, 0xB0, 0xC3),
                GetColor(_configuration.FemcBattleSubmenuHeaderAccent, "Battle Submenu Header Icon Accent", 0x5F, 0x00, 0x45),
                GetColor(_configuration.FemcBattleTacticsLightAccent, "Battle Tactics Light Accent", 0xFF, 0x81, 0xD5),
                GetColor(_configuration.FemcBattleTacticsMidAccent, "Battle Tactics Mid Accent", 0xCD, 0x44, 0x93),
                GetColor(_configuration.FemcBattleSpecialAccent, "Battle Special Alert Accent", 0xA6, 0x07, 0xFF),
                GetColor(_configuration.FemcBattleMutedLightAccent, "Battle Muted Light Accent", 0xD1, 0xD1, 0xD1)
            };
        }

        private RgbColor[] GetConfiguredBattleResultsPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcBattleResultsPrimaryDark, "Battle Results Primary Dark Accent", 0x6B, 0x01, 0x0A),
                GetColor(_configuration.FemcBattleResultsPaleAccent, "Battle Results Pale Accent", 0xFF, 0xE5, 0xEC),
                GetColor(_configuration.FemcBattleResultsRewardHighlight, "Battle Results Reward Highlight", 0xFF, 0xEB, 0x97),
                GetColor(_configuration.FemcBattleResultsWhite, "Battle Results White", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcBattleResultsBlueAccentA, "Battle Results Blue Accent A", 0x24, 0x94, 0xFF),
                GetColor(_configuration.FemcBattleResultsBlueAccentB, "Battle Results Blue Accent B", 0x24, 0x94, 0xFF),
                GetColor(_configuration.FemcBattleResultsBlueAccentC, "Battle Results Blue Accent C", 0x24, 0x94, 0xFF)
            };
        }

        private RgbColor[] GetConfiguredShufflePalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcShuffleDarkBase, "Shuffle Time Dark Base", 0x69, 0x17, 0x2A),
                GetColor(_configuration.FemcShuffleGlowA, "Shuffle Time Glow A", 0xFE, 0x5F, 0xFF),
                GetColor(_configuration.FemcShuffleMidBase, "Shuffle Time Mid Base", 0xB6, 0x35, 0x30),
                GetColor(_configuration.FemcShuffleGlowB, "Shuffle Time Glow B", 0xFF, 0x3D, 0x58),
                GetColor(_configuration.FemcShufflePrimaryAccent, "Shuffle Time Primary Accent", 0xFF, 0x4F, 0x64),
                GetColor(_configuration.FemcShuffleTranslucentSecondary, "Shuffle Time Translucent Secondary", 0x83, 0x8D, 0x00),
                GetColor(_configuration.FemcShuffleWhiteA, "Shuffle Time White A", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcShuffleDeepAccent, "Shuffle Time Deep Accent", 0xBD, 0x00, 0x3D),
                GetColor(_configuration.FemcShuffleBlack, "Shuffle Time Black", 0x00, 0x00, 0x00),
                GetColor(_configuration.FemcShuffleDarkSecondary, "Shuffle Time Dark Secondary", 0x29, 0x09, 0x63),
                GetColor(_configuration.FemcShuffleHighlight, "Shuffle Time Highlight", 0xFF, 0x76, 0x72),
                GetColor(_configuration.FemcShuffleWhiteB, "Shuffle Time White B", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcShuffleStrongAccent, "Shuffle Time Strong Accent", 0xFF, 0x00, 0x3C)
            };
        }

        private RgbColor[] GetConfiguredCampRootPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcCampRootPrimaryLight, "Camp Root Palette 00", 0xFF, 0xC1, 0xDC),
                GetColor(_configuration.FemcCampRootPaleAccent, "Camp Root Palette 01", 0xFD, 0xDA, 0xE9),
                GetColor(_configuration.FemcCampRootDarkAccent, "Camp Root Palette 02", 0x6B, 0x01, 0x0A),
                GetColor(_configuration.FemcCampRootMainHeaderTint, "Camp Root Palette 03 / MAIN Header Tint", 0xFF, 0x75, 0x9A),
                GetColor(_configuration.FemcCampRootWhite, "Camp Root Palette 04", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcCampRootPinkPurpleAccent, "Camp Root Palette 05", 0xFF, 0x68, 0xC6),
                GetColor(_configuration.FemcCampRootStrongAccent, "Camp Root Palette 06", 0xDE, 0x1F, 0x5B),
                GetColor(_configuration.FemcCampRootBlueGrayNeutral, "Camp Root Palette 07", 0x4E, 0x6D, 0x8B),
                GetColor(_configuration.FemcCampRootLimeHighlight, "Camp Root Palette 08", 0xF5, 0xFF, 0xA4),
                GetColor(_configuration.FemcCampRootCyanHighlight, "Camp Root Palette 09", 0x9A, 0xF5, 0xFF),
                GetColor(_configuration.FemcCampRootPinkHighlight, "Camp Root Palette 0A", 0xFF, 0xA4, 0xD1)
            };
        }

        private RgbColor[] GetConfiguredCampIFlashPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcCampIFlashPrimaryLight, "Camp iFlash 00 / Primary Light", 0xFF, 0xC1, 0xDC),
                GetColor(_configuration.FemcCampIFlashFullScreenTransition, "Camp iFlash 01 / Full Screen Transition", 0xFF, 0x75, 0x9A),
                GetColor(_configuration.FemcCampIFlashWarmAccent, "Camp iFlash 02 / Warm Accent", 0xFF, 0xA4, 0x9B),
                GetColor(_configuration.FemcCampIFlashCommandOrange, "Camp iFlash 06 / Command Orange", 0xFF, 0xC4, 0x49),
                GetColor(_configuration.FemcCampIFlashSubmenuAccent, "Camp iFlash 07 / Submenu Accent", 0xFF, 0x94, 0xFF),
                GetColor(_configuration.FemcCampIFlashSubmenuLightAccent, "Camp iFlash 08 / Submenu Light Accent", 0xFF, 0xB3, 0xDE),
                GetColor(_configuration.FemcCampCommandDarkBackground, "Camp iFlash 42 / Command Dark Background", 0x6B, 0x01, 0x0A),
                GetColor(_configuration.FemcCampCommandOpeningAccent, "Camp iFlash 43 / Command Opening Accent", 0xFF, 0x75, 0x9A),
                GetColor(_configuration.FemcCampRootMidAccent, "Menu Background / Camp iFlash 44", 0xFF, 0x75, 0x9A)
            };
        }

        private static readonly int[] CampIFlashPaletteIndices =
        {
            0, 1, 2, 6, 7, 8, 42, 43, 44
        };

        private RgbColor[] GetConfiguredFieldCursorPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcFieldCursorEffectType1, "Field Cursor Effect 1", 0xFF, 0x9B, 0xB1),
                GetColor(_configuration.FemcFieldCursorEffectType3, "Field Cursor Effect 3", 0xFF, 0xCC, 0x00),
                GetColor(_configuration.FemcFieldCursorEffectType2, "Field Cursor Effect 2", 0xBA, 0xFF, 0x00),
                GetColor(_configuration.FemcFieldCursorEffectType4, "Field Cursor Effect 4", 0xB1, 0x65, 0xFF),
                GetColor(_configuration.FemcFieldCursorEffectType5, "Field Cursor Effect 5", 0xFE, 0x60, 0x00),
                GetColor(_configuration.FemcFieldCursorFlashParticle, "Field Cursor Flash", 0xFF, 0x75, 0x9A),
                GetColor(_configuration.FemcFieldCursorRingCenter, "Field Cursor Ring", 0xCA, 0x48, 0xB4),
                GetColor(_configuration.FemcFieldNpcNameEffect, "NPC Name Effect", 0xFF, 0x9B, 0xB1)
            };
        }

        private RgbColor[] GetConfiguredDatePalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcDateTransitionAccent, "Date Transition Accent", 0xA9, 0x90, 0xFE),
                new RgbColor(0xEA, 0xE4, 0xFF), // Text: preserve vanilla FeMC colour.
                GetColor(_configuration.FemcDateMainAccent, "Date Main Accent", 0x74, 0x74, 0x74),
                GetColor(_configuration.FemcDateNormalDayA, "Date Normal Day A", 0x62, 0x5C, 0x88),
                GetColor(_configuration.FemcDateSelectedDayA, "Date Selected Day A", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.FemcDateSpecialMarker, "Date Special Marker", 0x57, 0x90, 0xAA),
                GetColor(_configuration.FemcDateSundaySpecialA, "Date Sunday Special A", 0x81, 0x66, 0x5C),
                GetColor(_configuration.FemcDateDetailAccent, "Date Detail Accent", 0x2A, 0x2A, 0x2A),
                GetColor(_configuration.FemcDateSundaySpecialB, "Date Sunday Special B", 0xA8, 0x92, 0x8B),
                GetColor(_configuration.FemcDateNormalDayB, "Date Normal Day B", 0x8F, 0x8B, 0xAA),
                GetColor(_configuration.FemcDateWeekdayDetail, "Date Weekday Detail", 0x7F, 0x7F, 0x7F),
                GetColor(_configuration.FemcDateSelectedDayB, "Date Selected Day B", 0xD1, 0xA2, 0x8D),
                GetColor(_configuration.FemcDateCurrentDay, "Date Current Day", 0xAF, 0xA5, 0xE9)
            };
        }

        private RgbColor[] GetConfiguredFieldDatePalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcFieldDateCornerBackground, "Field Date Corner Background", 0xFA, 0x99, 0xB4),
                GetColor(_configuration.FemcFieldDateSecondaryLightAccent, "Field Date Secondary Light Accent", 0xFD, 0xE6, 0xEE),
                GetColor(_configuration.FemcFieldDateDarkAccent, "Field Date Dark Accent", 0xA1, 0x28, 0x32),
                GetColor(_configuration.FemcFieldDateNumberWeekdayTint, "Field Date Number / Weekday Tint", 0xA1, 0x28, 0x32),
                GetColor(_configuration.FemcFieldDateLightAccent, "Field Date Light Accent", 0xFF, 0xC1, 0xDC)
            };
        }

        private RgbColor[] GetConfiguredFieldHudPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcFieldHudColor0, "Field HUD Color 0", 0xCA, 0x20, 0x43),
                GetColor(_configuration.FemcFieldHudColor1, "Field HUD Color 1", 0xFF, 0xC1, 0xDC),
                GetColor(_configuration.FemcFieldHudColor2, "Field HUD Color 2", 0x6B, 0x01, 0x0A)
            };
        }

        private RgbColor[] GetConfiguredSocialRankPalette()
        {
            return new[]
            {
                GetColor(_configuration.FemcSocialRankColor0, "Social Link Rank Color 0", 0xFF, 0xF0, 0xF0),
                GetColor(_configuration.FemcSocialRankColor1, "Social Link Rank Color 1", 0xFF, 0x5C, 0x80),
                GetColor(_configuration.FemcSocialRankColor2, "Social Link Rank Color 2", 0xFF, 0x5C, 0x80),
                GetColor(_configuration.FemcSocialRankColor3, "Social Link Rank Color 3", 0xFF, 0x77, 0xFD),
                GetColor(_configuration.FemcSocialRankColor4, "Social Link Rank Color 4", 0xFF, 0x5C, 0x80),
                GetColor(_configuration.FemcSocialRankColor5, "Social Link Rank Color 5", 0xB8, 0x2E, 0x4B),
                GetColor(_configuration.FemcSocialRankColor6, "Social Link Rank Color 6", 0xFF, 0x84, 0xA8),
                GetColor(_configuration.FemcSocialRankColor7, "Social Link Rank Color 7", 0xFF, 0x69, 0x94)
            };
        }

        private RgbColor[] GetConfiguredPolicePalette()
        {
            return new[]
            {
                GetColor(_configuration.PoliceColor01, "Police Lookup 0x00", 0xF3, 0xDC, 0xE2),
                GetColor(_configuration.PoliceColor02, "Police Lookup 0x01", 0xC0, 0x03, 0xA9),
                GetColor(_configuration.PoliceColor03, "Police Lookup 0x02", 0x8C, 0x04, 0x04),
                GetColor(_configuration.PoliceColor04, "Police Lookup 0x03", 0x42, 0x12, 0x12),
                GetColor(_configuration.PoliceColor05, "Police Lookup 0x04", 0x9E, 0x74, 0x92),
                GetColor(_configuration.PoliceColor06, "Police Lookup 0x05", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.PoliceColor07, "Police Lookup 0x06", 0x00, 0x00, 0x00),
                GetColor(_configuration.PoliceColor08, "Police Lookup 0x07", 0x9B, 0x10, 0x7B),
                GetColor(_configuration.PoliceColor09, "Police Lookup 0x08", 0xFC, 0x8F, 0xE0),
                GetColor(_configuration.PoliceColor10, "Police Lookup 0x09", 0xFF, 0xF4, 0x70),
                GetColor(_configuration.PoliceColor11, "Police Lookup 0x0A", 0x5E, 0x1E, 0x1E),
                GetColor(_configuration.PoliceColor12, "Police Lookup 0x0B", 0xFF, 0x64, 0xCB),
                GetColor(_configuration.PoliceColor13, "Police Lookup 0x0C", 0xF8, 0x85, 0xE8),
                GetColor(_configuration.PoliceColor14, "Police Lookup 0x0D", 0xFF, 0xA9, 0xB3),
                GetColor(_configuration.PoliceColor15, "Police Lookup 0x0E", 0xFF, 0x93, 0x9F),
                GetColor(_configuration.PoliceColor16, "Police Lookup 0x0F", 0xD9, 0x14, 0xB2),
                GetColor(_configuration.PoliceColor17, "Police Lookup 0x10", 0xFC, 0x8F, 0xE0),
                GetColor(_configuration.PoliceColor18, "Police Lookup 0x11", 0xED, 0x00, 0xC5),
                GetColor(_configuration.PoliceColor19, "Police Lookup 0x12", 0xF0, 0x28, 0xC0)
            };
        }

        private RgbColor[] GetConfiguredPharmacyPalette()
        {
            return new[]
            {
                GetColor(_configuration.PharmacyColor2C, "Pharmacy Palette 2C", 0xFF, 0xC1, 0xDC),
                GetColor(_configuration.PharmacyColor2D, "Pharmacy Palette 2D", 0xFF, 0x75, 0x9A),
                GetColor(_configuration.PharmacyColor2E, "Pharmacy Palette 2E", 0x6B, 0x01, 0x0A),
                GetColor(_configuration.PharmacyColor2F, "Pharmacy Palette 2F", 0xFF, 0xEE, 0xF6),
                GetColor(_configuration.PharmacyColor30, "Pharmacy Palette 30", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.PharmacyColor31, "Pharmacy Palette 31", 0xFF, 0x68, 0xC6),
                GetColor(_configuration.PharmacyColor32, "Pharmacy Palette 32", 0xFF, 0xC1, 0xDC),
                GetColor(_configuration.PharmacyColor35, "Pharmacy Palette 35", 0xFF, 0xF3, 0x70)
            };
        }

        private RgbColor[] GetConfiguredAntiquePalette()
        {
            return new[]
            {
                GetColor(_configuration.AntiqueColor36, "Antique Palette 36", 0xDE, 0x1F, 0x5B),
                GetColor(_configuration.AntiqueColor37, "Antique Palette 37", 0xFF, 0xEE, 0xF6),
                GetColor(_configuration.AntiqueColor38, "Antique Palette 38", 0x6B, 0x01, 0x0A),
                GetColor(_configuration.AntiqueColor3A, "Antique Palette 3A", 0xFF, 0xFF, 0xFF),
                GetColor(_configuration.AntiqueColor3B, "Antique Palette 3B", 0xF5, 0x8F, 0x02),
                GetColor(_configuration.AntiqueColor3E, "Antique Palette 3E", 0xFD, 0xDA, 0xE9),
                GetColor(_configuration.AntiqueColor3F, "Antique Palette 3F", 0xFF, 0xC3, 0x23)
            };
        }

        private void UpdateCampTransitionColor()
        {
            if (_campTransitionColorBuffer == 0)
                return;

            var transition = GetColor(
                _configuration.FemcSecondaryPanelFrameAccent,
                "Menu Transition Accent",
                0xFF,
                0x75,
                0x9A);

            memory.SafeWrite(
                _campTransitionColorBuffer,
                new byte[] { transition.R, transition.G, transition.B });
        }

        private void RegisterCampRendererColorPatches()
        {
            SigScan(
                "66 C7 45 ?? FF C4 C6 45 ?? 49 88 45 ?? C7 45 ?? 00 00 88 43",
                "Camp Command Header Orange Struct",
                address =>
                {
                    _campCommandHeaderStructColorPatch = (nuint)address;
                    ApplyCampRendererColors();
                });

            SigScan(
                "C6 44 24 30 49 C6 44 24 28 C4 C6 44 24 20 FF",
                "Camp Command Header Orange Args",
                address =>
                {
                    _campCommandHeaderArgsColorPatch = (nuint)address;
                    ApplyCampRendererColors();
                });

            SigScan(
                "41 B7 FF 41 B4 C4 41 B5 49",
                "Camp Command Header Orange Registers",
                address =>
                {
                    _campCommandHeaderRegisterColorPatch = (nuint)address;
                    ApplyCampRendererColors();
                });

            SigScan(
                "B8 49 01 07 FF 83 3D ?? ?? ?? ?? 00 B9 0D 27 36 FF",
                "Camp Foreground Dark Primitive",
                address =>
                {
                    _campForegroundDarkPrimitivePatch = (nuint)address;
                    ApplyCampRendererColors();
                });

            if (_hooks == null)
                return;

            SigScan(
                "88 44 24 33 0F 29 44 24 20 E8 ?? ?? ?? ?? E9 ?? ?? ?? ??",
                "Camp Transition Overlay Draw",
                address =>
                {
                    string[] asm =
                    {
                        "use64",
                        "push r10",
                        "push r11",
                        $"mov r11, 0x{_campTransitionColorBuffer:X}",
                        "mov r10b, byte [r11]",
                        "mov byte [rcx], r10b",
                        "mov r10b, byte [r11 + 1]",
                        "mov byte [rcx + 1], r10b",
                        "mov r10b, byte [r11 + 2]",
                        "mov byte [rcx + 2], r10b",
                        "pop r11",
                        "pop r10"
                    };

                    _campTransitionOverlayDrawHook = _hooks
                        .CreateAsmHook(
                            asm,
                            address + 9,
                            AsmHookBehaviour.ExecuteFirst)
                        .Activate();
                });
        }

        private void ApplyCampRendererColors()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette)
                return;

            var commandOrange = GetColor(
                _configuration.FemcCampCommandHeaderOrange,
                "Command Header Orange",
                0xFF,
                0xC4,
                0x49);

            if (_campCommandHeaderStructColorPatch != 0)
            {
                memory.SafeWrite(
                    _campCommandHeaderStructColorPatch + (nuint)4,
                    new byte[] { commandOrange.R, commandOrange.G });
                memory.SafeWrite(
                    _campCommandHeaderStructColorPatch + (nuint)9,
                    new byte[] { commandOrange.B });
            }

            if (_campCommandHeaderArgsColorPatch != 0)
            {
                memory.SafeWrite(
                    _campCommandHeaderArgsColorPatch + (nuint)4,
                    new byte[] { commandOrange.B });
                memory.SafeWrite(
                    _campCommandHeaderArgsColorPatch + (nuint)9,
                    new byte[] { commandOrange.G });
                memory.SafeWrite(
                    _campCommandHeaderArgsColorPatch + (nuint)14,
                    new byte[] { commandOrange.R });
            }

            if (_campCommandHeaderRegisterColorPatch != 0)
            {
                memory.SafeWrite(
                    _campCommandHeaderRegisterColorPatch + (nuint)2,
                    new byte[] { commandOrange.R });
                memory.SafeWrite(
                    _campCommandHeaderRegisterColorPatch + (nuint)5,
                    new byte[] { commandOrange.G });
                memory.SafeWrite(
                    _campCommandHeaderRegisterColorPatch + (nuint)8,
                    new byte[] { commandOrange.B });
            }

            var foregroundDark = GetColor(
                _configuration.FemcCampForegroundDarkPrimitive,
                "Camp Foreground Dark Primitive",
                0x49,
                0x01,
                0x07);

            if (_campForegroundDarkPrimitivePatch != 0)
            {
                memory.SafeWrite(
                    _campForegroundDarkPrimitivePatch + (nuint)1,
                    new byte[] { foregroundDark.R, foregroundDark.G, foregroundDark.B, 0xFF });
            }
        }

        private void RegisterGenderPaletteScans()
        {
            SigScan("0F 82 FF FF 0F 82 FF FF 23 24 25 FF FF FF FF FF 24 24 24 FF FF B5 71 FF", "Battle Panel Color Bank", address =>
            {
                _battlePanelPalette = (nuint)(address + 0x28);
                ApplyGenderColorBank(_battlePanelPalette, GetConfiguredBattlePanelPalette());
            });

            SigScan("18 78 FF FF 9B A0 A2 FF 3A 3A 3A FF 2D 2D 2D FF 42 42 42 FF FF FF FF FF", "Battle Menu Color Bank", address =>
            {
                _battleMenuPalette = (nuint)(address + 0x50);
                ApplyGenderColorBank(_battleMenuPalette, GetConfiguredBattleMenuPalette());
            });

            SigScan("00 3C 5F FF DC F3 FF FF FF EB 97 FF FF FF FF FF 24 94 FF FF 24 94 FF FF", "Battle Results Color Bank", address =>
            {
                _battleResultsPalette = (nuint)(address + 0x24);
                ApplyGenderColorBank(_battleResultsPalette, GetConfiguredBattleResultsPalette());
            });

            SigScan("00 23 63 FF 5F E2 FF 80 19 66 AE FF 45 3D FF 80 00 5F FF FF 41 00 83 5A", "Shuffle Time Color Bank", address =>
            {
                _shufflePalette = (nuint)(address + 0x34);
                ApplyGenderColorBank(_shufflePalette, GetConfiguredShufflePalette());
            });

            SigScan("01 63 C0 FF 6D B3 FF FF 00 3C 5F FF 24 94 FF FF FF FF FF FF FF C4 49 FF", "Shared Camp Color Bank", address =>
            {
                _sharedCampPalette = (nuint)(address + 0x64);
                ApplyGenderColorBank(_sharedCampPalette, GetConfiguredSharedCampPalette());
            });

            SigScan("6D AF FF FF 7A E2 FF FF 00 3C 5F FF 24 94 FF FF FF FF FF FF 5D 68 C6 FF", "Camp Root Color Bank", address =>
            {
                _campRootPalette = (nuint)(address + 0x2C);
                ApplyGenderColorBank(_campRootPalette, GetConfiguredCampRootPalette());
            });

            SigScan(
                "41 0F B6 84 97 ?? ?? ?? ?? 41 0F B6 8C 97 ?? ?? ?? ?? 88 44 24 50 41 0F B6 84 97 ?? ?? ?? ??",
                "Camp iFlash Route Color Lookup",
                address =>
                {
                    // FUN_14B3B67E0 indexes the Makoto row using imageBase + paletteRva.
                    // Resolve that RVA from the renderer instead of scanning mutable palette bytes.
                    var paletteRva = Marshal.ReadInt32((IntPtr)(address + 5));
                    _campIFlashPalette = (nuint)BaseAddress + (nuint)paletteRva + 0x180u;
                    ApplyCampIFlashPalette();
                });

            SigScan("00 CB FF 00 FF 9B B1 00 ?? ?? ?? ?? ?? ?? ?? ?? FF CC 00 00 FF CC 00 00 ?? ?? ?? ?? ?? ?? ?? ?? BA FF 00 00 BA FF 00 00 B1 65 FF 00 B1 65 FF 00 FE 60 00 00 FE 60 00 00 17 D2 FF 00 FF 75 9A 00 53 65 AB 00 CA 48 B4 00 00 CB FF 00 FF 9B B1 00", "Field Cursor Color Table", address =>
            {
                _fieldCursorPalette = (nuint)address;
                ApplyFieldCursorPalette();
            });

            SigScan("F5 94 1C 00 A9 90 FE 00 FF F3 DC 00 EA E4 FF 00 6A 63 58 00 74 74 74 00 59 5C 88 00 62 5C 88 00 FF FF FF 00 FF FF FF 00 57 90 AA 00 57 90 AA 00 88 72 5C 00 81 66 5C 00 2A 2A 2A 00 2A 2A 2A 00", "Date HUD Color Table A", address =>
            {
                _datePaletteA = (nuint)address;
                ApplyDatePalette();
            });

            SigScan("BB AC 9E 00 A8 92 8B 00 A2 9E BB 00 8F 8B AA 00 A9 A4 9D 00 7F 7F 7F 00 EE C0 93 00 D1 A2 8D 00 AF A5 E9 00 AF A5 E9 00", "Date HUD Color Table B", address =>
            {
                _datePaletteB = (nuint)address;
                ApplyDatePalette();
            });

            SigScan(
                "41 8B BC 84 ?? ?? ?? ?? 41 8B AC 84 ?? ?? ?? ?? C1 EF 08 C1 ED 08",
                "Persistent Field Date Route Color Lookup",
                address =>
                {
                    // dtdraw.c indexes DAT_140772C70 and DAT_140772C80 from the image base.
                    // Resolve the table from the instruction displacement instead of scanning
                    // the mutable colour bytes themselves, then step back to DAT_140772C60.
                    var dateColorRva = Marshal.ReadInt32((IntPtr)(address + 4));
                    _fieldDatePalette = (nuint)BaseAddress + (nuint)(dateColorRva - 0x10);
                    ApplyFieldDatePalette();
                });

            SigScan("00 CA 53 00 00 43 20 CA 00 FF B3 60 00 DC C1 FF 00 5F 3C 00 00 0A 01 6B", "Field HUD Color Table", address =>
            {
                _fieldHudPalette = (nuint)address;
                ApplyFieldHudPalette();
            });

            SigScan("78 B4 FF 00 FF F0 F0 00 96 96 96 00 40 7A FF 00 FF 5C 80 00 FF FF FF 00 40 7A FF 00 FF 5C 80 00 64 63 63 00 40 7A FF 00 FF 77 FD 00 9F 9F 9F 00", "Social Link Rank Color Table", address =>
            {
                _socialRankPalette = (nuint)address;
                ApplySocialRankPalette();
            });

            SigScan("DC F3 F3 00 03 6F C0 00 8C 04 04 00 12 39 42 00 74 93 9E 00 FF FF FF 00 00 00 00 00 10 5F 9B 00 8F D0 FC 00 FF F4 70 00 1E 47 5E", "Generic Facility Color Table Police Base", address =>
            {
                _facilityColorTable = (nuint)address;
                ApplyPolicePalette();
                ApplyFacilityPalette();
            });

            SigScan("66 C7 44 24 70 FF A9 BB 65 00 00 00 C6 44 24 72 B3", "Antique Stat Decrease Color", address =>
            {
                _antiqueComparisonDecreasePatch = (nuint)address;
                ApplyAntiqueComparisonColors();
            });

            SigScan("66 C7 44 24 70 8C E4 BB 66 00 00 00 C6 44 24 72 FA", "Antique Stat Increase Color", address =>
            {
                _antiqueComparisonIncreasePatch = (nuint)address;
                ApplyAntiqueComparisonColors();
            });

            SigScan("5A BE FF 00 44 9C FC 00 12 39 41 00 DC F3 FF 00 FF FF FF 00 00 F2 FF 00 74 AE EB 00 0F 52 5C 00", "Generic Facility Color Table Aohige 0x2C", address =>
            {
                
                var inferredBase = (nuint)((long)address - 0xB0);
                if (_facilityColorTable != 0 && _facilityColorTable != inferredBase)
                    LogError($"Facility table signatures disagree: 0x{_facilityColorTable:X} vs 0x{inferredBase:X}.");
                else
                    _facilityColorTable = inferredBase;

                ApplyPolicePalette();
                ApplyFacilityPalette();
            });

            RegisterCampHardcodedColorScans();
        }

        private void ApplyExperimentalFemcPalette()
        {
            UpdateCampTransitionColor();
            ApplyGenderColorBank(_battlePanelPalette, GetConfiguredBattlePanelPalette());
            ApplyGenderColorBank(_battleMenuPalette, GetConfiguredBattleMenuPalette());
            ApplyGenderColorBank(_battleResultsPalette, GetConfiguredBattleResultsPalette());
            ApplyGenderColorBank(_shufflePalette, GetConfiguredShufflePalette());
            ApplyGenderColorBank(_sharedCampPalette, GetConfiguredSharedCampPalette());
            ApplyGenderColorBank(_campRootPalette, GetConfiguredCampRootPalette());
            ApplyCampIFlashPalette();
            ApplyCampRendererColors();
            ApplyFieldCursorPalette();
            ApplyDatePalette();
            ApplyFieldDatePalette();
            ApplyFieldHudPalette();
            ApplySocialRankPalette();
            ApplyPolicePalette();
            ApplyFacilityPalette();
            ApplyAntiqueComparisonColors();
            ApplyCampHardcodedColors();
            ApplyHardcodedGenderColors();
        }

        private void ApplyGenderColorBank(nuint address, RgbColor[] colors)
        {
            if (!_configuration.ExperimentalGlobalFemcPalette || address == 0)
                return;

            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                memory.SafeWrite(address + (nuint)(i * 4), new byte[] { color.R, color.G, color.B });
            }
        }

        private void ApplyCampIFlashPalette()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette || _campIFlashPalette == 0)
                return;

            var colors = GetConfiguredCampIFlashPalette();

            for (var i = 0; i < CampIFlashPaletteIndices.Length; i++)
            {
                var color = colors[i];
                var paletteIndex = CampIFlashPaletteIndices[i];
                memory.SafeWrite(
                    _campIFlashPalette + (nuint)(paletteIndex * 4),
                    new byte[] { color.R, color.G, color.B });
            }
        }

        private void ApplyFieldCursorPalette()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette || _fieldCursorPalette == 0)
                return;

            var offsets = new[] { 0x00, 0x10, 0x20, 0x28, 0x30, 0x38, 0x40, 0x48 };
            var colors = GetConfiguredFieldCursorPalette();

            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                memory.SafeWrite(_fieldCursorPalette + (nuint)(offsets[i] + 4), new byte[] { color.R, color.G, color.B });
            }
        }

        private void ApplyDatePalette()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette)
                return;

            var colors = GetConfiguredDatePalette();

            if (_datePaletteA != 0)
            {
                for (var i = 0; i < 8; i++)
                {
                    var color = colors[i];
                    memory.SafeWrite(_datePaletteA + (nuint)(i * 8 + 4), new byte[] { color.R, color.G, color.B });
                }
            }

            if (_datePaletteB != 0)
            {
                for (var i = 0; i < 5; i++)
                {
                    var color = colors[i + 8];
                    memory.SafeWrite(_datePaletteB + (nuint)(i * 8 + 4), new byte[] { color.R, color.G, color.B });
                }
            }
        }

        private void ApplyFieldDatePalette()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette || _fieldDatePalette == 0)
                return;

            var colors = GetConfiguredFieldDatePalette();
            int[] femaleOffsets = { 0x04, 0x0C, 0x14, 0x1C, 0x2C };

            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                // dtdraw.c stores these as 00 BB GG RR. The renderer shifts the
                // dword before splitting it back into R/G/B draw arguments.
                memory.SafeWrite(
                    _fieldDatePalette + (nuint)femaleOffsets[i],
                    new byte[] { 0x00, color.B, color.G, color.R });
            }
        }

        private void ApplyFieldHudPalette()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette || _fieldHudPalette == 0)
                return;

            var colors = GetConfiguredFieldHudPalette();

            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                memory.SafeWrite(_fieldHudPalette + (nuint)(i * 8 + 4), new byte[] { 0x00, color.B, color.G, color.R });
            }
        }

        private void ApplySocialRankPalette()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette || _socialRankPalette == 0)
                return;

            var colors = GetConfiguredSocialRankPalette();

            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                memory.SafeWrite(_socialRankPalette + (nuint)(i * 0xC + 4), new byte[] { color.R, color.G, color.B });
            }
        }

        private void ApplyPolicePalette()
        {
            if (!_configuration.Police || _facilityColorTable == 0)
                return;

            
            
            
            
            memory.SafeWrite(_facilityColorTable, BuildPaletteBytes(GetConfiguredPolicePalette(), 0x00));
        }

        private void ApplyFacilityPalette()
        {
            if (_facilityColorTable == 0)
                return;

            if (_configuration.AohigePharmacy)
            {
                var pharmacyIndices = new[] { 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x35 };
                var pharmacy = GetConfiguredPharmacyPalette();

                for (var i = 0; i < pharmacy.Length; i++)
                {
                    var color = pharmacy[i];
                    memory.SafeWrite(_facilityColorTable + (nuint)(pharmacyIndices[i] * 4), new byte[] { color.R, color.G, color.B });
                }
            }

            if (_configuration.AntiqueShop)
            {
                var antiqueIndices = new[] { 0x36, 0x37, 0x38, 0x3A, 0x3B, 0x3E, 0x3F };
                var antique = GetConfiguredAntiquePalette();

                for (var i = 0; i < antique.Length; i++)
                {
                    var color = antique[i];
                    memory.SafeWrite(_facilityColorTable + (nuint)(antiqueIndices[i] * 4), new byte[] { color.R, color.G, color.B });
                }
            }
        }

        private void ApplyAntiqueComparisonColors()
        {
            if (!_configuration.AntiqueShop)
                return;

            var antiqueDecrease = GetColor(_configuration.AntiqueComparisonDecrease, "Antique Stat Decrease", 0xFF, 0xA9, 0xB3);
            var antiqueIncrease = GetColor(_configuration.AntiqueComparisonIncrease, "Antique Stat Increase", 0x8C, 0xE4, 0xFA);

            if (_antiqueComparisonDecreasePatch != 0)
            {
                memory.SafeWrite(_antiqueComparisonDecreasePatch + (nuint)5, new byte[] { antiqueDecrease.R, antiqueDecrease.G });
                memory.SafeWrite(_antiqueComparisonDecreasePatch + (nuint)16, new byte[] { antiqueDecrease.B });
            }

            if (_antiqueComparisonIncreasePatch != 0)
            {
                memory.SafeWrite(_antiqueComparisonIncreasePatch + (nuint)5, new byte[] { antiqueIncrease.R, antiqueIncrease.G });
                memory.SafeWrite(_antiqueComparisonIncreasePatch + (nuint)16, new byte[] { antiqueIncrease.B });
            }
        }

        private void RegisterCampHardcodedColorScans()
        {
            
            
            
            SigScanAll("B8 00 0A 01 6B", "Camp Independent #6B010A EAX", address =>
            {
                var rva = (long)(address - BaseAddress);
                switch (rva)
                {
                    case 0x11ED0A:
                        _socialLinkBustupTintPatchA = (nuint)address;
                        break;
                    case 0x11F269:
                        _socialLinkBustupTintPatchB = (nuint)address;
                        break;
                    case 0x2AE0AF:
                        _partyPanelCharacterNamePatch = (nuint)address;
                        break;
                    default:
                        
                        return;
                }
                ApplyCampHardcodedColors();
            });

            SigScanAll("B9 00 0A 01 6B", "Camp Skill Text Normal #6B010A", address =>
            {
                var rva = (long)(address - BaseAddress);
                if (rva == 0x2C616E)
                    _skillTextNormalPatchA = (nuint)address;
                else if (rva == 0x2C61B0)
                    _skillTextNormalPatchB = (nuint)address;
                else
                    return;
                ApplyCampHardcodedColors();
            });

            SigScanAll("B8 00 DC C1 FF", "Camp Skill Text Selected #FFC1DC", address =>
            {
                if ((long)(address - BaseAddress) != 0x2C6188)
                    return;
                _skillTextSelectedPatch = (nuint)address;
                ApplyCampHardcodedColors();
            });

            SigScanAll("81 CB 00 9C FF CB", "Camp Skill Text New #CBFF9C", address =>
            {
                if ((long)(address - BaseAddress) != 0x2C619F)
                    return;
                _skillTextNewPatch = (nuint)address;
                ApplyCampHardcodedColors();
            });

            // cmpstatus has two copies of the same Kotone tint setter: the local implementation
            // at 0x140158DE0 and the live target behind thunk_FUN_14B303F80. Keep both patch
            // sites tied to one logical config option, but locate them by surrounding code rather
            // than build-specific RVAs.
            SigScan(
                "75 06 8B 7C 01 18 EB 05 BF FF 9A 75 FF 48 8B CB 85 ED 74 11",
                "Camp Status Panel Accent Local",
                address =>
                {
                    var patch = (nuint)(address + 8);
                    if (!_statusAccentPatches.Contains(patch))
                        _statusAccentPatches.Add(patch);

                    ApplyCampHardcodedColors();
                });

            SigScan(
                "75 07 43 8B 7C 08 18 EB 05 BF FF 9A 75 FF 48 89 D9 85 ED 74 0D",
                "Camp Status Panel Accent Live",
                address =>
                {
                    var patch = (nuint)(address + 9);
                    if (!_statusAccentPatches.Contains(patch))
                        _statusAccentPatches.Add(patch);

                    ApplyCampHardcodedColors();
                });

            SigScan(
                "BA FF 9A 75 FF 39 1D ?? ?? ?? ?? B8 FF FF 94 24 48 8B 4F 50 0F 44 D0",
                "Camp Status Panel Accent State",
                address =>
                {
                    _statusStateAccentPatch = (nuint)address;
                    ApplyCampHardcodedColors();
                });

            SigScanAll("B2 DE 41 B0 1F 41 B1 5E", "Party Panel Status Marker #DE1F5E", address =>
            {
                var patch = (nuint)address;
                if (!_partyPanelStatusPatches.Contains(patch))
                    _partyPanelStatusPatches.Add(patch);
                ApplyCampHardcodedColors();
            });

            SigScan("41 B5 DE B2 1F 41 B1 5E", "Camp Root Status Marker #DE1F5E", address =>
            {
                _campPartyStatusPatch = (nuint)address;
                ApplyCampHardcodedColors();
            });
        }

        private void ApplyCampHardcodedColors()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette)
                return;

            var socialLinkBustup = GetColor(_configuration.FemcSocialLinkBustupTint, "Social Link Bust up Tint", 0x6B, 0x01, 0x0A);
            // Party names and skill labels are text. Their scanners stay registered for
            // compatibility/debugging, but themes intentionally leave the original game
            // immediates untouched so text contrast cannot be destroyed.
            var statusAccent = GetColor(_configuration.FemcCampStatusRendererAccent, "Status Panel Accent", 0xFF, 0x75, 0x9A);
            var partyStatus = GetColor(_configuration.FemcPartyPanelCharacterStatusAccent, "Party Panel Status Marker", 0xDE, 0x1F, 0x5E);
            var campPartyStatus = GetColor(_configuration.FemcCampPartyStatusMarkerAccent, "Camp Root Status Marker", 0xDE, 0x1F, 0x5E);

            WritePackedBgr0Immediate(_socialLinkBustupTintPatchA, socialLinkBustup, 1);
            WritePackedBgr0Immediate(_socialLinkBustupTintPatchB, socialLinkBustup, 1);

            foreach (var patch in _statusAccentPatches)
                memory.SafeWrite(patch + (nuint)1, new byte[] { statusAccent.R, statusAccent.B, statusAccent.G, 0xFF });

            if (_statusStateAccentPatch != 0)
                memory.SafeWrite(_statusStateAccentPatch + (nuint)1, new byte[] { statusAccent.R, statusAccent.B, statusAccent.G, 0xFF });

            foreach (var patch in _partyPanelStatusPatches)
            {
                memory.SafeWrite(patch + (nuint)1, new byte[] { partyStatus.R });
                memory.SafeWrite(patch + (nuint)4, new byte[] { partyStatus.G });
                memory.SafeWrite(patch + (nuint)7, new byte[] { partyStatus.B });
            }

            if (_campPartyStatusPatch != 0)
            {
                memory.SafeWrite(_campPartyStatusPatch + (nuint)2, new byte[] { campPartyStatus.R });
                memory.SafeWrite(_campPartyStatusPatch + (nuint)4, new byte[] { campPartyStatus.G });
                memory.SafeWrite(_campPartyStatusPatch + (nuint)7, new byte[] { campPartyStatus.B });
            }
        }

        private void WritePackedBgr0Immediate(nuint patch, RgbColor color, int operandOffset)
        {
            if (patch == 0)
                return;

            memory.SafeWrite(patch + (nuint)operandOffset, new byte[] { 0x00, color.B, color.G, color.R });
        }

        private void RegisterHardcodedGenderColorScans()
        {
            SigScan("40 B7 FF 40 B6 32 40 B5 96", "Hardcoded FeMC Battle Efficacy Accent", address =>
            {
                _battleEfficacyAccentPatch = (nuint)address;
                ApplyHardcodedGenderColors();
            });

            SigScanAll("BB 00 3C 00 64", "Hardcoded FeMC Persona Dark EBX", address =>
            {
                var patch = (nuint)address;
                if (!_personaDarkEbxPatches.Contains(patch))
                    _personaDarkEbxPatches.Add(patch);
                ApplyHardcodedGenderColors();
            });

            SigScanAll("BE 00 3C 00 64", "Hardcoded FeMC Persona Dark ESI", address =>
            {
                var patch = (nuint)address;
                if (!_personaDarkEsiPatches.Contains(patch))
                    _personaDarkEsiPatches.Add(patch);
                ApplyHardcodedGenderColors();
            });

            SigScanAll("B9 00 3C 00 64", "Hardcoded FeMC Persona Dark ECX", address =>
            {
                var patch = (nuint)address;
                if (!_personaDarkEcxPatches.Contains(patch))
                    _personaDarkEcxPatches.Add(patch);
                ApplyHardcodedGenderColors();
            });

            SigScan("B0 60 32 C9 B2 97", "Hardcoded FeMC Persona Initial Transition", address =>
            {
                _personaInitialTransitionPatch = (nuint)address;
                ApplyHardcodedGenderColors();
            });

            SigScan("BA 97 00 00 00 B8 00 00 60 05 B9 00 00 60 FF", "Hardcoded FeMC Persona Transition Primitive", address =>
            {
                _personaTransitionPrimitivePatch = (nuint)address;
                ApplyHardcodedGenderColors();
            });

            SigScan("40 B5 F4 F3 44 0F 10 25 ?? ?? ?? ?? 41 B4 58 41 B5 9E", "Hardcoded FeMC Dialogue Prompt", address =>
            {
                _dialoguePromptPatch = (nuint)address;
                ApplyHardcodedGenderColors();
            });

            SigScan(
                "C7 84 24 80 01 00 00 FF 7F 44 EC 41 BE 00 7F 44 EC",
                "Hardcoded FeMC Dialogue Choice Colors",
                address =>
                {
                    // [rsp+0x180] is the route-specific choice/text tint.
                    // The following MOV R14D immediate is the visible selected-row bar.
                    _dialogueChoiceTextPatch = (nuint)address;
                    _dialogueWindowPatch = (nuint)(address + 0x0B);
                    ApplyHardcodedGenderColors();
                });

            SigScan("BA 00 32 1E 46", "Hardcoded FeMC Field NPC Name", address =>
            {
                _fieldNpcNamePatch = (nuint)address;
                ApplyHardcodedGenderColors();
            });

            SigScan("C7 44 24 30 FF AD 9B FF", "Hardcoded FeMC Shuffle Result", address =>
            {
                _shuffleResultPatch = (nuint)address;
                ApplyHardcodedGenderColors();
            });
        }

        private void ApplyHardcodedGenderColors()
        {
            if (!_configuration.ExperimentalGlobalFemcPalette)
                return;

            var battlePrimary = GetColor(_configuration.FemcBattleMenuPrimaryAccent, "Battle Menu Primary Accent", 0xFF, 0x32, 0x96);
            var personaDark = GetColor(_configuration.FemcPersonaDarkModelAccent, "Persona Dark Model Accent", 0x64, 0x00, 0x3C);
            var personaPrimitive = GetColor(_configuration.FemcPersonaTransitionPrimitiveAccent, "Camp / Persona Transition Primitive Accent", 0x97, 0x00, 0x60);
            var dialoguePrompt = GetColor(_configuration.FemcDialoguePromptAccent, "Dialogue Prompt Accent", 0xF4, 0x58, 0x9E);
            var dialogueWindow = GetColor(_configuration.FemcDialogueWindowAccent, "Dialogue Choice Highlight", 0xEC, 0x44, 0x7F);
            var shuffleResult = GetColor(_configuration.FemcBattleShuffleResultAccent, "Shuffle Result Accent", 0xFF, 0xAD, 0x9B);

            if (_battleEfficacyAccentPatch != 0)
            {
                memory.SafeWrite(_battleEfficacyAccentPatch + (nuint)2, new byte[] { battlePrimary.R });
                memory.SafeWrite(_battleEfficacyAccentPatch + (nuint)5, new byte[] { battlePrimary.G });
                memory.SafeWrite(_battleEfficacyAccentPatch + (nuint)8, new byte[] { battlePrimary.B });
            }

            foreach (var patch in _personaDarkEbxPatches)
                memory.SafeWrite(patch + (nuint)1, new byte[] { 0x00, personaDark.B, personaDark.G, personaDark.R });

            foreach (var patch in _personaDarkEsiPatches)
                memory.SafeWrite(patch + (nuint)1, new byte[] { 0x00, personaDark.B, personaDark.G, personaDark.R });

            foreach (var patch in _personaDarkEcxPatches)
                memory.SafeWrite(patch + (nuint)1, new byte[] { 0x00, personaDark.B, personaDark.G, personaDark.R });

            if (_personaInitialTransitionPatch != 0)
            {
                memory.SafeWrite(_personaInitialTransitionPatch + (nuint)1, new byte[] { personaPrimitive.B });
                memory.SafeWrite(_personaInitialTransitionPatch + (nuint)2, new byte[] { 0xB1, personaPrimitive.G });
                memory.SafeWrite(_personaInitialTransitionPatch + (nuint)5, new byte[] { personaPrimitive.R });
            }

            if (_personaTransitionPrimitivePatch != 0)
            {
                memory.SafeWrite(_personaTransitionPrimitivePatch + (nuint)1, new byte[] { personaPrimitive.R, 0x00, 0x00, 0x00 });
                memory.SafeWrite(_personaTransitionPrimitivePatch + (nuint)6, new byte[] { 0x00, personaPrimitive.G, personaPrimitive.B, 0x05 });
                memory.SafeWrite(_personaTransitionPrimitivePatch + (nuint)11, new byte[] { 0x00, personaPrimitive.G, personaPrimitive.B, 0xFF });
            }

            if (_dialoguePromptPatch != 0)
            {
                memory.SafeWrite(_dialoguePromptPatch + (nuint)2, new byte[] { dialoguePrompt.R });
                memory.SafeWrite(_dialoguePromptPatch + (nuint)14, new byte[] { dialoguePrompt.G });
                memory.SafeWrite(_dialoguePromptPatch + (nuint)17, new byte[] { dialoguePrompt.B });
            }

            if (_dialogueWindowPatch != 0)
                memory.SafeWrite(_dialogueWindowPatch + (nuint)2, new byte[] { 0x00, dialogueWindow.B, dialogueWindow.G, dialogueWindow.R });

            if (_shuffleResultPatch != 0)
                memory.SafeWrite(_shuffleResultPatch + (nuint)4, new byte[] { shuffleResult.R, shuffleResult.G, shuffleResult.B, 0xFF });
        }


        private void OnModLoaderInitialized()
        {
            ApplyExperimentalFemcPalette();
        }

        private static byte[] BuildPaletteBytes(RgbColor[] colors, byte trailingByte)
        {
            var data = new byte[colors.Length * 4];

            for (var i = 0; i < colors.Length; i++)
            {
                var offset = i * 4;
                data[offset] = colors[i].R;
                data[offset + 1] = colors[i].G;
                data[offset + 2] = colors[i].B;
                data[offset + 3] = trailingByte;
            }

            return data;
        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;

            ApplyExperimentalFemcPalette();
            _colorLogger.UpdateConfig(configuration);

            _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
        }
        #endregion

        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Mod() { }
#pragma warning restore CS8618
        #endregion
    }
}
