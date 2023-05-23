using System;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Data;
using DatabaseClassLibrary;

namespace CarService
{
    static class Program
    {
        public const string ProgramID = "MS101";  // input 0 with out check authen...
        public const string ProgramName = "Program Name";
        public const string Description = "ชื่อโปรแกรม";
        public const string Module = "MS";
        public const string CustomizedBy = "Passakorn";
        public static DateTime DateCreated = DateTime.ParseExact("21/06/2016", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        public static DateTime DateModified = DateTime.ParseExact("21/06/2016", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        public const string Version = "2.0";
        public const string Release = "1.0";

        public static Process prcs;

        public static string AppPath = string.Empty;
        public static string ExePath = string.Empty;
        public static string ReportPath = string.Empty;
        public static string LoginID = string.Empty;
        public static string LoginName = string.Empty;
        public static string CompanyID = string.Empty;
        public static string CompanyName = string.Empty;
        public static string UserGroup = string.Empty;

        public static bool AllowInsert;
        public static bool AllowEdit;
        public static bool AllowDelete;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            prcs = Process.GetCurrentProcess();

            AppPath = Application.StartupPath;
            int s = AppPath.IndexOf("\\bin");

            if (s != -1)
            {
                AppPath = AppPath.Substring(0, s);
            }

            if (checkProcessRunning(ProgramID) == false)
            {
                prcs.Kill();
                return;
            }

            if (checkFormatDate() == false)
            {
                prcs.Kill();
                return;
            }

            if (checkUserLogin() == false)
            {
                prcs.Kill();
                return;
            }

            getSetting();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        static bool checkUserLogin()
        {
            IniFile ini = new IniFile();
            string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\CarServiceLogin.ini";
            try
            {
                LoginID = ini.Read(filePath, "main", "Username");
                CompanyID = ini.Read(filePath, "main", "CompanyID");

                Database.Connect();

                string strSQL;
                strSQL = " SELECT Employee.EmployeeID,Employee.EmployeeName,Employee.EmployeeSurname,[User].UserName,[User].CompanyID,[User].UserGroupID,AllowInsert,AllowEdit,AllowDelete ";
                strSQL += "   FROM Employee ";
                strSQL += " INNER JOIN [User] ON Employee.EmployeeID = [User].EmployeeID ";
                strSQL += " INNER JOIN UserAuthen ON [User].UserGroupID = UserAuthen.UserGroupID ";
                strSQL += "  WHERE [User].UserName = '" + LoginID + "' ";

                if (ProgramID != "0")
                {
                    strSQL += "  AND UserAuthen.ProgramID = '" + ProgramID + "' ";
                }

                DataTable dt = Database.GetData(strSQL);

                if (dt.Rows.Count > 0)
                {
                    Program.LoginName = dt.Rows[0]["EmployeeName"].ToString() + " " + dt.Rows[0]["EmployeeSurname"].ToString();
                    //Program.CompanyID = dt.Rows[0]["CompanyID"].ToString();
                    Program.UserGroup = dt.Rows[0]["UserGroupID"].ToString();

                    Program.AllowInsert = (bool)dt.Rows[0]["AllowInsert"];
                    Program.AllowEdit = (bool)dt.Rows[0]["AllowEdit"];
                    Program.AllowDelete = (bool)dt.Rows[0]["AllowDelete"];

                    strSQL = " SELECT * FROM Company ";
                    strSQL += " WHERE CompanyID = '" + CompanyID + "' ";
                    dt = Database.GetData(strSQL);
                    if (dt.Rows.Count > 0)
                    {
                        Program.CompanyName = dt.Rows[0]["CompanyFullName"].ToString();
                    }

                    dt = null;
                    Database.Close();
                    return true;
                }
                else
                {
                    string StringMsg = "คุณไม่มีสิทธิ์ในการใช้งานโปรแกรมนี้";
                    MessageBox.Show(StringMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dt = null;
                    Database.Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                string StringMsg = "เกิดข้อผิดพลาดในการเชื่อมต่อกับระบบ";
                StringMsg = StringMsg + (char)13 + (char)13;
                StringMsg = StringMsg + "System Msg: " + ex.Message;
                MessageBox.Show(StringMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        static void getSetting()
        {
            IniFile ini = new IniFile();
            string filePath = Program.AppPath + "\\config\\CarServiceConfig.ini";
            try
            {
                ExePath = ini.Read(filePath, "setting", "ExePath");
                ReportPath = ini.Read(filePath, "setting", "ReportPath");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        static Boolean checkProcessRunning(string Process_Name)
        {
            Process[] processes;
            string StringMsg;
            int count = 0;
            StringMsg = "โปรแกรมนี้ถูกเปิดไว้แล้ว";

            processes = Process.GetProcesses();
            foreach (Process instance in processes)
            {
                if (instance.ProcessName == Process_Name)
                {
                    count = count + 1;
                    if (count > 1)
                    {
                        MessageBox.Show(StringMsg, "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        static Boolean checkFormatDate()
        {
            string stringCulture;
            string StringDate;
            string StringMsg;

            stringCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            StringDate = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern.ToString();

            if (stringCulture != "th-TH")
            {
                StringMsg = "กรุณาเปลี่ยนรูปแบบของวันที่ในเครื่องคอมพิวเตอร์ของท่านเป็นภาษาไทย";
                StringMsg = StringMsg + (char)13;
                StringMsg = StringMsg + "รูปแบบ วัน/เดือน/ปี (d/M/yyyy) ";

                MessageBox.Show(StringMsg, "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
