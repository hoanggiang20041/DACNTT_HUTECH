using Microsoft.AspNetCore.Mvc;
using Chamsoc.Services;
using Microsoft.AspNetCore.Authorization;

namespace Chamsoc.Controllers
{
    [Authorize]
    [Route("MiroTalk")]
    public class MiroTalkController : Controller
    {
        private readonly MiroTalkMeetService _miroTalkService;

        public MiroTalkController(MiroTalkMeetService miroTalkService)
        {
            _miroTalkService = miroTalkService;
        }

        [HttpPost("CreateRoom")]
        public IActionResult CreateRoom([FromBody] MiroTalkCreateRoomRequest request)
        {
            try
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var currentUserName = User.Identity?.Name ?? "Người dùng";

                if (string.IsNullOrEmpty(currentUserId))
                {
                    return BadRequest("Không thể xác định người dùng");
                }

                var roomId = _miroTalkService.CreateRoom(
                    currentUserId,
                    request.ReceiverId ?? string.Empty,
                    currentUserName,
                    request.ReceiverName ?? string.Empty,
                    request.IsVideoCall
                );

                return Ok(new { roomId = roomId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi tạo phòng: {ex.Message}");
            }
        }

        [HttpGet("JoinRoom/{roomId}")]
        public IActionResult JoinRoom(string roomId)
        {
            try
            {
                var room = _miroTalkService.GetRoom(roomId);
                if (room == null)
                {
                    return NotFound("Phòng không tồn tại hoặc đã hết hạn");
                }

                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var currentUserName = User.Identity?.Name ?? "Người dùng";

                if (currentUserId != room.CallerId && currentUserId != room.ReceiverId)
                {
                    return Forbid("Bạn không có quyền tham gia phòng này");
                }

                ViewBag.RoomId = roomId;
                ViewBag.Room = room;
                ViewBag.IsVideoCall = room.IsVideoCall;
                ViewBag.CurrentUserName = currentUserName;

                // Trả về trang wrapper (same-origin) để có thể auto-điền/đóng tab
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi tham gia phòng: {ex.Message}");
            }
        }

    [HttpPost("EndRoom")]
    public IActionResult EndRoom([FromBody] MiroTalkEndRoomRequest request)
    {
        try
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var room = _miroTalkService.GetRoom(request.RoomId ?? string.Empty);

            if (room == null)
            {
                return NotFound("Phòng không tồn tại");
            }

            if (currentUserId != room.CallerId && currentUserId != room.ReceiverId)
            {
                return Forbid("Bạn không có quyền kết thúc phòng này");
            }

            _miroTalkService.EndRoom(request.RoomId ?? string.Empty);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Lỗi kết thúc phòng: {ex.Message}");
        }
    }
}

public class MiroTalkCreateRoomRequest
{
    public string? ReceiverId { get; set; }
    public string? ReceiverName { get; set; }
    public bool IsVideoCall { get; set; } = true;
}

public class MiroTalkEndRoomRequest
{
    public string? RoomId { get; set; }
}
}

