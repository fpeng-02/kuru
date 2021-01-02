using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBFinalRageRing : AbilitySequence
{
    [SerializeField] GameObject ring;

    public override IEnumerator cast()
    {
        GameObject go = Instantiate(ring, this.transform.position, Quaternion.identity);
        go.GetComponent<ExpandingRing>().setCaster(caster);
        yield break;
    }
}