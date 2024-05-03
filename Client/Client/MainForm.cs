using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace Client
{
    public partial class MainForm : Form
    {
        private NetworkStream _stream;
        private int authenticatedUserId;

        public MainForm()
        {
            InitializeComponent();
            CheckAuthToken();

            //InitializeUserChatsAndMessages();
        }



        //private async void InitializeUserChatsAndMessages()
        //{
        //    List<int> chatIds = GetAllChatIds(); 

        //    foreach (int chatId in chatIds)
        //    {
        //        var request = new
        //        {
        //            Type = "GET CHAT MESSAGES",
        //            Content = new
        //            {
        //                ChatId = chatId
        //            }
        //        };

        //        string requestJson = JsonSerializer.Serialize(request);
        //        byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
        //        await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

        //        byte[] buffer = new byte[1024];
        //        int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
        //        string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        //        var response = JsonSerializer.Deserialize<Response>(responseJson);

        //        if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
        //        {
        //            var messagesData = JsonSerializer.Deserialize<List<Message>>(response.Content);

                    
        //        }
        //        else
        //        {
        //            MessageBox.Show($"Ошибка при получении сообщений для чата {chatId}.");
        //        }
        //    }
        //}

      
        private void CheckAuthToken()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "authToken.txt");

            if (File.Exists(filePath))
            {
                string authToken = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(authToken))
                {
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
            int targetUserId = int.Parse(AddFriensField.Text);

            var request = new
            {
                Type = "ADD TO FRIENDLIST",
                Content = new
                {
                    SenderId = authenticatedUserId,
                    TargetId = targetUserId
                }
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("Пользователь успешно добавлен в список друзей!");
                //UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя в список друзей.");
            }
        }

        private void StartChat_Click(object sender, EventArgs e)
        {
            int selectedFriendId = (int)FriendListChat.SelectedValue;

            OpenChatWithUser(selectedFriendId);
        }

        private async void RemoveButton_Click(object sender, EventArgs e)
        {
            int targetUserId = (int)RemoveFriendList.SelectedValue;

            var request = new
            {
                Type = "REMOVE FROM FRIENDLIST",
                Content = new
                {
                    SenderId = authenticatedUserId,
                    TargetId = targetUserId
                }
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("Пользователь успешно удален из списка друзей!");
                //UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении пользователя из списка друзей.");
            }
        }

        private async void AddBlackList_Click(object sender, EventArgs e)
        {
            int targetUserId = int.Parse(AddBlacklistField.Text);

            var request = new
            {
                Type = "ADD TO BLACKLIST",
                Content = new
                {
                    SenderId = authenticatedUserId,
                    TargetId = targetUserId
                }
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("Пользователь успешно добавлен в черный список!");
                //UpdateBlackList();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя в черный список.");
            }
        }

        private async void RemoveBlackButton_Click(object sender, EventArgs e)
        {
            int targetUserId = (int)RemoveBlacklist.SelectedValue;

            var request = new
            {
                Type = "REMOVE FROM BLACKLIST",
                Content = new
                {
                    SenderId = authenticatedUserId,
                    TargetId = targetUserId
                }
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("Пользователь успешно удален из черного списка!");
                //UpdateBlackList();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении пользователя из черного списка.");
            }
        }

        //private async void UpdateFriendLists()
        //{
        //    var request = new
        //    {
        //        Type = "GET FRIEND LIST",
        //        Content = new
        //        {
        //            UserId = authenticatedUserId
        //        }
        //    };

        //    string requestJson = JsonSerializer.Serialize(request);
        //    byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
        //    await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

        //    byte[] buffer = new byte[1024];
        //    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
        //    string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        //    var response = JsonSerializer.Deserialize<Response>(responseJson);

        //    if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
        //    {
        //        var friends = JsonSerializer.Deserialize<List<Friend>>(response.Content);

        //        FriendListChat.DataSource = friends;

        //        RemoveFriendList.DataSource = friends;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка при получении списка друзей.");
        //    }
        //}


        //private async void UpdateBlackList()
        //{
        //    var request = new
        //    {
        //        Type = "GET BLACKLIST",
        //        Content = new
        //        {
        //            UserId = authenticatedUserId
        //        }
        //    };

        //    string requestJson = JsonSerializer.Serialize(request);
        //    byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
        //    await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

        //    byte[] buffer = new byte[1024];
        //    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
        //    string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        //    var response = JsonSerializer.Deserialize<Response>(responseJson);

        //    if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
        //    {
        //        var blacklistedUsers = JsonSerializer.Deserialize<List<BlacklistedUser>>(response.Content);

        //        RemoveBlacklist.DataSource = blacklistedUsers;
        //        RemoveBlacklist.DisplayMember = "Username"; 
        //        RemoveBlacklist.ValueMember = "UserId"; 
        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка при получении черного списка.");
        //    }
        //}


        //private async void UpdateChatLists()
        //{
        //    var request = new
        //    {
        //        Type = "GET USER CHATS AND MESSAGES",
        //        Content = new
        //        {
        //            UserId = authenticatedUserId
        //        }
        //    };

        //    string requestJson = JsonSerializer.Serialize(request);
        //    byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
        //    await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

        //    byte[] buffer = new byte[1024];
        //    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
        //    string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        //    var response = JsonSerializer.Deserialize<Response>(responseJson);

        //    if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
        //    {
        //        var chatData = JsonSerializer.Deserialize<ChatData>(response.Content);

        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка при получении чатов и сообщений.");
        //    }
        //}


        private void OpenChatWithUser(int userId)
        {
           
        }

       

        public class Response
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }
        }

    }
}
