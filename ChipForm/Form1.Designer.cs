namespace ChipForm
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_query = new System.Windows.Forms.Button();
            this.btn_queryDate = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_all = new System.Windows.Forms.Button();
            this.btn_weekTrade = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QueryDBChipDate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_weeks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_query
            // 
            this.btn_query.Location = new System.Drawing.Point(213, 27);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(111, 23);
            this.btn_query.TabIndex = 1;
            this.btn_query.Text = "新增單一週資料";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // btn_queryDate
            // 
            this.btn_queryDate.Location = new System.Drawing.Point(29, 27);
            this.btn_queryDate.Name = "btn_queryDate";
            this.btn_queryDate.Size = new System.Drawing.Size(75, 23);
            this.btn_queryDate.TabIndex = 3;
            this.btn_queryDate.Text = "日期讀取";
            this.btn_queryDate.UseVisualStyleBackColor = true;
            this.btn_queryDate.Click += new System.EventHandler(this.btn_queryDate_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 119);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(402, 152);
            this.textBox1.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(110, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(330, 27);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(98, 23);
            this.btn_all.TabIndex = 6;
            this.btn_all.Text = "更新最近時期集保";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // btn_weekTrade
            // 
            this.btn_weekTrade.Location = new System.Drawing.Point(191, 345);
            this.btn_weekTrade.Name = "btn_weekTrade";
            this.btn_weekTrade.Size = new System.Drawing.Size(91, 23);
            this.btn_weekTrade.TabIndex = 7;
            this.btn_weekTrade.Text = "三大上市週買賣";
            this.btn_weekTrade.UseVisualStyleBackColor = true;
            this.btn_weekTrade.Click += new System.EventHandler(this.btn_weekTrade_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(29, 299);
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(402, 22);
            this.textBox2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "XPath :";
            // 
            // QueryDBChipDate
            // 
            this.QueryDBChipDate.Location = new System.Drawing.Point(330, 56);
            this.QueryDBChipDate.Name = "QueryDBChipDate";
            this.QueryDBChipDate.Size = new System.Drawing.Size(98, 23);
            this.QueryDBChipDate.TabIndex = 11;
            this.QueryDBChipDate.Text = "查詢DB集保日期";
            this.QueryDBChipDate.UseVisualStyleBackColor = true;
            this.QueryDBChipDate.Click += new System.EventHandler(this.QueryDBChipDate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "三大上櫃週買賣";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_weekTrade2_Click);
            // 
            // txt_weeks
            // 
            this.txt_weeks.Location = new System.Drawing.Point(77, 346);
            this.txt_weeks.Multiline = true;
            this.txt_weeks.Name = "txt_weeks";
            this.txt_weeks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_weeks.Size = new System.Drawing.Size(100, 66);
            this.txt_weeks.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "設定週";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 442);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_weeks);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.QueryDBChipDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_weekTrade);
            this.Controls.Add(this.btn_all);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_queryDate);
            this.Controls.Add(this.btn_query);
            this.Name = "Form1";
            this.Text = "集保解析";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.Button btn_queryDate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Button btn_weekTrade;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button QueryDBChipDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_weeks;
        private System.Windows.Forms.Label label3;
    }
}

