namespace BM.Core.Infrastructure
{
    public class EngineContext
    {
        public static IEngine Initialize()
        {
            if (Singleton<IEngine>.Instance == null)
            {
                Singleton<IEngine>.Instance = new BMEngine();

                Singleton<IEngine>.Instance.Initialize();
            }

            return Singleton<IEngine>.Instance;
        }

        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize();
                }

                return Singleton<IEngine>.Instance;
            }
        }

    }
}
