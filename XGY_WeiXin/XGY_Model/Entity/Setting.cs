using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{
    /// <summary>
    /// 设置，存放一些临时的数据。
    /// </summary>
    public class Setting:BaseEntity
    {
        public virtual string Key { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Value { get; set; }
    }

    public class SettingMap:EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.ToTable("Setting");
            this.HasKey(x=>x.Id);            
            this.Property(x => x.Key).HasMaxLength(256);
            this.Property(x => x.Value).IsMaxLength();
            this.Property(x => x.DisplayName).HasMaxLength(256);
        }
    }
}
