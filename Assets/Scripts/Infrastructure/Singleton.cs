namespace Breakout.Infrastructure
{
    public class Singleton<T> where T : class , new()
    {
        private static T _instance = null;

        protected Singleton() { }
        
        public static T instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                
                return _instance;
            }
        }
    }
}