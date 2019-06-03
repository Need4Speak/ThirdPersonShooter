using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructable
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] Ragdoll ragdoll;

    /**
     * 重生点随机复活
     * */
    void SpawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }
    /**
     * 死亡
     * */
    public override void Die()
    {
        base.Die();
        ragdoll.EnableRagdoll(true);
        SpawnAtNewSpawnPoint();
    }

    /**
     * 受伤
     * */
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    [ContextMenu("Test Die")]
    void TestDie()
    {
        Die();
    }
}
