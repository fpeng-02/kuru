using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhase1 : Phase
{
    public override IEnumerator beginPhase()
    {
        // Animations would go here
        yield return delayAction();
    }

    public override IEnumerator delayAction()
    {
        // choose movement
        Quaternion randDir = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        TutorialBoss boss = (TutorialBoss)owner;
        boss.setDirVector(randDir * Vector3.right);
        yield return new WaitForSeconds(1.0f);
        yield return attackAction();
    }

    public override IEnumerator attackAction()
    {
        owner.cast("Tutorial Single Shot");
        yield return delayAction();
    }
}
