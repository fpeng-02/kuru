using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePhaseA : Phase
{
    public override IEnumerator beginPhase()
    {
        //Sprite to triangle
        Debug.Log("Laser should've casted..");
        owner.cast("Tri Laser");
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 10;
        yield return phaseLoop();
    }
    public override IEnumerator phaseLoop()
    {
        yield return null;
    }
    public override void exitPhase()
    {
        base.exitPhase();
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

    }

}
