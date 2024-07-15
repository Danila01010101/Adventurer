using EvolveGames;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private MENU _menu;

    public MENU Menu => _menu;
}