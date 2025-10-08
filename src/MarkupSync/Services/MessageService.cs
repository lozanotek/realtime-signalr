using Microsoft.Extensions.Caching.Memory;

using MarkupSync.Models;
using System.Text.Json;

namespace MarkupSync.Services;

public class MessageService : IMessageService
{
	private const string ActiveKey = "__message__active__key";
	private readonly IMemoryCache memoryCache;
	private readonly IWebHostEnvironment hostingEnvironment;

	public MessageService(IMemoryCache memoryCache, IWebHostEnvironment hostingEnvironment)
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
		var json = JsonSerializer.Serialize(message);
		File.WriteAllText(GetDataFilePath(), json);
	}

	private Message GetMessageFromFile()
	{
		var path = GetDataFilePath();
		using (var stream = File.OpenRead(path))
		{
			return JsonSerializer.Deserialize<Message>(new StreamReader(stream).ReadToEnd());
		}
	}

	public Message GetActiveMessage()
	{
		var cacheEntry = memoryCache.GetOrCreate(ActiveKey, entry =>
		{
			entry.SetAbsoluteExpiration(new TimeSpan(4, 0, 0));

			var message = GetMessageFromFile();
			return message;
		});

		return cacheEntry;
	}
}
