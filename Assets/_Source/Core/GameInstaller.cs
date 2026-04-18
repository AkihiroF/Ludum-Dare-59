using Camera;
using Core.States;
using Input;
using UnityEngine;
using Utils;
using Zenject;

namespace Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private Bootstrapper bootstrapper;
        [SerializeField] private GameStarter gameStarter;
        [SerializeField] private CameraMover cameraMover;
        [SerializeField] private  InputHandler inputHandler;
        public override void InstallBindings()
        {
            Container.Bind<SpatialRegion>().AsSingle().NonLazy();
            Container.Bind<CameraMover>().FromInstance(cameraMover).AsSingle();
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
            BindInput();
            BindStateMachine();
            Container.Bind<GameStarter>().FromInstance(gameStarter).AsSingle();
            Container.Bind<Bootstrapper>().FromInstance(bootstrapper).AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<PlayerInputActions>().AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<AGameState>().To<InitState>().AsSingle();
            Container.Bind<AGameState>().To<GameState>().AsSingle();
            Container.Bind<AGameState>().To<ExitState>().AsSingle();
            Container.Bind<StateMachine>().AsSingle();
        }
    }
}
