namespace vip.zeitvertreib
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Scp914;

    using Exiled.API.Features.Pickups;

    using MEC;

    using Scp914;

    using vip.zeitvertreib;
    using vip.zeitvertreib.config;
    using static vip.zeitvertreib.ZeitvertreibCommon;

    internal sealed class EventsHandler
    {
        public void On914UpgradeItemOnGround(UpgradingPickupEventArgs ev)
        {
            foreach (Scp914Changes change in Instance.Config.scp914changes)
            {
                if (ev.KnobSetting != change.knobSetting) continue;
                if (ev.Pickup.Type != change.input) continue;

                int rand = UnityEngine.Random.Range(0, 100);

                ItemUtils.replaceItem(ev.Pickup, ((rand < change.chance) ? change.outputSuccess : change.outputFail), ev.OutputPosition);
                ev.IsAllowed = false;

                break;
            }
        }

        public void On914UpgradeItemInInventory(UpgradingInventoryItemEventArgs ev)
        {
            foreach (Scp914Changes change in Instance.Config.scp914changes)
            {
                if (ev.KnobSetting != change.knobSetting) continue;
                if (ev.Item.Type != change.input) continue;

                int rand = UnityEngine.Random.Range(0, 100);
                
                ev.Player.RemoveItem(ev.Item.Serial);

                ev.IsAllowed = false;

                ItemType item = ((rand < change.chance) ? change.outputSuccess : change.outputFail);
                if (item != ItemType.None)
                    ev.Player.AddItem(item);
                    
                break;
            }
        }
    }
}
