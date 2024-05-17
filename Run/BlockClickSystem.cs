using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClickSystem : IEcsRunSystem
{
    readonly private EcsFilter<OnMouseDown, ClickableArea>.Exclude<Enable> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;

    private const float DIFFERENCE = 0.55f;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            int direction = ecsFilter.Get2(filter).direction;
            ref var blocksEntity = ref ecsFilter.Get2(filter).blocksInside;

            float sizeDifference = DIFFERENCE * 2 * runtimeData.size;

            Vector3 configDirection = -staticData.ConfigPositionToDirection(direction) * sizeDifference;

            for (int i = 0; i < blocksEntity.Length; i++)
            {
                if (blocksEntity[i] != EcsEntity.Null)
                {
                    ref Move move = ref blocksEntity[i].Get<Move>();
                    move.nextPosition = configDirection + blocksEntity[i].Get<TransformReference>().transform.position;
                }
            }

            entity.Del<OnMouseDown>();
            entity.Get<Enable>();
        }
    }
}
