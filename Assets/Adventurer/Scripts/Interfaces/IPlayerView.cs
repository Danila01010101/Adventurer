namespace Adventurer
{
    public interface IPlayerView
	{
        void Activate();
        void Deactivate();
        bool IsActive { get; }
	}
}