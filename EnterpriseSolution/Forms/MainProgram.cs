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

namespace EnterpriseSolution
{
    public partial class MainProgram : Form
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        private static Queue<Request> queue = new Queue<Request>();
        static MainProgram f;

        System.Drawing.Point lastPoint;

        public MainProgram()
        {
            InitializeComponent();
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel5.Hide();
            panel4.Show();
            monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today.AddMonths(2));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Show();
            string option = "SELECT l.username, l.email, CONVERT(l.last_logged_on USING utf8) AS last_on FROM login l";
            dataGridView1.DataSource = MySQLNetworking.GetList(option);
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Hide();
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
            Request request = new Request(UserCache.GetUsername(), DateTime.Now, textBox11.Text);
            richTextBox8.Text += request.ToString();
            queue.Enqueue(request);
           
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && textBox11.Text != "")
            {
                button14.Enabled = true;

            }
            else
            {
                button4.Enabled = false;
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
    }
}
