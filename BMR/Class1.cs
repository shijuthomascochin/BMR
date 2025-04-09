using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMR
{
    class class1
    {
        public static string g_proxyDomain = "", g_proxyServer = "", g_proxyPassword = "", g_proxyUser = "", g_GMServerURL = "", g_GMUserName = "", g_GMUserPass = "";
        public static string g_AgentFileName = "";
        public static int g_proxyPort;
        public static bool g_UseProxy, g_SaveGMPass;
        private static System.Net.WebProxy g_WebProxy;



        #region "Read/Write Configuration"

        /// <summary>
        ///  The default config
        /// </summary>
        public static void SetDefaultConfig()
        {
            class1.g_UseProxy = false;
            class1.g_proxyServer = "10.0.0.1";
            class1.g_proxyPort = 8080;
            class1.g_proxyUser = "";
            class1.g_proxyPassword = "";
            class1.g_proxyDomain = "";
            class1.g_GMUserName = "";
            class1.g_GMUserPass = "";
            class1.g_SaveGMPass = false;
            class1.g_GMServerURL = @"https://mail.google.com/mail/feed/atom";
        }

        /// <summary>
        /// Save the configuration
        /// </summary>
        /// <returns></returns>
        public static bool WriteOptions()
        {
            System.IO.FileStream myWriter = null;
            string[] vals = new string[15];

            try
            {

                //Not using the config parameters for the URL
                class1.g_GMServerURL = @"https://mail.google.com/mail/feed/atom";

                //IMP: Do not Change Sequence of the Parameters
                vals[0] = Convert.ToString(class1.g_UseProxy);
                vals[1] = (class1.g_proxyServer == null) ? "" : class1.g_proxyServer;
                vals[2] = Convert.ToString(class1.g_proxyPort);
                vals[3] = class1.g_proxyUser.Trim();
                vals[4] = class1.g_proxyPassword.Trim();
                vals[5] = class1.g_proxyDomain.Trim();
                vals[6] = basicEncrypt(class1.g_GMUserName.Trim());
                vals[7] = basicEncrypt(class1.g_GMUserPass.Trim());
                vals[8] = Convert.ToString(class1.g_SaveGMPass);
                vals[9] = class1.g_GMServerURL.Trim();
                vals[10] = class1.g_AgentFileName.Trim();
                myWriter = System.IO.File.OpenWrite(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\vals.conf");
                byte[] bytdata;
                string data = "";
                int i;

                for (i = 0; i < 15; i++)
                {
                    data += vals[i] + Convert.ToString(Convert.ToChar(13));
                }
                data = data.Substring(0, data.Length - 2);

                bytdata = System.Text.Encoding.UTF8.GetBytes(data);
                myWriter.Write(bytdata, 0, bytdata.Length);
                myWriter.Flush();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                //MessageBox.Show("Errors Occured while writing configuration ", "GMReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (!(myWriter == null))
                {
                    myWriter.Close();
                }
            }

        }

        /// <summary>
        /// Read the saved configuration
        /// </summary>
        /// <returns></returns>
        public static bool ReadOptions()
        {
            System.IO.FileStream reader = null;


            try
            {
                //Create a file if it does not exist. Create it with the default parameters.
                if (!System.IO.File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\vals.conf"))
                {
                    class1.g_AgentFileName = "merlin.acs";
                    WriteOptions();
                }
            }
            catch
            { }

            try
            {
                reader = System.IO.File.OpenRead(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\vals.conf");
                string readData;
                byte[] bytData = null;
                string[] vals = new string[15];

                bytData = new byte[reader.Length];

                reader.Read(bytData, 0, bytData.Length);
                readData = System.Text.Encoding.UTF8.GetString(bytData);

                vals = readData.Split(Convert.ToChar(13));
                //IMP: Do not Change Sequence of the Parameters
                class1.g_UseProxy = Convert.ToBoolean(vals[0]);
                class1.g_proxyServer = vals[1];
                class1.g_proxyPort = Convert.ToInt32(vals[2]);
                class1.g_proxyUser = vals[3];
                class1.g_proxyPassword = vals[4];
                class1.g_proxyDomain = vals[5];
                class1.g_GMUserName = basicDecrypt(vals[6]);
                class1.g_GMUserPass = basicDecrypt(vals[7]);
                class1.g_SaveGMPass = Convert.ToBoolean(vals[8]);
                class1.g_GMServerURL = vals[9];
                class1.g_AgentFileName = vals[10];


                //Not using the config parameters for the URL
                class1.g_GMServerURL = @"https://mail.google.com/mail/feed/atom";

                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
               // MessageBox.Show("Errors Occured while reading configuration. Some configuration will be changed ", "GMReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!(reader == null))
                {
                    reader.Close();
                }

                WriteOptions();
                return false;
            }
            finally
            {
                if (!(reader == null))
                {
                    reader.Close();
                }
            }
        }

        #endregion

        public class1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Application start
        /// </summary>
        /// 
        //[STAThread]
        //public static void Main()
        //{
        //    frmMain f = new frmMain();
        //    f.Show();
        //    Application.Run(f);
        //}

        /// <summary>
        /// Returns a new proxy object according to the proxy variables.
        /// </summary>
        /// <returns></returns>
        public static System.Net.WebProxy GetProxy()
        {
            if (g_WebProxy == null)
            {
                try
                {
                    System.Net.WebProxy myProxy = new System.Net.WebProxy(g_proxyServer, g_proxyPort);
                    myProxy.Credentials = new System.Net.NetworkCredential(g_proxyUser, g_proxyPassword, g_proxyDomain);
                    myProxy.BypassProxyOnLocal = true;
                    g_WebProxy = myProxy;
                    //System.Net.GlobalProxySelection.Select = myProxy;
                }
                catch
                {
                    //g_WebProxy=System.Net.GlobalProxySelection.GetEmptyWebProxy();
                }
            }

            return g_WebProxy;
        }

        /// <summary>
        /// Basic encryption. Converts to Base64 string
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        private static string basicEncrypt(string inVal)
        {
            string data;
            byte[] bytData = null;
            bytData = System.Text.Encoding.UTF8.GetBytes(inVal);
            data = Convert.ToBase64String(bytData);
            return data.Trim();
        }

        /// <summary>
        /// Basic decryption. Converts from Base64 string
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        private static string basicDecrypt(string inVal)
        {
            byte[] bytData = null;
            bytData = new byte[inVal.Length + 2];

            bytData = Convert.FromBase64String(inVal);
            inVal = System.Text.Encoding.UTF8.GetString(bytData);
            return inVal.Trim();
        }
    }
}

   

