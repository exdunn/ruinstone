using UnityEngine;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {
    public SpellStats _stats;
    public Projectile _projectile;
    public List<Behaviour> _behaviours;

    public bool isDone { get; set; }
    public bool isActive { get; set; }

    void Start() {
        _behaviours = new List<Behaviour>();
        isDone = false;
        isActive = false;
    }

    public abstract void Activate(GameObject caster, Transform target);
    public abstract void Finish();
}
