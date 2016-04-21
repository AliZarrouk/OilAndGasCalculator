using System;
using System.Collections.Generic;
using log4net;

namespace Core.DataAccess
{
    public abstract class CoreResponse<T>
    {
        public Guid Id { get; set; }

        public IEnumerable<String> Errors { get; set; }

        public T Result { get; set; }

        protected static readonly ILog Log = LogManager.GetLogger(typeof(CoreResponse<T>));

        protected CoreResponse()
        {
            Id = new Guid();

            Log.InfoFormat("Request created with ID {0}", Id);
        }
    }
}
