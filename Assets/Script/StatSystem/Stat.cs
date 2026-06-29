using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private bool needToBeReCalculated = true; // Only recalculate when modifiers change, otherwise return cached value.
    private float finalValue;

    public float GetValue()
    {
        if (needToBeReCalculated)
        {
            finalValue = GetFinalValue();
            needToBeReCalculated = false;
        }

        return finalValue;
    }

    public void AddModifier(float value, string source)
    {
        StatModifier modToAdd = new StatModifier(value, source);
        modifiers.Add(modToAdd);
        needToBeReCalculated = true;
    }

    public void RemoveModifier(string source)
    {
        //same: foreach(var modifier in modifiers) if (modifier.source == source) xóa modifier này
        modifiers.RemoveAll(modifier => modifier.source == source); // Xóa tất cả modifier được tạo bởi source này. listName.removeall(name => điều kiện) 
        needToBeReCalculated = true;
    }

    private float GetFinalValue()
    {
        float finalValue = baseValue;

        foreach (var modifier in modifiers)
        {
            finalValue += modifier.value;
        }

        return finalValue;
    }
    public void SetBaseValue(float value) => baseValue = value;
}

[Serializable]
public class StatModifier
{
    public float value;
    public string source; // e.g item,...etc

    public StatModifier(float value, string source) // Constructor
    {
        this.value = value;
        this.source = source;
    }
}
