using System;
using System.Windows.Forms;

namespace CarService
{
    class sharedFunction
    {
        public void InsertArrayElement<T>(ref T[] sourceArray, ref T newValue)
        {
            try
            {
                Int32 newPosition;
                newPosition = sourceArray.Length;
                if (newPosition < 0)
                {
                    newPosition = 0;
                }
                Array.Resize<T>(ref sourceArray, sourceArray.Length + 1);
                sourceArray[newPosition] = newValue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //MessageBox.Show(e.Message, "Error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        public static DateTime ConvertToDate(string datestring)
        {
            string[] formats;
            DateTime dateObject = DateTime.Today;
            try
            {
                formats = new string[] {"dd/MM/yyyy", "dd/MM/yy", "ddMMyyyy",
                                        "ddMMyy", "d/M/yy", "d/M/yyyy", "d/MM/yy", "dd/M/yy"};
                dateObject = DateTime.ParseExact(datestring, formats,
                    System.Globalization.CultureInfo.CurrentCulture, 
                    System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            }
            catch
            {
                throw new Exception("ระบุรูปแบบวันที่ไม่ถูกต้อง");
                //MessageBox.Show(e.Message, "Error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            return dateObject; //return result;
        }

        public static string SQLDateString(DateTime date)
        {
            string stringCulture;
            stringCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            if (stringCulture == "th-TH")
            {
                return (date.Year).ToString() + date.ToString("-MM-dd");
            }
            else
            {
                return date.ToString("yyyy-MM-dd");
            }

            //return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static int checkNumberKey(int key)
        {
            string character = "0123456789";
            if (key > 26)
            {
                if (character.IndexOf((char)key) == -1)
                {
                    key = 0;
                }
            }
            return key;
        }

        public static int checkUsernameKey(int key)
        {
            string character = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (key > 26)
            {
                if (character.IndexOf((char)key) == -1)
                {
                    key = 0;
                }
            }
            return key;
        }

        public static int checkPasswordKey(int key)
        {
            string character = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@#$";
            if (key > 26)
            {
                if (character.IndexOf((char)key) == -1)
                {
                    key = 0;
                }
            }
            return key;
        }

        public static int checkDateKey(int key)
        {
            string character = "0123456789/-";
            if (key > 26)
            {
                if (character.IndexOf((char)key) == -1)
                {
                    key = 0;
                }
            }
            return key;
        }
    }
}
