namespace vip.zeitvertreib.config
{
    using System.ComponentModel;

    public class BlitzerConfig
    {
        [Description("SCP-2176 spawned per shot (must be an integer) (multiply by 60 for firerate per second)")]
        public int rate { get; set; } = 5;

        [Description("How much ammo the weapon uses per shot")]
        public int ammoPerShot { get; set; } = 0;

        [Description("If the weapon can be reloaded")]
        public bool reloadable { get; set; } = false;

        [Description("Should a message be sent if this weapon is picked up / dropped \n\nMessages broadcasted when event is fired\nLeave strings empty to disable messages\nIn formatting you can use:\n{Name} -> Name of player\n{Id} -> Id of player\n{Room} -> Current room of player\n\nYou can also use the SCP: SL default formating\nlike <color=#f00> </color> or <size=50></size>\n\nImportant: \"AdminChat\" will send silently / without broadcast\n            for a broadcast configure \"AdminsBroadcast\"")]
        public BroadcastConfig broadcasts { get; set; } = new BroadcastConfig
        {
            pickedUp = new BroadcastConfigPart
            {
                AdminChat = "{Name}({Id}) picked up Blitzer in {Room}",
                AdminsBroadcast = "{Name} picked up Blitzer",
                PlayerBroadcast = "",
                MapBroadcast = "",
            },
            dropped = new BroadcastConfigPart
            {
                AdminChat = "{Name}({Id}) dropped Blitzer in {Room}",
                AdminsBroadcast = "{Name} dropped Blitzer",
                PlayerBroadcast = "<color=#f00>You dropped a dangerous weapon!</color>",
                MapBroadcast = "",
            }
        };
    }
}