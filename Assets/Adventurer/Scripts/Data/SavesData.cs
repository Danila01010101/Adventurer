using System;
using static Adventurer.SavesData.Data;
using UnityEngine;

namespace Adventurer
{
    public class SavesData : ISaveSwitcher
    {
        public SaveSlotData CurrentSlotData { get; private set; }

        private SavesSwitcher switcher;
        private Data data;
        private const string FILENAME = "/data.gd";

        public SavesData()
        {
            DataParser.Load(FILENAME);
        }

        public void Save()
        {
            switch (data.CurrentSlot)
            {
                case Slot.First:
                    data.CurrentSlot = Slot.First;
                    data.FirstSlotData = CurrentSlotData;
                    break;
                case Slot.Second:
                    data.CurrentSlot = Slot.Second;
                    data.SecondSlotData = CurrentSlotData;
                    break;
                case Slot.Third:
                    data.CurrentSlot = Slot.Third;
                    data.ThirdSlotData = CurrentSlotData;
                    break;
                case Slot.Fourth:
                    data.CurrentSlot = Slot.Fourth;
                    data.FourthSlotData = CurrentSlotData;
                    break;
                default:
                    throw new ArgumentException("Invalid slot parameters, cant switch to empty slot");
            }

            DataParser.Save(FILENAME, data);
        }

        public struct Data
        {
            public SaveSlotData FirstSlotData;
            public SaveSlotData SecondSlotData;
            public SaveSlotData ThirdSlotData;
            public SaveSlotData FourthSlotData;
            public Slot CurrentSlot;
            public enum Slot { None = 0, First = 1, Second = 2, Third = 3, Fourth = 4 }
        }

        public void ChooseSlot(Slot slot)
        {
            Debug.Log($"Switching slot { data.CurrentSlot } to slot { slot }");
            switcher.ChooseSlot(data, slot);
        }
    }
}