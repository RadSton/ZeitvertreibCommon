namespace vip.zeitvertreib
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    using vip.zeitvertreib.config;

    using Scp914;

    public sealed class Config : IConfig
    {

        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; }

        public AdminItemsConfig adminItems { get; set; } = new AdminItemsConfig();

        public List<Scp914Changes> scp914changes { get; set; } = new List<Scp914Changes>() {
            new Scp914Changes {
                knobSetting = Scp914KnobSetting.OneToOne,
                input = ItemType.KeycardMTFPrivate,
                outputSuccess = ItemType.KeycardContainmentEngineer,
                outputFail = ItemType.None,
                chance = 100
            }
        };
    }
}