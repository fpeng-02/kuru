        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailblazer : Entity
{
    [SerializeField] private List<float> phaseThresholds;
    private List<Phase> phases = new List<Phase>();
    private int currentPhase;

    public void nextPhase()
    {
        phases[currentPhase].exitPhase();
        currentPhase++;
        phases[currentPhase].init(this);
    }

    void Start()
    {   
        GetComponents<Phase>(phases);
        currentPhase = 0;
        phases[currentPhase].init(this);
    }

    public override void customUpdate()
    {
        if (hitPoints <= phaseThresholds[currentPhase]) {
            nextPhase();
        }
    }
}
