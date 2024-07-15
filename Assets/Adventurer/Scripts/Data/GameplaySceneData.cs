using Adventurer;
using Cinemachine;
using EvolveGames;
using GenshinImpactMovementSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataScriptableObject")]
public class GameplaySceneData : ScriptableObject
{
    [SerializeField] private PlayerUnpacker playerUnpacker;
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private GameplayUI gameplayUI;

    public PlayerUnpacker PlayerUnpacker => playerUnpacker;
    public PlayerController PlayerPrefab => playerPrefab;
    public GameplayUI GameplayUI => gameplayUI;
}