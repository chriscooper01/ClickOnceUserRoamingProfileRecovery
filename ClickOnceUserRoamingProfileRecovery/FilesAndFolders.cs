using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class FilesAndFolders
    {
        protected ClickOnceRecovery.CallBackDelegate CallBack;
        protected ClickOnceRecovery.StatusDelegate Status;
        protected ApplicationDataClass AppData;


        protected void Copy(string destination, string source, bool addDeploy)
        {
            
            DirectoryInfo info = new DirectoryInfo(source);
            if (!Directory.Exists(destination))
            {            
                Directory.CreateDirectory(destination);
            }

            foreach (var childDirectoy in info.GetDirectories())
            {
             
                string destinationChild = Path.Combine(destination, childDirectoy.Name);
                Copy(destinationChild, childDirectoy.FullName, addDeploy);
            }
            var files = info.GetFiles();
            Status(ProcessElementENUM.Files, "", files.Count());
            foreach (var file in files)
            {

                string destiniationName = Path.Combine(destination, file.Name);
                if (addDeploy)
                    destiniationName += ".deploy";
                else
                    destiniationName = destiniationName.Replace(".deploy", String.Empty);

                string sourceName = file.FullName;

                Status(ProcessElementENUM.Files, file.Name, -1);
                if (File.Exists(destiniationName))
                    File.Delete(destiniationName);

                    File.Copy(sourceName, destiniationName);


                File.SetCreationTime(destiniationName, DateTime.Now);

            }

        }
    }
}
