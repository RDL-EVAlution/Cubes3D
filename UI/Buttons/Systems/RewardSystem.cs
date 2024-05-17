using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : IEcsRunSystem
{
    private readonly EcsFilter<OnMouseDown, RewardButton> ecsFilter;

    private UI uI;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            uI.rewards.gameObject.SetActive(true);

            entity.Del<OnMouseDown>();
        }
    }
}
