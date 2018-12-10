using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramClient
{
    public partial class LoginScreen : Form
    {
        public Socket ClientSocket { get; set; }
        byte[] buffer;
        public LoginScreen()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.textBoxAddress.Text = this.LocalIPAddress();
            this.textBoxPort.Text = "34444";
           
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                ClientSocket.EndConnect(AR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendCallback(IAsyncResult AR)
        {
            try
            {
                ClientSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                int received = ClientSocket.EndReceive(AR);
                string text = Encoding.ASCII.GetString(buffer);
                MessagePackage package = new MessagePackage();
                Tuple<int, int, string, string, string> tuple = package.SliceMessage(text.Substring(0, received));
                if (tuple.Item1 == 0)
                {
                   LoggedInScreen loggedinScreen = new LoggedInScreen(ClientSocket, textBoxName.Text);
                   loggedinScreen.UsernameList = package.ToUsernameList(tuple.Item5);
                    this.Hide();
                   loggedinScreen.ShowDialog();
                }
                else if (tuple.Item1 == 5)
                {
                    MessageBox.Show("This username already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonConnect_Click_1(object sender, EventArgs e)
        {

            MessageBox.Show("clicked On connect Button");
            try
            {
                MessageBox.Show("clicked On connect Button1");
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(this.textBoxAddress.Text), Int32.Parse(this.textBoxPort.Text)), new AsyncCallback(ConnectCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

