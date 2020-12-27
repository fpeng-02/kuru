using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Entity owner;
    private Image healthBar;
    private float maxHP;

    void Start()
    {
        maxHP = owner.getHitPoints();
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = owner.getHitPoints() / maxHP;
    }
}
