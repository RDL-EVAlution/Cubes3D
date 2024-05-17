using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInMiddleCheckSystem : IEcsRunSystem
{
    private readonly EcsFilter<GeneratorTag> ecsFilter;

    private EcsWorld world;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach(var filter in ecsFilter)
        {
            if (runtimeData.countOfBlocks == 0)
            {
                EcsEntity entity = world.NewEntity();

                entity.Get<LevelEnd>().isWin = true;

                runtimeData.countOfBlocks--;
            }
        }
    }
}