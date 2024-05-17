using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonStateSystem : IEcsRunSystem
{
    private readonly EcsFilter<TwoStateButtonReference, OnMouseDown>.Exclude<PlayButton> ecsFilter;

    private EcsWorld world;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var states = ref ecsFilter.Get1(filter);

            states.imageComponent.sprite = states.imageComponent.sprite.Equals(states.onImage) ? states.offImage : states.onImage;

            entity.Del<OnMouseDown>();
        }
    }
}
