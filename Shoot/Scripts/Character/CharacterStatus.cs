using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatus : MonoBehaviour
{
    public event Action OnDeathHandler;

    public float HP = 100;
    public float maxHP = 100;

    public void Damage(float val)
    {
        if (HP <= 0) return;

        HP -= val;
        //限制HP只能在0到maxHP间。
        HP = Mathf.Clamp(HP, 0, maxHP);

        if (HP <= 0)
        {
            Death();
            return;
        }

        OnDamage();
    }

    protected virtual void OnDamage() { }

    protected virtual void Death()
    {
        //引发事件
        if (OnDeathHandler != null) OnDeathHandler();
    }
}
