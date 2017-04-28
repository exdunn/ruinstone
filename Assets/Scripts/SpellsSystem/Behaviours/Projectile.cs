using UnityEngine;
using System.Collections;
using System;

public class Projectile : Delivery {
    public const float THRESHOLD = 0.0005f;

    public float _speed = 0f;

    public bool _usesDir = false;

    public GameObject collidedTarget { get; set; }
    public Vector3 collidedLoc { get; set; }
    public bool collided { get; set; }
    public bool outOfRange { get; set; }
    public bool atLoc { get; set; }
    public Vector3 origin { get; set; }
    public Vector3 direction {
        get {
            return dir.normalized;
        }
        set {
            dir = value;
            dir.y = 0;
        }
    }
    public float distance {
        get {
            return Vector3.Distance(origin, this.transform.position);
        }
    }

    //public GameObject this;

    private Vector3 dir;

    private bool _start = false;
    private bool _move = false;
    private bool _moving = false;

    private bool debugged = false;

    void Start() {
        Init();
    }

    void Update() {
        if(!_start || isDone) {
            return;
        }

        if(Done()) {
            Finish();
            return;
        }

        if(_move) {   
            if(!debugged) {
                Debug.Log("C,O,P,D: " + this.transform.position + "; " + origin + "; " + point + "; " + direction);
                debugged = true;
            }
            point = new Vector3(point.x, origin.y, point.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, point, _speed * Time.deltaTime);

            if(Vector3.Distance(this.transform.position, point) <= THRESHOLD) {
                atLoc = true;
                collidedLoc = point;
            }

            if(distance >= _range) {
                outOfRange = true;
                collidedLoc = this.transform.position;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(Done() || !_start) {
            return;
        }

        if(other.tag == Types.TargetToString(_targetType)) {
            if(other.gameObject.GetComponent<WizardWars.PlayerController>().GetId() == caster.GetComponent<WizardWars.PlayerController>().GetId()) {
                return;
            }

            collided = true;
            collidedTarget = other.gameObject;
            collidedLoc = other.gameObject.transform.position;
        }
    }

    public override void DoEffect(GameObject caster, GameObject target, Vector3 point) {
        this.point = point;
        this.caster = caster;
        this.target = target;
        origin = caster.transform.position;
        Effect();
        _start = true;
    }

    protected override IEnumerator DuraEffect() {
        throw new NotImplementedException();
    }

    protected override void Effect() {
        SetPoint();
        _move = true;
    }

    protected override void Finish() {
        Debug.Log("Finish");
        _move = false;
        isDone = true;
        Destroy(this.gameObject);
    }

    private bool Done() {
        //Debug.Log("C, O, L: " + collided + "; " + outOfRange + "; " + atLoc);
        return collided || outOfRange || atLoc;
    }

    private void SetPoint() {
        if(_usesDir) { //Calculate direction, and max range point
            direction = point - origin;
            point = origin + (direction * _range);
            //point.Set(point.x, 1, point.z);
        }
        else { //Point = point
            direction = point - origin;
        }
    }

    public void Init() {
        //this = this.transform.GetChild(0).gameObject;
        collided = false;
        outOfRange = false;
        atLoc = false;
        origin = this.transform.position;
        _areaType = Types.Area.LINEAR;
        collidedTarget = null;
        collidedLoc = Vector3.zero;
    }
}