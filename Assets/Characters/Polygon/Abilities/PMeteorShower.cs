using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMeteorShower : AbilitySequence
{
    public override IEnumerator cast()
    {
        // each falling object marks a position first, then hits it? 
        // maybe each marker should be instantiated first? 
        yield break;
    }
}
