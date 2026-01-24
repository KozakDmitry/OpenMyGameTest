using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Unity.Android.Gradle.Manifest;

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


        public void RegisterObject<TObject>(TObject implementation) where TObject : IObject =>
            Implementation<TObject>.ServiceInstance = implementation;


        public void DeleteObject<TObject>(TObject implementation) where TObject : IObject =>
             Implementation<TObject>.ServiceInstance = default;


        public async UniTask<TObject> SingleAwait<TObject>(CancellationToken cts = default) where TObject : IObject
        {
            while (Implementation<TObject>.ServiceInstance == null && !cts.IsCancellationRequested)
            {
                await UniTask.Yield();
            }
            return Implementation<TObject>.ServiceInstance;
        }


        private static class Implementation<TService>
        {
            public static TService ServiceInstance;
        }



    }
}