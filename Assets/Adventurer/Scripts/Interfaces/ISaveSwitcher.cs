using static SavesContainer;

namespace Adventurer
{
    public interface ISaveSwitcher
	{
        void ChooseSlot(SavesData.Data.Slot slot);
	}
}