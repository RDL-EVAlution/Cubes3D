using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleAreaCheckSystem : IEcsRunSystem
{
    readonly private EcsFilter<OnTriggerEnter, MiddleArea> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var othersColliders = ref ecsFilter.Get1(filter).others;
            var middleArea = ecsFilter.Get2(filter);

            if (othersColliders[0].gameObject.TryGetComponent(out EntityReference entityReference))
            {
                ref var blockEntity = ref entityReference.entity;

                int blockIndex = blockEntity.Get<BlockTag>().index;

                if (blockIndex == middleArea.index)
                {
                    blockEntity.Get<TransformReference>().transform.position = middleArea.position;
                    blockEntity.Del<Move>();

                    runtimeData.countOfBlocks--;
                }

                othersColliders.Remove(othersColliders[0]);
            }

            entity.Del<OnTriggerEnter>();
        }
    }
}