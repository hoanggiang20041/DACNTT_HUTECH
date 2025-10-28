using Microsoft.AspNetCore.Mvc;
using Chamsoc.Services;
using Microsoft.AspNetCore.Authorization;

namespace Chamsoc.Controllers
{
    [Authorize]
    [Route("Jitsi")]
    public class JitsiController : Controller
    {
        private readonly JitsiMeetService _jitsiService;

        public JitsiController(JitsiMeetService jitsiService)
        {
            _jitsiService = jitsiService;
        }

        [HttpPost("CreateRoom")]
        public IActionResult CreateRoom([FromBody] CreateRoomRequest request)
        {
            try
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var currentUserName = User.Identity?.Name ?? "Người dùng";

                if (string.IsNullOrEmpty(currentUserId))
                {
                    return BadRequest("Không thể xác định người dùng");
                }

                var roomId = _jitsiService.CreateRoom(
                    currentUserId,
                    request.ReceiverId,
                    currentUserName,
                    request.ReceiverName,
                    request.IsVideoCall
                );

                return Ok(new { RoomId = roomId });
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
                var room = _jitsiService.GetRoom(roomId);
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
                ViewBag.CurrentUserId = currentUserId;
                ViewBag.CurrentUserName = currentUserName;
                ViewBag.IsVideoCall = room.IsVideoCall;

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi tham gia phòng: {ex.Message}");
            }
        }

        [HttpPost("EndRoom")]
        public IActionResult EndRoom([FromBody] EndRoomRequest request)
        {
            try
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var room = _jitsiService.GetRoom(request.RoomId);

                if (room == null)
                {
                    return NotFound("Phòng không tồn tại");
                }

                if (currentUserId != room.CallerId && currentUserId != room.ReceiverId)
                {
                    return Forbid("Bạn không có quyền kết thúc phòng này");
                }

                _jitsiService.EndRoom(request.RoomId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi kết thúc phòng: {ex.Message}");
            }
        }
    }

    public class CreateRoomRequest
    {
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public bool IsVideoCall { get; set; } = true;
    }

    public class EndRoomRequest
    {
        public string RoomId { get; set; }
    }
}
