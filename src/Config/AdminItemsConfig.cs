namespace vip.zeitvertreib.config
{
    using System.ComponentModel;

    public class AdminItemsConfig
    {
        public GrenaderConfig grenader { get; set; } = new GrenaderConfig();
        public BlitzerConfig blitzer { get; set; } = new BlitzerConfig();
        public Fake500Config fake500 { get; set; } = new Fake500Config();
    }
}