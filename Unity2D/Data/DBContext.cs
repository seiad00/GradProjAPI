using Microsoft.EntityFrameworkCore;
using Unity2D;

namespace Unity2D.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item_master>().HasData(
                new Item_master { Eid = 001, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 002, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 003, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 004, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 005, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 006, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 007, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 008, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 009, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 010, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 011, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 012, Type = "weapon", Base_atk = 1, Base_hp = 0, Base_armor = 0 },
                new Item_master { Eid = 013, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 014, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 015, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 016, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 017, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 018, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 019, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 020, Type = "armor", Base_atk = 0, Base_hp = 0, Base_armor = 1 },
                new Item_master { Eid = 021, Type = "accessory", Base_atk = 0.5, Base_hp = 0.5, Base_armor = 0 },
                new Item_master { Eid = 022, Type = "accessory", Base_atk = 0.5, Base_hp = 0.5, Base_armor = 0 },
                new Item_master { Eid = 023, Type = "accessory", Base_atk = 0.5, Base_hp = 0.5, Base_armor = 0 },
                new Item_master { Eid = 024, Type = "accessory", Base_atk = 0.5, Base_hp = 0.5, Base_armor = 0 },
                new Item_master { Eid = 025, Type = "extra", Base_atk = 0, Base_hp = 1, Base_armor = 0 },
                new Item_master { Eid = 026, Type = "extra", Base_atk = 0, Base_hp = 1, Base_armor = 0 }
            );
        }

        public DbSet<Player> players { get; set; }
        public DbSet<Item_master> item_masters { get; set; }
        public DbSet<Item_instance> item_instances { get; set; }
        public DbSet<Log_enhance> log_enhances { get; set; }
    }
}
