namespace vip.zeitvertreib
{
    using System.Collections.Generic;

    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.CustomItems.API.Features;

    using MEC;

    public class ZeitvertreibCommon : Plugin<Config>
    {
        private static ZeitvertreibCommon Singleton;
        public static ZeitvertreibCommon Instance => Singleton;
        private EventsHandler eventsHandler;
        private static Items itemClass = new Items();
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override string Author {get;} = "radston12";
        public override void OnEnabled()
        {
            Singleton = this;

            CustomItem.RegisterItems(overrideClass: itemClass);


            eventsHandler = new EventsHandler();
            //Exiled.Events.Handlers.Player.Verified += eventsHandler.OnVerified;
            Exiled.Events.Handlers.Scp914.UpgradingPickup += eventsHandler.On914UpgradeItemOnGround;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem += eventsHandler.On914UpgradeItemInInventory;

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