using Adventurer;
using EvolveGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataScriptableObject")]
public class GameplaySceneData : ScriptableObject
{
    [SerializeField] private PlayerUnpacker playerUnpacker;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private CoroutineStarter gameplayCoroutineStarter;

    public PlayerUnpacker PlayerUnpacker => playerUnpacker;
    public GameplayUI GameplayUI => gameplayUI;
    public CoroutineStarter GameplayCoroutineStarter => gameplayCoroutineStarter;
}