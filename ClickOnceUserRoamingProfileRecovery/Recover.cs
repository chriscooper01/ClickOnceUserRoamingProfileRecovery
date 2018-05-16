using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class Recover : FilesAndFolders
    {
         private const string USERFOLDERLOCATION = @"C:\Users";
        private const string APPLOCATION = @"AppData\Local\Apps\2.0";
        private const int FIRSTELEMENTMIN = 11;
        private const int FIRSTELEMENTSTART = 0;
        private const int SECONDELEMENTMIN = 2;
        private const int SECONDELEMENTSTART = 11;
        
        
        private const string RANDOMSIDEBYSIDERELOCATION = @"Software\Classes\Software\Microsoft\Windows\CurrentVersion\Deployment\SideBySide\2.0";
        private const string RANDOMSIDEBYSIDEVALUE = "ComponentStore_RandomString";

        public static void Run(ApplicationDataClass appData, CallBackDelegate callBack, StatusDelegate status)
        {
            Recover process = new Recover();
            process.Status = status;
            process.CallBack = callBack;
            process.AppData = appData;
            Thread th = new Thread(new ThreadStart(process.RunProcess));
           
            th.Name = "Click Once Recovery";
            th.IsBackground = true;
            th.Start();
        }

        

        public void RunProcess()
        {
            if (!Directory.Exists(AppData.ClickOnceLocation))
            {
                RegistrationWrite.SetRegistrationElemnents();
                AppData.ClickOnceLocation = setClickOnceFolderLocation();
            }
            AppData.ClickOnceLocation = setClickOnceFolderLocation();
            
            string requiredFolder = Path.Combine(AppData.ClickOnceLocation, AppData.CurrentFolder);
            if (!Directory.Exists(requiredFolder))
            {
                Status(ProcessElementENUM.Element, "", 2);
                Status(ProcessElementENUM.Element, "Health Options binaries", -1);
                Copy(AppData.ClickOnceLocation, AppData.RecoveryLocation, false);

            }
            Status(ProcessElementENUM.Element, "", 4);
            Status(ProcessElementENUM.Element, "Open Health Options", -1);
            DirectoryInfo info = new DirectoryInfo(AppData.RecoveryLocation);

            

            var file = info.Parent.GetFiles().FirstOrDefault(x => x.Extension.Equals(".appref-ms"));
            if(file !=null)
            {
                System.Diagnostics.Process.Start(file.FullName);
            }



            CallBack();
        }



        /// <summary>
        /// This will get the latest user folder that starts with the current enviroment user name
        /// </summary>
        /// <returns></returns>
        private string getLatestUsersFolder()
        {
            DirectoryInfo info = new DirectoryInfo(USERFOLDERLOCATION);
            var directory = info.GetDirectories().OrderByDescending(x=>x.LastWriteTime).FirstOrDefault(x => x.Name.StartsWith(Environment.UserName.ToString(),StringComparison.InvariantCultureIgnoreCase));
            if (directory != null)
                return directory.FullName;

            return String.Empty;
        }

        /// <summary>
        /// This get a string value from the required registration location and then it will use a sub method to take
        /// required folder and sub folder if the key is long enough.
        /// </summary>
        /// <returns></returns>
        private List<string> getRandomInstallFolderNames()
        {


            using (Microsoft.Win32.RegistryKey uninstallKey = Registry.CurrentUser.OpenSubKey(RANDOMSIDEBYSIDERELOCATION))
            {
                List<string> fileName = new List<string>();
                string randomFolderName = String.Empty;
                if (uninstallKey != null)
                {
                    var keyValue = uninstallKey.GetValue(RANDOMSIDEBYSIDEVALUE);
                   
                    fileName.Add(getRandomInstallFolderName(keyValue.ToString(), FIRSTELEMENTSTART, FIRSTELEMENTMIN));
                    fileName.Add(getRandomInstallFolderName(keyValue.ToString(), SECONDELEMENTSTART, SECONDELEMENTMIN));

                }
                return fileName.Where(x=>!x.Equals(String.Empty)).ToList();
            }
        }
        /// <summary>
        /// This will take the 24 length string from the registration key and take the elements required
        /// to use as a folder name
        /// </summary>
        /// <param name="registrationValue"></param>
        /// <param name="startPoint"></param>
        /// <param name="minsize"></param>
        /// <returns></returns>
        private string getRandomInstallFolderName(string registrationValue, int startPoint, int minsize)
        {
            string randomFolderName = String.Empty;
            if (!String.IsNullOrWhiteSpace(registrationValue) && registrationValue.Length > minsize) 
            {
                randomFolderName = registrationValue;
                randomFolderName = String.Format("{0}.{1}", randomFolderName.Substring(startPoint, 8), randomFolderName.Substring((startPoint+8), 3));
            }
                
            return randomFolderName;
            
        }

        /// <summary>
        /// This wil l set the Click once folder location string, but it will check if the new random folders exists
        /// if not it will create them ready
        /// </summary>
        /// <returns></returns>
        private string setClickOnceFolderLocation()
        {
            string location = String.Empty;

            string userFolderLocation = getLatestUsersFolder();
            List<string> randomInstallname = getRandomInstallFolderNames();
            
            //Combine the user and the application location
            location = Path.Combine(userFolderLocation,APPLOCATION);
            //Combine the location with the random folder names
            foreach(string randomElement in randomInstallname)
            {

                location = Path.Combine(location, randomElement);
                if (!Directory.Exists(location))
                    Directory.CreateDirectory(location);

            }//END foreach(string randomElement in randomInstallname)
            

            return location;

        }

      
    }
}
