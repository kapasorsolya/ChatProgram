namespace ChatProgramClient
{
    partial class LoggedInScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnChatRoom = new System.Windows.Forms.Button();
            this.btnToEveryone = new System.Windows.Forms.Button();
            this.listBoxContactList = new System.Windows.Forms.ListBox();
            this.lblContactList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChatRoom
            // 
            this.btnChatRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChatRoom.Location = new System.Drawing.Point(198, 54);
            this.btnChatRoom.Name = "btnChatRoom";
            this.btnChatRoom.Size = new System.Drawing.Size(120, 44);
            this.btnChatRoom.TabIndex = 7;
            this.btnChatRoom.Text = "Chat room";
            this.btnChatRoom.UseVisualStyleBackColor = true;
            // 
            // btnToEveryone
            // 
            this.btnToEveryone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToEveryone.Location = new System.Drawing.Point(198, 119);
            this.btnToEveryone.Name = "btnToEveryone";
            this.btnToEveryone.Size = new System.Drawing.Size(120, 44);
            this.btnToEveryone.TabIndex = 6;
            this.btnToEveryone.Text = "Message to everyone";
            this.btnToEveryone.UseVisualStyleBackColor = true;
            this.btnToEveryone.Click += new System.EventHandler(this.btnToEveryone_Click);
            // 
            // listBoxContactList
            // 
            this.listBoxContactList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxContactList.FormattingEnabled = true;
            this.listBoxContactList.Location = new System.Drawing.Point(30, 22);
            this.listBoxContactList.Name = "listBoxContactList";
            this.listBoxContactList.Size = new System.Drawing.Size(144, 199);
            this.listBoxContactList.TabIndex = 5;
            this.listBoxContactList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxContactList_MouseDoubleClick);
            // 
            // lblContactList
            // 
            this.lblContactList.AutoSize = true;
            this.lblContactList.Location = new System.Drawing.Point(37, 6);
            this.lblContactList.Name = "lblContactList";
            this.lblContactList.Size = new System.Drawing.Size(63, 13);
            this.lblContactList.TabIndex = 4;
            this.lblContactList.Text = "Contact List";
            // 
            // LoggedInScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 270);
            this.Controls.Add(this.btnChatRoom);
            this.Controls.Add(this.btnToEveryone);
            this.Controls.Add(this.listBoxContactList);
            this.Controls.Add(this.lblContactList);
            this.Name = "LoggedInScreen";
            this.Text = "You logged In succesfully";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChatRoom;
        private System.Windows.Forms.Button btnToEveryone;
        private System.Windows.Forms.ListBox listBoxContactList;
        private System.Windows.Forms.Label lblContactList;
    }
}

