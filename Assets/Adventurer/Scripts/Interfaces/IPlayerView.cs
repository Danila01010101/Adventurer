namespace Adventurer
{
    public interface IPlayerView
	{
        void ChangeToThirdPersonView();
        void ChangeToFirstPersonView();
        bool IsThirdViewActive { get; }
	}
}