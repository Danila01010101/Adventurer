using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIWindow
{
    [SerializeField] private Button continueGame;
    [SerializeField] private Button selectSave;
    [SerializeField] private Button options;
    [SerializeField] private Button exitGame;

    public override void Initialize()
    {
        continueGame.onClick.AddListener(Continue);
        selectSave.onClick.AddListener(ShowSlotWindow);
        options.onClick.AddListener(ShowOptions);
        exitGame.onClick.AddListener(Exit);
    }

    public override void Hide()
    {
        base.Hide();
    }

    private void Continue()
    {
        throw new NotImplementedException();
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