using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCreateBees : AbilitySequence
{
    [SerializeField] private GameObject bee;
    [SerializeField] private int count;

    public override IEnumerator cast()
    {
        // spawn count bees, radially (not sure if this is a good idea...)
        for (int i = 0; i < count; i++) {
            float randomOffset = Random.Range(-15f, 15f);
            GameObject go = Instantiate(bee, this.transform.position, Quaternion.Euler(0, 0, i * 360 / count + randomOffset));
            Bee beeProj = go.GetComponent<Bee>();
            beeProj.initializeQuaternion(Quaternion.Euler(0, 0, i * 360 / count + randomOffset));
            beeProj.setOwner(caster);
            beeProj.callStart();
            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        }
        yield break;
    }
}
