using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonPhase : Phase
{
    public override IEnumerator beginPhase()
    {
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        yield break;
    }
}
