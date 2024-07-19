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
        BindPlayerViewSwitcher();
    }

    private void BindCoroutineStarter() 
    {
        var coroutineSpawner = Container.InstantiatePrefabForComponent<CoroutineStarter>(gameplaySceneData.GameplayCoroutineStarter);
        Container.BindInterfacesAndSelfTo<CoroutineStarter>().FromInstance(coroutineSpawner).AsSingle();
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
        (ThirdViewPlayer, FirstPersonPlayer) playerViews = Instantiate(gameplaySceneData.PlayerUnpacker).Unpack();
        ThirdViewPlayer thirdPersonViewPlayer = playerViews.Item1;
        FirstPersonPlayer firstPersonViewPlayer = playerViews.Item2;
        Container.BindInterfacesAndSelfTo<FirstPersonPlayer>().FromInstance(firstPersonViewPlayer).AsSingle();
        Container.BindInterfacesAndSelfTo<ThirdViewPlayer>().FromInstance(thirdPersonViewPlayer).AsSingle();
    }

    private void BindPlayerViewSwitcher()
    {
        Container.BindInterfacesAndSelfTo<PlayerViewSwitcher>().AsSingle().NonLazy();
    }

    private void BindPlayerUIMediator() => 
        Container.BindInterfacesAndSelfTo<PlayerUIMediator>().AsSingle().NonLazy();
}