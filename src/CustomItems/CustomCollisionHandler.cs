
namespace vip.zeitvertreib.CustomItems
{
    using System;
    using InventorySystem.Items.ThrowableProjectiles;
    using Exiled.API.Features;
    using Exiled.API.Features.Pickups;
    using UnityEngine;

    /// <summary>
    /// Collision Handler for grenades.
    /// </summary>
    public class CustomCollisionHandler : MonoBehaviour
    {
        private bool initialized;

        /// <summary>
        /// Gets the thrower of the grenade.
        /// </summary>
        public GameObject Owner { get; private set; }

        /// <summary>
        /// Gets the grenade itself.
        /// </summary>
        public EffectGrenade Grenade { get; private set; }

        public void Init(GameObject owner, ThrownProjectile grenade)
        {
            Owner = owner;
            Grenade = (EffectGrenade)grenade;
            initialized = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            try
            {
                if (!initialized)
                    return;

                if (Grenade.TargetTime <= 1f)
                    return;
                
                if (collision.collider.gameObject == Owner || collision.collider.gameObject.TryGetComponent<EffectGrenade>(out _))
                    return;

                Grenade.TargetTime = 1f;
            }
            catch (Exception exception)
            {
                Log.Error($"{nameof(OnCollisionEnter)} error:\n{exception}");
                Destroy(this);
            }
        }
    }

}