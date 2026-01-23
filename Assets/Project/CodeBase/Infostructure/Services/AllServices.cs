namespace Assets.Project.CodeBase.Infostructure.Services
{
    public class AllServices 
    {

        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService>
        {
            public static TService ServiceInstance;
        }
    }
}