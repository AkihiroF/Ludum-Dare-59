namespace InteractionSystem
{
    public interface IInteraction
    {
        void OnLook(bool isEnabled = true);
        void Interact();
    }
}