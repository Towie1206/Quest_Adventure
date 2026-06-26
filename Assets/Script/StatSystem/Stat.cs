using System;
using UnityEngine;

[Serializable]
public class Stat 
{
    [SerializeField] private float baseValue;

    public float GetValue()
    { 
        return baseValue; 
    }

    //buff or item affecting base value
    //all calcualtionds done here
}
