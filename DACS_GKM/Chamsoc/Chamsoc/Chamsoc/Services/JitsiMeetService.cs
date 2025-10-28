using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chamsoc.Services
{
    public class JitsiMeetService
    {
        private readonly Dictionary<string, JitsiRoom> _activeRooms;
        
        public JitsiMeetService()
        {
            _activeRooms = new Dictionary<string, JitsiRoom>();
        }

        public string CreateRoom(string callerId, string receiverId, string callerName, string receiverName, bool isVideoCall = true)
        {
            // Tạo room ID duy nhất
            var roomId = GenerateRoomId(callerId, receiverId);
            
            var room = new JitsiRoom
            {
                RoomId = roomId,
                CallerId = callerId,
                ReceiverId = receiverId,
                CallerName = callerName,
                ReceiverName = receiverName,
                IsVideoCall = isVideoCall,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _activeRooms[roomId] = room;
            
            return roomId;
        }

        public JitsiRoom GetRoom(string roomId)
        {
            return _activeRooms.TryGetValue(roomId, out var room) ? room : null;
        }

        public void EndRoom(string roomId)
        {
            if (_activeRooms.TryGetValue(roomId, out var room))
            {
                room.IsActive = false;
                room.EndedAt = DateTime.UtcNow;
            }
        }

        public void CleanupExpiredRooms()
        {
            var expiredRooms = new List<string>();
            var cutoffTime = DateTime.UtcNow.AddHours(-2); // Rooms older than 2 hours

            foreach (var kvp in _activeRooms)
            {
                if (kvp.Value.CreatedAt < cutoffTime)
                {
                    expiredRooms.Add(kvp.Key);
                }
            }

            foreach (var roomId in expiredRooms)
            {
                _activeRooms.Remove(roomId);
            }
        }

        private string GenerateRoomId(string callerId, string receiverId)
        {
            // Tạo room ID duy nhất dựa trên caller và receiver
            var sortedIds = new[] { callerId, receiverId }.OrderBy(x => x).ToArray();
            var baseId = $"{sortedIds[0]}-{sortedIds[1]}";
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return $"{baseId}-{timestamp}";
        }
    }

    public class JitsiRoom
    {
        public string RoomId { get; set; }
        public string CallerId { get; set; }
        public string ReceiverId { get; set; }
        public string CallerName { get; set; }
        public string ReceiverName { get; set; }
        public bool IsVideoCall { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
