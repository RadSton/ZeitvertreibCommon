namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PlayerSize : ICommand
    {
        public string Command { get; } = "playersize";
        public string Description { get; } = "Changes size of a player";
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
                response = "Benutzung:\nplayersize (RemoteAdmin-Id) (x) (y) (z)";
                return false;
            }

            Player target = RAUserIdParser.getByCommandArgument(arguments.At(0));

            if (target == null) {
                response = "Ungültiger Spieler";
                return false;
            }

            if (!float.TryParse(arguments.At(1), out float x) || !float.TryParse(arguments.At(2), out float y) || !float.TryParse(arguments.At(3), out float z))
            {
                response = "Ungültige Größe\n Benutzung: playersize (RemoteAdmin-Id) (x) (y) (z)";
                return false;
            }
            target.Scale = new(x, y, z);

            response = "Der Spieler " + target.DisplayNickname + " ist nun " + target.Scale.ToString() + " groß!";

            return true;
        }
    }
}