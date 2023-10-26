using System.Collections.Generic;

namespace JournalReport_Export.bean
{
    public class Recipt_Info
    {
        public Recipt_Head Head { get; set; }
        public List<Recipt_Body> recipt_Bodies { get; set; }
    }
    public class Recipt_Head
    {
        public string jour_num { get; set; }
        public string ac_date { get; set; }
        public string reci_name { get; set; }
        public string reci_num { get; set; }
        public string debit_amount { get; set; }
        public string credit_amount { get; set; }
        public string empl_Name { get; set; }
        public string empl_date { get; set; }
    }
    public class Recipt_Body
    {
        public string acc_no { get; set; }
        public string account { get; set; }
        public string remark { get; set; }
        public string dc_type { get; set; }
        public string amt { get; set; }
        public string cu_name { get; set; }
        public string cu_no { get; set; }
    }
}
