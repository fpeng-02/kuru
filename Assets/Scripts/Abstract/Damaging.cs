using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damaging : MonoBehaviour
{
    protected Entity owner;
    public void setOwner(Entity owner) { this.owner = owner; }
}
