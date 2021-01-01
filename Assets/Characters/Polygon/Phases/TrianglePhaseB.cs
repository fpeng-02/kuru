using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePhaseB : Phase
{
    [SerializeField] private float interval;

    // Start is called before the first frame update
    public override IEnumerator beginPhase()
    {
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while(true)
        {
            owner.cast("Bouncing Stream");
            yield return new WaitForSeconds(interval);
        }
    }

    public override void exitPhase()
    {
        base.exitPhase();
    }

    // Update is called once per frame

}
