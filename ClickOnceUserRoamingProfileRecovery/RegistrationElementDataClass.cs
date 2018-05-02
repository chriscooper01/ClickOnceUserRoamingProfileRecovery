using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class RegistrationElementDataClass
    {
        public string UserName { get; set; }
        public string RegistrationElementPath { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public object ValueObject { get; set; }
        public string ValueString { get; set; }
        
        public int Level { get; set; }
        public Microsoft.Win32.RegistryValueKind RegistryValueType { get; set; }
    }
}
