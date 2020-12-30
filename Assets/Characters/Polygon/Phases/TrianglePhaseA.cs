using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePhaseA : Phase
{
    public override IEnumerator beginPhase()
    {
        Debug.Log("Laser should've casted..");
        owner.cast("TriLaser");
        yield return phaseLoop();
    }
    public override IEnumerator phaseLoop()
    {
        yield return null;
    }

}
