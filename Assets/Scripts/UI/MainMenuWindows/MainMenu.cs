using UnityEngine;

public class MainMenu : UIWindow
{
    public override void Initialize() { }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowOptions()
    {
        UIWindowManager.Show<Options>();
    }
}