using static SavesContainer;

namespace Adventurer
{
    public interface ISlotChooser
	{
        void ChooseSlot(SavesData.Data.Slot slot);
	}
}