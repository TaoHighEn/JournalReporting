using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "../ExportTemplate/OUTPUT";
            DirectoryInfo di = new DirectoryInfo(filepath);
            FileInfo[] files = di.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
            {
                XSSFWorkbook work;
                using (FileStream fs = File.Open(file.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    work = new XSSFWorkbook(fs);
                    string s_name = work.GetSheetAt(work.NumberOfSheets - 1).SheetName;
                    if (s_name == "Evaluation Warning")
                    {
                        work.RemoveSheetAt(work.NumberOfSheets - 1);
                        using (FileStream fs2 = new FileStream(file.FullName, FileMode.Create, FileAccess.Write))
                        {
                            work.Write(fs2);
                            fs2.Close();
                        }
                        fs.Close();
                    }
                }
            }
        }
    }
}
