using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    private RuntimeData runtimeData;

    public void Init()
    {
        EcsEntity entity = world.NewEntity();

        entity.Get<GeneratorTag>();
        entity.Get<GenerateScene>().index = 0;
        runtimeData.levelIndex = 0;
    }
}
