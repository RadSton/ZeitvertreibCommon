namespace vip.zeitvertreib
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using Exiled.CustomItems.API.Features;
    
    using vip.zeitvertreib.CustomItems;

    public class Items
    {
        [Description("The list of custom items.")]
        public List<CustomItem> ITEMS { get; private set; } = new List<CustomItem>
        {
            new Grenader(),
            new Blitzer(),
            new Fake500(),
        };
    }
}