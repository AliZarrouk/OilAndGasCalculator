using System;

namespace Core.DataAccess
{
    public abstract class CoreRequest<T>
    {
        public Guid Id { get; set; }

        public virtual T Input { get; set; }
    }
}
