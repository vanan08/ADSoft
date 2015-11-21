using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ADSoft.Core.Models.Mapping
{
    public class ChatAccountMap : EntityTypeConfiguration<ChatAccount>
    {
        public ChatAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);


            // Table & Column Mappings
            this.ToTable("ChatAccounts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.NickName).HasColumnName("NickName");
        }
    }
}
