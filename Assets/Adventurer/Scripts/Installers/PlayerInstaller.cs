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
        BindCoroutineStarter();
        BindGameplayCanvas();
        BindPlayer();
        BindItemChanger();
    }

    private void BindCoroutineStarter() 
    {
        var coroutineSpawner = Container.InstantiatePrefabForComponent<CoroutineStarter>(gameplaySceneData.GameplayCoroutineStarter);
        Container.BindInterfacesAndSelfTo<ICoroutineStarter>().FromInstance(coroutineSpawner).AsSingle();
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
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(thirdPersonViewPlayer);
        var firstPersonViewPlayer = Container.InstantiatePrefabForComponent<PlayerController>(gameplaySceneData.PlayerPrefab, playerSpawnPoint.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(firstPersonViewPlayer).AsSingle();
        BindPlayerViewSwitcher(firstPersonViewPlayer, thirdPersonViewPlayer);
    }

    private void BindPlayerViewSwitcher(IPlayerView firstPersonController, IPlayerView thirdPersonController)
    {
        var playerViewSwitcher = new PlayerViewSwitcher(firstPersonController, thirdPersonController);
        Container.BindInterfacesAndSelfTo<PlayerViewSwitcher>().FromInstance(playerViewSwitcher).AsSingle().NonLazy();
    }

    private void BindPlayerUIMediator() => 
        Container.BindInterfacesAndSelfTo<PlayerUIMediator>().AsSingle().NonLazy();
}