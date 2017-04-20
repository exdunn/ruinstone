using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Projectile : Behaviour {
    public float _speed = 0f;
    public bool _pass = false;

    public Queue<GameObject> targets { get; set; }
    public bool collided { get; set; }
    public bool outOfRange { get; set; }
    public Transform origin { get; set; }
    public Vector3 direction { get; set; }
    public float distance {
        get {
            return Vector3.Distance(origin.position, this.transform.position);
        }
    }

    private Rigidbody _rigidbody;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        collided = false;
        outOfRange = false;
        origin = this.transform;
        targets = new Queue<GameObject>();

        _areaType = Types.Area.LINEAR;
    }

    void FixedUpdate() {
        if(Done()) {
            Finish();
        }
        else {
            if(distance >= _range) {
                outOfRange = true;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(!this.enabled || Done()) {
            return;
        }

        if(other.tag == Types.TargetToString(_targetType)) {
            collided = true;
            targets.Enqueue(other.gameObject);
        }
    }

    void OnEnable() {
        Effect();
    }

    void OnDisable() {
        Finish();
    }

    public bool Done() {
        return collided && !_pass || outOfRange;
    }

    protected override IEnumerator Duration() {
        //Does nothing
        Debug.Log("DuraEffect method in Projectile does nothing.");
        yield return null;
    }

    protected override void Finish() {
        _rigidbody.velocity *= 0;
        isDone = true;
    }

    public override void DuraEffect() {
        StartCoroutine(Duration());
    }

    public override void Effect() {
        Vector3 force = direction.normalized * _speed;
        _rigidbody.velocity = force;
    }
}
