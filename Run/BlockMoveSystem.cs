using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveSystem : IEcsRunSystem
{
    readonly private EcsFilter<Move, BlockTag, TransformReference> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            ref var move = ref ecsFilter.Get1(filter);
            ref var transform = ref ecsFilter.Get3(filter).transform;

            transform.position = Vector3.Lerp(transform.position, move.nextPosition, Time.deltaTime * 5);
        }
    }
}