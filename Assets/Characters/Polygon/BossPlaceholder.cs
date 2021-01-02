using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlaceholder : MonoBehaviour
{
    // specific for the pentagon phase
    public Vector3 startPos;
    public Vector3 endPos;
    public float timeToDestination;
    private float t;

    void Start()
    {
        t = 0;
    }

    public void Update()
    {
        t += Time.deltaTime/timeToDestination;
        transform.position = Vector2.Lerp(startPos, endPos, t);
        if (Vector3.Magnitude(transform.position - endPos) < 0.1)
        {
            GameObject.FindGameObjectWithTag("Boss").transform.position = endPos;
            Destroy(this.gameObject);
        }
    }
}
