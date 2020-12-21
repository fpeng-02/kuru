using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSingleShot : GenericAbility
{
    public override GameObject getAttack(int index)
    {
        return this.attacks[0];
    }
    public override Vector3 getSpawnVector(int index)
    {
        return this.transform.position;
    }
    public override Quaternion getSpawnAngle(int index)
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerPos.z = 0.0F;
        Vector3 dirVector = playerPos - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
    public override float getInterval(int index)
    {
        return 0.1f;
    }
}
