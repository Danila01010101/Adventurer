using Adventurer;
using System;

public class SavesContainer
{
    private SaveSlotData currentSlotData => globalData.CurrentSlotData;
    private SavesData globalData;

    #region Get/Set Methods

    #endregion

    public SavesContainer() 
    {
        globalData = new SavesData();
    }

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