using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private UI uI;

    public void Init()
    {
        CreateMainMenuButtons();
        CreateCloseButtons();
        CreateTwoStateButtons();
        CreateSettingsButtons();
    }

    private void CreateMainMenuButtons()
    {
        EcsEntity playButtonEntity = ecsWorld.NewEntity();
        playButtonEntity.Get<PlayButton>();
        uI.playButton.entity = playButtonEntity;

        EcsEntity storeButtonEntity = ecsWorld.NewEntity();
        storeButtonEntity.Get<StoreButton>();
        uI.storeButton.entity = storeButtonEntity;

        EcsEntity settingsButtonEntity = ecsWorld.NewEntity();
        settingsButtonEntity.Get<SettingsButton>();
        uI.settingsButton.entity = settingsButtonEntity;

        EcsEntity rewardButtonEntity = ecsWorld.NewEntity();
        rewardButtonEntity.Get<RewardButton>();
        uI.rewardButton.entity = rewardButtonEntity;
    }

    private void CreateCloseButtons()
    {
        EcsEntity closeStoreButtonEntity = ecsWorld.NewEntity();
        closeStoreButtonEntity.Get<CloseButton>().panel = uI.store;
        uI.storeCloseButton.entity = closeStoreButtonEntity;

        EcsEntity closeSettingsButtonEntity = ecsWorld.NewEntity();
        closeSettingsButtonEntity.Get<CloseButton>().panel = uI.settings;
        uI.settingsCloseButton.entity = closeSettingsButtonEntity;

        EcsEntity closeInstructionsButtonEntity = ecsWorld.NewEntity();
        closeInstructionsButtonEntity.Get<CloseButton>().panel = uI.instructions;
        uI.instructionCloseButton.entity = closeInstructionsButtonEntity;

        EcsEntity closeRewardButtonEntity = ecsWorld.NewEntity();
        closeRewardButtonEntity.Get<CloseButton>().panel = uI.rewards;
        uI.rewardCloseButton.entity = closeRewardButtonEntity;
    }

    private void CreateTwoStateButtons()
    {
        for (int i = 0; i < uI.twoStateButtons.Length; i++)
        {
            EcsEntity twoStateButtonEntity = ecsWorld.NewEntity();
            twoStateButtonEntity.Get<TwoStateButtonReference>() = new TwoStateButtonReference
                                                                    (uI.twoStateButtons[i].onImage,
                                                                     uI.twoStateButtons[i].offImage,
                                                                     uI.twoStateButtons[i].imageComponent);
            uI.twoStateButtonsEntityReference[i].entity = twoStateButtonEntity;
        }
    }

    private void CreateSettingsButtons()
    {
        EcsEntity instructionButtonEntity = ecsWorld.NewEntity();
        instructionButtonEntity.Get<InstructionButton>();
        uI.instructionButton.entity = instructionButtonEntity;
    }
}