using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ADSoft.Core.Models.Mapping
{
    public class BankMap : EntityTypeConfiguration<Bank>
    {
        public BankMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);


            // Table & Column Mappings
            this.ToTable("Banks");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Ten).HasColumnName("Ten");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.STK).HasColumnName("STK");
            this.Property(t => t.CTK).HasColumnName("CTK");
        }
    }
}
