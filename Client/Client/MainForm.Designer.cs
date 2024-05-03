using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            MassageSend = new Button();
            MassegeBox = new TextBox();
            ChatBox = new TextBox();
            FriendListChat = new ComboBox();
            StartChat = new Button();
            AddBlackList = new Button();
            AddFriendList = new Button();
            LabelFriendList = new Label();
            LabelAddFriednList = new Label();
            LabelBlackList = new Label();
            RemoveBlacklist = new ComboBox();
            LabelRemoveBlackList = new Label();
            RemoveBlackButton = new Button();
            RemoveFriendList = new ComboBox();
            LabelRemovFriendLost = new Label();
            RemoveButton = new Button();
            LabelAddFriendList = new Label();
            SearchUser = new TextBox();
            SearchChatStart = new Button();
            LabelSearch = new Label();
            AddFriensField = new TextBox();
            groupMembersComboBox = new ComboBox();
            createGroupChatButton = new Button();
            StartGroupChat = new Label();
            ChatList = new ComboBox();
            ConnectChatButton = new Button();
            AddBlacklistField = new TextBox();
            LabelUsername = new Label();
            LabelTag = new Label();
            SuspendLayout();
            // 
            // MassageSend
            // 
            MassageSend.Location = new Point(713, 415);
            MassageSend.Name = "MassageSend";
            MassageSend.Size = new Size(75, 23);
            MassageSend.TabIndex = 0;
            MassageSend.Text = "Send";
            MassageSend.UseVisualStyleBackColor = true;
            // 
            // MassegeBox
            // 
            MassegeBox.Location = new Point(286, 416);
            MassegeBox.Name = "MassegeBox";
            MassegeBox.Size = new Size(421, 23);
            MassegeBox.TabIndex = 1;
            // 
            // ChatBox
            // 
            ChatBox.Location = new Point(286, 52);
            ChatBox.Multiline = true;
            ChatBox.Name = "ChatBox";
            ChatBox.Size = new Size(502, 357);
            ChatBox.TabIndex = 2;
            // 
            // FriendListChat
            // 
            FriendListChat.FormattingEnabled = true;
            FriendListChat.Location = new Point(12, 157);
            FriendListChat.Name = "FriendListChat";
            FriendListChat.Size = new Size(187, 23);
            FriendListChat.TabIndex = 3;
            // 
            // StartChat
            // 
            StartChat.Location = new Point(205, 156);
            StartChat.Name = "StartChat";
            StartChat.Size = new Size(75, 23);
            StartChat.TabIndex = 4;
            StartChat.Text = "Chat";
            StartChat.UseVisualStyleBackColor = true;
            StartChat.Click += StartChat_Click;
            // 
            // AddBlackList
            // 
            AddBlackList.Location = new Point(205, 288);
            AddBlackList.Name = "AddBlackList";
            AddBlackList.Size = new Size(75, 23);
            AddBlackList.TabIndex = 6;
            AddBlackList.Text = "Add";
            AddBlackList.UseVisualStyleBackColor = true;
            AddBlackList.Click += AddBlackList_Click;
            // 
            // AddFriendList
            // 
            AddFriendList.Location = new Point(205, 113);
            AddFriendList.Name = "AddFriendList";
            AddFriendList.Size = new Size(75, 23);
            AddFriendList.TabIndex = 8;
            AddFriendList.Text = "Add";
            AddFriendList.UseVisualStyleBackColor = true;
            AddFriendList.Click += AddFriendList_Click;
            // 
            // LabelFriendList
            // 
            LabelFriendList.AutoSize = true;
            LabelFriendList.Location = new Point(16, 139);
            LabelFriendList.Name = "LabelFriendList";
            LabelFriendList.Size = new Size(61, 15);
            LabelFriendList.TabIndex = 9;
            LabelFriendList.Text = "Friend List";
            // 
            // LabelAddFriednList
            // 
            LabelAddFriednList.AutoSize = true;
            LabelAddFriednList.Location = new Point(12, 138);
            LabelAddFriednList.Name = "LabelAddFriednList";
            LabelAddFriednList.Size = new Size(100, 15);
            LabelAddFriednList.TabIndex = 10;
            LabelAddFriednList.Text = "Add to Friend List";
            // 
            // LabelBlackList
            // 
            LabelBlackList.AutoSize = true;
            LabelBlackList.Location = new Point(12, 270);
            LabelBlackList.Name = "LabelBlackList";
            LabelBlackList.Size = new Size(96, 15);
            LabelBlackList.TabIndex = 11;
            LabelBlackList.Text = "Add To Black List";
            // 
            // RemoveBlacklist
            // 
            RemoveBlacklist.FormattingEnabled = true;
            RemoveBlacklist.Location = new Point(12, 332);
            RemoveBlacklist.Name = "RemoveBlacklist";
            RemoveBlacklist.Size = new Size(187, 23);
            RemoveBlacklist.TabIndex = 12;
            // 
            // LabelRemoveBlackList
            // 
            LabelRemoveBlackList.AutoSize = true;
            LabelRemoveBlackList.Location = new Point(12, 314);
            LabelRemoveBlackList.Name = "LabelRemoveBlackList";
            LabelRemoveBlackList.Size = new Size(133, 15);
            LabelRemoveBlackList.TabIndex = 13;
            LabelRemoveBlackList.Text = "Remove From Black List";
            // 
            // RemoveBlackButton
            // 
            RemoveBlackButton.Location = new Point(205, 331);
            RemoveBlackButton.Name = "RemoveBlackButton";
            RemoveBlackButton.Size = new Size(75, 23);
            RemoveBlackButton.TabIndex = 14;
            RemoveBlackButton.Text = "Remove";
            RemoveBlackButton.UseVisualStyleBackColor = true;
            RemoveBlackButton.Click += RemoveBlackButton_Click;
            // 
            // RemoveFriendList
            // 
            RemoveFriendList.FormattingEnabled = true;
            RemoveFriendList.Location = new Point(12, 200);
            RemoveFriendList.Name = "RemoveFriendList";
            RemoveFriendList.Size = new Size(187, 23);
            RemoveFriendList.TabIndex = 15;
            // 
            // LabelRemovFriendLost
            // 
            LabelRemovFriendLost.AutoSize = true;
            LabelRemovFriendLost.Location = new Point(12, 182);
            LabelRemovFriendLost.Name = "LabelRemovFriendLost";
            LabelRemovFriendLost.Size = new Size(138, 15);
            LabelRemovFriendLost.TabIndex = 16;
            LabelRemovFriendLost.Text = "Remove From Friend List";
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(205, 199);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(75, 23);
            RemoveButton.TabIndex = 17;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // LabelAddFriendList
            // 
            LabelAddFriendList.AutoSize = true;
            LabelAddFriendList.Location = new Point(16, 95);
            LabelAddFriendList.Name = "LabelAddFriendList";
            LabelAddFriendList.Size = new Size(100, 15);
            LabelAddFriendList.TabIndex = 18;
            LabelAddFriendList.Text = "Add to Friend List";
            // 
            // SearchUser
            // 
            SearchUser.Location = new Point(12, 69);
            SearchUser.Name = "SearchUser";
            SearchUser.Size = new Size(187, 23);
            SearchUser.TabIndex = 19;
            // 
            // SearchChatStart
            // 
            SearchChatStart.Location = new Point(205, 69);
            SearchChatStart.Name = "SearchChatStart";
            SearchChatStart.Size = new Size(75, 23);
            SearchChatStart.TabIndex = 20;
            SearchChatStart.Text = "Chat";
            SearchChatStart.UseVisualStyleBackColor = true;
            // 
            // LabelSearch
            // 
            LabelSearch.AutoSize = true;
            LabelSearch.Location = new Point(12, 51);
            LabelSearch.Name = "LabelSearch";
            LabelSearch.Size = new Size(42, 15);
            LabelSearch.TabIndex = 21;
            LabelSearch.Text = "Search";
            // 
            // AddFriensField
            // 
            AddFriensField.Location = new Point(12, 113);
            AddFriensField.Name = "AddFriensField";
            AddFriensField.Size = new Size(187, 23);
            AddFriensField.TabIndex = 22;
            // 
            // groupMembersComboBox
            // 
            groupMembersComboBox.FormattingEnabled = true;
            groupMembersComboBox.Location = new Point(12, 244);
            groupMembersComboBox.Name = "groupMembersComboBox";
            groupMembersComboBox.Size = new Size(187, 23);
            groupMembersComboBox.TabIndex = 24;
            // 
            // createGroupChatButton
            // 
            createGroupChatButton.Location = new Point(205, 244);
            createGroupChatButton.Name = "createGroupChatButton";
            createGroupChatButton.Size = new Size(75, 23);
            createGroupChatButton.TabIndex = 25;
            createGroupChatButton.Text = "Chat";
            createGroupChatButton.UseVisualStyleBackColor = true;
            // 
            // StartGroupChat
            // 
            StartGroupChat.AutoSize = true;
            StartGroupChat.Location = new Point(12, 226);
            StartGroupChat.Name = "StartGroupChat";
            StartGroupChat.Size = new Size(95, 15);
            StartGroupChat.TabIndex = 26;
            StartGroupChat.Text = "Start Group Chat";
            // 
            // ChatList
            // 
            ChatList.FormattingEnabled = true;
            ChatList.Location = new Point(520, 23);
            ChatList.Name = "ChatList";
            ChatList.Size = new Size(187, 23);
            ChatList.TabIndex = 27;
            // 
            // ConnectChatButton
            // 
            ConnectChatButton.Location = new Point(713, 24);
            ConnectChatButton.Name = "ConnectChatButton";
            ConnectChatButton.Size = new Size(75, 23);
            ConnectChatButton.TabIndex = 28;
            ConnectChatButton.Text = "Connect";
            ConnectChatButton.UseVisualStyleBackColor = true;
            // 
            // AddBlacklistField
            // 
            AddBlacklistField.Location = new Point(16, 289);
            AddBlacklistField.Name = "AddBlacklistField";
            AddBlacklistField.Size = new Size(187, 23);
            AddBlacklistField.TabIndex = 29;
            // 
            // LabelUsername
            // 
            LabelUsername.AutoSize = true;
            LabelUsername.Location = new Point(12, 9);
            LabelUsername.Name = "LabelUsername";
            LabelUsername.Size = new Size(38, 15);
            LabelUsername.TabIndex = 30;
            LabelUsername.Text = "label1";
            // 
            // LabelTag
            // 
            LabelTag.AutoSize = true;
            LabelTag.Location = new Point(12, 32);
            LabelTag.Name = "LabelTag";
            LabelTag.Size = new Size(38, 15);
            LabelTag.TabIndex = 31;
            LabelTag.Text = "label2";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(LabelTag);
            Controls.Add(LabelUsername);
            Controls.Add(AddBlacklistField);
            Controls.Add(ConnectChatButton);
            Controls.Add(ChatList);
            Controls.Add(StartGroupChat);
            Controls.Add(createGroupChatButton);
            Controls.Add(groupMembersComboBox);
            Controls.Add(AddFriensField);
            Controls.Add(LabelSearch);
            Controls.Add(SearchChatStart);
            Controls.Add(SearchUser);
            Controls.Add(LabelAddFriendList);
            Controls.Add(RemoveButton);
            Controls.Add(LabelRemovFriendLost);
            Controls.Add(RemoveFriendList);
            Controls.Add(RemoveBlackButton);
            Controls.Add(LabelRemoveBlackList);
            Controls.Add(RemoveBlacklist);
            Controls.Add(LabelBlackList);
            Controls.Add(LabelFriendList);
            Controls.Add(AddFriendList);
            Controls.Add(AddBlackList);
            Controls.Add(StartChat);
            Controls.Add(FriendListChat);
            Controls.Add(ChatBox);
            Controls.Add(MassegeBox);
            Controls.Add(MassageSend);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button MassageSend;
        private TextBox MassegeBox;
        private TextBox ChatBox;
        private ComboBox FriendListChat;
        private Button StartChat;
        private ComboBox BlackList;
        private Button AddBlackList;
        private Button AddFriendList;
        private Label LabelFriendList;
        private Label LabelAddFriednList;
        private Label LabelBlackList;
        private ComboBox RemoveBlacklist;
        private Label LabelRemoveBlackList;
        private Button RemoveBlackButton;
        private ComboBox RemoveFriendList;
        private Label LabelRemovFriendLost;
        private Button RemoveButton;
        private Label LabelAddFriendList;
        private TextBox SearchUser;
        private Button SearchChatStart;
        private Label LabelSearch;
        private TextBox AddFriensField;
        private ComboBox groupMembersComboBox;
        private Button createGroupChatButton;
        private Label StartGroupChat;
        private ComboBox ChatList;
        private Button ConnectChatButton;
        private TextBox AddBlacklistField;
        private Label LabelUsername;
        private Label LabelTag;
    }
}
