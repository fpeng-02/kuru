using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBAHit : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    void OnTriggerEnter2D(Collider2D col)
    {
        Entity tempEntity = col.GetComponent<Entity>();
        if (tempEntity != null)
        {
            tempEntity.setHitPoints(tempEntity.getHitPoints() - damage);
        }
        Destroy(this);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
