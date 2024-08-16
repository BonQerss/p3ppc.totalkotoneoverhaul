using System.ComponentModel;
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
        [DisplayName("Title Screen")]
        [Description("Replaces Title Screen background + assets.")]
        [DefaultValue(true)]
        public bool FEMCTitleScreen { get; set; } = true;

        [DisplayName("Analysis")]
        [Description("Replaces analysis screen colors.")]
        [DefaultValue(true)]
        public bool ILoveJesus { get; set; } = true;

        [DisplayName("AOA Prompt")]
        [Description("Replaces All Out Attack prompt screen.")]
        [DefaultValue(true)]
        public bool AOA { get; set; } = true;

        [DisplayName("Time Limit")]
        [Description("Replaces time limit box during the first full moon mission.")]
        [DefaultValue(true)]
        public bool Timer { get; set; } = true;

        [DisplayName("Player Advantage")]
        [Description("Replaces player advantage icon and animation.")]
        [DefaultValue(true)]
        public bool Advantage { get; set; } = true;

        [DisplayName("Fusion Spells")]
        [Description("Replaces fusion spell assets with red versions.")]
        [DefaultValue(true)]
        public bool FusionSpells { get; set; } = true;

        [DisplayName("Persona Summoning Smoke")]
        [Description("Replaces summoning smoke around the player during the Evoker animation.")]
        [DefaultValue(true)]
        public bool FSMOKE { get; set; } = true;

        [DisplayName("AOA Background")]
        [Description("Replaces the blue All Out Attack backgrounds with pink ones.")]
        [DefaultValue(true)]
        public bool AOABackground { get; set; } = true;
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
