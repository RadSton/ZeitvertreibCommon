namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;

    using CommandSystem;

    using Exiled.Permissions.Extensions;
    using Exiled.API.Features;
    using Exiled.CustomItems;
    using Exiled.CustomItems.Events;
    using Exiled.CustomItems.API.Features;
    using Exiled.API.Features.Pools;

    public class Give : ICommand
    {
        public string Command { get; } = "give";
        public string Description { get; } = "Gives a AdminItem";
        public string[] Aliases { get; } = new string[] { "g", "gi" };

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!Permissions.CheckPermission(player, "zeitvertreib.team"))
            {
                response = "You dont have permission to execute this command!";
                return false;
            }

            if(arguments.Count == 0) {
                response = $"Adminitem \"\" nicht gefunden!";
                return false;
            }

            if (!CustomItem.TryGet(arguments.At(0), out CustomItem item))
            {
                response = $"Adminitem {arguments.At(0)} nicht gefunden!";
                return false;
            }

            item.Give(player);

            // TODO: GIVE
            response = $"Dir wurde {item.Name} gegeben!";
            return true;
        }
    }
}