using UnityEngine;

public class Skill_Shard : Skill_Base
{
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2f; // thoi gian kích nổ



    public void CreateShard()
    {

        if (upgradeType == SkillUpgradeType.None)
            return;

        GameObject shard = Instantiate(shardPrefab,transform.position,Quaternion.identity);
        shard.GetComponent<SkillObject_Shard>().SetUpShard(detonateTime);
    }
}
