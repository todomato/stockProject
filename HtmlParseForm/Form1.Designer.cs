namespace HtmlParseForm
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
            this.txt_Url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txt_xPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ck_unicode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txt_Url
            // 
            this.txt_Url.Location = new System.Drawing.Point(53, 12);
            this.txt_Url.Name = "txt_Url";
            this.txt_Url.Size = new System.Drawing.Size(307, 22);
            this.txt_Url.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Url : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "xPath : ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 102);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(597, 248);
            this.textBox2.TabIndex = 11;
            // 
            // txt_xPath
            // 
            this.txt_xPath.Location = new System.Drawing.Point(53, 40);
            this.txt_xPath.Name = "txt_xPath";
            this.txt_xPath.Size = new System.Drawing.Size(307, 22);
            this.txt_xPath.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "查詢";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // ck_unicode
            // 
            this.ck_unicode.AutoSize = true;
            this.ck_unicode.Location = new System.Drawing.Point(90, 79);
            this.ck_unicode.Name = "ck_unicode";
            this.ck_unicode.Size = new System.Drawing.Size(79, 16);
            this.ck_unicode.TabIndex = 15;
            this.ck_unicode.Text = "啟用UTF-8";
            this.ck_unicode.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 372);
            this.Controls.Add(this.ck_unicode);
            this.Controls.Add(this.txt_Url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txt_xPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Html解析器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txt_xPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ck_unicode;
    }
}

