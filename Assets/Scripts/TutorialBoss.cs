using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Entity
{

    private void castRandomAbility()
    {
        int toCast = Random.Range(0, this.abilities.Count);
        Debug.Log("cast ability " + toCast);
        cast(toCast);
    }

    public override void move()
    {
        Debug.Log("S** with D**");
    }

    void Start()
    {
        InvokeRepeating("castRandomAbility", 0.0f, 3.0f);
    }
}
