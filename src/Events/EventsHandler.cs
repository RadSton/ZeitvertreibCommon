namespace vip.zeitvertreib
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Scp914;


    using MEC;

    using Scp914;

    internal sealed class EventsHandler
    {
        public void On914UpgradeItemOnGround(UpgradingPickupEventArgs ev)
        {
            if (ev.KnobSetting == Scp914KnobSetting.OneToOne)
            {
                if (ev.Pickup.Type == ItemType.KeycardMTFPrivate)
                {
                    ItemUtils.replaceItem(ev.Pickup, ItemType.KeycardContainmentEngineer, ev.OutputPosition);
                    ev.IsAllowed = false;
                }
            }

        }

        public void On914UpgradeItemInInventory(UpgradingInventoryItemEventArgs ev)
        {
            if (ev.KnobSetting == Scp914KnobSetting.OneToOne)
            {
                if (ev.Item.Type == ItemType.KeycardMTFPrivate)
                {
                    ev.Player.AddItem(ItemType.KeycardContainmentEngineer);
                    ev.Player.RemoveItem(ev.Item.Serial);
                    ev.IsAllowed = false;
                }
            }

        }
    }
}
