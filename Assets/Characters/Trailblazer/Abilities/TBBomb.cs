using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBBomb : AbilitySequence
{
    [SerializeField] GameObject bomb;

    public override IEnumerator cast()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        GameObject child = (GameObject)Instantiate(bomb, this.transform.position, Quaternion.Euler(0, 0, 0));
        ThrowableBomb b = child.GetComponent<ThrowableBomb>();
        b.setOwner(this.gameObject.GetComponent<Entity>());
        b.setTargetPos(playerPos);
        b.StartCoroutine("throwBomb");
        //child.GetComponent<Projectile>().initializeDirVector(new Vector3(0, 0, 0));
        //child.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, 0));
        yield return null;
    }
}
