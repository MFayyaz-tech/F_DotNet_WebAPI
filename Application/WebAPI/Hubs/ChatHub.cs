using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using System;
using BU.DTO.DTOs.Chat;
using BU.Services.IServices.Chat;
using System.Collections.Generic;
using BU.Services.Services.Chat;

public class ChatHub : Hub
{
    private static ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();
    private readonly IFeChatService _feChatService;

    public ChatHub(IFeChatService feChatService)
    {
        _feChatService = feChatService;
    }

    public async Task SendMessageToUser(FeChatDTO message)
    {
        _feChatService.Add(message);

        if (_userConnections.TryGetValue(message.ReceiverId.ToString(), out var connectionId))
        {
            var msg = message;
            Console.Write(msg);

            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }

    public override async Task OnConnectedAsync()
    {
        string userId = Context.GetHttpContext()?.Request.Query["userId"];
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections[userId] = Context.ConnectionId;
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var item = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        if (!item.Equals(default(KeyValuePair<string, string>)))
        {
            _userConnections.TryRemove(item.Key, out _);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task UserTyping(string reciverId, string senderId, bool isTyping)
    {
        if (_userConnections.TryGetValue(reciverId, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("UserTyping", reciverId,senderId, isTyping);
        }
    }
}
