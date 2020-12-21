using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleShot : GenericAbility
{
    public override GameObject getAttack(int index) { return attacks[0]; }

    public override Vector3 getSpawnVector(int index)
    {
        /*
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.0F;
        Vector3 dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        return this.transform.position + dirVector * spawnDistance; */
        return this.transform.position;
    }

    public override Quaternion getSpawnAngle(int index)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.0F;
        Vector3 dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    public override float getInterval(int index) { return 0.0f; }
}
