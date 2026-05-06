using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Unity2D.Data;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Unity2D
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerDataController : ControllerBase
    {
        private readonly DBContext _context;

        public PlayerDataController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User ID not found in token.");
            var player = _context.players.FirstOrDefault(p => p.Id == userId);
            if (player == null)
                return NotFound("Player not found.");
            var playerItems = (from instance in _context.item_instances
                               where instance.Id == userId
                               join master in _context.item_masters on instance.Eid equals master.Eid
                               select new PlayerItemDto
                               {
                                   Id = player.Id,
                                   Iid = instance.Iid,
                                   Eid = instance.Eid,
                                   Type = master.Type,
                                   Dup_count = instance.Dup_count,
                                   Enhance_level = instance.Enhance_level,
                                   Enhance_fail_count = instance.Enhance_fail_count,
                                   Base_atk = master.Base_atk,
                                   Base_hp = master.Base_hp,
                                   Base_armor = master.Base_armor
                               }).ToList();
            var playerData = new PlayerDataDto
            {
                Id = player.Id,
                Username = player.Username,
                Level = player.Level,
                Exp = player.Exp,
                Currency = player.Currency,
                Items = playerItems
            };
            
            return Ok(playerData);
        }

        [HttpPost("update")]
        public IActionResult UpdateData([FromBody] PlayerDataDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User ID not found in token.");
            var player = _context.players.FirstOrDefault(p => p.Id == userId);
            if (player == null)
                return NotFound("Player not found.");
            player.Level = dto.Level;
            player.Exp = dto.Exp;
            player.Currency = dto.Currency;
            foreach (var item in dto.Items)
            {
                var instance = _context.item_instances.FirstOrDefault(i => i.Eid == item.Eid && i.Id == userId);
                if (instance != null)
                {
                    instance.Dup_count = item.Dup_count;
                    instance.Enhance_level = item.Enhance_level;
                    instance.Enhance_fail_count = item.Enhance_fail_count;
                }
                else
                {
                    instance = new Item_instance
                    {
                        Id = userId,
                        Eid = item.Eid,
                        Dup_count = item.Dup_count,
                        Enhance_level = item.Enhance_level,
                        Enhance_fail_count = item.Enhance_fail_count
                    };
                    _context.item_instances.Add(instance);
                }
            }
            _context.SaveChanges();
            return Ok("Player data updated successfully.");
        }

        [HttpPost("log")]
        public async Task<IActionResult> PostEnhancementLog([FromBody] EnhanceLogDto request)
        {
            // 1. DTO 데이터를 DB 모델로 변환
            var newLog = new Log_enhance
            {
                Iid = request.Iid,
                Level_before = request.Level_before,
                Level_after = request.Level_after,
                Success = request.Success
            };

            // 2. DB에 추가 및 저장
            _context.log_enhances.Add(newLog);
            await _context.SaveChangesAsync();

            // 3. 클라이언트에 성공 응답 반환
            return Ok(new { Message = "Log saved successfully" });
        }
    }
}
