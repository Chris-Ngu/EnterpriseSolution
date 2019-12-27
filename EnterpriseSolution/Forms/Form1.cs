using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace EnterpriseSolution.Forms
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser chrome;
        public Form1()
        {
            InitializeComponent();
            InitializeChrome();
        }

        private void InitializeChrome()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chrome = new ChromiumWebBrowser("https://teamup.com/ksyi1rb8biep5reynj");
            this.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
        }
    }
}
