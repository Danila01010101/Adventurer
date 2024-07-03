using System;
using static Adventurer.SavesData;

namespace Adventurer
{
    public static class SlotChooser
    {
        public static Action<Data.Slot> slotChanged;

        public static SaveSlotData GetSlot(Data data, Data.Slot slot)
        {
            switch (slot)
            {
                case Data.Slot.First:
                    return data.FirstSlotData;
                case Data.Slot.Second:
                    return data.SecondSlotData;
                case Data.Slot.Third:
                    return data.ThirdSlotData;
                case Data.Slot.Fourth:
                    return data.FourthSlotData;
                default:
                    throw new ArgumentException("Invalid slot parameters, cant switch to empty slot");
            }
        }
    }
}