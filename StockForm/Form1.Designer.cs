namespace StockForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.txt_xPath = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Url = new System.Windows.Forms.TextBox();
            this.btn_每月營收 = new System.Windows.Forms.Button();
            this.btn_sessionK = new System.Windows.Forms.Button();
            this.btn_ratio = new System.Windows.Forms.Button();
            this.txt_stock = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "查詢";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // txt_xPath
            // 
            this.txt_xPath.Location = new System.Drawing.Point(67, 43);
            this.txt_xPath.Name = "txt_xPath";
            this.txt_xPath.Size = new System.Drawing.Size(307, 22);
            this.txt_xPath.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(22, 105);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(597, 144);
            this.textBox2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "xPath : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Url : ";
            // 
            // txt_Url
            // 
            this.txt_Url.Location = new System.Drawing.Point(67, 15);
            this.txt_Url.Name = "txt_Url";
            this.txt_Url.Size = new System.Drawing.Size(307, 22);
            this.txt_Url.TabIndex = 5;
            // 
            // btn_每月營收
            // 
            this.btn_每月營收.Location = new System.Drawing.Point(103, 76);
            this.btn_每月營收.Name = "btn_每月營收";
            this.btn_每月營收.Size = new System.Drawing.Size(75, 23);
            this.btn_每月營收.TabIndex = 6;
            this.btn_每月營收.Text = "更新每月營收";
            this.btn_每月營收.UseVisualStyleBackColor = true;
            this.btn_每月營收.Click += new System.EventHandler(this.btn_revenue_Click);
            // 
            // btn_sessionK
            // 
            this.btn_sessionK.Location = new System.Drawing.Point(184, 76);
            this.btn_sessionK.Name = "btn_sessionK";
            this.btn_sessionK.Size = new System.Drawing.Size(75, 23);
            this.btn_sessionK.TabIndex = 7;
            this.btn_sessionK.Text = "更新季K";
            this.btn_sessionK.UseVisualStyleBackColor = true;
            this.btn_sessionK.Click += new System.EventHandler(this.btn_sessionK_Click);
            // 
            // btn_ratio
            // 
            this.btn_ratio.Location = new System.Drawing.Point(265, 76);
            this.btn_ratio.Name = "btn_ratio";
            this.btn_ratio.Size = new System.Drawing.Size(75, 23);
            this.btn_ratio.TabIndex = 8;
            this.btn_ratio.Text = "單季財務比率";
            this.btn_ratio.UseVisualStyleBackColor = true;
            this.btn_ratio.Click += new System.EventHandler(this.btn_ratio_Click);
            // 
            // txt_stock
            // 
            this.txt_stock.Location = new System.Drawing.Point(62, 272);
            this.txt_stock.Name = "txt_stock";
            this.txt_stock.Size = new System.Drawing.Size(100, 22);
            this.txt_stock.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "stock :";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(176, 271);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 11;
            this.btn_search.Text = "search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 345);
            this.textBox1.MaxLength = 2147483646;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(597, 144);
            this.textBox1.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 495);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "search";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 620);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_stock);
            this.Controls.Add(this.btn_ratio);
            this.Controls.Add(this.btn_sessionK);
            this.Controls.Add(this.btn_每月營收);
            this.Controls.Add(this.txt_Url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txt_xPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "HtmlParseTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_xPath;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Url;
        private System.Windows.Forms.Button btn_每月營收;
        private System.Windows.Forms.Button btn_sessionK;
        private System.Windows.Forms.Button btn_ratio;
        private System.Windows.Forms.TextBox txt_stock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}

