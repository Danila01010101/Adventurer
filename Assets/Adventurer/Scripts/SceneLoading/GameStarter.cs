using Zenject;

namespace Adventurer
{
    public class GameStarter
	{
		private ISceneLoader sceneLoader;
        private SavesContainer savesContainer;

		[Inject]
    	private void Construct(ISceneLoader sceneLoader, SavesContainer saves)
		{
			this.sceneLoader = sceneLoader;
			this.savesContainer = saves;
            MainMenu.OnContinueButtonPress += () => sceneLoader.Load(savesContainer.currentSlotData);
        }
	}
}