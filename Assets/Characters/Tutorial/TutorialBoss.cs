using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Entity
{
    private List<Phase> phases = new List<Phase>();
    [SerializeField] private List<float> phaseThresholds;
    private int currentPhase;
    private bool revived = false;

    private void nextPhase()
    {
        phases[currentPhase].StopAllCoroutines();
        currentPhase++;
        phases[currentPhase].init(this);
    }

    void Start()
    {
        GetComponents<Phase>(phases);
        currentPhase = 0;
        phases[currentPhase].init(this);
    }
    
    protected override void checkDie()
    {
        if (hitPoints < 0) {
            if (!revived) {
                nextPhase();
                revived = true;
            } else {
                Destroy(this.gameObject);
            }
        
        }
    }

    public override void customUpdate()
    {
        if (hitPoints < phaseThresholds[currentPhase]) {
            nextPhase();
        }
    }
}
