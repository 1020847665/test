using BestWise.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 系统菜单扩展
    /// </summary>
    public class SysFunctionExt : Sys_Function
    {
        /// <summary>
        /// 扩展系统菜单
        /// </summary>
        public SysFunctionExt()
            : base()
        {
            this._parentId = _parentId;
            this.state = state;
            this.Id = Id;
            this.ParentId = ParentId;
            this.Name = Name;
            this.Identify = Identify;
            this.IsSelected = IsSelected;
            this.Type = Type;
            this.Sort = Sort;
            this.Notes = Notes;
            this.IsDeleted = IsDeleted;
            this.Cdt = Cdt;
            this.Cby = Cby;
            this.Mdt = Mdt;
            this.Mby = Mby;
            this.Subset = new List<SysFunctionExt>();
        }


        /// <summary>
        /// 
        /// </summary>
        public string _parentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<SysFunctionExt> Subset { get; set; }
    }
}
