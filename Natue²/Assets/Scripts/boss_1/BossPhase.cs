using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    public int health;

    public List<BossAttack> attacks = new List<BossAttack>();
    public List<float> times = new List<float>();
    public List<int> repetitions = new List<int>();

    public int GetHealth()
    {
        return health;
    }
}
