using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDelayedStrike : AbilitySequence
{
    [SerializeField] private GameObject delayedStrike;
    [SerializeField] private float interval;
    [SerializeField] private int strikes;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float yLowerBound;
    [SerializeField] private float yUpperBound;

    public override IEnumerator cast()
    {
        for (int i = 0; i < strikes; i++) {
            Vector3 pos = new Vector3(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound), 0);
            GameObject tmpStrike = (GameObject)Instantiate(delayedStrike, pos, Quaternion.identity);
            tmpStrike.GetComponent<DelayedStrike>().setOwner(caster);
            yield return new WaitForSeconds(interval);
        }
        yield break;
    }
}
