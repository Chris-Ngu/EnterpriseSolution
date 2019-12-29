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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;

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
            MainScreen.Show();
            ChatTab.Hide();
            ChromiumBrowser.Hide();
            EmailTab.Hide();
            f = this;
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChattingServiceEndPoint");
            server = _channelFactory.CreateChannel();

            //Logging into chat server whenever the form gets generated
            int returnValue = server.Login(EnterpriseSolution.UserCache.username);
            if (returnValue  == 1)
            {
                StatusBox.Text = "Status: YOU ARE ALREADY LOGGED IN, TRY AGAIN";
            }
            else if (returnValue == 0)
            {
                StatusBox.Text = "Status: Logged in";
                StatusBox.Enabled = false;
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
            this.LoadingMessage.Text = "Welcome back, " + EnterpriseSolution.UserCache.username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainScreen.Hide();
            ChatTab.Show();
            ChromiumBrowser.Hide();
            EmailTab.Hide();
            ManagementTab.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ChromiumBrowser.Show();
            MainScreen.Hide();
            ChatTab.Hide();
            EmailTab.Hide();

        }
        private void InitializeChrome()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chrome = new ChromiumWebBrowser("https://teamup.com/ksyi1rb8biep5reynj");
            this.ChromiumBrowser.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MainScreen.Hide();
            ChatTab.Hide();
            ChromiumBrowser.Hide();
            EmailTab.Hide();
            ManagementTab.Show();
            string option = "SELECT l.username, l.email, CONVERT(l.last_logged_on USING utf8) AS last_on FROM login l";
            SQLViewer.DataSource = MySQLNetworking.GetList(option);
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            MainScreen.Hide();
            ChatTab.Hide();
            ChromiumBrowser.Hide();
            EmailTab.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainScreen.Hide();
            ChatTab.Hide();
            ChromiumBrowser.Hide();
            EmailTab.Hide();
            ManagementTab.Hide();

        }
        public void TakeMessage(string message, string username)
        {
            MessagingHistory.Text += username + ": " + message + "\n";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            server.SendMessageToAll(MessagingTypingMessage.Text, EnterpriseSolution.UserCache.username); //sends the messages to all people connected to the chat
            TakeMessage(MessagingTypingMessage.Text, EnterpriseSolution.UserCache.username); //Takes the message into the textbox client side
            MessagingTypingMessage.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var message = new MailMessage(Email.Text, Recipient.Text);
            message.Subject = Subject.Text;
            message.Body = Body.Text;

            using (SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587))
            {
                mailer.Credentials = new NetworkCredential(Email.Text, Password.Text);
                mailer.EnableSsl = true;
                mailer.Send(message);
            }

            Recipient.Text = null;
            Subject.Text = null;
            Body.Text = null;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            StartReceiving();
        }

        private void StartReceiving()
        {
            Task.Run(() =>
            {
                using (IImapClient client = new ImapClient("imap.gmail.com", 993, Email.Text,
                        Password.Text, AuthMethod.Login, true))
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
                f.ReceivedMailList.AppendText("From: " + m.From + "\n" +
                                          "Subject: " + m.Subject + "\n" +
                                           "Body: " + m.Body + "\n");
            });
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (TableChoice.Text != "" && SQLStatements.Text != "" && !(SQLSummary.Text.Equals("Insert SQL summary")) && !(SQLSummary.Text.Equals("")))
            {
                Request request = new Request(UserCache.username, DateTime.Now, SQLStatements.Text, SQLSummary.Text);
                SQLQueue.Text += request.ToString();
                queue.Enqueue(request);
            }
            else
            {
                System.Windows.MessageBox.Show("Please check if all fields are entered correctly and try again");
            }

        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            SQLViewer.DataSource = null;
            string option = "";

            if (TableChoice.Text.Equals("Employee information"))
            {
                option = "Select l.username, l.email, CONVERT(l.last_logged_on USING utf8) as last_on FROM login l";
            }
            else if (TableChoice.Text.Equals("Department information"))
            {
                option = "SELECT CONVERT(Dno USING utf8) as Dno, Dname, Mgr FROM department";
            }
            else if (TableChoice.Text.Equals("Supervising Dataset"))
            {
                // need to add here whenever I add table in mysql
            }

            SQLViewer.DataSource = MySQLNetworking.GetList(option);
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            SQLSummary.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MainScreen.Show();
            ChatTab.Hide();
            ChromiumBrowser.Hide();
            EmailTab.Hide();
            ManagementTab.Hide();
        }
        //Serializing to store into MySQL server
        private void button15_Click(object sender, EventArgs e)
        {
            Notes note = new Notes(PersonalNotes.Text);
            XmlSerializer serializer = new XmlSerializer(note.GetType());
            using (StreamWriter tw = new StreamWriter(@"C:\Users\xxchr\Desktop\NoteSaved.xml"))
            {
                serializer.Serialize(tw, note);
            }
            note = null;

            //Deserializer
/*            XmlSerializer deserializer = new XmlSerializer(typeof(Notes));
            TextReader reader = new StreamReader(@"C:\Users\xxchr\Desktop\NoteSaved.xml");
            object obj = deserializer.Deserialize(reader);
            note = (Notes)obj;
            reader.Close();
            Console.WriteLine(note.ToString());*/

        }

    }
}
