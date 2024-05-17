using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : IEcsRunSystem
{
    private readonly EcsFilter<GeneratorTag, Timer> ecsFilter;
    private readonly EcsFilter<GeneratorTag, GenerateScene> reloadEcsFilter;

    private EcsWorld world;
    private UI uI;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var duration = ref ecsFilter.Get2(filter).duration;

            duration -= Time.deltaTime;

            uI.timerBackground.fillAmount = duration / 30.0f;
            uI.timerText.text = duration.ToString();

            if (duration <= 0)
            {
                EcsEntity extraEntity = world.NewEntity();

                extraEntity.Get<LevelEnd>().isWin = false;
                entity.Del<Timer>();
            }
        }

        foreach (var filter in reloadEcsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            entity.Get<Timer>().duration = 30.0f;
        }
    }
}
