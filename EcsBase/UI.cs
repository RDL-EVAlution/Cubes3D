using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public EntityReference playButton;
    public EntityReference storeButton;
    public EntityReference settingsButton;
    public EntityReference rewardButton;

    public EntityReference settingsCloseButton;
    public EntityReference instructionCloseButton;
    public EntityReference storeCloseButton;
    public EntityReference rewardCloseButton;

    public EntityReference[] twoStateButtonsEntityReference;
    public TwoStateButton[] twoStateButtons;

    public EntityReference instructionButton;

    public Text score;
    public Text coins;
    public Text reward;

    public GameObject settings;
    public GameObject instructions;
    public GameObject store;
    public GameObject rewards;

    public GameObject menu;
    public GameObject timer;
    public GameObject head;

    public Image timerBackground;
    public Text timerText;
}
