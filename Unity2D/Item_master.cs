using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unity2D
{
    public class Item_master
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int Eid { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required double Base_atk { get; set; }
        [Required]
        public required double Base_hp { get; set; }
        [Required]
        public required double Base_armor { get; set; }
    }
}
