using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ADSoft.Core.Models.Mapping
{
    public class DiscussMap : EntityTypeConfiguration<Discuss>
    {
        public DiscussMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);


            // Table & Column Mappings
            this.ToTable("Discuss");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
