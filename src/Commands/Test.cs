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
    using UnityEngine;

    using static vip.zeitvertreib.ZeitvertreibCommon;


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Test : ICommand
    {
        public string Command { get; } = "dumb";
        public string Description { get; } = "Prints custom parsed Fake500 config";
        public string[] Aliases { get; } = new string[] { };

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            Vector3 pos = player.Position;

            //GameObject room = Room.Get(pos).GameObject;

            //iterate(room);

            if (arguments.At(0) == null)
            {
                response = "inkompetentestes wesen";
                return true;
            }

            Debug.Log(": NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values  NetworkClient.prefabs.Values");

            GameObject found = null;

            foreach (var spawnable in Mirror.NetworkClient.prefabs.Values)
            {
                if (spawnable.name.StartsWith(arguments.At(0)))
                {
                    found = spawnable;
                    break;
                }
            }

            if (found == null)
            {
                response = "inkompetent";
                return true;
            }

            for (int i = 0; i < 100; i++)
            {
              //  pos.y = pos.y + 0.05f;
                GameObject obj = UnityEngine.Object.Instantiate(found, pos, Quaternion.Euler(player.Rotation.eulerAngles));
                Mirror.NetworkServer.Spawn(obj);
            }
           // GameObject obj = UnityEngine.Object.Instantiate(found, pos, Quaternion.Euler(player.Rotation.eulerAngles));
        //    Mirror.NetworkServer.Spawn(obj);

            response = "Player.pos: " + player.Position + " GameObject.pos: " + player.GameObject.transform.position;
            return true;
        }


        public void iterate(GameObject start, string path = "/")
        {
            foreach (Transform childTransfrom in start.transform)
            {
                GameObject obj = childTransfrom.gameObject;

                Debug.Log(":" + path + " -> " + obj.name);
                Component[] components = obj.GetComponents(typeof(Component));

                foreach (Component component in components)
                    Debug.Log(" - " + component.ToString());

                iterate(obj, path + obj.name + "/");
            }
        }
    }
}