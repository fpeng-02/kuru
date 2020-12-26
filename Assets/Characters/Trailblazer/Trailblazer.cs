using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailblazer : Entity
{
    private List<Phase> phases = new List<Phase>();
    private int currentPhase;

    void Start()
    {
        GetComponents<Phase>(phases);
        currentPhase = 0;
        phases[currentPhase].init(this);
    }
}
