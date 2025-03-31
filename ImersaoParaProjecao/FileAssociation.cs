using Microsoft.Win32;
using System.Security.AccessControl;

namespace ImersaoParaProjecao
{
    public static class FileAssociation
    {
        public static void Set(string extension, string keyName, string openWith, string fileDescription)
        {
            var openWithFileName = new FileInfo(openWith).Name;

            Environment.SetEnvironmentVariable(openWithFileName, openWith);

            var keyBase = Registry.ClassesRoot.CreateSubKey(extension)
                                              .CreateSubKey("OpenWithList")
                                              .CreateSubKey(openWithFileName);

            var openWithFull = $"\"{openWith}\"";

            var openMethod = Registry.ClassesRoot.CreateSubKey(keyName);
            openMethod.SetValue("", fileDescription);
            openMethod.CreateSubKey("DefaultIcon")
                      .SetValue("", $"{openWithFull},0");

            var shell = openMethod.CreateSubKey("Shell");
            
            shell.CreateSubKey("edit")
                 .CreateSubKey("command")
                 .SetValue("", $"{openWithFull} \"%1\"");

            shell.CreateSubKey("open")
                 .CreateSubKey("command")
                 .SetValue("", $"{openWithFull} \"%1\"");
            
            keyBase.Close();
            openMethod.Close();
            shell.Close();

            var currentUser = Registry.CurrentUser.CreateSubKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.pdf")
                                                  .OpenSubKey("OpenWithProgids", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
            currentUser?.SetValue("Progid", keyName, RegistryValueKind.String);
            currentUser?.Close();
        }
    }
}