using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using S22.Imap;
using CefSharp;
using CefSharp.WinForms;

namespace EnterpriseSolution
{
    public partial class MainProgram : Form
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public ChromiumWebBrowser chrome;
        private static Queue<Request> queue = new Queue<Request>();
        static MainProgram f;

        System.Drawing.Point lastPoint;

        public MainProgram()
        {
            InitializeComponent();
            InitializeChrome();
            panel2.Show();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            f = this;
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChattingServiceEndPoint");
            server = _channelFactory.CreateChannel();

            //Logging into chat server whenever the form gets generated
            int returnValue = server.Login(EnterpriseSolution.UserCache.GetUsername());
            if (returnValue  == 1)
            {
                textBox6.Text = "Status: YOU ARE ALREADY LOGGED IN, TRY AGAIN";
            }
            else if (returnValue == 0)
            {
                textBox6.Text = "Status: Logged in";
                textBox6.Enabled = false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void MainProgram_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        
        private void MainProgram_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new System.Drawing.Point(e.X, e.Y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void MainProgram_Activated(object sender, EventArgs e)
        {
            this.textBox2.Text = "Welcome back, " + EnterpriseSolution.UserCache.GetUsername();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Show();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            panel4.Show();
            panel2.Hide();
            panel3.Hide();
            panel5.Hide();
            panel6.Hide();
            panel8.Hide();

        }
        private void InitializeChrome()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chrome = new ChromiumWebBrowser("https://teamup.com/ksyi1rb8biep5reynj");
            this.panel4.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Show();
            panel8.Hide();
            string option = "SELECT l.username, l.email, CONVERT(l.last_logged_on USING utf8) AS last_on FROM login l";
            dataGridView1.DataSource = MySQLNetworking.GetList(option);
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Show();
            panel6.Hide();
            panel8.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel8.Show();

        }
        public void TakeMessage(string message, string username)
        {
            richTextBox4.Text += username + ": " + message + "\n";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            server.SendMessageToAll(richTextBox5.Text, EnterpriseSolution.UserCache.GetUsername()); //sends the messages to all people connected to the chat
            TakeMessage(richTextBox5.Text, EnterpriseSolution.UserCache.GetUsername()); //Takes the message into the textbox client side
            richTextBox5.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var message = new MailMessage(textBox7.Text, textBox9.Text);
            message.Subject = textBox10.Text;
            message.Body = richTextBox6.Text;

            using (SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587))
            {
                mailer.Credentials = new NetworkCredential(textBox7.Text, textBox8.Text);
                mailer.EnableSsl = true;
                mailer.Send(message);
            }

            textBox9.Text = null;
            textBox10.Text = null;
            richTextBox6.Text = null;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            StartReceiving();
        }

        private void StartReceiving()
        {
            Task.Run(() =>
            {
                using (IImapClient client = new ImapClient("imap.gmail.com", 993, textBox7.Text,
                        textBox8.Text, AuthMethod.Login, true))
                {
                    if (client.Supports("IDLE") == false)
                    {
                        System.Windows.MessageBox.Show("Server does not support IMAP IDLE");
                        return;
                    }
                    client.NewMessage += new EventHandler<IdleMessageEventArgs>(OnNewMessage);
                    while (true) ;
                }
            });
        }
        static void OnNewMessage(object sender, IdleMessageEventArgs e)
        {
            System.Windows.MessageBox.Show("New message");
            MailMessage m = e.Client.GetMessage(e.MessageUID, FetchOptions.Normal);
            f.Invoke((MethodInvoker)delegate
            {
                f.richTextBox7.AppendText("From: " + m.From + "\n" +
                                          "Subject: " + m.Subject + "\n" +
                                           "Body: " + m.Body + "\n");
            });
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && textBox11.Text != "" && !(textBox12.Text.Equals("Insert SQL summary")) && !(textBox12.Text.Equals("")))
            {
                Request request = new Request(UserCache.GetUsername(), DateTime.Now, textBox11.Text, textBox12.Text);
                richTextBox8.Text += request.ToString();
                queue.Enqueue(request);
            }
            else
            {
                System.Windows.MessageBox.Show("Please check if all fields are entered correctly and try again");
            }

        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string option = "";

            if (comboBox2.Text.Equals("Employee information"))
            {
                option = "Select l.username, l.email, CONVERT(l.last_logged_on USING utf8) as last_on FROM login l";
            }
            else if (comboBox2.Text.Equals("Department information"))
            {
                option = "SELECT CONVERT(Dno USING utf8) as Dno, Dname, Mgr FROM department";
            }
            else if (comboBox2.Text.Equals("Supervising Dataset"))
            {
                // need to add here whenever I add table in mysql
            }

            dataGridView1.DataSource = MySQLNetworking.GetList(option);
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            textBox12.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel8.Hide();
        }

        //Could create private method to move all panels to resize on open space
        bool toggled = false;
        private void button15_Click(object sender, EventArgs e)
        {
            //if tab is out
            if (toggled == false) 
            {
                HideTab();

            }
            //if tab is already minimized
            else if (toggled == true)
            {
                ShowTab();
            }
        }
        //Seperate into different private methods to move elements around on panel
        private void HideTab()
        {
            toggled = true;
            button10.Location = new System.Drawing.Point(11, 11);
            button15.Location = new System.Drawing.Point(11, 44);
            panel1.Hide();
            panel2.Size = new System.Drawing.Size(946, 556);
            panel2.Location = new System.Drawing.Point(49, 8);
            panel6.Size = new System.Drawing.Size(978, 571);
            panel6.Location = new System.Drawing.Point(41, 0);
            panel3.Size = new System.Drawing.Size(961, 556);
            panel3.Location = new System.Drawing.Point(31, 16);
            panel4.Size = new System.Drawing.Size(975, 564);
            panel4.Location = new System.Drawing.Point(34, 8);
            panel5.Size = new System.Drawing.Size(976, 572);
            panel5.Location = new System.Drawing.Point(33, 0);
            panel8.Size = new System.Drawing.Size(980, 564);
            panel8.Location = new System.Drawing.Point(36, 8);
        }
        private void ShowTab()
        {
            toggled = false;
            button10.Location = new System.Drawing.Point(210, 12);
            button15.Location = new System.Drawing.Point(66, 44);
            panel1.Show();
            panel2.Size = new System.Drawing.Size(762, 556);
            panel2.Location = new System.Drawing.Point(233, 8);
            panel6.Size = new System.Drawing.Size(780, 571);
            panel6.Location = new System.Drawing.Point(239, 0);
            panel3.Size = new System.Drawing.Size(759, 556);
            panel3.Location = new System.Drawing.Point(233, 16);
            panel4.Size = new System.Drawing.Size(776, 564);
            panel4.Location = new System.Drawing.Point(233, 8);
            panel5.Size = new System.Drawing.Size(773, 572);
            panel5.Location = new System.Drawing.Point(236, 0);
            panel8.Size = new System.Drawing.Size(783, 564);
            panel8.Location = new System.Drawing.Point(233, 8);
        }
    }
}
