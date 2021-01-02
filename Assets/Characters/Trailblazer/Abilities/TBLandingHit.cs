using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBLandingHit : AbilitySequence
{
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private float spawnDistance;

    public override IEnumerator cast()
    {
        GameObject child = (GameObject)Instantiate(meleeHitbox, this.transform.position, Quaternion.identity);
        child.GetComponent<Projectile>().setOwner(this.gameObject.GetComponent<Entity>());
        child.GetComponent<Projectile>().initializeQuaternion(Quaternion.identity);
        yield return null;
    }
}
