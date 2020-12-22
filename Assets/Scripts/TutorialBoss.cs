using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Entity
{
    public void Update()
    {
        // update spell CDs
        updateCooldowns();
        cast(0);
        cast(1);
    }
    public override void move()
    {
        Debug.Log("S** with D**");
    }
}
