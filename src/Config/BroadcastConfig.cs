namespace vip.zeitvertreib.config
{
    using System.ComponentModel;

    public class BroadcastConfig
    {
        public BroadcastConfigPart pickedUp { get; set; } = new BroadcastConfigPart();
        public BroadcastConfigPart dropped { get; set; } = new BroadcastConfigPart();
    }

    public class BroadcastConfigPart
    {
        public string AdminChat { get; set; } = "";
        public string AdminsBroadcast { get; set; } = "";
        public string PlayerBroadcast { get; set; } = "";
        public string MapBroadcast { get; set; } = "";
    }
}