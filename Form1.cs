using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WebSocketTest
{
    public partial class Form1 : Form
    {
        CefSharp.WinForms.ChromiumWebBrowser browser;
        string password = string.Empty;
        string login = string.Empty;
        public Form1()
        {
            InitializeComponent();
            
            browser = new ChromiumWebBrowser();
            CefSettings settings = new CefSettings();

            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            tableLayoutPanel1.Controls.Add(browser, 1, 0);
            browser.Dock = DockStyle.Fill;
            browser.Enabled = false;

            //Wait for the MainFrame to finish loading
            browser.FrameLoadEnd += (sender, args) =>
            {
                //Wait for the MainFrame to finish loading
                if (args.Frame.IsMain)
                {

                    args.Frame.ExecuteJavaScriptAsync(string.Format("(function() {{document.getElementById('User_login').value='{0}';}})()", login));
                    //string returnValue = "";


                    args.Frame.ExecuteJavaScriptAsync(string.Format("(function() {{document.getElementById('User_password').value='{0}';}})()",password));

                    //var task1 = args.Frame.EvaluateScriptAsync(@"document.getElementsByName('login')[0].click();");
                    //await task1.ContinueWith(t =>
                    //{
                    //    if (!t.IsFaulted)
                    //    {
                    //        var response = t.Result;

                    //        if (response.Success && response.Result != null)
                    //        {
                    //            returnValue = response.Result.ToString();
                    //        }
                    //    }
                    //});
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            password = textBox2.Text;
            browser.Load("http://localhost:4200");
            browser.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


            Cef.Shutdown();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            browser.GetMainFrame().ExecuteJavaScriptAsync(@"(function() {{document.getElementById('login').click();}})()");
        }
    }
}
