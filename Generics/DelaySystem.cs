using Leopotam.Ecs;
using UnityEngine;

public class DelaySystem : IEcsRunSystem
{
    readonly private EcsFilter<Delay> ecsFilter;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var duration = ref ecsFilter.Get1(filter).duration;

            duration -= Time.deltaTime;

            if (duration <= 0.0f)
            {
                entity.Del<Delay>();
            }
        }
    }
}
