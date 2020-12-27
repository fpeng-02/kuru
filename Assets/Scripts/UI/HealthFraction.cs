using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFraction : MonoBehaviour
{
    [SerializeField] private Entity owner;
    private Text healthFraction;
    private float maxHP;

    void Start()
    {
        maxHP = owner.getHitPoints();
        healthFraction = GetComponent<Text>();
    }

    void Update()
    {
        healthFraction.text = owner.getHitPoints()  + "/" + maxHP;
    }
}
