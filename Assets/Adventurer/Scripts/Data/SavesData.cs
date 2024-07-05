using System;
using UnityEngine;

namespace Adventurer
{
    public class SavesData : ISlotChooser
    {
        public SaveSlotData CurrentSlotData;
        public bool IsDataEmpty => data.CurrentSlot == (int)Slot.None;

        public enum Slot { None = 0, First = 1, Second = 2, Third = 3, Fourth = 4 }

        private Data data;
        private const SceneID startLocation = SceneID.StartLocation;
        private const string FILENAME = "/data.gd";

        public SavesData()
        {
            data = DataParser.Load(FILENAME);

            if (data.CurrentSlot != (int)Slot.None)
                CurrentSlotData = SlotChooser.GetSlot(data, (Slot)data.CurrentSlot);
        }

        public void Save()
        {
            switch ((Slot)data.CurrentSlot)
            {
                case Slot.First:
                    data.CurrentSlot = (int) Slot.First;
                    data.FirstSlotData = CurrentSlotData;
                    break;
                case Slot.Second:
                    data.CurrentSlot = (int) Slot.Second;
                    data.SecondSlotData = CurrentSlotData;
                    break;
                case Slot.Third:
                    data.CurrentSlot = (int) Slot.Third;
                    data.ThirdSlotData = CurrentSlotData;
                    break;
                case Slot.Fourth:
                    data.CurrentSlot = (int) Slot.Fourth;
                    data.FourthSlotData = CurrentSlotData;
                    break;
                default:
                    throw new ArgumentException("Invalid slot parameters, cant switch to empty slot");
            }

            DataParser.Save(FILENAME, data);
        }

        [Serializable]
        public struct Data
        {
            public SaveSlotData FirstSlotData;
            public SaveSlotData SecondSlotData;
            public SaveSlotData ThirdSlotData;
            public SaveSlotData FourthSlotData;
            public int CurrentSlot;
        }

        public void ChooseSlot(Slot slot)
        {
            Debug.Log($"Switching slot { data.CurrentSlot } to slot { slot }");
            CurrentSlotData =  SlotChooser.GetSlot(data, slot);
            data.CurrentSlot = (int)slot;
            if ((SceneID)CurrentSlotData.LastSceneIndex == SceneID.Bootstrap)
                CurrentSlotData.LastSceneIndex = (int)SceneID.StartLocation;
        }
    }
}