using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSystem : IEcsRunSystem
{
    private readonly EcsFilter<OnMouseDown, SettingsButton> ecsFilter;

    private UI uI;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            uI.settings.gameObject.SetActive(true);

            entity.Del<OnMouseDown>();
        }
    }
}
