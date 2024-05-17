using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSystem : IEcsRunSystem
{
    private readonly EcsFilter<OnMouseDown, CloseButton> ecsFilter;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var panel = ref ecsFilter.Get2(filter).panel;

            panel.SetActive(false);

            entity.Del<OnMouseDown>();
        }
    }
}
