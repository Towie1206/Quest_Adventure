using System;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackSpeed;

    // physical
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction; // armor penetration: xuyen giap 

    // Elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
