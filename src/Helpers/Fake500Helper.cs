namespace vip.zeitvertreib
{

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;

    using Exiled.API.Features;
    using Exiled.API.Features.Pickups;

    using vip.zeitvertreib.config;

    using CustomPlayerEffects;

    using static vip.zeitvertreib.ZeitvertreibCommon;


    public class Fake500Helper
    {
        public static void applyEffects(Player p, List<EffectConfig> config)
        {
            StatusEffectBase statusEffectBase;
            foreach (EffectConfig configItem in config)
                if (p.ReferenceHub.playerEffectsController.TryGetEffect(configItem.effect, out statusEffectBase))
                    p.EnableEffect(statusEffectBase, configItem.intensity, configItem.duration, true);
        }

        private static T GetFromDictionary<T>(System.Collections.Generic.Dictionary<object, object> dictionary, string key)
        {
            object obj;

            if (!dictionary.TryGetValue(key, out obj))
                return (T)obj;

            return (T)obj;
        }
        private static System.String GetString(System.Collections.Generic.Dictionary<object, object> dictionary, string key)
        {
            return GetFromDictionary<System.String>(dictionary, key);
        }


        private static int GetInt(System.Collections.Generic.Dictionary<object, object> dictionary, string key)
        {
            int val;
            if (!Int32.TryParse(GetString(dictionary, key), out val))
                return 0;

            return val;
        }

        public static List<DeathEventConfig> parseDeathEvents(List<object> list)
        {
            List<DeathEventConfig> result = new List<DeathEventConfig>();

            foreach (object obj in list)
            {
                System.Collections.Generic.Dictionary<object, object> dictionary = (System.Collections.Generic.Dictionary<object, object>)obj;

                System.String eventName = GetString(dictionary, "event_name");
                int chance = GetInt(dictionary, "chance");

                switch (eventName)
                {
                    case "kick":
                        System.String message = GetString(dictionary, "message");
                        for (int i = 0; i < chance; i++)
                            result.Add(new KickDeathEventConfig
                            {
                                chance = chance,
                                message = message
                            });
                        break;
                    case "effect":
                        List<object> objectEffects = GetFromDictionary<List<object>>(dictionary, "effects");

                        List<EffectConfig> effects = new List<EffectConfig>();

                        foreach (object ob in objectEffects)
                        {
                            Dictionary<object, object> effectsDictionary = (Dictionary<object, object>)ob;

                            System.String effect = GetString(effectsDictionary, "effect");
                            byte intensity = (byte)GetInt(effectsDictionary, "intensity");
                            int duration = GetInt(effectsDictionary, "duration");

                            effects.Add(new EffectConfig
                            {
                                effect = effect,
                                intensity = intensity,
                                duration = duration,
                            });
                        }
                        for (int i = 0; i < chance; i++)
                            result.Add(new EffectDeathEventConfig
                            {
                                chance = chance,
                                effects = effects,
                            });
                        break;
                    case "hurt":
                        int damage = GetInt(dictionary, "damage");
                        System.String reason = GetString(dictionary, "reason");
                        for (int i = 0; i < chance; i++)
                            result.Add(new HurtDeathEventConfig
                            {
                                chance = chance,
                                damage = damage,
                                reason = reason
                            });
                        break;

                    case "position":
                        int x = GetInt(dictionary, "added_x");
                        int y = GetInt(dictionary, "added_y");
                        int z = GetInt(dictionary, "added_z");
                        for (int i = 0; i < chance; i++)
                            result.Add(new PositionDeathEventConfig
                            {
                                chance = chance,
                                addedX = x,
                                addedY = y,
                                addedZ = z,
                            });
                        break;
                    default:
                    case "explode":
                        for (int i = 0; i < chance; i++)
                            result.Add(new ExplodeDeathEventConfig
                            {
                                chance = chance
                            });
                        break;
                    case "tesla":
                        for (int i = 0; i < chance; i++)
                            result.Add(new TeslaDeathEventConfig
                            {
                                chance = chance
                            });
                        break;
                }

            }

            return result;
        }

    }
}
