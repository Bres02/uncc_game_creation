using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public List<BossPhase> phases = new List<BossPhase>();

    private int currentHealth;
    private int phaseCounter;
    private BossPhase currentPhase;

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

    void NextPhase()
    {
        phaseCounter++;
        if (phaseCounter < phases.Count)
        {
            currentPhase = phases[phaseCounter];
            currentHealth = currentPhase.GetHealth();
        }
        else
        {
            Defeated();
        }
    }

    void Start()
    {
        phaseCounter = -1;
        NextPhase();
    }

    void FixedUpdate()
    {
        
    }
}
