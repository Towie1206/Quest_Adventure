using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;


    public float GetPhysicalDamage(out bool isCrit) // từ biến local giờ có thể truy cập ở ngoài method 
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

    public float GetArmorMitigation(float armorReduction) // khả năng giảm sát thương của giáp
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue() * 1;  // bonus armor from vitality + 1 pre VIT
        float totalArmor = baseArmor + bonusArmor;

        //float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1); 
        float reductionMultiplier = Mathf.Clamp01(1 - armorReduction); // if pass .4f // 1 - .4f = .6f // 60% armor used 
        float effectiveArmor = totalArmor * reductionMultiplier; // giáp thực tế 

        // armor / ( armor + scalingConstant) scalingConstant : hằng số tỷ lệ = 100
        float mitagation = effectiveArmor / (effectiveArmor + 100); // e.g have 150 armor do formula 150 /250 = 0.6 so take 60% damage

        float mitigationCap = .85f; // max mitigation will be capped at 85%

        float finalMitigation = Mathf.Clamp(mitagation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        // chuyển thành số thập phân nhân ( e.g 30 / 100 = .3f ) // bo qua 30% giap
        float finalReduction = offense.armorReduction.GetValue() / 100;

        return finalReduction;
    }

    public float GetEvasion() // khả năng né tránh = dodge
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // each agility give 0.5% evasion

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 85; // Max evasion will be capped at 85%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap); //value,min,max make sure value in range min - max

        return finalEvasion;
    }
    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }
}
