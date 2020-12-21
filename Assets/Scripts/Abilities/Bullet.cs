using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public override Vector3 getDirVector() { return this.initialDir; }
    public override Quaternion getQuaternion() { return this.quat; }


}
