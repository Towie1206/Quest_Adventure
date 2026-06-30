using UnityEngine;



public class Entity_Stats : MonoBehaviour
{
    public Stat_SetupSO defaultStatSetup;


    public Stat_ResourceGroup resources;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;
    public Stat_MajorGroup major;

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1) //scaleFactor e.g make a clone and clone only deal X% damage of your
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();

        float bonusElementalDamage = major.intelligence.GetValue() * 1; // bonus ElementalDamage from intelligence + 1 pre INT

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }

        if (lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }

        if (highestDamage <= 0)
        {
            element = ElementType.None;
            return 0;
        }

        float bonusFire = (element == ElementType.Fire) ? 0 : fireDamage * .5f;
        float bonusice = (element == ElementType.Ice) ? 0 : iceDamage * .5f;
        float bonuslightning = (element == ElementType.Lightning) ? 0 : lightningDamage * .5f;

        float weakerElementalDamage = bonusFire + bonusice + bonuslightning;
        float finalDamage = highestDamage + weakerElementalDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType element) // kháng phép
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * .5f;// bonus ElementalResistance from intelligence + 0.5% pre INT

        switch (element)
        {
            case ElementType.Fire: baseResistance = defense.fireRes.GetValue(); break;
            case ElementType.Ice: baseResistance = defense.iceRes.GetValue(); break;
            case ElementType.Lightning: baseResistance = defense.lightningRes.GetValue(); break;

        }

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f;
        float finalResistance = Mathf.Clamp(resistance, 0, resistanceCap) / 100; //convert value into 0 to 1 multiplier

        return finalResistance; // % kháng sẽ giảm

    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1) // từ biến local giờ có thể truy cập ở ngoài method 
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

        return finalDamage * scaleFactor;
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
        float baseMaxHealth = resources.maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return resources.maxHealth;
            case StatType.HealthRegen: return resources.healthRegen;

            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Intelligence: return major.intelligence;
            case StatType.Vitality: return major.vitality;

            case StatType.AttackSpeed: return offense.attackSpeed;
            case StatType.Damage: return offense.damage;
            case StatType.CritChance: return offense.critChance;
            case StatType.CritPower: return offense.critPower;
            case StatType.ArmorReduction: return offense.armorReduction;

            case StatType.FireDamage: return offense.fireDamage;
            case StatType.IceDamage: return offense.iceDamage;
            case StatType.LightningDamage: return offense.lightningDamage;

            case StatType.Armor: return defense.armor;
            case StatType.Evasion: return defense.evasion;

            case StatType.FireResistance: return defense.fireRes;
            case StatType.IceResistance: return defense.iceRes;
            case StatType.LightningResistance: return defense.lightningRes;

            default:
                return null;
        }
    }

    [ContextMenu("Update Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if(defaultStatSetup == null) 
        {
            Debug.Log("No defauls stat setup assigned");
            return;
        }

        resources.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resources.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);

        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.agility.SetBaseValue(defaultStatSetup.agility);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);

        offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offense.damage.SetBaseValue(defaultStatSetup.damage);
        offense.critChance.SetBaseValue(defaultStatSetup.critChance);
        offense.critPower.SetBaseValue(defaultStatSetup.critPower);
        offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);

        offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
        offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);

        defense.armor.SetBaseValue(defaultStatSetup.armor);
        defense.evasion.SetBaseValue(defaultStatSetup.evasion);

        defense.iceRes.SetBaseValue(defaultStatSetup.iceResistance);
        defense.fireRes.SetBaseValue(defaultStatSetup.fireResistance);
        defense.lightningRes.SetBaseValue(defaultStatSetup.lightningResistance);


    }
}
