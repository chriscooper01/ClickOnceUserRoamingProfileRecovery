using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class RegistrationWrite
    {
        private const string MAINREGISTRATIONLOCATION = @"Software\Classes\Software\Microsoft\Windows\CurrentVersion\Deployment\SideBySide\2.0";
        private static string openSub = String.Empty;
        public static void SetRegistrationElemnents()
        {
            List<RegistrationElementDataClass> registrationData = XmlFunctions.GetRegistrationData();
            registrationData = registrationData.Where(x => x.UserName.Equals(Environment.UserName)).ToList();

            var registrationDataGroupped = registrationData.OrderBy(x => x.Level).GroupBy(g => g.Level).ToList();
            foreach (var registrationDataGrouppedElement in registrationDataGroupped)
            {
                foreach (var registrationDataElement in registrationDataGrouppedElement.ToList())
                {
                    string registrationPath = registrationDataElement.RegistrationElementPath.Replace(@"HKEY_CURRENT_USER\", String.Empty);
                    Microsoft.Win32.RegistryKey registrationElement = Registry.CurrentUser.OpenSubKey(registrationPath, true);                    
                    if (registrationElement == null)
                    {
                        Registry.CurrentUser.CreateSubKey(registrationPath);
                        registrationElement = Registry.CurrentUser.OpenSubKey(registrationPath, true);
                    }

                    if (!String.IsNullOrWhiteSpace(registrationDataElement.ValueName))
                        registrationElement.SetValue(registrationDataElement.ValueName, registrationDataElement.ValueObject);


                }//END using (Microsoft.Win32.RegistryKey registrationElement = Registry.CurrentUser.OpenSubKey(MAINREGISTRATIONLOCATION,true))



            }//END foreach(var registrationDataElement in registrationDataGrouppedElement.ToList())

                    



        }

    }
}
