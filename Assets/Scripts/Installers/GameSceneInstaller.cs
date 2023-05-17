using Grid;
using Infrastructure;
using Plugins.Zenject.Source.Install;
using ScriptableObjects;
using UI;
using UnityEditor.VersionControl;
using UnityEngine;
using Utilities;
using Zenject;

namespace Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public GameConfigSO GameConfigSo;
        public GridGeneratorService GridGeneratorService;
        public InputService InputService;
        public override void InstallBindings()
        {
            Container.Bind<GameConfigSO>().FromInstance(GameConfigSo);
            Container.Bind<JsonReader>().AsSingle().NonLazy();

            Container.Bind<CrosswordGenerator>().AsSingle();
            Container.Bind<GridGeneratorService>().FromInstance(GridGeneratorService).AsSingle();
            Container.Bind<GridDataService>().AsSingle();
            Container.Bind<GridVisualService>().AsSingle();

            Container.Bind<MessageBus>().AsSingle().NonLazy();
            Container.Bind<InputService>().FromInstance(InputService).AsSingle();
        }
    }
}