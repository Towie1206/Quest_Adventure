using UnityEngine;

public enum SkillUpgradeType 
{
    None,

    // ------ Dash Tree ------
    Dash, // Dash to avoid damage
    Dash_CloneOnStart, // Create a clone when dash starts
    Dash_CloneOnStartAndArrival, // Create a clone when dash starts and end
    Dash_ShardOnStats, // Create a shard when dash starts
    Dash_ShardOnStartAndArrival, // Create a shard when dash starts and ends

    // ------ Shard Tree ------
    Shard, // The shard explodes when touched by an enemy or time goes up
    Shard_MoveToEnemy, // Shard will move towards nearest enemy
    Shard_MultiCast, // Shard ability can have up to N charges. You can cast them all in a raw ( có n lần, có thể dùng liên tiếp)
    Shard_Teleport, // You can swap places with the last shard you created
    Shard_TeleportHpRewind, // When you swap place with shard, your hp% is same as it was when you created shard

}
