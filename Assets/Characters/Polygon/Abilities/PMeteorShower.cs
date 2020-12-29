using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMeteorShower : AbilitySequence
{
    [SerializeField] private GameObject strike;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float yLowerBound;
    [SerializeField] private int count;
    [SerializeField] private float meteorInterval;

    public override IEnumerator cast()
    {
        GameObject go;
        // spawn some meteors, then make the boss crash down?
        for (int i = 0; i < count; i++) {
            go = (GameObject)Instantiate(strike, new Vector3(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound), 0f), Quaternion.identity);
            go.GetComponent<FallingStrike>().setCaster(caster);
            go.GetComponent<FallingStrike>().meteor();
            yield return new WaitForSeconds(meteorInterval);
        }
        go = (GameObject)Instantiate(strike, new Vector3(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound), 0f), Quaternion.identity);
        go.GetComponent<FallingStrike>().setCaster(caster);
        go.GetComponent<FallingStrike>().boss(this.gameObject);
        yield return new WaitForSeconds(meteorInterval);
    }
}
