using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Unity2D
{
    public class Item_instance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Iid { get; set; }

        [ForeignKey("Item_master")]
        public required int Eid { get; set; }
        public Item_master? Item_master { get; set; }

        [ForeignKey("Player")]
        public required string Id { get; set; }
        public Player? Player { get; set; }

        [Required]
        public required int Dup_count { get; set; }
        [Required]
        public required int Enhance_level { get; set; }
        [Required]
        public required int Enhance_fail_count { get; set; }

        public ICollection<Log_enhance>? Log_enhance { get; set; }
    }
}
