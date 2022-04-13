using CefSharp;
using CefSharp.WinForms;

namespace WallPaperThree
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ChromiumWebBrowser browser = new ChromiumWebBrowser();
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void resetBrowerPanelSize() {
            
            this.panel1.Width = this.Size.Width;
            this.panel1.Height = this.Size.Height;
            this.panel1.Left = 0;
            this.panel1.Top = 0;

            //浏览器内核绑定到panel上
            this.panel1.Controls.Add(browser);
            browser.SetBounds(0,0, this.Size.Width, this.Size.Height);
        }
        public void setBrowerURL(string url) {
            //Create a new instance in code or add via the designer
            //var browser = new ChromiumWebBrowser();
            //this.panel1.Controls.Add(browser);

            //Load a different url
            browser.LoadUrl(url);
            //browser.LoadUrlAsync(url);
            //browser.LoadHtml(url);
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Location = new System.Drawing.Point(30, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(641, 280);
            this.panel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
    }
}

