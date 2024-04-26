using EvolveGames;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private MENU _menu;

    public MENU Menu => _menu;
}