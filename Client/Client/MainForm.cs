using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Security.AccessControl;
using static Client.Login;
using Client.classes;
using System.Text.Json.Serialization;


namespace Client
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer chatUpdateTimer;
        private System.Windows.Forms.Timer FriendsListTimer;
        private System.Windows.Forms.Timer BlacklListTimer;
        private User UserData { get; set; }
       

        public MainForm(User userData)
        {
            InitializeComponent();
            InitializeUserChatsAndMessages();

            UserData = userData;

            chatUpdateTimer = new System.Windows.Forms.Timer();
            chatUpdateTimer.Interval = 5000; 
            chatUpdateTimer.Tick += ChatUpdateTimer_Tick;

            FriendsListTimer = new System.Windows.Forms.Timer();
            FriendsListTimer.Interval = 5000;
            FriendsListTimer.Tick += FriendsLisTimer_Tick;

            BlacklListTimer = new System.Windows.Forms.Timer();
            BlacklListTimer.Interval = 5000;
            BlacklListTimer.Tick += BlacklListpdateTimer_Tick;
            LabelUsername.Text = UserData.Username;
            LabelTag.Text = UserData.Tag;
          

            chatUpdateTimer.Start();
            FriendsListTimer.Start();
            BlacklListTimer.Start();

            UpdateBlackList();
            UpdateFriendLists();
            UpdateChatLists();
        }

       

        private void BlacklListpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateBlackList();
        }

        private void FriendsLisTimer_Tick(object? sender, EventArgs e)
        {
            UpdateFriendLists();
        }
        private void ChatUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateChatLists();
        }

        private void StartChatMessageUpdateTimer(int chatId)
        {
            System.Windows.Forms.Timer chatMessageUpdateTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 
            };

            chatMessageUpdateTimer.Tick += (sender, e) => UpdateChatBox(chatId);

            chatMessageUpdateTimer.Start();
        }



        private NetworkStream InitializeNetworkStream()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8000);
                NetworkStream stream = client.GetStream();
                return stream; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ������������� �������� ����������: {ex.Message}");
                return null; 
            }
        }

       

        private async void InitializeUserChatsAndMessages()
        {

        
        }

        private async void AddFriendList_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream(); 
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }
            int targetUserId = int.Parse(AddFriensField.Text);


            var request = new
            {
                Type = "ADD TO FRIENDLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = UserData.Id,
                    TargetId = targetUserId
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("������������ ������� �������� � ������ ������!");
                UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("������ ��� ���������� ������������ � ������ ������.");
            }
            stream.Close();
        }

        private void StartChat_Click(object sender, EventArgs e)
        {
            User selectedFriend = (User)FriendListChat.SelectedValue;

            OpenChatWithUser(selectedFriend.Id);
        }

        private async void RemoveButton_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            User targetUser = (User)RemoveFriendList.SelectedValue;
          

            var request = new
            {
                Type = "REMOVE FROM FRIENDLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = UserData.Id,
                    TargetId = targetUser.Id,
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("������������ ������� ������ �� ������ ������!");
                UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("������ ��� �������� ������������ �� ������ ������.");
            }
            stream.Close();
        }

        private async void AddBlackList_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            int targetUserId = int.Parse(AddBlacklistField.Text);


            var request = new
            {
                Type = "ADD TO BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = UserData.Id,
                    TargetId = targetUserId
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("������������ ������� �������� � ������ ������!");
                UpdateBlackList();
            }
            else
            {
                MessageBox.Show("������ ��� ���������� ������������ � ������ ������.");
            }
            stream.Close();
        }

        private async void RemoveBlackButton_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            int targetUserId = (int)RemoveBlacklist.SelectedValue;


            var request = new
            {
                Type = "REMOVE FROM BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = UserData.Id,
                    TargetId = targetUserId
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("������������ ������� ������ �� ������� ������!");
                UpdateBlackList();
            }
            else
            {
                MessageBox.Show("������ ��� �������� ������������ �� ������� ������.");
            }
            stream.Close();
        }

        private async void UpdateFriendLists()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }
            var request = new
            {
                Type = "GET FRIEND LIST",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = UserData.Id
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var friends = JsonSerializer.Deserialize<List<User>>(response.Content);

                FriendListChat.DataSource = friends;
                FriendListChat.DisplayMember = "UserName"; 

                RemoveFriendList.DataSource = friends;
                RemoveFriendList.DisplayMember = "UserName";
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������ ������.");
            }
            stream.Close();
        }


        private async void UpdateBlackList()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }
            var request = new
            {
                Type = "GET BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = UserData.Id
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var blacklistedUsers = JsonSerializer.Deserialize<List<User>>(response.Content);

                RemoveBlacklist.DataSource = blacklistedUsers;
                RemoveBlacklist.DisplayMember = "Username"; 
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������� ������.");
            }
            stream.Close();
        }


        private async void UpdateChatLists()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }
            var request = new
            {
                Type = "GET USER CHATS",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = UserData.Id
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024 * 48];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var chatList = JsonSerializer.Deserialize<List<Chat>>(response.Content, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });

                ChatList.DataSource = chatList;

                ChatList.DisplayMember = "ChatName"; // ��� � �������
                
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ����� � ���������.");
            }
            stream.Close();
        }

        private async Task UpdateChatBox(int chatId)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            var request = new
            {
                Type = "GET CHAT MESSAGES",
                Content = JsonSerializer.Serialize(new
                {
                    ChatId = chatId
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024 * 48];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var chatMessages = JsonSerializer.Deserialize<List<Message>>(response.Content);

                UpdateChatBoxDisplay(chatMessages);
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ��������� ����.");
            }

            stream.Close();
        }


        private void UpdateChatBoxDisplay(List<Message> chatMessages)
        {
            ChatBox.Clear();

            foreach (var message in chatMessages)
            {
                string messageString = $"{message.SentTime} - {message.SenderUsername}: {message.�ontent}";

                ChatBox.AppendText(messageString + Environment.NewLine);
            }
        }

        private async Task SendMessage(int chatId, string messageContent)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            var message = new Message
            {
                ChatId = chatId,
                �ontent = messageContent,
                SenderUsername = LabelUsername.Text,
                SentTime = DateTime.Now
            };

            var request = new
            {
                Type = "SEND MESSAGE",
                Content = JsonSerializer.Serialize(new
                {
                    ChatId = chatId,
                    Message = message
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                int newChatId = chatId;

                if (!string.IsNullOrEmpty(response.Content))
                {
                    newChatId = JsonSerializer.Deserialize<int>(response.Content);
                }

                await UpdateChatBox(newChatId);

                if (chatId != newChatId)
                {
                    chatId = newChatId;
                }
            }
            else
            {
                MessageBox.Show("������ ��� �������� ���������.");
            }

            stream.Close();
        }


        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            string messageContent = MessageTextField.Text;

            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            if (ChatList.SelectedItem != null)
            {
                dynamic selectedItem = ChatList.SelectedItem;
                int chatId = selectedItem.ChatId;

                if (chatId == 0)
                {
                    int newChatId = await SendFirstMessage(selectedItem, messageContent);
                }
                else
                {
                    
                    await SendMessage(chatId, messageContent);
                }
            }
            else
            {
                MessageBox.Show("����������, �������� ��� ��� �������� ���������.");
                return;
            }

            MessageTextField.Clear();
            stream.Close();
        }


        private async Task<int> SendFirstMessage(founderuser foundUser, string messageContent)
        {

            if (foundUser == null)
            {
                MessageBox.Show("������� ������� ������������.");
                return -1;
            }

            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return -1;
            }

            var request = new
            {
                Type = "SEND MESSAGE", 
                Content = JsonSerializer.Serialize(new
                {
                    TargetUserId = foundUser.Tag, 
                    MessageContent = messageContent,
                    ChatId = 0 
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                int chatId = JsonSerializer.Deserialize<int>(response.Content);

                OpenChatWithUser(chatId);

                return chatId; 
            }
            else
            {
                MessageBox.Show("������ ��� �������� ������� ���������.");
                return -1; 
            }

            stream.Close();
        }



        private async void SearchChatStart_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }

            string searchTag = SearchUser.Text;

            if (!string.IsNullOrEmpty(searchTag))
            {
                await SearchUserByTag(searchTag);
            }
            else
            {
                MessageBox.Show("����������, ������� ��� ��� ������.");
            }
            stream.Close();
        }

        private async Task<founderuser> SearchUserByTag(string tag)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return null;
            }

            var searchRequest = new
            {
                Tag = tag
            };

            var request = new
            {
                Type = "SEARCH USER BY TAG",
                Content = JsonSerializer.Serialize(searchRequest)
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var foundUser = JsonSerializer.Deserialize<founderuser>(response.Content);

                stream.Close();

                return foundUser;
            }
            else if (response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                MessageBox.Show("������������ � ��������� ����� �� ������.");
            }
            else
            {
                MessageBox.Show("��������� ������ ��� ������ ������������.");
            }

            stream.Close();

            return null;
        }

        private void ConnectChatButton_Click(object sender, EventArgs e)
        {
            var selectedItem = ChatList.SelectedItem;

            if (selectedItem != null)
            {
             
                dynamic selectedChat = selectedItem;

                int chatId = selectedChat.ChatId;

                OpenChatWithUser(chatId);
            }
            else
            {
                MessageBox.Show("����������, �������� ��� ��� �����������.");
            }
        }


        private async void OpenChatWithUser(int chatId)
        {
            if (chatId == 0)
            {
                MessageBox.Show("��� ��� ����� ������������ ��� �� �����.");
                return;
            }

            await UpdateChatBox(chatId);

            StartChatMessageUpdateTimer(chatId);
        }
        public class SearchUserByTagRequest
        {
            public string Tag { get; set; }
        }

        public class founderuser
        {
            public string Name { get; set; }
            public int Tag { get; set; }
        }

        public class Chat
        {
            public int ChatId { get; set; }
            public string ChatName { get; set; }
        }


        public class Message
        {
            public string �ontent { get; set; }
            public int ChatId { get; set; }
            public  DateTime  SentTime { get; set; }
            public string SenderUsername { get; set; }

        }

        public class Response
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }
        }

        public class AuthData
        {
            public string Token { get; set; }

            public int ID { get; set; }
            public string Tag { get; set; }
            public string Usermane { get; set; }

            public List<Chat> Chat { get; set; }

            public List<User> Friends { get; set; }

            public List<User> Blocked { get; set; }
        }

        public class ChatData
        {
            public string UserId { get; set; }
        }
    }
}
