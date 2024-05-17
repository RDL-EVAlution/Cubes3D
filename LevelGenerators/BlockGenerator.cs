using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BlockGenerator : IEcsRunSystem
{
    readonly private EcsFilter<GeneratorTag, GenerateScene> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;
    private LevelConfig levelConfig;

    private const float DIF = 0.55f;
    //const ConfigPositionToDirection
    private int[,] DOUBLE_POS = new int[4, 2] {             {  1,  1 },
                                                            {  1, -1 },
                                                            { -1,  1 },
                                                            { -1, -1 }};

    private int[,] TRIPLE_POS = new int[9, 2] {             {  1,  1 },
                                                            {  1,  0 },
                                                            {  1, -1 },
                                                            {  0,  1 },
                                                            {  0,  0 },
                                                            {  0, -1 },
                                                            { -1,  1 },
                                                            { -1,  0 },
                                                            { -1, -1 }};

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);

            var index = ecsFilter.Get2(filter).index;

            if (sceneData.blocksParentTransform.childCount > 0)
            {
                EntityReference[] deletedEtityReferences = 
                    sceneData.blocksParentTransform.transform.GetComponentsInChildren<EntityReference>();

                for (int i = 0; i < deletedEtityReferences.Length; i++)
                {
                    deletedEtityReferences[i].entity.Destroy();
                    GameObject.Destroy(sceneData.blocksParentTransform.transform.GetChild(i).gameObject);
                }
            }

            float sizeDifference = DIF * 2 * runtimeData.size;

            runtimeData.countOfBlocks = LevelConfigParser.GetLevelFromIndex(index, levelConfig).Length;

            if (runtimeData.size == 2)
            {
                for (int i = 0; i < LevelConfigParser.GetLevelFromIndex(index, levelConfig).Length; i++)
                {
                    Vector3 basePosition = new Vector3(DIF * DOUBLE_POS[i, 0], 0.555f, DIF * DOUBLE_POS[i, 1]); //
                    Vector3 configDirection = staticData.ConfigPositionToDirection(
                        LevelConfigParser.GetLevelFromIndex(index, levelConfig)[i]) * sizeDifference;
                    Vector3 currentPosition = basePosition + configDirection;

                    GameObject instance = GameObject.Instantiate(
                        staticData.block, currentPosition, Quaternion.identity, sceneData.blocksParentTransform);

                    EcsEntity blockEntity = world.NewEntity();
                    blockEntity.Get<GameObjectReference>().gameObject = instance;
                    blockEntity.Get<BlockTag>().index = i;
                    blockEntity.Get<TransformReference>().transform = instance.transform;

                    EntityReference reference = instance.GetComponent<EntityReference>();

                    reference.entity = blockEntity;
                }
            }
            else if (runtimeData.size == 3)
            {
                for (int i = 0; i < LevelConfigParser.GetLevelFromIndex(index, levelConfig).Length; i++)
                {
                    Debug.Log(i);

                    Vector3 basePosition = new Vector3(2 * DIF * TRIPLE_POS[i, 0], 0.555f, 2 * DIF * TRIPLE_POS[i, 1]); //
                    Vector3 configDirection = staticData.ConfigPositionToDirection(
                        LevelConfigParser.GetLevelFromIndex(index, levelConfig)[i]) * sizeDifference;
                    Vector3 currentPosition = basePosition + configDirection;

                    GameObject instance = GameObject.Instantiate(
                        staticData.block, currentPosition, Quaternion.identity, sceneData.blocksParentTransform);

                    EcsEntity blockEntity = world.NewEntity();
                    blockEntity.Get<GameObjectReference>().gameObject = instance;
                    blockEntity.Get<BlockTag>().index = i;
                    blockEntity.Get<TransformReference>().transform = instance.transform;

                    EntityReference reference = instance.GetComponent<EntityReference>();

                    reference.entity = blockEntity;
                }
            }
            else
            {
                Debug.LogError("IncorrectSizeOfTileGrid");
            }

            entity.Del<GenerateScene>();
        }
    }
}