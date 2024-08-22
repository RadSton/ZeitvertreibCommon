namespace vip.zeitvertreib.config
{

    using Scp914;
    
    using System.ComponentModel;

    public class Scp914Changes
    {
        [Description("Possible values for knobSetting ´s are (Scp914.Scp914KnobSetting Enum): Rough, Coarse, OneToOne, Fine, VeryFine")]
        public Scp914KnobSetting knobSetting { get; set; } = Scp914KnobSetting.OneToOne;

        [Description("Input can be any ItemType except None")]
        public ItemType input { get; set; } = ItemType.KeycardJanitor;

        [Description("outputSuccess can be any ItemType that is given when the chance is on the players side")]
        public ItemType outputSuccess { get; set; } = ItemType.None;

        [Description("outputFail can be any ItemType that is given when the chance is not met")]
        public ItemType outputFail { get; set; } = ItemType.None;

        [Description("Chance value here is a percentage from 1-100 !")]
        public int chance { get; set; } = 100;
    }
}