using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;

public class CallHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _activeCalls = new();
    // Map userId -> set of connectionIds
    private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> _userIdToConnections = new();

    public async Task CallUser(string targetUserId, string callerName, string callerAvatar, object offer)
    {
        try
        {
            // Kiểm tra targetUserId có hợp lệ
            if (string.IsNullOrEmpty(targetUserId))
            {
                throw new HubException("Invalid user ID.");
            }

            // Kiểm tra offer không null
            if (offer == null)
            {
                throw new HubException("Offer cannot be null.");
            }

            // Kiểm tra xem người dùng đích có đang trong cuộc gọi không
            if (_activeCalls.ContainsKey(targetUserId))
            {
                throw new HubException("User is busy.");
            }

            // Đánh dấu cuộc gọi đang diễn ra
            _activeCalls.TryAdd(targetUserId, Context.UserIdentifier);
            _activeCalls.TryAdd(Context.UserIdentifier, targetUserId);

            // Gửi cuộc gọi đến người dùng đích
            await Clients.User(targetUserId).SendAsync("ReceiveCall", Context.UserIdentifier, callerName, callerAvatar, offer);
        }
        catch (Exception ex)
        {
            // Log lỗi chi tiết
            Console.WriteLine($"Error in CallUser: {ex.Message}");
            throw new HubException($"Failed to call user: {ex.Message}");
        }
    }

    public override async Task OnConnectedAsync()
    {
        // Gán UserId cho kết nối (ưu tiên từ query: /callHub?userId=... )
        var httpContext = Context.GetHttpContext();
        var userIdFromQuery = httpContext?.Request?.Query["userId"].ToString();
        var userId = string.IsNullOrWhiteSpace(userIdFromQuery) ? Context.UserIdentifier : userIdFromQuery;
        if (!string.IsNullOrEmpty(userId))
        {
            Console.WriteLine($"User {userId} connected with ConnectionId: {Context.ConnectionId}");
            // Add connection to user group and keep mapping
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            var bag = _userIdToConnections.GetOrAdd(userId, _ => new ConcurrentBag<string>());
            bag.Add(Context.ConnectionId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var httpContext = Context.GetHttpContext();
        var userIdFromQuery = httpContext?.Request?.Query["userId"].ToString();
        var userId = string.IsNullOrWhiteSpace(userIdFromQuery) ? Context.UserIdentifier : userIdFromQuery;
        if (!string.IsNullOrEmpty(userId))
        {
            // Nếu người dùng đang trong cuộc gọi, kết thúc cuộc gọi
            if (_activeCalls.TryRemove(userId, out string targetUserId))
            {
                await Clients.Group(targetUserId).SendAsync("CallEnded");
                _activeCalls.TryRemove(targetUserId, out _);
            }
            Console.WriteLine($"User {userId} disconnected. ConnectionId: {Context.ConnectionId}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task GetUserConnections()
    {
        var userId = Context.UserIdentifier;
        var connections = Clients.Users(new[] { userId });
        await Clients.Caller.SendAsync("ReceiveConnections", connections);
    }

    public async Task AnswerCall(string callerId, object answer)
    {
        try
        {
            if (!_activeCalls.ContainsKey(callerId))
            {
                throw new HubException("Call no longer exists.");
            }
            await Clients.Group(callerId).SendAsync("ReceiveAnswer", answer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AnswerCall: {ex.Message}");
            throw;
        }
    }

    public async Task SendIceCandidate(string targetUserId, object candidate)
    {
        try
        {
            if (!_activeCalls.ContainsKey(targetUserId))
            {
                throw new HubException("Call no longer exists.");
            }
            await Clients.Group(targetUserId).SendAsync("ReceiveIceCandidate", candidate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SendIceCandidate: {ex.Message}");
            throw;
        }
    }

    public async Task RejectCall(string callerId)
    {
        try
        {
            if (_activeCalls.TryRemove(callerId, out string targetUserId))
            {
                await Clients.Group(callerId).SendAsync("CallRejected");
                _activeCalls.TryRemove(targetUserId, out _);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in RejectCall: {ex.Message}");
            throw;
        }
    }

    public async Task EndCall(string targetUserId)
    {
        try
        {
            if (_activeCalls.TryRemove(Context.UserIdentifier, out _))
            {
                await Clients.Group(targetUserId).SendAsync("CallEnded");
                _activeCalls.TryRemove(targetUserId, out _);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in EndCall: {ex.Message}");
            throw;
        }
    }

    public async Task SendMicState(string targetUserId, bool isEnabled)
    {
        try
        {
            if (!_activeCalls.ContainsKey(targetUserId))
            {
                throw new HubException("Call no longer exists.");
            }
            await Clients.Group(targetUserId).SendAsync("ReceiveMicState", isEnabled);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SendMicState: {ex.Message}");
            throw;
        }
    }

    public async Task SendVideoCallNotification(string targetUserId, string callerName, string roomId, bool isVideoCall)
    {
        try
        {
            Console.WriteLine($"Sending video call notification to {targetUserId}, room: {roomId}");
            // Use group by userId to ensure delivery regardless of authentication mapping
            await Clients.Group(targetUserId).SendAsync("ReceiveVideoCallNotification", Context.UserIdentifier, callerName, roomId, isVideoCall);
            Console.WriteLine($"Video call notification sent successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SendVideoCallNotification: {ex.Message}");
            throw;
        }
    }

    // MiroTalk P2P Methods
    public async Task JoinRoom(string roomId)
    {
        try
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            Console.WriteLine($"User {Context.UserIdentifier} joined room {roomId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error joining room: {ex.Message}");
            throw;
        }
    }

    public async Task SendOffer(string roomId, object offer)
    {
        try
        {
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveOffer", offer, Context.UserIdentifier);
            Console.WriteLine($"Offer sent to room {roomId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending offer: {ex.Message}");
            throw;
        }
    }

    public async Task SendAnswer(string roomId, object answer)
    {
        try
        {
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveAnswer", answer);
            Console.WriteLine($"Answer sent to room {roomId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending answer: {ex.Message}");
            throw;
        }
    }

    public async Task SendIceCandidateToRoom(string roomId, object candidate)
    {
        try
        {
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveIceCandidate", candidate);
            Console.WriteLine($"ICE candidate sent to room {roomId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending ICE candidate: {ex.Message}");
            throw;
        }
    }
}