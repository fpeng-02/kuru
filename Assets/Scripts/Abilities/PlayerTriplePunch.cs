using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriplePunch : GenericAbility
{

    private bool isSet = false;
    private Vector3 dirVector;

    public override GameObject getAttack(int index) { return attacks[0]; }

    public override Vector3 getSpawnVector(int index)
    {
        if (!isSet) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0.0F;
            dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
            dirVector.z = 0;
            dirVector = dirVector.normalized;
            isSet = true;
        }
        return this.transform.position + dirVector * spawnDistance;
    }

    public override Quaternion getSpawnAngle(int index)
    {
        if (!isSet) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0.0F;
            dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
            dirVector.z = 0;
            dirVector = dirVector.normalized;
        }
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    public override float getInterval(int index) { return 0.1f; }
}
