using System;
using UnityEngine;
using Adventurer;

public class GameSaver : MonoBehaviour
{
    private SaveSlotData _data;
    private const string FILENAME = "/data.gd";

    #region Get/Set Methods

    #endregion

    public static GameSaver Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        _data = DataParser.Load(FILENAME);
    }

    public void Save()
    {
        if (_data != null)
        {
            DataParser.Save(FILENAME, _data);
        }
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