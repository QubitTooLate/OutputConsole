using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace OutputConsole.Logic
{
    class ColorRegister
    {
        /*static void UpdateRegistrySettings()
        {
            string directory = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
            directory = @"Console\" + directory.Replace(@"\", "_");
            RegistryKey key = Registry.CurrentUser.OpenSubKey(directory, true);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(directory);
                SetRegistry(key);
                Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
            else
            {
                SetRegistry(key);
            }
        }

        static void SetRegistry(RegistryKey key)
        {
            key.SetValue("FontSize", 0x00070003);

            key.SetValue("ColorTable00", 0x00000000);
            key.SetValue("ColorTable01", 0x00ffffff);

            key.SetValue("ColorTable02", 0x000000FF);
            key.SetValue("ColorTable03", 0x000076FF);
            key.SetValue("ColorTable04", 0x0000EBFF);
            key.SetValue("ColorTable05", 0x0000FF9D);
            key.SetValue("ColorTable06", 0x0000FF27);
            key.SetValue("ColorTable07", 0x004EFF00);
            key.SetValue("ColorTable08", 0x00C4FF00);
            key.SetValue("ColorTable09", 0x00FFC400);
            key.SetValue("ColorTable10", 0x00FF4E00);
            key.SetValue("ColorTable11", 0x00FF0027);
            key.SetValue("ColorTable12", 0x00FF009D);
            key.SetValue("ColorTable13", 0x00EB00FF);
            key.SetValue("ColorTable14", 0x007600FF);
            key.SetValue("ColorTable15", 0x000400FF);

            key.Close();
        }*/
    }
}
