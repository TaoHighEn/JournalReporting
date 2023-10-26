namespace JournalReport_Export
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_ReciptName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Export = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_FileSelect = new System.Windows.Forms.ComboBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_ReciptName
            // 
            this.cb_ReciptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ReciptName.Font = new System.Drawing.Font("新細明體", 20F);
            this.cb_ReciptName.FormattingEnabled = true;
            this.cb_ReciptName.Location = new System.Drawing.Point(270, 45);
            this.cb_ReciptName.Margin = new System.Windows.Forms.Padding(2);
            this.cb_ReciptName.Name = "cb_ReciptName";
            this.cb_ReciptName.Size = new System.Drawing.Size(230, 35);
            this.cb_ReciptName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 20F);
            this.label1.Location = new System.Drawing.Point(125, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "憑證類別";
            // 
            // start_date
            // 
            this.start_date.Font = new System.Drawing.Font("新細明體", 20F);
            this.start_date.Location = new System.Drawing.Point(270, 110);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(230, 39);
            this.start_date.TabIndex = 2;
            // 
            // end_date
            // 
            this.end_date.Font = new System.Drawing.Font("新細明體", 20F);
            this.end_date.Location = new System.Drawing.Point(270, 175);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(230, 39);
            this.end_date.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 20F);
            this.label2.Location = new System.Drawing.Point(125, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "期間起日";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 20F);
            this.label3.Location = new System.Drawing.Point(125, 183);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "期間迄日";
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("新細明體", 20F);
            this.btn_Export.Location = new System.Drawing.Point(270, 322);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(230, 55);
            this.btn_Export.TabIndex = 6;
            this.btn_Export.Text = "列印傳票";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 20F);
            this.label4.Location = new System.Drawing.Point(125, 243);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 27);
            this.label4.TabIndex = 8;
            this.label4.Text = "報表格式";
            // 
            // cb_FileSelect
            // 
            this.cb_FileSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FileSelect.Font = new System.Drawing.Font("新細明體", 14F);
            this.cb_FileSelect.FormattingEnabled = true;
            this.cb_FileSelect.Location = new System.Drawing.Point(270, 246);
            this.cb_FileSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cb_FileSelect.Name = "cb_FileSelect";
            this.cb_FileSelect.Size = new System.Drawing.Size(230, 27);
            this.cb_FileSelect.TabIndex = 7;
            this.cb_FileSelect.Click += new System.EventHandler(this.cb_FileSelect_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Font = new System.Drawing.Font("新細明體", 12F);
            this.lbl_Status.Location = new System.Drawing.Point(639, 46);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(44, 16);
            this.lbl_Status.TabIndex = 9;
            this.lbl_Status.Text = "Status";
            this.lbl_Status.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 389);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_FileSelect);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.end_date);
            this.Controls.Add(this.start_date);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_ReciptName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "列印傳票_V0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_ReciptName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_FileSelect;
        private System.Windows.Forms.Label lbl_Status;
    }
}

