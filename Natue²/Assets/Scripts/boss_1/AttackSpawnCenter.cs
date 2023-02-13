using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawnCenter : BossAttack
{
    public GameObject prefab;

    public override void StartAttack()
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
