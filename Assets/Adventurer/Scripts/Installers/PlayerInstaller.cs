using Cinemachine;
using EvolveGames;
using GenshinImpactMovementSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameplaySceneData gameplaySceneData;
    [SerializeField] private Transform playerSpawnPoint;

    public override void InstallBindings()
    {
        BindPlayer();
        //var player = BindFirstPersonPlayer();
        //var canvas = BindGameplayCanvas();
        //BindItemChange(player.ItemChange, canvas.Menu.Logo);
    }

    private PlayerController BindFirstPersonPlayer()
    {
        var player = Container.InstantiatePrefabForComponent<PlayerController>(gameplaySceneData.PlayerPrefab, playerSpawnPoint.position, Quaternion.identity, null);
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
        return player;
    }
    
    private GameplayUI BindGameplayCanvas()
    {
        var canvas = Container.InstantiatePrefabForComponent<GameplayUI>(gameplaySceneData.GameplayUI);
        Container.BindInterfacesAndSelfTo<MENU>().FromInstance(canvas.Menu).AsSingle();
        return canvas;
    }

    private void BindPlayer()
    {
        var player = Instantiate(gameplaySceneData.PlayerUnpacker).Unpack();
        Container.Bind<Player>().FromInstance(player);
    }

    private void BindItemChange(ItemChange itemChange, Image logo) => itemChange.Initialize(logo);
}