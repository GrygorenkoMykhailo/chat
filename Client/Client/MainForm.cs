using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Security.AccessControl;
using static Client.Login;


namespace Client
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer chatUpdateTimer;
        private System.Windows.Forms.Timer FriendsListTimer;
        private System.Windows.Forms.Timer BlacklListTimer;
       

        public MainForm()
        {
            InitializeComponent();
            CheckAuthToken();
            InitializeUserChatsAndMessages();


            chatUpdateTimer = new System.Windows.Forms.Timer();
            chatUpdateTimer.Interval = 5000; 
            chatUpdateTimer.Tick += ChatUpdateTimer_Tick;

            FriendsListTimer = new System.Windows.Forms.Timer();
            FriendsListTimer.Interval = 5000;
            FriendsListTimer.Tick += FriendsLisTimer_Tick;

            BlacklListTimer = new System.Windows.Forms.Timer();
            BlacklListTimer.Interval = 5000;
            BlacklListTimer.Tick += BlacklListpdateTimer_Tick;
          

            chatUpdateTimer.Start();
            FriendsListTimer.Start();
            BlacklListTimer.Start();
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
                MessageBox.Show($"Ошибка при инициализации сетевого соединения: {ex.Message}");
                return null; 
            }
        }

       

        private async void InitializeUserChatsAndMessages()
        {

        
        }
        
        

        private AuthData GetAuthData()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "authData.json");

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<AuthData>(jsonData);
            }

            return null;
        }


        private void CheckAuthToken()
        {
            var authData = GetAuthData();

            if (authData != null && !string.IsNullOrEmpty(authData.Token))
            {
                LabelUsername.Text = authData.Usermane;
                LabelTag.Text = authData.Tag;

                MessageBox.Show("Токен найден. Продолжаем работу с пользователем.");
            }
            else
            {
                MessageBox.Show("Требуется авторизация.");
                Login loginForm = new Login();
                loginForm.Show();
                this.Close();
            }
        }


        private async void AddFriendList_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream(); 
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }
            int targetUserId = int.Parse(AddFriensField.Text);
            var authData = GetAuthData();


            var request = new
            {
                Type = "ADD TO FRIENDLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = authData.ID,
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
                MessageBox.Show("Пользователь успешно добавлен в список друзей!");
                UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя в список друзей.");
            }
            stream.Close();
        }

        private void StartChat_Click(object sender, EventArgs e)
        {
            int selectedFriendId = (int)FriendListChat.SelectedValue;

            OpenChatWithUser(selectedFriendId);
        }

        private async void RemoveButton_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            int targetUserId = (int)RemoveFriendList.SelectedValue;
            var authData = GetAuthData();
          

            var request = new
            {
                Type = "REMOVE FROM FRIENDLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = authData.ID,
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
                MessageBox.Show("Пользователь успешно удален из списка друзей!");
                UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении пользователя из списка друзей.");
            }
            stream.Close();
        }

        private async void AddBlackList_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            int targetUserId = int.Parse(AddBlacklistField.Text);
            var authData = GetAuthData();


            var request = new
            {
                Type = "ADD TO BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = authData.ID,
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
                MessageBox.Show("Пользователь успешно добавлен в черный список!");
                UpdateBlackList();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя в черный список.");
            }
            stream.Close();
        }

        private async void RemoveBlackButton_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            int targetUserId = (int)RemoveBlacklist.SelectedValue;
            var authData = GetAuthData();


            var request = new
            {
                Type = "REMOVE FROM BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = authData.ID,
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
                MessageBox.Show("Пользователь успешно удален из черного списка!");
                UpdateBlackList();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении пользователя из черного списка.");
            }
            stream.Close();
        }

        private async void UpdateFriendLists()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }
            var authData = GetAuthData();
            var request = new
            {
                Type = "GET FRIEND LIST",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = authData.ID
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
                var friends = JsonSerializer.Deserialize<List<Friend>>(response.Content);

                FriendListChat.DataSource = friends;
                FriendListChat.DisplayMember = "UserName"; 
                FriendListChat.ValueMember = "UserId"; 

                RemoveFriendList.DataSource = friends;
                RemoveFriendList.DisplayMember = "UserName";
                RemoveFriendList.ValueMember = "UserId";
            }
            else
            {
                MessageBox.Show("Ошибка при получении списка друзей.");
            }
            stream.Close();
        }


        private async void UpdateBlackList()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }
            var authData = GetAuthData();
            var request = new
            {
                Type = "GET BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = authData.ID
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
                var blacklistedUsers = JsonSerializer.Deserialize<List<BlacklistedUser>>(response.Content);

                RemoveBlacklist.DataSource = blacklistedUsers;
                RemoveBlacklist.DisplayMember = "Username"; 
                RemoveBlacklist.ValueMember = "UserId"; 
            }
            else
            {
                MessageBox.Show("Ошибка при получении черного списка.");
            }
            stream.Close();
        }


        private async void UpdateChatLists()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }
            var authData = GetAuthData();
            var request = new
            {
                Type = "GET USER CHATS",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = authData.ID
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
                var chatList = JsonSerializer.Deserialize<List<Chat>>(response.Content);

                ChatList.DataSource = chatList;

                ChatList.DisplayMember = "ChatName"; // Имя в будущем
                
            }
            else
            {
                MessageBox.Show("Ошибка при получении чатов и сообщений.");
            }
            stream.Close();
        }

        private async Task UpdateChatBox(int chatId)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
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

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var chatMessages = JsonSerializer.Deserialize<List<Message>>(response.Content);

                UpdateChatBoxDisplay(chatMessages);
            }
            else
            {
                MessageBox.Show("Ошибка при получении сообщений чата.");
            }

            stream.Close();
        }


        private void UpdateChatBoxDisplay(List<Message> chatMessages)
        {
            ChatBox.Clear();

            foreach (var message in chatMessages)
            {
                string messageString = $"{message.SentTime} - {message.SenderUsername}: {message.Сontent}";

                ChatBox.AppendText(messageString + Environment.NewLine);
            }
        }

        private async Task SendMessage(int chatId, string messageContent)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            var message = new Message
            {
                ChatId = chatId,
                Сontent = messageContent,
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
                MessageBox.Show("Ошибка при отправке сообщения.");
            }

            stream.Close();
        }


        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            string messageContent = MessageTextField.Text;

            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
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
                MessageBox.Show("Пожалуйста, выберите чат для отправки сообщения.");
                return;
            }

            MessageTextField.Clear();
            stream.Close();
        }


        private async Task<int> SendFirstMessage(founderuser foundUser, string messageContent)
        {

            if (foundUser == null)
            {
                MessageBox.Show("Сначала найдите пользователя.");
                return -1;
            }

            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
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
                MessageBox.Show("Ошибка при отправке первого сообщения.");
                return -1; 
            }

            stream.Close();
        }



        private async void SearchChatStart_Click(object sender, EventArgs e)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            string searchTag = SearchUser.Text;

            if (!string.IsNullOrEmpty(searchTag))
            {
                await SearchUserByTag(searchTag);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите тег для поиска.");
            }
            stream.Close();
        }

        private async Task<founderuser> SearchUserByTag(string tag)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
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
                MessageBox.Show("Пользователь с указанным тегом не найден.");
            }
            else
            {
                MessageBox.Show("Произошла ошибка при поиске пользователя.");
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
                MessageBox.Show("Пожалуйста, выберите чат для подключения.");
            }
        }


        private async void OpenChatWithUser(int chatId)
        {
            if (chatId == 0)
            {
                MessageBox.Show("Чат для этого пользователя еще не начат.");
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

        public class User
        {
            public int Tag { get; set; }
        }

        public class Chat
        {
            public int ChatId { get; set; }
            public string ChatName { get; set; }
        }


        public class Message
        {
            public string Сontent { get; set; }
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

            public List<Friends> Friends { get; set; }

            public List<Blocked> Blocked { get; set; }
        }

        public class ChatData
        {
            public string UserId { get; set; }
        }

        public class Friend
        {
            public string UserId { get; set; }
        }

        public class BlacklistedUser
        {
            public string UserId { get; set; }
        }
    }
}
