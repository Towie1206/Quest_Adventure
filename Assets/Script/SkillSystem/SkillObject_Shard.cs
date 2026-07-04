using UnityEngine;

public class SkillObject_Shard : SkillObject_Base
{
    [SerializeField] private GameObject vfxPrefab;


    public void SetUpShard(float detinationTime) //thời gian đến đích
    {
        Invoke(nameof(Explode), detinationTime);//thực thi (method) sau một time xác định
    }

    private void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        Instantiate(vfxPrefab , transform.position, Quaternion.identity);

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == null)
            return;

        Explode();
    }

}
