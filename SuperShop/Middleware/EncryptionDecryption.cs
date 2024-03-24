using Newtonsoft.Json;
using SuperShop.Model;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace SuperShop.Middleware
{
    public class EncryptionDecryption
    {
        private readonly RequestDelegate next;
        private byte[] key = Encoding.UTF8.GetBytes("67-6F-14-F0-86-BE-CA-DC-EF-76-2A-99-4E-CB-6D-BF-33-44-E0-2E-C4-CC-74-7C-40-62-C0-75-2B-99-F3-91".Replace('-',' '));
        private byte[] iv = Encoding.UTF8.GetBytes("6D-02-1A-41-79-2D-4F-49-CF-88-E7-90-84-E7-1C-2E".Replace('-', ' '));

        public EncryptionDecryption(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await next(context);

                    if (context.Response.ContentType?.ToLower().Contains("application/json") == true)
                    {
                        memStream.Seek(0, SeekOrigin.Begin);
                        var responseBody = await new StreamReader(memStream).ReadToEndAsync();
                        var result = JsonConvert.DeserializeObject<MessageHelperModel>(responseBody);

                        if (result!= null)
                        {
                            var jsonResponse = JsonConvert.SerializeObject(result);
                            jsonResponse = Encrypt(jsonResponse);
                            var responseBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                            context.Response.ContentLength = responseBuffer.Length;
                            context.Response.Body = originalBody;
                            await context.Response.Body.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                        }
                    }
                    else
                    {
                        memStream.Seek(0, SeekOrigin.Begin);
                        await memStream.CopyToAsync(originalBody);
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Generate256BitKey();
                aesAlg.IV = GenerateIV();
                /*string s = BitConverter.ToString(aesAlg.IV);
                s = BitConverter.ToString(aesAlg.Key);*/

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        public byte[] Generate256BitKey()
        {
            // Create a new instance of the RNGCryptoServiceProvider for generating random bytes
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // Create a byte array to store the random key
                byte[] keyBytes = new byte[32]; // 256 bits / 8 = 32 bytes

                // Generate random bytes and fill the key array
                rng.GetBytes(keyBytes);

                return keyBytes;
            }
        }
        public byte[] GenerateIV()
        {
            // Create a new instance of the RNGCryptoServiceProvider for generating random bytes
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // Create a byte array to store the random IV
                byte[] ivBytes = new byte[16]; // 128 bits / 8 = 16 bytes (AES block size)

                // Generate random bytes and fill the IV array
                rng.GetBytes(ivBytes);

                return ivBytes;
            }
        }
    }
}
