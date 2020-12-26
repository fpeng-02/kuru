using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhase4 : Phase
{
    public override IEnumerator beginPhase()
    {
        // Animations would go here
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        TutorialBoss boss = (TutorialBoss)owner;
        while (true) {
            // "delay" phase
            Quaternion randDir = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            boss.setDirVector(randDir * Vector3.right);
            yield return new WaitForSeconds(1.0f);

            // attack
            boss.setDirVector(new Vector3(0, 0, 0));
            owner.cast("Tutorial Delayed Strike");
            owner.cast("Tutorial Laser Attack");
            yield return new WaitForSeconds(2.2f);
        }
    }
}
