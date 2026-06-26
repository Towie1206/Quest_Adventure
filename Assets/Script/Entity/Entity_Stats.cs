using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;


    public float GetPhysicalDamage( out bool isCrit) // từ biến local giờ có thể truy cập ở ngoài method 
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue() * 1;
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChange = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f; // bonus critchance from agility + 0.3 pre AGI
        float totalCritChance = baseCritChange + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f; // bonus critpower from strength + 0.5 pre STR
        float critPower = (baseCritPower + bonusCritPower) / 100; // chuyển thành số thập phân nhân với damage ( e.g 150 / 100 = 1.5f * totalDamage)

        isCrit = Random.Range(0, 100) < totalCritChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;
    }
    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // each agility give 0.5% evasion

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 85; // Max evasion will be capped at 85%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap); //value,min,max make sure value in range min - max

        return finalEvasion;
    }
}
