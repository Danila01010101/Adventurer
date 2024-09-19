using System;
using UnityEngine;
using static Adventurer.SaveSlotData;

namespace Adventurer
{
    public class SavesData : ISlotChooser
    {
        public SaveSlotData CurrentSlotData;
        public bool IsDataEmpty => data.CurrentSlot == Slot.None;

        private Data data;
        private const SceneID startLocation = SceneID.StartLocation;
        private const string FILENAME = "/data.gd";

        public SavesData()
        {
            data = DataParser.Load(FILENAME);

            if (data.CurrentSlot != Slot.None)
                CurrentSlotData = SlotChooser.GetSlot(data, (Slot)data.CurrentSlot);
        }

        public void Save()
        {
            switch ((Slot)data.CurrentSlot)
            {
                case SaveSlotData.Slot.First:
                    data.CurrentSlot = Slot.First;
                    data.FirstSlotData = CurrentSlotData;
                    break;
                case SaveSlotData.Slot.Second:
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

            SaveGame();
        }

        [Serializable]
        public struct Data
        {
            public SaveSlotData FirstSlotData;
            public SaveSlotData SecondSlotData;
            public SaveSlotData ThirdSlotData;
            public SaveSlotData FourthSlotData;
            public Slot CurrentSlot;
        }

        public void ChooseSlot(Slot slot)
        {
            Debug.Log($"Switching slot { data.CurrentSlot } to slot { slot }");
            CurrentSlotData =  SlotChooser.GetSlot(data, slot);
            data.CurrentSlot = slot;
            if (CurrentSlotData.LastSceneIndex == SceneID.Bootstrap)
                CurrentSlotData.LastSceneIndex = SceneID.StartLocation;
        }

        public void SaveGame() => DataParser.Save(FILENAME, data);
    }
}