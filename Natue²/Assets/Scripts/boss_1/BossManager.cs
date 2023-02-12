using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public List<BossPhase> phases = new List<BossPhase>();

    private int currentHealth;
    private int phaseCounter;
    private BossPhase currentPhase;
    private float bossTimer;
    private bool onCooldown;
    private List<BossAttack> attackList = new List<BossAttack>();
    private List<float> timeList = new List<float>();
    private int listCounter;

    public void Damage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            NextPhase();
        }
    }

    void Defeated()
    {

    }

    void InitializeAttackLists(BossPhase phase)
    {
        attackList.Clear();
        timeList.Clear();
        listCounter = -1;
        for (int i = 0; i < phase.attacks.Count; i++)
        {
            for (int j = 0; j < phase.repetitions[i]; j++)
            {
                attackList.Add(phase.attacks[i]);
                timeList.Add(phase.times[i] / 2);
            }
        }
        onCooldown = true;
        bossTimer = 0;
    }

    void NextPhase()
    {
        phaseCounter++;
        if (phaseCounter < phases.Count)
        {
            currentPhase = phases[phaseCounter];
            currentHealth = currentPhase.GetHealth();
            InitializeAttackLists(currentPhase);
        }
        else
        {
            Defeated();
        }
    }

    void Start()
    {
        bossTimer = 0; 
        phaseCounter = -1;
        NextPhase();
    }

    void FixedUpdate()
    {
        bossTimer -= Time.deltaTime;
        if (bossTimer < 0)
        {

        }
    }
}
