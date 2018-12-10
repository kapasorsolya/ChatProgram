using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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



    }
}
