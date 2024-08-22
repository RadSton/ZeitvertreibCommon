namespace vip.zeitvertreib
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    using Utf8Json;

    using static vip.zeitvertreib.ZeitvertreibCommon;


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PrintFake500Config : ICommand
    {
        public string Command { get; } = "printfake500config";
        public string Description { get; } = "Prints custom parsed Fake500 config";
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

            string customParsed = JsonSerializer.ToJsonString(DeathEventConfig);
            string generic = JsonSerializer.ToJsonString(Instance.Config);

            response = "Custom parsed Fake500 Config: \n\n" + customParsed + "\n\nGeneric Config: \n\n" + generic;

            return true;
        }
    }
}