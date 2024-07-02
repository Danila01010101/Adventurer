using System;
using UnityEngine;
using static Adventurer.SavesData;

namespace Adventurer
{
    public class SavesSwitcher
    {
        private SavesData savesData;

        public static Action<Data.Slot> slotChanged;

        public SaveSlotData ChooseSlot(Data data, Data.Slot slot)
        {
            switch (slot)
            {
                case Data.Slot.First:
                    data.CurrentSlot = Data.Slot.First;
                    return data.FirstSlotData;
                case Data.Slot.Second:
                    data.CurrentSlot = Data.Slot.Second;
                    return data.SecondSlotData;
                case Data.Slot.Third:
                    data.CurrentSlot = Data.Slot.Third;
                    return data.ThirdSlotData;
                case Data.Slot.Fourth:
                    data.CurrentSlot = Data.Slot.Fourth;
                    return data.FourthSlotData;
                default:
                    throw new ArgumentException("Invalid slot parameters, cant switch to empty slot");
            }
        }
    }
}