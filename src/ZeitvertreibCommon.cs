namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;

    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.CustomItems.API.Features;

    using vip.zeitvertreib.config;

    using MEC;

    public class ZeitvertreibCommon : Plugin<Config>
    {
        private static ZeitvertreibCommon Singleton;
        public static ZeitvertreibCommon Instance => Singleton;
        public static List<DeathEventConfig> DeathEventConfig = new List<DeathEventConfig>();
        public static string VERSION { get; set; } = "1.0.1";
        private EventsHandler eventsHandler;
        private static Items itemClass = new Items();
        public override PluginPriority Priority { get; } = PluginPriority.Last;
        public override string Author { get; } = "radston12";
        
        public override void OnEnabled()
        {
            Singleton = this;

            CustomItem.RegisterItems(overrideClass: itemClass);

            eventsHandler = new EventsHandler();
            Exiled.Events.Handlers.Scp914.UpgradingPickup += eventsHandler.On914UpgradeItemOnGround;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem += eventsHandler.On914UpgradeItemInInventory;

            Log.Info(Config.adminItems.fake500.deathEvents);
            try
            {
                ZeitvertreibCommon.DeathEventConfig = Fake500Helper.parseDeathEvents(Config.adminItems.fake500.deathEvents);
            }
            catch (Exception e)
            {              
                Log.Error("IMPORTANT! -------------------------- IMPORTANT!");
                Log.Error("If this is the first start with the plugin the server needs to be restarted before Fake500 can be used since the empty default config cannot be parsed!");
                Log.Error("------------------------------------------------");
                Log.Error("Config zeitvertreib_commons.adminitems.fake500.deathEvents could not be parsed!");
                Log.Error("If you believe this is an error please open a gitub issue (https://github.com/RadSton/ZeitvertreibCommon/issues/new)");
                Log.Error(e);
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomItem.UnregisterItems();

            Exiled.Events.Handlers.Scp914.UpgradingPickup -= eventsHandler.On914UpgradeItemOnGround;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem -= eventsHandler.On914UpgradeItemInInventory;

            base.OnDisabled();
        }
    }
}