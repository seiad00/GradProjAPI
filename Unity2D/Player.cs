using System.ComponentModel.DataAnnotations;

namespace Unity2D
{
    public class Player
    {
        [Key]
        public required string Id { get; set; }

        [Required] 
        public required string Password { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }

        public int Currency { get; set; } = 100;

        public int Level { get; set; } = 1;

        public int Exp { get; set; } = 0;

        [Required]
        public required string Username { get; set; }

    }
}
