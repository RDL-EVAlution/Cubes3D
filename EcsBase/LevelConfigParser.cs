using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LevelConfigParser
{
    public static int[] GetLevelFromIndex(int index, LevelConfig config)
    {
        switch (index)
        {
                case 0: return config.firstLevel;

                case 1: return config.secondLevel;

                case 2: return config.thirdLevel;

                case 3: return config.fourthLevel;

                default: return null;
        }
    }

    public static LevelConfig SetLevelConfig(LevelConfig levelConfig)
    {
        return JsonUtility.FromJson<LevelConfig>(File.ReadAllText(Application.streamingAssetsPath + "/LevelConfig.json"));
    }
}
