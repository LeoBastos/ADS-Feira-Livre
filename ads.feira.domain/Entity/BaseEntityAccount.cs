using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ads.feira.domain.Entity
{
    public abstract class BaseEntityAccount : IBaseEntityAccount
    {
        protected BaseEntityAccount()
        {
            _Insert = DateTime.Now;
            IsActive = true;
        }

        public Guid Id { get; set; }
        public DateTime _Insert { get; set; }
        public DateTime? _Update { get; private set; }
        public bool _Deleted { get; private set; }
        public bool IsActive { get; set; }

        protected void SetUpdate()
        {
            _Update = DateTime.Now;
        }

        protected void SetDeleted()
        {
            _Deleted = true;
        }
    }
    public interface IBaseEntityAccount
    {
    }
}
