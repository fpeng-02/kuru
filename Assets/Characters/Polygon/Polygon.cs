using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : Entity
{
    [SerializeField] private List<float> phaseThresholds;
    private List<Phase> phases = new List<Phase>();
    private int currentPhase;
    private Rigidbody2D rb2d;

    // specific for the pentagon phase
    private bool falling;
    public Vector3 meteorStrikePosition;
    public GameObject spawner;
    public void setFalling(bool falling) { this.falling = falling; }
    public Quaternion quat;

    public void nextPhase()
    {
        phases[currentPhase].exitPhase();
        currentPhase++;
        phases[currentPhase].init(this);
    }

    public void test()
    {
        cast("Meteor Shower");
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponents<Phase>(phases);
        currentPhase = 0;
        phases[currentPhase].init(this);
    }

    public override void customUpdate()
    {
        if (hitPoints <= phaseThresholds[currentPhase]) {
            nextPhase();
        }
    }

    public void FixedUpdate()
    {
        if (falling) {
            rb2d.MovePosition(rb2d.transform.position + quat * Vector3.right * speed * Time.deltaTime);
            if ((this.transform.position - meteorStrikePosition).magnitude < 0.1) {
                if (spawner != null) Destroy(spawner);
                falling = false;
            }
        }
    }
}
