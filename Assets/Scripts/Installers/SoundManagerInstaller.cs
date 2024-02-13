using UnityEngine;
using Zenject;

namespace Main
{
    public class SoundManagerInstaller : MonoInstaller
    {
        [SerializeField] private SoundManager _soundManager;
        public override void InstallBindings()
        {
            var soundManagerPref = Container.InstantiatePrefabForComponent<SoundManager>(_soundManager);
            Container.Bind<SoundManager>().FromInstance(soundManagerPref).AsSingle();
        }
    }
}

