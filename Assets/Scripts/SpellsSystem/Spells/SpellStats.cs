using UnityEngine;
using System;
using System.Collections.Generic;

public class SpellStats {

    int id;
    string name;
    string description;

    /// <summary>
    /// projectile, ground, self
    /// </summary>
    string behaviour;

    float damage;
    float range;
    float radius;
    float speed;
    float duration;

    #region Get Methods

    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetBehaviour()
    {
        return behaviour;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetRadius()
    {
        return radius;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetDuration()
    {
        return duration;
    }

    #endregion

    #region Set Methods

    public void SetId(int value)
    {
        id = value;
    }

    public void SetName(string value)
    {
        name = value;
    }

    public void SetDescription(string value)
    {
        description = value;
    }

    public void SetBehaviour(string value)
    {
        behaviour = value;
    }

    public void SetDamage(int value)
    {
        damage = value;
    }

    public void SetRange(int value)
    {
        range = value;
    }

    public void SetRadius(int value)
    {
        radius = value;
    }

    public void SetSpeed(int value)
    {
        speed = value;
    }

    public void SetDuration(int value)
    {
        duration = value;
    }

    #endregion
}
