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
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;

    using UnityEngine;

    // from SCP SL Assembly 
    using InventorySystem;
    using PlayerRoles;
    using RemoteAdmin;
    using static Broadcast;


    [CustomItem(ItemType.GunCom45)]
    public class Blitzer : CustomWeapon
    {
        public override uint Id { get; set; } = 2;
        public override string Name { get; set; } = "Blitzer";
        public override string Description { get; set; } = "Dieser \"Aperat\" ist zu mächtig. Es sollte weggesperrt werden jedoch findet die Menschheit das Töten wichtiger ist.";
        public override float Weight { get; set; } = 0f;
        public override float Damage { get; set; }
        public override byte ClipSize { get; set; } = byte.MaxValue;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties{};

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        protected override void OnDropping(DroppingItemEventArgs ev)
        {
            ev.Player.Broadcast(10, "<size=40><color=#F00>DANGER: YOU DROPPED A WEAPON BELONGING TO \n max.bambus76</color></size>\n", BroadcastFlags.Normal, true);

            CommandProcessor.ProcessAdminChat("[ZEITVERTREIB-COMMONS] " + ev.Player.DisplayNickname + " dropped dangerous weapon (Blitzer)", new ServerConsoleSender());
        }

        protected override void OnPickingUp(PickingUpItemEventArgs ev)
        {
            Map.Broadcast(10, "<size=40><color=#F00>DANGER: A WEAPON BELONGING TO \n max.bambus76 WAS PICKED UP</color></size>\n", BroadcastFlags.Normal, true);
            
            CommandProcessor.ProcessAdminChat("[ZEITVERTREIB-COMMONS] " + ev.Player.DisplayNickname + " picked up dangerous weapon (Blitzer)", new ServerConsoleSender());
            CommandProcessor.ProcessAdminChat("[ZEITVERTREIB-COMMONS] Use command \"ci l ii\" to see all picked up weapons", new ServerConsoleSender());
        }


        protected override void OnShooting(ShootingEventArgs ev)
        {
            ev.IsAllowed = false;
            for (int i = 0; i < 5; i++)
            {
                Projectile projectile = ev.Player.ThrowGrenade(ProjectileType.Scp2176, true).Projectile; 
                projectile.GameObject.AddComponent<CollisionHandler>().Init(ev.Player.GameObject, projectile.Base);
            }
        }

    }
}