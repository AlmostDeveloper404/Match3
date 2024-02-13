using UnityEngine;
using Zenject;

namespace Main
{
    public class LevelControllerInstaller : MonoInstaller
    {
        [SerializeField] private LevelController _levelController;

        public override void InstallBindings()
        {
            Container.Bind<LevelController>().FromInstance(_levelController).AsSingle();
        }
    }
}

