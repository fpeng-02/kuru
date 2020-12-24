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
        //TutorialBoss boss = owner.gameObject.GetComponent<TutorialBoss>();
        TutorialBoss boss = (TutorialBoss)owner;
        boss.testLol(); 

        yield return new WaitForSeconds(1.0f);
        yield return attackAction();
    }

    public override IEnumerator attackAction()
    {
        owner.cast("Tutorial Single Shot");
        yield return delayAction();
    }
}
