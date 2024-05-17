using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndSystem : IEcsRunSystem
{
    readonly private EcsFilter<LevelEnd> ecsFilter;
    readonly private EcsFilter<GeneratorTag> generatorFilter;
    readonly private EcsFilter<ClickableArea> clickableAreaFilter;

    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    private UI uI;
    private RuntimeData runtimeData;

    public void Run()
    {
        if (generatorFilter.GetEntitiesCount() <= 0) return;

        foreach (var filter in ecsFilter)
        {
            ref var entity = ref ecsFilter.GetEntity(filter);
            ref var generatorEntity = ref generatorFilter.GetEntity(0);
            ref var levelEnd = ref ecsFilter.Get1(filter);

            if (levelEnd.isWin && runtimeData.levelIndex < 3)
            {
                runtimeData.levelIndex++;
                generatorEntity.Get<GenerateScene>().index = runtimeData.levelIndex;
            }
            else
            {
                generatorEntity.Del<Timer>();

                uI.menu.SetActive(true);
                Image image = uI.playButton.gameObject.GetComponent<Image>();
                image.sprite = uI.playButton.gameObject.GetComponent<TwoStateButton>().offImage;

                foreach (var i in clickableAreaFilter)
                {
                    //clickableAreaFilter.GetEntity(i).Del<ClickableArea>();
                }
            }

            entity.Destroy();
        }
    }
}
