using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : Projectile
{
    public override Vector3 getDirVector() { return this.initialDir; }
    public override Quaternion getQuaternion() { return this.quat; }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0) {
            Destroy(this.gameObject);
        }
    }
}
