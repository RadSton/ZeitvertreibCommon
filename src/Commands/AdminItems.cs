namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.API.Features.Pools;
    using Exiled.API.Features.Pickups;
    using Exiled.Permissions.Extensions;


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdminItems : ParentCommand
    {

        public AdminItems() => LoadGeneratedCommands();

        public override string Command { get; } = "adminitems";
        public override string Description { get; } = "Gives CustomItems to a admin";
        public override string[] Aliases { get; } = new string[] { "ai", "admini" };

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Give());
            RegisterCommand(new Show());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!sender.CheckPermission("zeitvertreib.team"))
            {
                response = "You don't have the permissions to execute this command.";
                return false;
            }

            StringBuilder stringBuilder = StringBuilderPool.Pool.Get();
            stringBuilder.AppendLine("Version: " + ZeitvertreibCommon.VERSION + "\nAvailable commands: ");
            stringBuilder.AppendLine("- adminitems SHOW - Zeigt alle Admin-Items an.");
            stringBuilder.AppendLine("- adminitems GIVE [RemoteAdminId] [ItemId] - Gibt dem User ein Admin-Item an.");

            response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
            return false;
        }
    }
}