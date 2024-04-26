using EvolveGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataScriptableObject")]
public class GameplaySceneData : ScriptableObject
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private GameplayUI _gameplayUI;

    public PlayerController PlayerPrefab => _playerPrefab;
    public GameplayUI GameplayUI => _gameplayUI;
}