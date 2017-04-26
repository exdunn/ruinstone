using UnityEngine;
using System.Collections.Generic;

public abstract class Payload : Behaviour {
    public float _power = 0f;
    public float _duration = 0f;
    public float _area = 0f;

    public Types.Ability _abilityType = Types.Ability.DAMAGE;

    protected float _internal = 0f;

    void Start() {

    }

}
