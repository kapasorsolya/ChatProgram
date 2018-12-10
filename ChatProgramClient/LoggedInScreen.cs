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
    }
}

