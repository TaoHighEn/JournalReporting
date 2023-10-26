using Dapper;
using JournalReport_Export.bean;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Workbook2 = Spire.Xls.Workbook;
using Worksheet2 = Spire.Xls.Worksheet;

namespace JournalReport_Export
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// template file path
        /// </summary>
        private protected string filepath = "../ExportTemplate";
        public MainForm()
        {
            InitializeComponent();
            ControlEnable(true);
            setComboItem();
        }
        /// <summary>
        /// 取得ComboList列表
        /// </summary>
        public void setComboItem()
        {
            List<ComboInfo> comboInfos = new List<ComboInfo>();
            comboInfos = this.getComboInfo();
            foreach (ComboInfo item in comboInfos)
            {
                ComboItem comboItem = new ComboItem(item.ar_reno, item.ar_rena);
                cb_ReciptName.Items.Add(comboItem);
            }
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DirectoryInfo di = new DirectoryInfo(filepath);
            FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (var info in files)
            {
                cb_FileSelect.Items.Add(info.Name);
            }
        }

        private List<ComboInfo> getComboInfo()
        {
            string connstring = @"Data Source=10.0.1.15;Initial Catalog='wpap1';Persist Security Info=True;User ID=iemis;Password=ooooo";
            using (SqlConnection sqlConnection = new SqlConnection(connstring))
            {
                sqlConnection.Open();
                string sqlcmd = "Select distinct a.ar_reno,b.ar_rena from ieac00h a inner join iepb10h b on a.ar_reno = b.ar_reno";
                List<ComboInfo> list = sqlConnection.Query<ComboInfo>(sqlcmd).ToList();
                return list;
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            this.ControlEnable(false);
            try
            {
                string connstring = @"Data Source=10.0.1.15;Initial Catalog='wpap1';Persist Security Info=True;User ID=iemis;Password=ooooo";
                List<Recipt_Head> recipt_Heads = new List<Recipt_Head>();
                //List<Recipt_Info> recipt_infos = new List<Recipt_Info>();
                using (SqlConnection sqlConnection = new SqlConnection(connstring))
                {
                    string start_date = this.start_date.Value.ToString("yyyy/MM/dd");
                    string end_date = this.end_date.Value.ToString("yyyy/MM/dd");
                    string ar_reno = ((ComboItem)cb_ReciptName.SelectedItem).getValue();
                    string sqlcmd = string.Format(@"Select 
                                            a.ac_vno jour_num,
                                            a.ac_date ac_date,
                                            c.ar_rena reci_name,
                                            a.ar_no reci_num,
                                            a.ac_dsum debit_amount,
                                            a.ac_csum credit_amount,
                                            d.pa_name empl_Name,
                                            a.ie_ymd empl_date
                                            From ieac00h a
                                            join iepb10h c on a.ar_reno = c.ar_reno
                                            join iepa00h d on d.pa_no = a.ac_enter
                                            Where 1=1 
                                            AND a.ar_reno = '{0}' 
                                            AND a.ac_date between '{1}' AND '{2}'",
                                                    ar_reno, start_date, end_date);
                    recipt_Heads = sqlConnection.Query<Recipt_Head>(sqlcmd).ToList();
                    foreach (var recipt_head in recipt_Heads)
                    {
                        Recipt_Info recipt_info = new Recipt_Info()
                        {
                            Head = recipt_head,
                            recipt_Bodies = this.getReciptBody(sqlConnection, recipt_head)
                        };
                        this.PrintJournalExport(recipt_info);
                    }
                }
            }
            catch (Exception ex)
            {
                this.lbl_Status.Text = "STOP";

                this.ControlEnable(true);
            }
            this.ControlEnable(true);
        }
        public List<Recipt_Body> getReciptBody(SqlConnection sqlConnection, Recipt_Head recipt_Head)
        {
            string sqlcmd = string.Format(@"Select
                                            b.ac_no acc_no,
                                            b.ac_na account,
                                            a.ac_remk remark,
                                            a.ac_dc dc_type,
                                            ac_amt amt,
                                            c.cu_na cu_name,
                                            a.ac_vs1 cu_no
                                            From ieac00d1 a 
                                            join ieacnoh b on a.ac_no=b.ac_no
                                            left join iecusuh c on a.ac_vs1 = c.cu_no
                                            where ac_vno = '{0}'
                                            order by a.ac_seq",
                                            recipt_Head.jour_num);
            return sqlConnection.Query<Recipt_Body>(sqlcmd).ToList();
        }
        public void PrintJournalExport(Recipt_Info recipt_info)
        {
            string[] body_c = { "A", "V", "AR", "BE", "BR" };
            string[] body_r = { "7", "11", "15", "19", "23", "27" };
            int page_count = (recipt_info.recipt_Bodies.Count() % 6) == 0 ? (recipt_info.recipt_Bodies.Count() / 6) : (recipt_info.recipt_Bodies.Count() / 6) + 1;
            string template = cb_FileSelect.Text;
            string jour_type = ((ComboItem)cb_ReciptName.SelectedItem).Text;
            string source_file = filepath + "//" + template;
            string out_file_path = filepath + "//OUTPUT//" + jour_type;
            if (!Directory.Exists(out_file_path))
            {
                Directory.CreateDirectory(out_file_path);
            }
            //item counting
            int item_ser = 0;
            for (int i = 0; i < page_count; i++)
            {
                string output_file = DateTime.Now.ToString("yyyyMMdd_hhmmssff") + "_" + recipt_info.Head.ac_date.Replace("/", "") + "_" + template;
                string output_fullname = out_file_path + "//" + output_file;
                File.Copy(source_file, output_fullname);
                using (Workbook2 _workbook = new Workbook2())
                {
                    _workbook.LoadFromFile(source_file);
                    using (Workbook2 out_work = new Workbook2())
                    {
                        out_work.LoadFromFile(output_fullname);
                        Worksheet2 worksheet = out_work.Worksheets[0];
                        worksheet.Range["J1"].Text = recipt_info.Head.ac_date;
                        worksheet.Range["J2"].Text = recipt_info.Head.jour_num;
                        worksheet.Range["BQ1"].Text = recipt_info.Head.reci_name;
                        worksheet.Range["BQ2"].Text = recipt_info.Head.reci_num;
                        worksheet.Range["CF2"].Text = string.Format((i + 1).ToString() + "/" + page_count.ToString());
                        //Body
                        var item = recipt_info.recipt_Bodies;
                        for (int j = 0; j < 6; j++)
                        {
                            worksheet.Range["A" + body_r[j]].Text = item[item_ser].acc_no + "  " + item[item_ser].account;
                            worksheet.Range["V" + body_r[j]].Text = item[item_ser].remark;
                            if (item[item_ser].dc_type == "1")
                            {
                                worksheet.Range["AR" + body_r[j]].NumberFormat = "#,##";
                                worksheet.Range["AR" + body_r[j]].NumberValue = double.Parse(item[item_ser].amt);
                            }
                            else if (item[item_ser].dc_type == "2")
                            {
                                worksheet.Range["BE" + body_r[j]].NumberFormat = "#,##";
                                worksheet.Range["BE" + body_r[j]].NumberValue = double.Parse(item[item_ser].amt);
                            }
                            if (!string.IsNullOrEmpty(item[item_ser].cu_name))
                            {
                                worksheet.Range["BR" + body_r[j]].Text = item[item_ser].cu_name + "(" + item[item_ser].cu_no + ")";
                            }
                            item_ser++;
                            if (item_ser == item.Count)
                            {
                                worksheet.Range["V31"].Text = "合計";
                                worksheet.Range["AR31"].NumberFormat = "#,##";
                                worksheet.Range["AR31"].NumberValue = double.Parse(recipt_info.Head.debit_amount);
                                worksheet.Range["BE31"].NumberFormat = "#,##";
                                worksheet.Range["BE31"].NumberValue = double.Parse(recipt_info.Head.credit_amount);
                                break;
                            }
                            else if (j + 1 >= 6)
                            {
                                worksheet.Range["V31"].Text = "接續頁";
                            }
                        }
                        worksheet.Range["BU35"].Text = recipt_info.Head.empl_Name + "\n" + recipt_info.Head.ac_date;
                        out_work.Save();
                    }
                }
            }
            #region old
            //string out_file_name = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + recipt_info.Head.ac_date.Replace("/", "") + "_" + template;
            //string out_file = out_file_path + "//" + out_file_name;
            //File.Copy(source_file, out_file);
            //using (Workbook2 _workbook = new Workbook2())
            //{
            //    _workbook.LoadFromFile(source_file);
            //    using (Workbook2 workbook = new Workbook2())
            //    {
            //        workbook.LoadFromFile(out_file);

            //        for (int i = 0; i < page_count; i++)
            //        {
            //            if (i > 0)
            //            {
            //                workbook.Worksheets.Add("sheet" + i);
            //                workbook.Worksheets[i].CopyFrom(_workbook.Worksheets[0]);
            //            }
            //            using (Worksheet2 worksheet = workbook.Worksheets[i])
            //            {
            //                //Head
            //                worksheet.Range["J1"].Text = recipt_info.Head.ac_date;
            //                worksheet.Range["J2"].Text = recipt_info.Head.jour_num;
            //                worksheet.Range["BQ1"].Text = recipt_info.Head.reci_name;
            //                worksheet.Range["BQ2"].Text = recipt_info.Head.reci_num;
            //                worksheet.Range["CF2"].Text = string.Format((i + 1).ToString() + "/" + page_count.ToString());
            //                //Body
            //                var item = recipt_info.recipt_Bodies;
            //                for (int j = 0; j < 6; j++)
            //                {
            //                    worksheet.Range["A" + body_r[j]].Text = item[item_ser].acc_no + "  " + item[item_ser].account;
            //                    worksheet.Range["V" + body_r[j]].Text = item[item_ser].remark;
            //                    if (item[item_ser].dc_type == "1")
            //                    {
            //                        worksheet.Range["AR" + body_r[j]].NumberFormat = "#,##";
            //                        worksheet.Range["AR" + body_r[j]].NumberValue = double.Parse(item[item_ser].amt);
            //                    }
            //                    else if (item[item_ser].dc_type == "2")
            //                    {
            //                        worksheet.Range["BE" + body_r[j]].NumberFormat = "#,##";
            //                        worksheet.Range["BE" + body_r[j]].NumberValue = double.Parse(item[item_ser].amt);
            //                    }
            //                    if (!string.IsNullOrEmpty(item[item_ser].cu_name))
            //                    {
            //                        worksheet.Range["BR" + body_r[j]].Text = item[item_ser].cu_name + "(" + item[item_ser].cu_no + ")";
            //                    }
            //                    item_ser++;
            //                    if (item_ser == item.Count)
            //                    {
            //                        worksheet.Range["V31"].Text = "合計";
            //                        worksheet.Range["AR31"].NumberFormat = "#,##";
            //                        worksheet.Range["AR31"].NumberValue = double.Parse(recipt_info.Head.debit_amount);
            //                        worksheet.Range["BE31"].NumberFormat = "#,##";
            //                        worksheet.Range["BE31"].NumberValue = double.Parse(recipt_info.Head.credit_amount);
            //                        break;
            //                    }
            //                    else if (j + 1 >= 6)
            //                    {
            //                        worksheet.Range["V31"].Text = "接續頁";
            //                    }
            //                }
            //                worksheet.Range["BU35"].Text = recipt_info.Head.empl_Name + "\n" + recipt_info.Head.ac_date;
            //                workbook.Save();
            //            }
            //        }
            //    }
            //}
            #endregion
            //XSSFWorkbook work;
            //using (FileStream fs = File.Open(out_file, FileMode.Open, FileAccess.ReadWrite))
            //{
            //    work = new XSSFWorkbook(fs);
            //    work.RemoveSheetAt(work.NumberOfSheets - 1);
            //    using (FileStream fs2 = new FileStream(out_file, FileMode.Create, FileAccess.Write))
            //    {
            //        work.Write(fs2);
            //        fs2.Close();
            //    }
            //    fs.Close();
            //}
        }

        public void ControlEnable(bool state)
        {
            this.cb_ReciptName.Enabled = state;
            this.cb_FileSelect.Enabled = state;
            this.start_date.Enabled = state;
            this.end_date.Enabled = state;
            this.btn_Export.Enabled = state;
            if (!state)
            {
                this.lbl_Status.Visible = !state;
            }
            else
            {
                this.lbl_Status.Visible = !state;
                this.lbl_Status.Text = "列印中";
            }
        }

        private void cb_FileSelect_Click(object sender, EventArgs e)
        {
            cb_FileSelect.Items.Clear();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DirectoryInfo di = new DirectoryInfo(filepath);
            FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (var info in files)
            {
                cb_FileSelect.Items.Add(info.Name);
            }
        }
    }
    public class ComboInfo
    {
        public string ar_reno { get; set; }
        public string ar_rena { get; set; }
    }
    public class ComboItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public ComboItem(string value, string text)
        {
            Value = value;
            Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
        public string getValue()
        {
            return Value;
        }
    }
}
