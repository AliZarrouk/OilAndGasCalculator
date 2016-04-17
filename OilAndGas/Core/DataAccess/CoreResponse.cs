using System;
using System.Collections.Generic;

namespace Core.DataAccess
{
    public abstract class CoreResponse<T>
    {
        public Guid Id { get; set; }

        public ICollection<String> Errors { get; set; }

        public virtual T Result { get; set; }

        protected CoreResponse()
        {
            Id = new Guid();
        }
    }
}
