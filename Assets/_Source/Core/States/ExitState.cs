using LevelSystem;
using Utils;
using Zenject;

namespace Core.States
{
    public sealed class ExitState : AGameState
    {
        private readonly SceneLoader _sceneLoader;
        [Inject]
        public ExitState(SceneLoader exitLoader)
        {
            _sceneLoader = exitLoader;
        }

        public override void Enter()
        {
            _sceneLoader.SimpleLoadScene();
            StarCounter.Clear();
        }
    }
}