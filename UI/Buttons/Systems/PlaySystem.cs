using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySystem : IEcsRunSystem
{
    private readonly EcsFilter<OnMouseDown, PlayButton> ecsFilter;

    private EcsWorld world;
    private RuntimeData runtimeData;
    private UI uI;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            EcsEntity generatorEntity = world.NewEntity();

            generatorEntity.Get<GeneratorTag>();
            generatorEntity.Get<GenerateScene>().index = 0;
            generatorEntity.Get<Timer>().duration = 30.0f;
            runtimeData.levelIndex = 0;

            uI.menu.SetActive(false);
            uI.head.SetActive(false);
            uI.timer.SetActive(true);

            entity.Del<OnMouseDown>();
        }
    }
}
