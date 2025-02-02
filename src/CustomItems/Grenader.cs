namespace vip.zeitvertreib.CustomItems
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Components;
    using Exiled.API.Features.Pickups.Projectiles;
    using Exiled.API.Features.Spawn;
    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;


    using Exiled.API.Enums;

    using InventorySystem;
    using UnityEngine;


    using PlayerRoles;

    using RemoteAdmin;
    using static Broadcast;

    using vip.zeitvertreib;
    using static vip.zeitvertreib.ZeitvertreibCommon;



    [CustomItem(ItemType.GunCom45)]
    public class Grenader : CustomWeapon
    {
        public override uint Id { get; set; } = 1;
        public override string Name { get; set; } = "Granatenteilchenbeschleuniger";
        public override string Description { get; set; } = "Dieser \"Aperat\" ist zu mächtig. Es sollte weggesperrt werden jedoch findet die Menschheit das Töten wichtiger ist.";
        public override float Weight { get; set; } = 0f;
        public override float Damage { get; set; } = 0f;
        public override byte ClipSize { get; set; } = 36;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties { };

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            ev.IsAllowed = Instance.Config.adminItems.grenader.reloadable;
        }

        protected override void OnDropping(DroppingItemEventArgs ev)
        {
            string adminChat = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.dropped.AdminChat);
            string adminBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.dropped.AdminsBroadcast);
            string playerBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.dropped.PlayerBroadcast);
            string mapBroudcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.dropped.MapBroadcast);

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
            string adminChat = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.pickedUp.AdminChat);
            string adminBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.pickedUp.AdminsBroadcast);
            string playerBroadcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.pickedUp.PlayerBroadcast);
            string mapBroudcast = RAUserIdParser.applyBroadcastFormatting(ev.Player, Instance.Config.adminItems.grenader.broadcasts.pickedUp.MapBroadcast);

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

            for (int i = 0; i < 5; i++)
            {
                Projectile projectile = ev.Player.ThrowGrenade(ProjectileType.FragGrenade, true).Projectile;
                projectile.GameObject.AddComponent<CustomCollisionHandler>().Init(ev.Player.GameObject, projectile.Base);
                
                //if (Instance.Config.adminItems.grenader.explodeOnImpact)
                //    projectile.GameObject.AddComponent<CustomCollisionHandler>().Init(ev.Player.GameObject, projectile.Base);
            }

            //if (ev.Player.CurrentItem is Firearm firearm)
            //    firearm.Ammo -= (byte) Instance.Config.adminItems.grenader.ammoPerShot;
        }

    }
}