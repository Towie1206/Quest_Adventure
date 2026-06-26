using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;
        return baseHp + bonusHp;
    }

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // each agility give 0.5% evasion

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 85; //evasion will be capped at 85%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap); //value,min,max make sure value in range min - max

        return finalEvasion;
    }
}
