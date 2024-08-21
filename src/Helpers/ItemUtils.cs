namespace vip.zeitvertreib
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Exiled.API.Features;
    using Exiled.API.Features.Pickups;

    using UnityEngine;

    public static class ItemUtils
    {

        public static void replaceItem(Pickup old, ItemType newItemType, Vector3 pos)
        {
            if(newItemType != ItemType.None) 
                Pickup.CreateAndSpawn(newItemType, pos, old.Rotation, old.PreviousOwner);
                
            old.Destroy();
        }
}
}