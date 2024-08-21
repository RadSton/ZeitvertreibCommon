namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.API.Features.Pickups;
    using Exiled.Permissions.Extensions;


    using UnityEngine;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnItems : ICommand
    {
        public string Command { get; } = "spawnitems";
        public string Description { get; } = "Spawns an amount of items with specified attributes";
        public string[] Aliases { get; } = new string[] { };

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!sender.CheckPermission("zeitvertreib.team"))
            {
                response = "You don't have the permissions to execute this command.";
                return false;
            }

            if (arguments.Count != 4)
            {
                response = "Benutzung:\nspawnitems (RemoteAdmin-Id) (ItemId) (Scale) (Amount)";
                return false;
            }

            Player target = RAUserIdParser.getByCommandArgument(arguments.At(0));

            if (target == null)
            {
                response = "Ungültiger Spieler";
                return false;
            }


            Vector3 position = target.Position;

            if (!Enum.TryParse(arguments.At(1), true, out ItemType type))
            {
                response = $"Ungültige ItemId: {arguments.At(1)}";
                return false;
            }

            if (!float.TryParse(arguments.At(2), out float scaleValue) || !float.TryParse(arguments.At(3), out float amount))
            {
                response = "Ungültiger Scale oder Amount\n Benutzung: spawnitems (RemoteAdmin-Id) (ItemId) (Scale) (Amount)";
                return false;
            }

            Vector3 scale = new Vector3(scaleValue, scaleValue, scaleValue);

            if (!(amount > 0) && amount > 250)
            {
                response = "Ungültiger Amount, er muss positiv sein und maximal 250!\n Benutzung: spawnitems (RemoteAdmin-Id) (ItemId) (Scale) (Amount)";
                return false;
            }

            for (int i = 0; i < amount; i++)
            {
                Pickup p = Pickup.CreateAndSpawn(type, position, Quaternion.identity, target);
                p.Scale = scale;
            }

            response = "Item" + (amount > 1 ? "s" : "") + " gespawnt!";

            return true;
        }
    }
}