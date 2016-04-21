using System;
using log4net;

namespace Core.DataAccess
{
    public abstract class CoreRequest<T>
    {
        public Guid Id { get; set; }

        public T Input { get; set; }

        protected static readonly ILog Log = LogManager.GetLogger(typeof(CoreRequest<T>));

        protected CoreRequest()
        {
            Id = new Guid();

            Log.InfoFormat("Request created with ID {0}", Id);
        }
    }
}
