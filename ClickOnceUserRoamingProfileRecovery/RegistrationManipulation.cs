using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class RegistrationManipulation
    {

        public static void ExportRegistration(string recoveryLocation)
        {

            DirectoryInfo info = new DirectoryInfo(recoveryLocation);
            string registrationDirectory = Path.Combine(info.Parent.FullName, "Registration");
            if (!Directory.Exists(registrationDirectory))
                Directory.CreateDirectory(registrationDirectory);


            
            RegistrationManipulation.export(Path.Combine(registrationDirectory, "CurrentVersions.reg"), @"HKEY_CURRENT_USER\Software\Classes\Software\Microsoft\Windows\CurrentVersion");
            RegistrationManipulation.export(Path.Combine(registrationDirectory, "UnInstall.reg"), @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\d12366065cf17eb6");


        }

        public static void ImportRegistration(string recoveryLocation)
        {

            DirectoryInfo info = new DirectoryInfo(recoveryLocation);
            string registrationDirectory = Path.Combine(info.Parent.FullName, "Registration");
            if (Directory.Exists(registrationDirectory))
            {
                

                RegistrationManipulation.import(Path.Combine(registrationDirectory, "CurrentVersions.reg"));
                RegistrationManipulation.import(Path.Combine(registrationDirectory, "UnInstall.reg"));

            }
                

            

        }
        private static void export(string exportPath, string registryPath)
        {
            string path = "\"" + exportPath + "\"";
            string key = "\"" + registryPath + "\"";
            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;

                proc = Process.Start("regedit.exe", "/e " + path + " " + key);
                proc.WaitForExit();
            }
            catch (Exception)
            {
                proc.Dispose();
            }
        }


        public static void import(string importFile)
        {

            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;

                proc = Process.Start("regedit.exe", "/s " + importFile);
                proc.WaitForExit();
            }
            catch (Exception)
            {
                proc.Dispose();
            }
        }
    }
}
