using System.Security.Cryptography;
using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;

    [SerializeField] protected float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        lastTimeUsed = lastTimeUsed - cooldown;
    }

    public virtual void TryUseSkill()
    {

    }

    public void SetSkillUpgrade(UpgradeData upgrade)
    {
        upgradeType = upgrade.upgradeType;
        cooldown = upgrade.coolDown;
    }

    public bool CanUseSkill()
    {

        if(upgradeType == SkillUpgradeType.None)
            return false;
        if(OnCoolDown())
        {

            return false;
        }

        return true;
    }    

    protected bool Unlocked(SkillUpgradeType upgradeToCheck) => upgradeType == upgradeToCheck;

    private bool OnCoolDown() => Time.time < lastTimeUsed + cooldown; // use skill in 15s in the game and cooldown is 5 so when time in game is 20s it true
    public void SetSkillOnCooldown () => lastTimeUsed = Time.time; // lưu time in game
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed += cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;
}

