using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClickOnceRecovery
{

    public class XmlFunctions
    {

        private const string REGISTRATIONDATAFILENAME = "registrationData.hodat";
        public static List<RegistrationElementDataClass> GetRegistrationData()
        {
            string path = Path.Combine(Application.StartupPath, REGISTRATIONDATAFILENAME);
            if (!File.Exists(path))
                return null;


            string xmlContent = File.ReadAllText(path);
            xmlContent = xmlContent.Replace("&#x0;", "\\0");


            List<RegistrationElementDataClass>  dataObject = new List<RegistrationElementDataClass>();
            dataObject = (List<RegistrationElementDataClass>)ObjectFromXMLString(dataObject.GetType(), xmlContent);
            


            return dataObject;
        }



        public static void SetRegistrationData(List<RegistrationElementDataClass> dataObject)
        {
            string path = Path.Combine(Application.StartupPath, REGISTRATIONDATAFILENAME);
            string content = ToXMLString(dataObject);
            if (File.Exists(path)) 
                File.Delete(path);


            File.WriteAllText(path, content);

        }



        public static string ToXMLString(object item)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(item.GetType());
            serializer.Serialize(stringwriter, item);
            return stringwriter.ToString();
        }



        public static object ObjectFromXMLString(Type typeOfObject, string xmlFile)
        {
            try
            {
                if (String.IsNullOrEmpty(xmlFile))
                    return Activator.CreateInstance(typeOfObject);

                XmlSerializer serializer = new XmlSerializer(typeOfObject);
                StringReader rdr = new StringReader(xmlFile);
                return serializer.Deserialize(rdr);
            }
            catch (Exception e)
            {
                //Something went wrong.  
                //The parent method will need to be able to handle a null object return
            }

            return null;


        }
    }
}
