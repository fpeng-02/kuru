using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : Projectile
{
    public override Vector3 getDirVector() { return new Vector3(0, 0, 0) ; }
    public override Quaternion getQuaternion() { return this.quat; }
}
