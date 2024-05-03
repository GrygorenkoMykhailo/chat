using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Security.AccessControl;


namespace Client
{
    public partial class MainForm : Form
    {
        

        public MainForm()
        {
            InitializeComponent();
            CheckAuthToken();
            InitializeNetworkStream();

            InitializeUserChatsAndMessages();
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
                MessageBox.Show("����� ������. ���������� ������ � �������������.");
            }
            else
            {
                MessageBox.Show("��������� �����������.");
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
                MessageBox.Show("������ ��� ������������� �������� ����������.");
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
                MessageBox.Show("������������ ������� �������� � ������ ������!");
                UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("������ ��� ���������� ������������ � ������ ������.");
            }
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
                MessageBox.Show("������ ��� ������������� �������� ����������.");
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
                MessageBox.Show("������������ ������� ������ �� ������ ������!");
                UpdateFriendLists();
            }
            else
            {
                MessageBox.Show("������ ��� �������� ������������ �� ������ ������.");
            }
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
                MessageBox.Show("������������ ������� �������� � ������ ������!");
                UpdateBlackList();
            }
            else
            {
                MessageBox.Show("������ ��� ���������� ������������ � ������ ������.");
            }
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
                MessageBox.Show("������������ ������� ������ �� ������� ������!");
                UpdateBlackList();
            }
            else
            {
                MessageBox.Show("������ ��� �������� ������������ �� ������� ������.");
            }
        }

        private async void UpdateFriendLists()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }
            var authData = GetAuthData();
            var request = new
            {
                Type = "GET FRIEND LIST",
                Content = JsonSerializer.Serialize( new
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

            if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
            {
                var friends = JsonSerializer.Deserialize<List<Friend>>(response.Content);

                FriendListChat.DataSource = friends;
                RemoveFriendList.DataSource = friends;
            }
            else if (response.StatusCode == (int)System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("No friends found for the specified user.");
            }
            else
            {
                MessageBox.Show("An error occurred while fetching the friend list.");
            }
        }



        private async void UpdateBlackList()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
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

            if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
            {
                var blacklistedUsers = JsonSerializer.Deserialize<List<BlacklistedUser>>(response.Content);

                RemoveBlacklist.DataSource = blacklistedUsers;
                RemoveBlacklist.DisplayMember = "Username";
                RemoveBlacklist.ValueMember = "UserId";
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������� ������.");
            }
        }


        private async void UpdateChatLists()
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("������ ��� ������������� �������� ����������.");
                return;
            }
            var authData = GetAuthData();
            var request = new
            {
                Type = "GET USER CHATS ",
                Content =JsonSerializer.Serialize( new
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

            if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
            {
                var chatData = JsonSerializer.Deserialize<ChatData>(response.Content);

            }
            else
            {
                MessageBox.Show("������ ��� ��������� ����� � ���������.");
            }
        }


        private void OpenChatWithUser(int userId)
        {

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
