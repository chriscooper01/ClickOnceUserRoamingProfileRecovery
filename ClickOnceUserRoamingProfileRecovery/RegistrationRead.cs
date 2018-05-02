using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class RegistrationRead
    {
        //Computer\HKEY_CURRENT_USER\
        private const string MAINREGISTRATIONLOCATION = @"Software\Classes\Software\Microsoft\Windows\CurrentVersion\Deployment\SideBySide";
        private static List<string> requiredRegistrationElements = new List<string>()
        {
            @"2.0"
            
        };
        //Main elements would be
        //    @"Components",
        //    @"Marks",
        //    @"PackageMetadata",
        //    @"StateManager",
        //    @"Applications",
        //    @"Families",
        //    @"Visibility"
        
            

        public static void GetKeysValues()
        {
            List<RegistrationElementDataClass> registrationData = getRemoveUserData();

            using (Microsoft.Win32.RegistryKey registrationElement = Registry.CurrentUser.OpenSubKey(MAINREGISTRATIONLOCATION))
            {
                if (registrationElement.SubKeyCount > 0)
                {
                    foreach (string registrationLocation in requiredRegistrationElements)
                    {

                        Microsoft.Win32.RegistryKey registrationSubKey = registrationElement.OpenSubKey(registrationLocation);
                        if (registrationSubKey != null)
                        {
                            registrationData.Add(new RegistrationElementDataClass()
                            {
                                RegistrationElementPath = registrationSubKey.Name,
                                ValueName = String.Empty,
                                ValueString = String.Empty,
                                Level = 0,
                                KeyName = registrationLocation,
                                UserName = Environment.UserName
                            });
                        }
                        getKeyElement(registrationSubKey, registrationLocation, 0, ref registrationData);
                        

                       

                    }//END foreach(string registrationLocation in requiredRegistrationElements)

                }//if (registrationElement.SubKeyCount > 0)
                

            }



            XmlFunctions.SetRegistrationData(registrationData);
        }


        private static List<RegistrationElementDataClass> getKeyElement(Microsoft.Win32.RegistryKey registrationElement, string subKeyName, int level , ref List<RegistrationElementDataClass>  registrationData)
        {
            

           if (registrationElement.ValueCount > 0)
                { 

                    List<RegistrationElementDataClass> registrationValue = getValue(registrationElement, subKeyName, level);
                registrationData.AddRange(registrationValue);

            }

            if (registrationElement.SubKeyCount > 0)
            {
                int subLevel = level + 1;
                List<string> names = registrationElement.GetSubKeyNames().ToList();
                foreach (string name in names)
                {
                    Microsoft.Win32.RegistryKey registrationSubKey = registrationElement.OpenSubKey(name);
                    if (registrationElement.ValueCount > 0)
                    {

                        List<RegistrationElementDataClass> registrationValue = getValue(registrationSubKey, subKeyName, subLevel);
                        registrationData.AddRange(registrationValue);

                    }
                    if (registrationSubKey.SubKeyCount > 0)
                    {
                       
                        List<string> subKeyNames = registrationSubKey.GetSubKeyNames().ToList();
                        int subSubLevel = subLevel + 1;
                        foreach (string subsubKeyName in subKeyNames)
                        {                         
                            getKeyElement(registrationSubKey.OpenSubKey(subsubKeyName), subKeyName, subSubLevel, ref registrationData);
                            
                        }//END foreach (string subKeyName in subKeyNames)
                    }
                   
                   

                }//END foreach(string name in names)
            }return registrationData;
          
        }

        private static List<RegistrationElementDataClass> getValue(Microsoft.Win32.RegistryKey registrationSubKey, string subKeyName, int level)
        {
            List<RegistrationElementDataClass> registrationData = new List<RegistrationElementDataClass>();
            
            List<string> valueNames = registrationSubKey.GetValueNames().ToList();

            if (valueNames.Count > 0)
            {
                foreach (string valueName in valueNames)
                {
                    RegistryValueKind typeOfValue = RegistryValueKind.None;                   
                    object registrationValue = registrationSubKey.GetValue(valueName);
                    string valueStirng =  getStringValue(registrationSubKey, registrationValue, valueName, ref typeOfValue);


                    registrationData.Add(new RegistrationElementDataClass()
                    {
                        RegistrationElementPath = registrationSubKey.Name,
                        ValueName = valueName,
                        ValueObject = registrationValue,
                        RegistryValueType = typeOfValue,
                        ValueString = valueStirng,
                        Level = level,
                        KeyName = subKeyName,
                        UserName = Environment.UserName,
                    });


                }//END foreach (string valueName in valueNames)
            }
          


            return registrationData;
        }
        private static string getStringValue(Microsoft.Win32.RegistryKey registrationSubKey, object registrationValue,  string valueName, ref RegistryValueKind typeOfValue)
        {
            string valueStirng = String.Empty;            
            typeOfValue = registrationSubKey.GetValueKind(valueName);

            try
            {
                switch (typeOfValue)
                {
                    case RegistryValueKind.Binary:
                        valueStirng = Convert.ToBase64String((byte[])registrationValue);
                        break;
                    case RegistryValueKind.String:
                        valueStirng = registrationValue.ToString();
                        break;

                }
            }
            catch
            {

            }

            return valueStirng;
        }

       private static List<RegistrationElementDataClass> getRemoveUserData()
       {
            List<RegistrationElementDataClass> registrationData = XmlFunctions.GetRegistrationData();
            if (registrationData == null)
                registrationData = new List<RegistrationElementDataClass>(); 
            //Get current user data and remove if present
            List<RegistrationElementDataClass> userRegistrationData = registrationData.Where(x => x.UserName.Equals(Environment.UserName)).ToList();

            foreach(RegistrationElementDataClass record in userRegistrationData)
                registrationData.Remove(record);


            return registrationData;
       }

    }
}
