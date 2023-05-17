using Plugins.Zenject.Source.Install;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using Zenject;

namespace Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public GameConfigSO GameConfigSo;
        public override void InstallBindings()
        {
            Container.Bind<GameConfigSO>().FromInstance(GameConfigSo).AsSingle();
            Container.Bind<JsonReader>().AsSingle().NonLazy();
            Container.Bind<GridDataService>().AsSingle().NonLazy();
        }
    }
}