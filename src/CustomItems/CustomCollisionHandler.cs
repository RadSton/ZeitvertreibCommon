
namespace vip.zeitvertreib.CustomItems
{
    using System;
    using System.Collections.Generic;
    using InventorySystem.Items.ThrowableProjectiles;
    using Exiled.API.Features;
    using Exiled.API.Features.Pickups;
    using UnityEngine;
    using MEC;

    /// <summary>
    /// Collision Handler for grenades.
    /// </summary>
    public class CustomCollisionHandler : MonoBehaviour
    {
        private bool initialized;

        private int Counter = 0; 

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
            if(initialized) return;

            Owner = owner;
            Grenade = (EffectGrenade)grenade;
            initialized = true;
        }


        private void FixedUpdate() {
            if(!initialized) return;
            
            Counter++;
        }

        private void OnCollisionEnter(Collision collision)
        {
            try
            {
                if (!initialized)
                    return;

                if(Counter <= 10) 
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