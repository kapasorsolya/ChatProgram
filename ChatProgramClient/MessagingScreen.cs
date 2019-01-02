using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramClient
{
    public partial class MessagingScreen : Form
    {
        public Socket ClientSocket { get; set; }
        public List<string> Partners { get; set; }
        public string MyName { get; set; }
        public LoggedInScreen parentScreen { get; set; }
        public int MessageType { get; set; }
        byte[] buffer;
       
        public MessagingScreen(Socket socket)
        {
            ClientSocket = socket;
            buffer = new byte[ClientSocket.ReceiveBufferSize];
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            btnSendFile.Enabled = true;
            try
            {
                MessagePackage package = new MessagePackage();
                string toSendMessage;
                toSendMessage = package.ConcatenateMessage(MessageType, 0, MyName, this.Text, textBoxType.Text);
                byte[] buff = Encoding.ASCII.GetBytes(toSendMessage);
                ClientSocket.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (SocketException) { }
            catch (Exception ex)
            {
                MessageBox.Show("2 " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendCallback(IAsyncResult AR)
        {
            try
            {
                ClientSocket.EndSend(AR);
                textBoxType.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("3 " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AppendToTextBox(string text, string from)
        {

            if (!text.Equals(string.Empty))
            {
                textBoxMessageShow.Text += from;
                textBoxMessageShow.Text += ": ";
                textBoxMessageShow.Text += text;
                textBoxMessageShow.Text += "\r\n";
            }
            
        }

       
        private void MessagingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentScreen.RemoveFromOpenedWindowsList(this.Text);
        }

        private void btnSendFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                string fileString = string.Empty;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.AutoUpgradeEnabled = false;
                ofd.Filter = "Text File|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;
                    byte[] fileByte = File.ReadAllBytes(path);
                    fileString += Encoding.ASCII.GetString(fileByte);

                    MessagePackage package = new MessagePackage();
                    string toSendMessage;
                    toSendMessage = package.ConcatenateMessage(MessageType, 1, MyName, this.Text, fileString);
                    byte[] buff = Encoding.ASCII.GetBytes(toSendMessage);
                    ClientSocket.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                }
            }
            catch (SocketException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
