using System;
using static Adventurer.SavesData;

namespace Adventurer
{
    public static class SlotChooser
    {
        public static Action<Data> slotChanged;

        public static SaveSlotData GetSlot(Data data, SaveSlotData.Slot slot)
        {
            switch (slot)
            {
                case SaveSlotData.Slot.First:
                    return data.FirstSlotData;
                case SaveSlotData.Slot.Second:
                    return data.SecondSlotData;
                case SaveSlotData.Slot.Third:
                    return data.ThirdSlotData;
                case SaveSlotData.Slot.Fourth:
                    return data.FourthSlotData;
                default:
                    throw new ArgumentException("Invalid slot parameters, cant switch to empty slot");
            }
        }
    }
}