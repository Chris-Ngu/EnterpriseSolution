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

namespace EnterpriseSolution
{
    public partial class MainProgram : Form
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;

        public static explicit operator MainProgram(Window v)
        {
            throw new NotImplementedException();
        }

        System.Drawing.Point lastPoint;

        public MainProgram()
        {
            InitializeComponent();
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
            login = new NetworkCredential(textBox10.Text, textBox11.Text);
            client = new SmtpClient(textBox13.Text);
            client.Port = Convert.ToInt32(textBox12.Text);
            client.EnableSsl = checkBox1.Checked;
            client.Credentials = login;
            msg = new MailMessage { From = new MailAddress(textBox10.Text + textBox13.Text.Replace("smtp.", "@"), EnterpriseSolution.UserCache.GetUsername(), Encoding.UTF8) };
            msg.To.Add(new MailAddress(textBox7.Text));

            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                msg.To.Add(new MailAddress(textBox8.Text));
            }
            msg.Subject = textBox9.Text;
            msg.Body = richTextBox6.Text;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userstate = "Sending...";
            client.SendAsync(msg, userstate);
            
        }
        //Fix the messagebox issue and check the smtp to match an existing account(line 132)
        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("{0} send canceled.", e.UserState), "Message", MessageBoxButton.OK, MessageBoxIcon.Information);
            }
            if (e.Error!= null)
            {
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButton.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Your message has been sent", "Message", MessageBoxButton.OK, MessageBoxIcon.Information);
            }
        }
    }
}
