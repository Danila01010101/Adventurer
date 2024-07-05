using Zenject;

namespace Adventurer
{
    public class CheckpointListener
	{
        private SavesContainer savesContainer;

        [Inject]
		private void Construct(SavesContainer savesContainer)
		{
			this.savesContainer = savesContainer;
			PlayerSaveTrigger.PlayerEnteredSaveZone += savesContainer.Save;
		}
	}
}