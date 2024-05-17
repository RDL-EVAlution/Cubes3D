using Leopotam.Ecs;
using UnityEngine;

public class BlockCollisionSystem : IEcsRunSystem
{
    readonly private EcsFilter<OnCollisionEnter, TransformReference, BlockTag> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;

    private const float DIFFERENCE = 0.55f;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            Debug.Log("OnCollisionEnter");

            ref var entity = ref ecsFilter.GetEntity(filter);
            var collision = ecsFilter.Get1(filter).collision;
            ref var transform = ref ecsFilter.Get2(filter).transform;
            ref var index = ref ecsFilter.Get3(filter).index;

            if (transform.position == collision.transform.position)
            {
                entity.Del<OnCollisionEnter>();
                return;
            }

            ref var otherEntity = ref collision.gameObject.GetComponent<EntityReference>().entity;

            ref var otherTransform = ref otherEntity.Get<TransformReference>().transform;

            var difference = (transform.position - collision.transform.position) * 5;

            entity.Get<Move>().nextPosition = transform.position + difference;
            otherEntity.Get<Move>().nextPosition = otherTransform.position - difference;

            index = -1;

            entity.Del<OnCollisionEnter>();
            otherEntity.Del<OnCollisionEnter>();

            EcsEntity levelEndEntity = world.NewEntity();

            levelEndEntity.Get<LevelEnd>().isWin = false;
        }
    }
}
