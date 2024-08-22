namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CommandSystem;

    using Exiled.Permissions.Extensions;
    using Exiled.API.Features;
    using Exiled.CustomItems;
    using Exiled.CustomItems.Events;
    using Exiled.CustomItems.API.Features;
    using Exiled.API.Features.Pools;

    public class Show : ICommand
    {
        public string Command { get; } = "show";
        public string Description { get; } = "Shows all AdminItems";
        public string[] Aliases { get; } = new string[] { "s", "sh" };

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!Permissions.CheckPermission(player, "zeitvertreib.team"))
            {
                response = "You dont have permission to execute this command!";
                return false;
            }

            StringBuilder message = StringBuilderPool.Pool.Get().AppendLine();

            message.Append("[Registered items via Exiled.CustomItems (").Append(CustomItem.Registered.Count).AppendLine(")]");

            foreach (CustomItem customItem in CustomItem.Registered.OrderBy(item => item.Id))
                message.Append("[ID: ").Append(customItem.Id).Append(" | NAME: ").Append(customItem.Name).Append(" (Item: ").Append(customItem.Type).Append(')').AppendLine("]");

            message.AppendLine("").Append("Um die Items zu erhalten entweder \"adminitems give [ID]\" oder \"adminitems give [NAME]\"");
            response = StringBuilderPool.Pool.ToStringReturn(message);
            return true;
        }
    }
}