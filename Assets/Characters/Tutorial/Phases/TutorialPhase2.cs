using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhase2 : Phase
{
    public override IEnumerator beginPhase()
    {
        // Animations would go here
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // "delay" phase
            Quaternion randDir = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            TutorialBoss boss = (TutorialBoss)owner;
            boss.setDirVector(randDir * Vector3.right);
            yield return new WaitForSeconds(1.0f);

            // attack
            owner.cast("Tutorial 3 Shotgun");
        }
    }
}
