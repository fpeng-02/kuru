using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private List<Effect> effects;
    [SerializeField] private readonly string name;
    [SerializeField] private readonly double cooldown;
    private double cooldownProgress;


    public double getCooldownProgress() { return cooldownProgress; }
    public void setCooldownProgress(double cool) { cooldownProgress = cool; }
    public string getName() { return name; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
