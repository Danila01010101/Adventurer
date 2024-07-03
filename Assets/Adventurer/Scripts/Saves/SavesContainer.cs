using Adventurer;
using System;
using Zenject;

public class SavesContainer : ISlotDataNotifier
{
    public SaveSlotData currentSlotData => globalData.CurrentSlotData;

    private SavesData globalData;

    #region Get/Set Methods

    #endregion

    [Inject]
    private void Construct(SavesData data)
    {
        globalData = data;
    }

    public bool HasDataSelected() => globalData.IsDataEmpty == false;

    private void SetValueInBounds<T>(T min, T max, T valueToSet, out T settableValue) where T : IComparable
    {
        if (valueToSet.CompareTo(max) <= 0 || valueToSet.CompareTo(min) >= 0)
        {
            settableValue = valueToSet;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}