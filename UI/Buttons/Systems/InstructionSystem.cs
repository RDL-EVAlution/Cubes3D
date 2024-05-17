using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionSystem : IEcsRunSystem
{
    private readonly EcsFilter<OnMouseDown, InstructionButton> ecsFilter;

    private UI uI;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            uI.instructions.gameObject.SetActive(true);

            entity.Del<OnMouseDown>();
        }
    }
}
