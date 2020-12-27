using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPhase5 : Phase
{
    [SerializeField] private float interval;
    public override IEnumerator beginPhase()
    {
        //Introduction cutscene
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true)
        {
            // delay
            owner.cast("Chain Pound");

            yield return new WaitForSeconds(interval);
            // laser!!
        }
    }

    public override void exitPhase()
    {
        StopAllCoroutines();
    }
}
