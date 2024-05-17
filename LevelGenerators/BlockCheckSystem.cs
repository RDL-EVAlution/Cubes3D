using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheckSystem : IEcsRunSystem
{
    readonly private EcsFilter<OnTriggerEnter, ClickableArea> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            Debug.Log("BlockCheckSystem");

            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var othersColliders = ref ecsFilter.Get1(filter).others;
            ref var blocksInsideCollider = ref ecsFilter.Get2(filter).blocksInside;

            for (int i = 0; i < othersColliders.Count; i++)
            {
                if (othersColliders[i].gameObject.TryGetComponent(out EntityReference entityReference))
                {
                    var blockEntity = entityReference.entity;

                    int blockIndex = blockEntity.Get<BlockTag>().index;

                    if (blockIndex >= 0) blocksInsideCollider[blockIndex] = blockEntity;
                }
            }


            entity.Del<OnTriggerEnter>();
        }
    }
}