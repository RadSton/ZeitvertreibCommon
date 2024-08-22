namespace vip.zeitvertreib.CustomItems
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Components;
    using Exiled.API.Features.Pickups.Projectiles;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;

    using Item = Exiled.API.Features.Items.Item;


    using Exiled.API.Enums;

    using InventorySystem;
    using UnityEngine;


    using PlayerRoles;
    using CustomPlayerEffects;
    using Utils;

    using RemoteAdmin;
    using static Broadcast;

    using MEC;

    using static vip.zeitvertreib.ZeitvertreibCommon;

    [CustomItem(ItemType.SCP500)]
    public class Fake500 : CustomItem
    {
        public override uint Id { get; set; } = 3;
        public override string Name { get; set; } = "Fake500";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 0f;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        { };


        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            base.UnsubscribeEvents();
        }

        protected override void OnChanging(ChangingItemEventArgs ev)
        {
            if (Instance.Config.adminItems.fake500.showItemInfoExiledText)
                base.OnChanging(ev);
        }

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            if (Instance.Config.adminItems.fake500.showPickedUpExiledText)
                base.OnAcquired(player, item, displayMessage);
        }


        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;

            Timing.CallDelayed(1f, () =>
            {
                Fake500Helper.applyEffects(ev.Player, Instance.Config.adminItems.fake500.effectsOnConsumtion);

                Timing.CallDelayed(Instance.Config.adminItems.fake500.delayBeforeDeathEvent, () =>
                {
                    try
                    {
                        if (DeathEventConfig == null || DeathEventConfig.Count < 1)
                        {
                            ev.Player.Kill(DamageType.Unknown, "SCP-500 failed to kill you but did it anyways");
                            return;
                        }
                        int rand = UnityEngine.Random.Range(1, DeathEventConfig.Count);
                        DeathEventConfig[rand].execute(ev.Player);

                    }
                    catch (Exception e)
                    {
                        ev.Player.Kill(DamageType.Unknown, "SCP-500 failed to kill you but did it anyways");
                        Log.Error("An error was caught when randomizing Fake-500 death chances");
                        Log.Error("If this is not a config error please report the following error to the Github-Repo (https://github.com/RadSton/ZeitvertreibCommon/issues/new)");
                        Log.Error(e);
                    }
                });
            });



        }
    }
}