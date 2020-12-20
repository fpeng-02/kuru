using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Ability : MonoBehaviour
{
    private List<Effect> effects;
    private readonly string name;
    private readonly double cooldown;
    private double cooldownProgress;


    public double getCooldownProgress()
    {
        return cooldownProgress;
    }
    public void setCooldownProgress(double cool)
    {
        cooldownProgress = cool;
    }

    public string getName()
    {
        return name;
    }
    public void abilityAction()
    {

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
