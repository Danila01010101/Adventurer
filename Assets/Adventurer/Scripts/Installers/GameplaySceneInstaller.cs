using EvolveGames;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private GameplaySceneData _gameplaySceneData;
    [SerializeField] private Transform _playerSpawnPoint;

    public override void InstallBindings()
    {
        var player = BindPlayer();
        var canvas = BindGameplayCanvas();
        BindItemChange(player.ItemChange, canvas.Menu.Logo);
    }

    private PlayerController BindPlayer()
    {
        var player = Container.InstantiatePrefabForComponent<PlayerController>(_gameplaySceneData.PlayerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
        return player;
    }
    
    private GameplayUI BindGameplayCanvas()
    {
        var canvas = Container.InstantiatePrefabForComponent<GameplayUI>(_gameplaySceneData.GameplayUI);
        Container.BindInterfacesAndSelfTo<MENU>().FromInstance(canvas.Menu).AsSingle();
        return canvas;
    }

    private void BindItemChange(ItemChange itemChange, Image logo) => itemChange.Initialize(logo);
}