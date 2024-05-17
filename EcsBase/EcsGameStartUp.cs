using UnityEngine;
using Leopotam.Ecs;

public class EcsGameStartUp : MonoBehaviour
{
    [SerializeField] private StaticData staticData;
    [SerializeField] private SceneData sceneData;
    [SerializeField] private UI uI;
    private RuntimeData runtimeData;
    private LevelConfig levelConfig;

    private EcsWorld ecsWorld;
    private EcsSystems systems;

    private void Start()
    {
        runtimeData = new RuntimeData();
        levelConfig = new LevelConfig();
        levelConfig = LevelConfigParser.SetLevelConfig(levelConfig);

        ecsWorld = new EcsWorld();
        systems = new EcsSystems(ecsWorld);

        systems
            .Add(new UIInitSystem())
            //.Add(new LevelInitSystem()) //
            .Add(new TimerSystem())
            .Add(new TileGenerator())
            .Add(new BlockGenerator())

            .Add(new BlockCheckSystem())

            .Add(new BlockClickSystem())
            .Add(new BlockMoveSystem())
            .Add(new BlockCollisionSystem())

            .Add(new MiddleAreaCheckSystem())
            .Add(new BlockInMiddleCheckSystem())

            .Add(new LevelEndSystem())

            .Add(new PlaySystem())
            .Add(new SettingsSystem())
            .Add(new StoreSystem())
            .Add(new RewardSystem())
            .Add(new InstructionSystem())
            .Add(new CloseSystem())
            .Add(new ChangeButtonStateSystem())

            .Inject(staticData)
            .Inject(sceneData)
            .Inject(uI)
            .Inject(runtimeData)
            .Inject(levelConfig)
            .Init();
    }

    private void Update()
    {
        systems.Run();
    }

    private void OnDestroy()
    {
        systems?.Destroy();
        systems = null;
        ecsWorld?.Destroy();
        ecsWorld = null;
    }
}
