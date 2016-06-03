using Castle.Windsor;

namespace Core.Utilities
{
    public static class IoC
    {
        private static readonly object LockObj = new object();

        private static IWindsorContainer _container = new WindsorContainer();

        public static IWindsorContainer Container
        {
            get { return _container; }

            set
            {
                lock (LockObj)
                {
                    _container = value;
                }
            }
        }
    }
}
