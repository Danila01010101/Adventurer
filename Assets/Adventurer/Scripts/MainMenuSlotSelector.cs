using Zenject;

namespace Adventurer
{
    public class MainMenuSlotSelector
	{
        private ISlotChooser slotChooser;

        [Inject]
    	private void Construct(ISlotChooser slotChooser)
        {
            this.slotChooser = slotChooser;
            SlotSelectionWindow.OnSlotSelected += slotChooser.ChooseSlot;
        }
	}
}