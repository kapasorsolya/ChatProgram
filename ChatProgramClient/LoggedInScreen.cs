using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
namespace ChatProgramClient
{
    public partial class LoggedInScreen : Form
    {
        public Socket ClientSocket { get; set; }
        public List<string> UsernameList { get; set; }
        string myName;
        byte[] buffer;
        Dictionary<string, MessagingScreen> openWindows;

        public LoggedInScreen(Socket socket, string myname)
        {
            ClientSocket = socket;
            myName = myname;
            buffer = new byte[ClientSocket.ReceiveBufferSize];
            openWindows = new Dictionary<string, MessagingScreen>();
            InitializeComponent();
            lblContactList.Text = myName + "'s contact list";
        }

        private void LoggedInScreen_Load(object sender, EventArgs e)
        {
            foreach (string name in UsernameList)
            {
                if (name.Equals(myName))
                    continue;
                listBoxContactList.Items.Add(name);
            }
            ClientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int received = ClientSocket.EndReceive(ar);
                string text = Encoding.ASCII.GetString(buffer);
                MessagePackage package = new MessagePackage();
                Tuple<int, int, string, string, string> tuple = package.SliceMessage(text.Substring(0, received));
                //uzenet tipusa
                switch (tuple.Item1)
                {
                    case 0:
                        UsernameList = package.ToUsernameList(tuple.Item5);
                        listBoxContactList.Items.Clear();
                        foreach (string name in UsernameList)
                        {
                            if (name.Equals(myName))
                                continue;
                            listBoxContactList.Items.Add(name);
                        }
                        this.ClientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    //mindenkinek kuldes
                    case 3:
                        //ha nincs fajl
                        if (tuple.Item2 == 0)
                        {
                            if (openWindows.ContainsKey(tuple.Item4) == true)
                            {
                                MessagingScreen megnyitott;
                                megnyitott = openWindows[tuple.Item4];
                                megnyitott.AppendToTextBox(tuple.Item5, tuple.Item3);
                                this.ClientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                            }
                            else
                            {
                                MessagingScreen messagingScreen = OpenNewMessagingScreen(UsernameList, tuple.Item4, 3);
                                messagingScreen.AppendToTextBox(tuple.Item5, tuple.Item3);
                                openWindows.Add(tuple.Item4, messagingScreen);
                                messagingScreen.Show();
                                this.ClientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                                Application.Run();
                            }

                        }
                        break;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

       }

        private void LoggedInScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        public void RemoveFromOpenedWindowsList(string name)
        {
            openWindows.Remove(name);
        }

        private void btnToEveryone_Click(object sender, EventArgs e)
        {
            if (openWindows.ContainsKey("everyone") == false)
            {
                MessagingScreen messagingScreen = OpenNewMessagingScreen(UsernameList, "everyone", 3);
                openWindows.Add("everyone", messagingScreen);
                messagingScreen.Show();

            }

        }

        private MessagingScreen OpenNewMessagingScreen(List<string> usernameList, string user, int type)
        {
            MessagingScreen messagingScreen = new MessagingScreen(this.ClientSocket);
            messagingScreen.Text = user;
            messagingScreen.Partners = UsernameList;
            messagingScreen.MyName = this.myName;
            messagingScreen.MessageType = type;
            messagingScreen.parentScreen = this;
            return messagingScreen;
        }

        private void listBoxContactList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string partner = listBoxContactList.GetItemText(listBoxContactList.SelectedItem);
                List<string> partners = new List<string>();
                partners.Add(partner);
                if (openWindows.ContainsKey(partner) == false)
                {
                    MessagingScreen messagingScreen = OpenNewMessagingScreen(UsernameList, partner, 1);
                    openWindows.Add(partner, messagingScreen);
                    messagingScreen.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logged in double click:   " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

    }
}

