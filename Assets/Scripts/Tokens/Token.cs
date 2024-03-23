using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Token : MonoBehaviour
{
    public int MaxActionPoints;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currHp;
    protected bool isDamageable;

    [SerializeField] protected Vector3 StartingPosition;

    protected virtual void Init()
    {
        MaxActionPoints = 0;
        maxHp = 1;
        currHp = 1;
        isDamageable = false;

        StartingPosition = new Vector3(0,0,0);
    }

    protected virtual void OnHpReduced(int damage)
    {
        currHp -= damage;
        //update UIs

        if (currHp <= 0) OnDeath();
    }

    protected virtual void OnDeath()
    {

    }
}
