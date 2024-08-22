namespace vip.zeitvertreib.CustomItems
{
    using System;
    using System.Collections.Generic;

    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Components;
    using Exiled.API.Features.Pickups.Projectiles;
    using Exiled.API.Features.Spawn;
    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;

    using UnityEngine;

    // from SCP SL Assembly 
    using InventorySystem;
    using PlayerRoles;
    using RemoteAdmin;
    using static Broadcast;


    using vip.zeitvertreib;
    using static vip.zeitvertreib.ZeitvertreibCommon;


    [CustomItem(ItemType.GunCom45)]
    public class Blitzer : CustomWeapon
    {
        public override uint Id { get; set; } = 2;
        public override string Name { get; set; } = "Blitzer";
        public override string Description { get; set; } = "Dieser \"Aperat\" ist zu mächtig. Es sollte weggesperrt werden jedoch findet die Menschheit das Töten wichtiger ist.";
        public override float Weight { get; set; } = 0f;
        public override float Damage { get; set; } = 0f;
        public override byte ClipSize { get; set; } = byte.MaxValue;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties { };

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            ev.IsAllowed = Instance.Config.adminItems.blitzer.reloadable;
        }

        protected override void OnDropping(DroppingItemEventArgs ev)
        {
            string adminChat = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.dropped.AdminChat);
            string adminBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.dropped.AdminsBroadcast);
            string playerBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.dropped.PlayerBroadcast);
            string mapBroudcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.dropped.MapBroadcast);

            if (playerBroadcast.Length > 0)
                ev.Player.Broadcast(10, mapBroudcast, BroadcastFlags.Normal, true);
            if (adminChat.Length > 0)
                CommandProcessor.ProcessAdminChat(adminChat, new ServerConsoleSender());
            if (mapBroudcast.Length > 0)
                Map.Broadcast(10, mapBroudcast, BroadcastFlags.Normal, true);
            if (adminBroadcast.Length > 0)
                ev.Player.Broadcast(10, adminBroadcast, BroadcastFlags.AdminChat, true);
        }

        protected override void OnPickingUp(PickingUpItemEventArgs ev)
        {
            string adminChat = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.pickedUp.AdminChat);
            string adminBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.pickedUp.AdminsBroadcast);
            string playerBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.pickedUp.PlayerBroadcast);
            string mapBroudcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.blitzer.broadcasts.pickedUp.MapBroadcast);

            if (playerBroadcast.Length > 0)
                ev.Player.Broadcast(10, mapBroudcast, BroadcastFlags.Normal, true);
            if (adminChat.Length > 0)
                CommandProcessor.ProcessAdminChat(adminChat, new ServerConsoleSender());
            if (mapBroudcast.Length > 0)
                Map.Broadcast(10, mapBroudcast, BroadcastFlags.Normal, true);
            if (adminBroadcast.Length > 0)
                ev.Player.Broadcast(10, adminBroadcast, BroadcastFlags.AdminChat, true);
        }



        protected override void OnShooting(ShootingEventArgs ev)
        {
            ev.IsAllowed = false;
            for (int i = 0; i < Instance.Config.adminItems.blitzer.rate; i++) // To calculate the per second value multiply by 60 (rate of COM45 / sec)
                ev.Player.ThrowGrenade(ProjectileType.Scp2176, true);

            if (ev.Player.CurrentItem is Firearm firearm)
                firearm.Ammo -= (byte) Instance.Config.adminItems.blitzer.ammoPerShot;
        }

    }
}