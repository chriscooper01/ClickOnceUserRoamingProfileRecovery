using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickOnceRecovery
{
    public delegate void CallBackDelegate();
    public delegate void StatusDelegate(ProcessElementENUM element,string message, int total);
    public partial class RecoveryForm : Form
    {

        private string process;
        public RecoveryForm(string arg)
        {

            process = arg.Trim().ToLower();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           //

            

            lblProcessElement.Text = String.Empty;
            lblProcessFolder.Text = String.Empty;
            lblProcessFile.Text = String.Empty;
            lblProgessTitle.Text = String.Empty;
            ApplicationDataClass dataObject = getDataObject();
            if (dataObject == null)
            {
                MessageBox.Show(this, "No configration data found.\r\nPlease contact Health Diagnostics IT support team", "Configuration missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                if (process.Equals("backup"))
                {
                    lblProgessTitle.Text = "Health Options binary backup";
                    
                    Backup.Run(dataObject, new CallBackDelegate(CallBack), new StatusDelegate(Status));
                }
                else
                {

                    Recover.Run(dataObject, new CallBackDelegate(CallBack), new StatusDelegate(Status));
                }//END if (process.Equals("backup"))


            }//END if(dataObject==null)


        }

        private ApplicationDataClass getDataObject()
        {
            string path = Path.Combine(Application.StartupPath, "applicationdata.hodat");
            if (!File.Exists(path))
                return null;


            string xmlContent = File.ReadAllText(path);
            List<ApplicationDataClass> dataObject = new List<ApplicationDataClass>();
            dataObject = (List<ApplicationDataClass>)XmlFunctions.ObjectFromXMLString(dataObject.GetType(), xmlContent);
            var userRecord = dataObject.FirstOrDefault(x => x.Username.Equals(Environment.UserName.Trim()));
            return userRecord;
        }

        public void CallBack()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {

                CallBackDelegate d = new CallBackDelegate(CallBack);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Close();
            }
        }
        public void Status(ProcessElementENUM element, string message, int total)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {

                StatusDelegate d = new StatusDelegate(Status);
                this.Invoke(d, new object[] { element, message, total });
            }
            else
            {
                switch(element)
                {
                    case ProcessElementENUM.Element:
                        setFileProgress(prbElementMain, lblProcessElement, message, total);                        
                        break;
                    case ProcessElementENUM.Files:
                        setFileProgress(prbFileMain, lblProcessFile, message, total);
                        
                        break;
                    case ProcessElementENUM.Folder:
                        setFileProgress(prbFolderMain, lblProcessFolder, message, total);
                        
                        break;
                }
                

            }
        }
        
        private void setFileProgress(ProgressBar progressBar, Label processLabel, string message, int total)
        {
            

            if (prbFileMain == null)
            {
                progressBar = new ProgressBar();// prb;
                progressBar.Style = ProgressBarStyle.Continuous;
            }

            if (!String.IsNullOrWhiteSpace(message))
            {
                processLabel.Text = message;

            }
            if (total > 0)
            {
                progressBar.Maximum = total;
                progressBar.Value = 0;
                progressBar.Step = 0;
                progressBar.Refresh();
            }
            {
                if (progressBar.Maximum.Equals(0) || progressBar.Maximum.Equals(progressBar.Value))
                    progressBar.Maximum = (progressBar.Maximum + 4);

                progressBar.Value++;
            }
            Application.DoEvents();
        }
    }
}
