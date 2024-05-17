using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public GameObject doubleSizeCross;
    public GameObject tripleSizeCross;
    public GameObject block;

    public Vector3 ConfigPositionToDirection(int i)
    {
        if (i == 1)
        {
            return Vector3.right;
        }
        else if (i == 2)
        {
            return Vector3.forward;
        }
        else if (i == 3)
        {
            return Vector3.left;
        }
        else if (i == 4)
        {
            return Vector3.back;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
