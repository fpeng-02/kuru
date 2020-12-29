using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonPhase : Phase
{
    [SerializeField] private float interval;

    public override IEnumerator beginPhase()
    {
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            owner.cast("Meteor Shower");
            yield return new WaitForSeconds(interval);
        }
    }
}
