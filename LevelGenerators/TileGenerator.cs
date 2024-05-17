using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : IEcsRunSystem
{
    readonly private EcsFilter<GeneratorTag, GenerateScene> ecsFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;
    private LevelConfig levelConfig;

    public void Run()
    {
        foreach (var filter in ecsFilter)
        {
            var index = ecsFilter.Get2(filter).index;

            if (index < 0) Debug.LogError("IncorrectLevelIndex");

            runtimeData.size = (int) Mathf.Sqrt(LevelConfigParser.GetLevelFromIndex(index, levelConfig).Length);

            GameObject prefab = null;

            if(runtimeData.size == 2)
            {
                prefab = staticData.doubleSizeCross;
            }
            else if(runtimeData.size == 3)
            {
                prefab = staticData.tripleSizeCross;
            }
            else
            {
                Debug.LogError("IncorrectSizeOfTileGrid");
            }


            if (sceneData.tilesParentTransform.childCount > 0)
            {
                EntityReference[] deletedEtityReferences = 
                    sceneData.tilesParentTransform.transform.GetComponentsInChildren<EntityReference>();

                for (int i = 0; i < deletedEtityReferences.Length; i++)
                {
                    deletedEtityReferences[i].entity.Destroy();
                }

                GameObject.Destroy(sceneData.tilesParentTransform.transform.GetChild(0).gameObject);
            }


            GameObject instance = Object.Instantiate(prefab, sceneData.tilesParentTransform);

            EntityReference[] entityReferences = new EntityReference[instance.transform.childCount - 2];
            EcsEntity[] swipeContainerEntities = new EcsEntity[instance.transform.childCount - 2];

            for (int i = 0; i < instance.transform.childCount - 2; i++)
            {
                entityReferences[i] = instance.transform.GetChild(i).GetComponent<EntityReference>();

                swipeContainerEntities[i] = world.NewEntity();
                swipeContainerEntities[i].Get<ClickableArea>().direction = i + 1;
                swipeContainerEntities[i].Get<ClickableArea>().blocksInside = 
                    new EcsEntity[LevelConfigParser.GetLevelFromIndex(index, levelConfig).Length];

                entityReferences[i].entity = swipeContainerEntities[i];
            }

            EntityReference[] middleContainers = new EntityReference[instance.transform.GetChild(5).childCount];
            EcsEntity[] middleContainerEntities = new EcsEntity[instance.transform.GetChild(5).childCount];

            for (int i = 0; i < instance.transform.GetChild(5).childCount; i++)
            {
                middleContainers[i] = instance.transform.GetChild(5).GetChild(i).GetComponent<EntityReference>();

                middleContainerEntities[i] = world.NewEntity();
                middleContainerEntities[i].Get<MiddleArea>().index = i;
                middleContainerEntities[i].Get<MiddleArea>().position = 
                    instance.transform.GetChild(4).GetChild(0).GetChild(i).position + Vector3.up / 2.0f;

                middleContainers[i].entity = middleContainerEntities[i];
            }
        }
    }
}