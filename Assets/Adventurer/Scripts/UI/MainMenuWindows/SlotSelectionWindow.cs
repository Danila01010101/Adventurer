using System;
using UnityEngine;
using UnityEngine.UI;
using static Adventurer.SavesData.Data;

public class SlotSelectionWindow : UIWindow
{
    [SerializeField] private Button firstSlotButton;
    [SerializeField] private Button secondSlotButton;
    [SerializeField] private Button thirdSlotButton;
    [SerializeField] private Button fourthSlotButton;

    public static Action<Slot> OnSlotSelected;

    public override void Initialize() 
    {
        firstSlotButton.onClick.AddListener(() => SlotSelected(Slot.First));
        secondSlotButton.onClick.AddListener(() => SlotSelected(Slot.Second));
        thirdSlotButton.onClick.AddListener(() => SlotSelected(Slot.Third));
        fourthSlotButton.onClick.AddListener(() => SlotSelected(Slot.Fourth));
    }

    private void SlotSelected(Slot slot)
    {
        OnSlotSelected?.Invoke(slot);
    }
}