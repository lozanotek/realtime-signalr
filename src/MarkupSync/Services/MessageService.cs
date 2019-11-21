using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json;
using MarkupSync.Models;

namespace MarkupSync.Services
{
    public class MessageService : IMessageService
    {
        private const string ActiveKey = "__message__active__key";
        private readonly IMemoryCache memoryCache;
        private readonly IHostingEnvironment hostingEnvironment;

        public MessageService(IMemoryCache memoryCache, IHostingEnvironment hostingEnvironment)
        {
            this.memoryCache = memoryCache;
            this.hostingEnvironment = hostingEnvironment;
        }

        private string GetDataFilePath()
        {
            return Path.Combine(hostingEnvironment.WebRootPath, "data", "message.json");
        }

        public void SetActiveMessage(Message message)
        {
            memoryCache.Set(ActiveKey, message, new TimeSpan(4, 0, 0));
            SaveMessage(message);
        }

        private void SaveMessage(Message message)
        {
            var json = JsonConvert.SerializeObject(message, Formatting.Indented);
            File.WriteAllText(GetDataFilePath(), json);
        }

        private Message GetMessageFromFile()
        {
            var path = GetDataFilePath();
            using (var stream = File.OpenRead(path))
            {
                return JsonConvert.DeserializeObject<Message>(new StreamReader(stream).ReadToEnd());
            }
        }

        public Message GetActiveMessage()
        {
            var cacheEntry = memoryCache.GetOrCreate(ActiveKey, entry =>
            {
                entry.SetAbsoluteExpiration(new System.TimeSpan(4, 0, 0));

                var message = GetMessageFromFile();
                return message;
            });

            return cacheEntry;
        }
    }
}
