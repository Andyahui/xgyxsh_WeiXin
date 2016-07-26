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
            this.Id=Guid.NewGuid();                        //这里需要新建个ID，要是没有为其赋值。
        }
        public virtual Guid Id { get; set; }
        public virtual DateTime CreateTime { get; set; }
    }
}
