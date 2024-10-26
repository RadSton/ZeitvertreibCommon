namespace vip.zeitvertreib.config
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Components;
    using Exiled.API.Features.Pickups.Projectiles;
    using Exiled.API.Features.Spawn;
    using Exiled.API.Enums;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;


    using PlayerRoles;
    using CustomPlayerEffects;
    using Utils;
    using InventorySystem;
    using RemoteAdmin;

    
    using UnityEngine;

    public class Fake500Config
    {
        [Description("If the default Exiled.CustomItems text should be shown when item is picked up\nThis option is usless because it would reveal that it is no ordinary SCP-500")]
        public bool showPickedUpExiledText { get; set; } = false;

        [Description("If the default Exiled.CustomItems text should be shown when item is selected from the inventory\nThis option is usless because it would reveal that it is no ordinary SCP-500")]
        public bool showItemInfoExiledText { get; set; } = false;

        [Description("Delay in seconds after the initial effects the death events take place")]
        public float delayBeforeDeathEvent { get; set; } = 2f;

        [Description("Effects given immediatly after consumtion")]
        public List<EffectConfig> effectsOnConsumtion { get; set; } = new List<EffectConfig>() {
            new EffectConfig{
                effect = "AmnesiaVision",
                intensity = 10,
                duration = 100,
            },
            new EffectConfig{
                effect = "Blinded",
                intensity = 10,
                duration = 100,
            },
        };

        [Description("Possible events are: \n\n- \"kick\": Kicks player with message\n   -> message: Kick message \n\n- \"tesla\": Teleports player inbetween tesla gate\n\n- \"effect\": Applies effects to player\n   -> effects:\n        - effect: \"Blinded\"\n          intensity: 10\n          duration: 100\n\n- \"hurt\": Applies direct damage to player with custom death message\n   -> damage: Integer of damage (capped at 65535)\n   -> reason: Death reason string\n\n- \"position\": Adds defined values to the position of a player (f.e. tp 50 meters up)\n   -> addedX: float that is added to the x coordinate of the player´s position vector\n   -> addedY: float that is added to the y coordinate of the player´s position vector\n   -> addedZ: float that is added to the z coordinate of the player´s position vector\n\n- \"explode\": Makes the player explode (default explosion can damage surroundings) \n\n\nChance is an Integer (Note that 0 will result in the event beeing disabled)\nThe higher \"Chance\" is the higher is the probability of it being executed \nHow to calculate percentage from chance:\n  Sum up all chances from every deathEvent then inverse them (german: Kehrwert) (1 divided by <SUM OF CHANCES>) \n  multiply that value by the chance of the event you would like to get the percentage from\n  and finally multiply again by 100 to get the final percent chance\n\n(Ensnared -> Effect ID for Slowness | idk why?)")]
        public List<object> deathEvents { get; set; } = new List<object>() {
            new KickDeathEventConfig{
                message = "Timed out"
            },
            new ExplodeDeathEventConfig{},
            new EffectDeathEventConfig{
                effects = new List<EffectConfig>() {
                    new EffectConfig{
                        effect = "CardiacArrest",
                        intensity = 1,
                        duration = 100,
                    },
                    new EffectConfig{
                        effect = "SeveredHands",
                        intensity = 1,
                        duration = 1,
                    }
                }
            },
            new HurtDeathEventConfig{
                damage = 65535,
                reason = "You were bitten very hard by Belu`s pet!"
            },
            new PositionDeathEventConfig{
                addedX = 0,
                addedY = 50,
                addedZ = 0,
            },
            new EffectDeathEventConfig{
                effects = new List<EffectConfig>() {
                    new EffectConfig{
                        effect = "SeveredHands",
                        intensity = 1,
                        duration = 1,
                    }
                }
            },
            new TeslaDeathEventConfig{ },
            new EffectDeathEventConfig{
                effects = new List<EffectConfig>() {
                    new EffectConfig{
                        effect = "CardiacArrest",
                        intensity = 1,
                        duration = 100,
                    },
                    new EffectConfig{
                        effect = "Flashed",
                        intensity = 1,
                        duration = 100,
                    },
                    new EffectConfig{
                        effect = "Ensnared",
                        intensity = 1,
                        duration = 100,
                    },
                }
            }
        };

        [Description("Should a message be sent if this weapon is picked up / dropped \n\nMessages broadcasted when event is fired\nLeave strings empty to disable messages\nIn formatting you can use:\n{Name} -> Name of player\n{Id} -> Id of player\n{Room} -> Current room of player\n\nYou can also use the SCP: SL default formating\nlike <color=#f00> </color> or <size=50></size>\n\nImportant: \"AdminChat\" will send silently / without broadcast\n            for a broadcast configure \"AdminsBroadcast\"")]
        public BroadcastConfig broadcasts { get; set; } = new BroadcastConfig
        {
            pickedUp = new BroadcastConfigPart
            {
                AdminChat = "",
                AdminsBroadcast = "",
                PlayerBroadcast = "",
                MapBroadcast = "",
            },
            dropped = new BroadcastConfigPart
            {
                AdminChat = "",
                AdminsBroadcast = "",
                PlayerBroadcast = "",
                MapBroadcast = "",
            }
        };
    }

    public class EffectConfig
    {
        public string effect { get; set; }
        public byte intensity { get; set; }
        public int duration { get; set; }
    }

    public abstract class DeathEventConfig
    {
        public abstract string eventName { get; set; }
        public int chance { get; set; } = 1;

        public abstract void execute(Player p);
    }

    public class KickDeathEventConfig : DeathEventConfig
    {

        public override string eventName { get; set; } = "kick";
        public string message { get; set; }

        public override void execute(Player p)
        {
            ServerConsole.Disconnect(p.ReferenceHub.gameObject, "You have been kicked. Reason: " + message);
        }
    }
    public class EffectDeathEventConfig : DeathEventConfig
    {
        public override string eventName { get; set; } = "effect";
        public List<EffectConfig> effects { get; set; }

        public override void execute(Player p)
        {
            Fake500Helper.applyEffects(p, effects);
        }
    }
    public class HurtDeathEventConfig : DeathEventConfig
    {
        public override string eventName { get; set; } = "hurt";
        public int damage { get; set; }
        public string reason { get; set; }

        public override void execute(Player p)
        {
            p.Hurt(p, damage, DamageType.Unknown, null, reason);
        }
    }

    public class PositionDeathEventConfig : DeathEventConfig
    {
        public override string eventName { get; set; } = "position";
        public int addedX { get; set; }
        public int addedY { get; set; }
        public int addedZ { get; set; }

        public override void execute(Player p)
        {
            p.Position += new Vector3(addedX, addedY, addedZ);
        }
    }

    public class TeslaDeathEventConfig : DeathEventConfig
    {
        public override string eventName { get; set; } = "tesla";

        public override void execute(Player p)
        {
            p.Position = TeslaGateController.Singleton.TeslaGates[0].Position;
            p.Position += new Vector3(0, 2, 0);
            p.EnableEffect<Ensnared>((byte)100, 100f, true);
        }
    }

    public class ExplodeDeathEventConfig : DeathEventConfig
    {
        public override string eventName { get; set; } = "explode";

        public override void execute(Player p)
        {
            ExplosionUtils.ServerExplode(p.ReferenceHub, false);
        }
    }

}