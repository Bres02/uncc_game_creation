using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandomlySpawnTop : BossAttack
{
    public GameObject prefab;

    public float height;
    public float minRange;
    public float maxRange;

    public override void StartAttack()
    {
        Instantiate(prefab, new Vector3(Random.Range(minRange, maxRange), height, 0), Quaternion.identity);
    }
}
