using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Unity2D
{
    public class Log_enhance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Lid { get; set; }

        [ForeignKey("Item_instance")] 
        public required int Iid { get; set; }
        public Item_instance? Item_instance { get; set; }

        [Required]
        public required int Level_before { get; set; }
        [Required]
        public required int Level_after { get; set; }
        [Required]
        public required bool Success { get; set; }
        public int Currency_used { get; set; } = 0;
    }
}
