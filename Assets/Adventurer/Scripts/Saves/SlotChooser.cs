using System;
using static Adventurer.SavesData;

namespace Adventurer
{
    public static class SlotChooser
    {
        public static Action<Data> slotChanged;

        public static SaveSlotData GetSlot(Data data, Slot slot)
        {
            switch (slot)
            {
                case Slot.First:
                    return data.FirstSlotData;
                case Slot.Second:
                    return data.SecondSlotData;
                case Slot.Third:
                    return data.ThirdSlotData;
                case Slot.Fourth:
                    return data.FourthSlotData;
                default:
                    throw new ArgumentException("Invalid slot parameters, cant switch to empty slot");
            }
        }
    }
}