using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HPInterface : MonoBehaviour
{
    public int HP;
    public int MaxHP = 10;
    public abstract void TakeDamage(int damage);

}
