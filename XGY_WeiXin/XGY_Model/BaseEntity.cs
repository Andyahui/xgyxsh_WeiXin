using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model
{
    /// <summary>
    /// 实体的基类
    /// </summary>
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateTime = DateTime.Now;
            this.ModifiedTime=DateTime.Now;
        }
        public virtual Guid Id { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime ModifiedTime { get; set; }
        public virtual string Ip { get; set; }
    }
}
