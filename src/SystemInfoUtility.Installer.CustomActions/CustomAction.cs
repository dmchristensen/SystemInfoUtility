using Microsoft.Deployment.WindowsInstaller;
using System;
using System.IO;

namespace SystemInfoUtility.Installer.CustomActions
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult WriteServerAddressToFile(Session session)
        {
            session.Log($"{DateTime.Now}: Begin WriteServerAddressToFile");

            session.Log($"{DateTime.Now}: Current Directory is {Environment.CurrentDirectory}");

            var serverAddress = session["SERVER_ADDRESS"];

            session.Log($"{DateTime.Now}: Writing text file with server address to {serverAddress}");

            File.WriteAllText("wix_ca.txt", serverAddress);

            session.Log($"{DateTime.Now}: End WriteServerAddressToFile");

            return ActionResult.Success;
        }
    }
}
