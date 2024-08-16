using p3ppc.totalkotoneoverhaul.Configuration;
using p3ppc.totalkotoneoverhaul.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Memory;
using Reloaded.Memory.Interfaces;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using PAK.Stream.Emulator;
using PAK.Stream.Emulator.Interfaces;
using static p3ppc.totalkotoneoverhaul.Utils;
using p3ppc.totalkotoneoverhaul;

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

        public Mod(ModContext context)
        {
               
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;

            Utils.Initialise(_logger, _configuration, _modLoader);
            var memory = Memory.Instance;
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
                Utils.SigScan("C7 45 ?? 00 01 25 FF", "Femc Title Screen", address =>
                {
                    memory.SafeWrite((nuint)(address + 3), new byte[] { 0xB2, 0x31, 0x46, 0xFF });
                });

               Utils.SigScan("75 ?? F6 83 ?? ?? ?? ?? 02 74 ?? E8 ?? ?? ?? ??", "Fixing my mistakes", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0x90, 0x90 });
                });

                Utils.SigScan("0F BA F0 07 ?? ?? ?? ?? ?? ?? ??", "Pink Loading Card + Title config", 4,
                address =>
                {
                    memory.SafeWrite((nuint)(address + 2), new byte[] { 0xE8 });
                });

                criFsApi.AddProbingPath("Title Screen/P5REssentials/CPK");
            }

            if (_configuration.AOA)
            {
                SigScan("C6 44 24 38 FF C6 44 24 30 CF 48 8B CF C6 44 24 28 9C", "AOA Prompt", address =>
                {
                    memory.SafeWrite((nuint)(address), new byte[] { 0xC6, 0x44, 0x24, 0x38, 0xDB, 0xC6, 0x44, 0x24, 0x30, 0xBF, 0x48, 0x8B, 0xCF, 0xC6, 0x44, 0x24, 0x28, 0xFF });
                });

                SigScan("C6 44 24 38 FF C6 44 24 30 CF 44 8D 42 6F C6 44 24 28 9C", "AOA Prompt PT 2 BECAUSE OF THE STUPID FUCKING ROUNDED EDGE AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", address =>
                {
                    memory.SafeWrite((nuint)address, new byte[] { 0xC6, 0x44, 0x24, 0x38, 0xDB, 0xC6, 0x44, 0x24, 0x30, 0xBF, 0x44, 0x8D, 0x42, 0x6F, 0xC6, 0x44, 0x24, 0x28, 0xFF });
                });

                criFsApi.AddProbingPath("AOA/P5REssentials/CPK");
            }

            if (_configuration.Timer)
            {
                SigScan("C6 44 24 ?? 5B", "TImer R", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { 0xFF });
                });

                SigScan("C6 44 24 ?? FF C6 44 24 ?? 5B", "Timer G", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { 0x5B });
                });

                SigScan("C6 44 24 30 ED", "Timer B", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { 0x9A });
                });
            }

            if (_configuration.Advantage)
            {
                SigScan("C6 44 24 38 FF C6 44 24 30 19 44 88 74 24 28", "Advantage - Center Glow", address =>
                {
                    memory.SafeWrite((nuint)(address), new byte[] { 0xC6, 0x44, 0x24, 0x38, 0x79, 0xC6, 0x44, 0x24, 0x30, 0x00, 0xC6, 0X44, 0x24, 0x28, 0xFF });
                });

                SigScan("66 C7 45 48 43 59 C6 45 4A FC", "Advantage - Center Diamond", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { 0xC9, 0x42, 0xC6, 0x45, 0x4A, 0x68 });
                });

                SigScan("66 C7 45 48 66 82 C6 45 4A FF", "Advantage - Text", address =>
                {
                    memory.SafeWrite((nuint)(address + 4), new byte[] { 0xEE, 0x8C, 0xC6, 0x45, 0x4A, 0xE1 });
                });

                SigScan("C7 45 48 66 82 FF FF", "Advantage - Square Outline", address =>
                {
                    memory.SafeWrite((nuint)(address + 3), new byte[] { 0xEE, 0x8C, 0xE1 });
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


            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic
        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;
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