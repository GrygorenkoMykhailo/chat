using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace Client.Managers
{
    public class BlacklistManager
    {
     
       public async Task AddToBlacklistAsync(int targetUserTag, int senderId)
        {
            var request = new
            {
                Type = "ADD TO BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = senderId,
                    TargetTag = targetUserTag
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

            NetworkStream stream = _networkManager.InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            string responseJson = await _networkManager.ReadResponseAsync(stream);
            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("Пользователь успешно добавлен в черный список!");
                await _mainForm.UpdateBlackList();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя в черный список.");
            }
        }

        public async Task RemoveFromBlacklistAsync(int targetUserTag, int senderId)
        {
            var request = new
            {
                Type = "REMOVE FROM BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    SenderId = senderId,
                    TargetTag = targetUserTag
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

            NetworkStream stream = _networkManager.InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            string responseJson = await _networkManager.ReadResponseAsync(stream);
            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                MessageBox.Show("Пользователь успешно удален из черного списка!");
                await _mainForm.UpdateBlackList();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении пользователя из черного списка.");
            }
        }

        public async Task UpdateBlackListAsync(int userId)
        {
            var request = new
            {
                Type = "GET BLACKLIST",
                Content = JsonSerializer.Serialize(new
                {
                    UserId = userId
                })
            };

            string requestJson = JsonSerializer.Serialize(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

            NetworkStream stream = _networkManager.InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }

            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

            string responseJson = await _networkManager.ReadResponseAsync(stream);
            var response = JsonSerializer.Deserialize<Response>(responseJson);

            if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
            {
                var blacklistedUsers = JsonSerializer.Deserialize<List<BlacklistedUser>>(response.Content);
                _mainForm.UpdateBlacklistDataSource(blacklistedUsers);
            }
            else
            {
                MessageBox.Show("Ошибка при получении черного списка.");
            }
        }
    }
}
