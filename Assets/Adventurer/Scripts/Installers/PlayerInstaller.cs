using Adventurer;
using EvolveGames;
using GenshinImpactMovementSystem;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameplaySceneData gameplaySceneData;
    [SerializeField] private Transform playerSpawnPoint;

    public override void InstallBindings()
    {
        BindPlayer();
        BindPlayerViewSwitcher();
        BindItemChanger();
        BindGameplayCanvas();
    }

    private void BindCoroutineStarter() 
    {
        var coroutineSpawner = Container.InstantiatePrefabForComponent<CoroutineStarter>(gameplaySceneData.GameplayCoroutineStarter);
        Container.BindInterfacesTo<ICoroutineStarter>().FromInstance(coroutineSpawner);
    }
    
    private void BindGameplayCanvas()
    {
        var canvas = Container.InstantiatePrefabForComponent<GameplayUI>(gameplaySceneData.GameplayUI);
        Container.BindInterfacesAndSelfTo<MENU>().FromInstance(canvas.Menu).AsSingle();
    }

    private void BindItemChanger()
    {
        Container.BindInterfacesAndSelfTo<ItemChange>().AsSingle();
    }

    private void BindPlayer()
    {
        var thirdPersonViewPlayer = Instantiate(gameplaySceneData.PlayerUnpacker).Unpack();
        Container.Bind<Player>().FromInstance(thirdPersonViewPlayer);
        var firstPersonViewPlayer = Container.InstantiatePrefabForComponent<PlayerController>(gameplaySceneData.PlayerPrefab, playerSpawnPoint.position, Quaternion.identity, null);
        Container.Bind<PlayerController>().FromInstance(firstPersonViewPlayer).AsSingle();
    }

    private void BindPlayerViewSwitcher() => 
        Container.BindInterfacesAndSelfTo<PlayerViewSwitcher>().AsSingle().NonLazy();

    private void BindPlayerUIMediator() => 
        Container.BindInterfacesAndSelfTo<PlayerUIMediator>().AsSingle().NonLazy();
}