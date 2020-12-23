using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float hitPoints;
    [SerializeField] private string entityName;
    private bool invulnerable = false;
    protected List<AbilitySequence> abilities = new List<AbilitySequence>();
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
    
    public void setInvulnerable(bool invulnerable) { this.invulnerable = invulnerable; }
    public bool getInvulnerable() { return this.invulnerable; }
    public void setSpeed(float speed) { this.speed = speed; }
    public float getSpeed() { return this.speed; }
    public void setHitPoints(float hp) { this.hitPoints = hp; }
    public float getHitPoints() { return this.hitPoints; }
    public string getEntityName() { return this.entityName; }

    /// <summary>
    /// Use this signature for bosses; it's more likely you'll need exact names for abilities for them
    /// </summary>
    /// <param name="abilityName"></param>
    public void cast(string abilityName)
    {
        foreach (AbilitySequence ability in abilities) {
            if (ability.getAbilityName() == abilityName) {
                ability.setCaster(this);
                StartCoroutine(ability.cast());
                cooldownTimers.Add(ability.getAbilityName(), ability.getCooldown());
            }
        }
    }

    /// <summary>
    /// Use this signature for players; it's more likely that a key is mapped to an index instead of an ability name
    /// </summary>
    /// <param name="index"></param>
    public void cast(int index)
    {
        // check CDs here 
        if (!cooldownTimers.ContainsKey(abilities[index].getAbilityName())) {
            AbilitySequence toCast = abilities[index];
            toCast.setCaster(this);
            StartCoroutine(toCast.cast());
            cooldownTimers.Add(toCast.getAbilityName(), toCast.getCooldown());
        }
        // TODO: cooldown UI?
    }

    protected void updateCooldowns()
    {
        Dictionary<string, float> tmpCooldownTimers = new Dictionary<string, float>();
        foreach (KeyValuePair<string, float> timer in cooldownTimers) {
            if (timer.Value - Time.deltaTime < 0) {
                continue;
            }
            tmpCooldownTimers.Add(timer.Key, timer.Value - Time.deltaTime);
        }
        cooldownTimers = tmpCooldownTimers;
    }

    protected virtual void checkDie()
    {
        if (hitPoints <= 0) {
            // TODO: make this virtual so you can have unique death animations?
            Destroy(this.gameObject);
        }
    }

    protected void entityUpdate()
    {
        updateCooldowns();
        checkDie();
    }

    void Awake()
    {
        GetComponents<AbilitySequence>(abilities);
    }

    public virtual void Update()
    {
        entityUpdate();
    }
}
