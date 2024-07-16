using Adventurer;
using EvolveGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataScriptableObject")]
public class GameplaySceneData : ScriptableObject
{
    [SerializeField] private PlayerUnpacker playerUnpacker;
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private CoroutineStarter gameplayCoroutineStarter;

    public PlayerUnpacker PlayerUnpacker => playerUnpacker;
    public PlayerController PlayerPrefab => playerPrefab;
    public GameplayUI GameplayUI => gameplayUI;
    public CoroutineStarter GameplayCoroutineStarter => gameplayCoroutineStarter;
}