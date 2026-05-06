namespace Unity2D
{
    public class LoginDto
    {
        public required string Id { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterDto
    {
        public required string Id { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
    }

    public class PlayerDataDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Currency { get; set; }

        public required List<PlayerItemDto> Items { get; set; }
    }

    public class PlayerItemDto
    {
        public required string Id { get; set; }
        public required int Iid { get; set; }
        public required int Eid { get; set; }
        public required string Type { get; set; }
        public required int Dup_count { get; set; }
        public required int Enhance_level { get; set; }
        public required int Enhance_fail_count { get; set; }
        public required double Base_atk { get; set; }
        public required double Base_hp { get; set; }
        public required double Base_armor { get; set; }
    }

    public class EnhanceLogDto
    {
        public required int Iid { get; set; }
        public required int Level_before { get; set; }
        public required int Level_after { get; set; }
        public required bool Success { get; set; }
    }
}
