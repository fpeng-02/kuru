using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : Entity
{
    [SerializeField] private List<float> phaseThresholds;
    private List<Phase> phases = new List<Phase>();
    private int currentPhase;
    private Rigidbody2D rb2d;

    // specific for the hexagon phase
    private bool dashing;
    public void setDashing(bool dashing) { this.dashing = dashing; }
    public Quaternion quat;


    public void nextPhase()
    {
        phases[currentPhase].exitPhase();
        currentPhase++;
        phases[currentPhase].init(this);
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
        if (dashing) {
            rb2d.MovePosition(rb2d.transform.position + quat * Vector3.right * speed * Time.deltaTime);
        }
    }
}
