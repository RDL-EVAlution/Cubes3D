using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSystem : IEcsRunSystem
{
    private readonly EcsFilter<OnMouseDown, StoreButton> ecsFilter;

    private UI uI;

    public void Run()
    {
        foreach(var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            uI.store.gameObject.SetActive(true);

            entity.Del<OnMouseDown>();
        }
    }
}
