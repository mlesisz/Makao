using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Server.Models;

namespace Server
{
    public class Authentication
    {
        private static string salt{ get; set; } = "R2estyd##2rtso";
        /*private static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }*/
        private string CreateHash(string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt+password));
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
        public async Task<bool> Register(string login, string password)
        {
            User user = await MakaoServerProtocol.Database.GetUserAsync(login);
            if (user == null)
            {
                user = new User()
                {
                    Nick = login,
                    Password = CreateHash(password)
                };
                await MakaoServerProtocol.Database.SaveUserAsync(user);
                return true;
            }
            else
                return false;
        }

        public async Task<(User,string)> Login (string login, string password)
        {
            string token ;
            User user = await MakaoServerProtocol.Database.GetUserAsync(login);
            if (user != null)
            {

                if (user.Password == CreateHash(password))
                {
                    token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    return (user, token);
                }
                else
                    return (null,null);
            }
            else
                return (null, null);
        }

    }
}
