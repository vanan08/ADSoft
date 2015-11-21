using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ADSoft.Core.Models.Mapping
{
    public class LotteryMap : EntityTypeConfiguration<Lottery>
    {
        public LotteryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Lotteries");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Jackpots).HasColumnName("Jackpots");
            this.Property(t => t.First).HasColumnName("First");
            this.Property(t => t.Second).HasColumnName("Second");
            this.Property(t => t.Third).HasColumnName("Third");
            this.Property(t => t.Fourth).HasColumnName("Fourth");
            this.Property(t => t.Fifth).HasColumnName("Fifth");
            this.Property(t => t.Six).HasColumnName("Six");
            this.Property(t => t.Seven).HasColumnName("Seven");
            this.Property(t => t.Html).HasColumnName("Html");
            this.Property(t => t.JackpotsDate).HasColumnName("JackpotsDate");
            
        }
    }
}
