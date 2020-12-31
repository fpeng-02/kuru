using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonPhase : Phase
{
    [SerializeField] private float interval;

    public override IEnumerator beginPhase()
    {
        this.transform.position = Vector3.zero;
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            owner.cast("Create Bees");
            yield return new WaitForSeconds(interval);
        }
    }
}
