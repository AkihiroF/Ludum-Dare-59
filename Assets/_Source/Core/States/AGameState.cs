namespace Core.States
{
    public abstract class AGameState
    {
        public virtual void Enter() {}
        public virtual void Exit() {}
    }
}