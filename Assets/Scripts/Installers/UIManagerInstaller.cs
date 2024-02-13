using UnityEngine;
using Zenject;

namespace Main
{
    public class UIManagerInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
        }
    }
}

