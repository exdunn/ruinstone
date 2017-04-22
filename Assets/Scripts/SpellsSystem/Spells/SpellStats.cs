using UnityEngine;
using System;
using System.Collections.Generic;

public class SpellStats: MonoBehaviour {

    [SerializeField]
    private int id;

    [SerializeField]
    private string spellName;

    [SerializeField]
    private string description;

    /// <summary>
    /// projectile, ground, self
    /// </summary>
    [SerializeField]
    private string behaviour;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float radius;

    [SerializeField]
    private float range;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float delay;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private Sprite runeSprite;

    [SerializeField]
    private Sprite highlightedRuneSprite;

    public Sprite GetRuneSprite()
    {
        return runeSprite;
    }

    public Sprite GetHighlightedRuneSprite()
    {
        return highlightedRuneSprite;
    }
}
