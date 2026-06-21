using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>(); // getcomponent mỗi khi sử dụng thay vì chỉ getcomponent 1 lần ở awake


    public override void TakeDamage(float damage,Transform damageDealer)
    {
        //!try enter battle state 
        //?if damedealer == player;
        // enemy.player == damageDealer;
        base.TakeDamage(damage, transform);

        if (isDead)
            return;

        if (damageDealer.GetComponent<Player>() != null) // mean : người thực hiện đòn đánh có component Player
            enemy.TryEnterBattleState(damageDealer);

       
    }
}
