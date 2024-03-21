using Newtonsoft.Json;
using SuperShop.Model;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SuperShop.Middleware
{
    public class EncryptionDecryption
    {
        private readonly RequestDelegate next;

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

                        if (result != null)
                        {
                            var jsonResponse = JsonConvert.SerializeObject(result.data);
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
    }
}
