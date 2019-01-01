using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramClient
{
    public partial class SelectChatRoom : Form
    {
        public List<string> UserNameList { get; set; }
        public LoggedInScreen ParentScreen { get; set; }
        List<string> selectedNames;

        public SelectChatRoom()
        {
            selectedNames = new List<string>();
            InitializeComponent();
        }

        private void SelectChatRoom_Load(object sender, EventArgs e)
        {
            foreach (string name in UserNameList)
            {
                checkedListBox.Items.Add(name);
            }
            
        }
        private void SelectChatRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentScreen.SetChatRoomParticipants(selectedNames);
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string name in checkedListBox.CheckedItems)
                {
                    selectedNames.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SelectChatRoom:   " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();

        }
    }
}
