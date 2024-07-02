using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SlotSelectionWindow : UIWindow
{
    [SerializeField] private Button firstSlotButton;
    [SerializeField] private Button secondSlotButton;
    [SerializeField] private Button thirdSlotButton;
    [SerializeField] private Button fourthSlotButton;

    [Inject]
    private void Construct(SavesContainer data)
    {
        
    }

    public override void Initialize() { }
}