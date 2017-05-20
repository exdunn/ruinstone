using UnityEngine;
using System;
using System.Collections.Generic;

public class SpellStats: MonoBehaviour {

    [SerializeField]
    public int id;

    [SerializeField]
    public string spellName;

    [SerializeField]
    public string description;

    /// <summary>
    /// projectile, ground, self
    /// </summary>
    [SerializeField]
    public string behaviour;

    [SerializeField]
    public float damage;

    [SerializeField]
    public float projRadius;

    [SerializeField]
    public float areaRadius;

    [SerializeField]
    public float range;

    [SerializeField]
    public float speed;

    [SerializeField]
    public float duration;

    [SerializeField]
    public float delay;

    [SerializeField]
    public float cooldown;

    [SerializeField]
    public Sprite runeSprite;

    [SerializeField]
    public Sprite highlightedRuneSprite;

    [SerializeField]
    public Sprite iconSprite;
}
