using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBAController : Ability, ICastable
{
    [SerializeField] private GameObject meleeAttack;

    public override void abilityAction()
    {
        Instantiate(meleeAttack, this.transform.position, Quaternion.Euler(new Vector3(0, 0, rotationAngle()));
    }
    public float rotationAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23F; //The distance between the camera and object
        Vector3 objectPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mousePos = mousePos - objectPos;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        return angle;
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
