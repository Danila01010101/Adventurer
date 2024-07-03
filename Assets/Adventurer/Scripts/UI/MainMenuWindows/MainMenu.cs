using Adventurer;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : UIWindow
{
    [SerializeField] private Button continueGame;
    [SerializeField] private Button selectSave;
    [SerializeField] private Button options;
    [SerializeField] private Button exitGame;

    private ISlotDataNotifier dataNotifier;

    public static Action OnContinueButtonPress;

    [Inject]
    private void Construct(ISlotDataNotifier dataNotifier)
    {
        this.dataNotifier = dataNotifier;
    }

    public override void Initialize()
    {
        continueGame.onClick.AddListener(Continue);
        selectSave.onClick.AddListener(ShowSlotWindow);
        options.onClick.AddListener(ShowOptions);
        exitGame.onClick.AddListener(Exit);
    }

    public override void Show()
    {
        base.Show();
        continueGame.interactable = dataNotifier.HasDataSelected();
    }

    public override void Hide()
    {
        base.Hide();
    }

    private void Continue()
    {
        OnContinueButtonPress?.Invoke();
    }

    private void ShowSlotWindow()
    {
        UIWindowManager.Show<SlotSelectionWindow>();
    }

    private void ShowOptions()
    {
        UIWindowManager.Show<Options>();
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        continueGame.onClick.RemoveListener(Continue);
        selectSave.onClick.RemoveListener(ShowSlotWindow);
        options.onClick.RemoveListener(ShowOptions);
        exitGame.onClick.RemoveListener(Exit);
    }
}