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

        protected override void OnChanging(ChangingItemEventArgs ev){}

        protected override void OnAcquired(Player player, Item item, bool displayMessage){}


        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;

            Timing.CallDelayed(1f, () =>
            {

                ev.Player.EnableEffect<AmnesiaVision>(10f, true);
                ev.Player.EnableEffect<Blinded>(10f, true);

                Timing.CallDelayed(2f, () =>
                {
                    int rand = UnityEngine.Random.Range(1, 9);
                    switch (rand)
                    {
                        case 1: 
                            ServerConsole.Disconnect(ev.Player.ReferenceHub.gameObject, "You have been kicked. Reason: Timed out");
                            break;
                        case 2:
                            ExplosionUtils.ServerExplode(ev.Player.ReferenceHub);
                            break;
                        case 3: 
                            ev.Player.EnableEffect<CardiacArrest>(100f, true);
                            ev.Player.EnableEffect<SeveredHands>(100f, true);
                            break;
                        case 4: 
                            ev.Player.Hurt(ev.Player, 65535, DamageType.Unknown, null, "You were bitten very hard by Belu`s pet!");
                            break;
                        case 5: 
                            ev.Player.Position += new Vector3(0, 50, 0);
                            break;
                        case 6: 
                            ev.Player.EnableEffect<SeveredHands>(100f, true);
                            break;
                        case 7:
                            ev.Player.Position = TeslaGateController.Singleton.TeslaGates[0].Position;
                            ev.Player.Position += new Vector3(0, 2, 0);
                            ev.Player.EnableEffect<Ensnared>((byte)100, 100f, true);
                            break;
                        case 8:
                            ev.Player.EnableEffect<Flashed>(100, true);
                            ev.Player.EnableEffect<CardiacArrest>(100f, true);
                            ev.Player.EnableEffect<Ensnared>((byte)100, 100f, true);
                            break;
                    }
                });

            });



        }
    }
}