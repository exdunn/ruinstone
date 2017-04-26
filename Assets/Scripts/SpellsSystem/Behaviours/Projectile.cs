using UnityEngine;
using System.Collections;

public class Projectile : Delivery {
    public const float THRESHOLD = 0.05f;
    public float _speed = 0f;
    public bool _pass = false;

    //public Queue<GameObject> targets { get; set; }
    public GameObject collidedTarget { get; set; }
    public Transform collidedLoc { get; set; }
    public bool collided { get; set; }
    public bool outOfRange { get; set; }
    public bool atLoc { get; set; }
    public Transform origin { get; set; }
    public Vector3 direction { get; set; }
    public float distance {
        get {
            return Vector3.Distance(origin.position, this.transform.position);
        }
    }

    public Rigidbody _rigidbody;

    void Start() {
        Init();
    }

    void FixedUpdate() {
        Debug.Log("Fixed Update");
        if(Done()) {
            Debug.Log("DONE!");
            Finish();
        }
        else {
            if(distance >= _range) {
                outOfRange = true;
                collidedLoc = point;
            }
            if(point != null && Vector3.Distance(this.transform.position, point.position) <= THRESHOLD) {
                atLoc = true;
                collidedLoc = point;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with " + other);
        Debug.Log("Tag: " + other.tag);
        Debug.Log("First check: " + (Done()));
        if(Done()) {
            return;
        }
        else if(other.tag == Types.TargetToString(_targetType)) {
            Debug.Log("Collided with an enemy.");
            collided = true;
            //targets.Enqueue(other.gameObject);
            collidedTarget = other.gameObject;
            collidedLoc = other.transform;
        }
    }

    public bool Done() {
        Debug.Log("Collided, OutOfRange, AtLoc: " + collided + ", " + outOfRange + ", " + atLoc);
        return collided && !_pass || outOfRange || atLoc;
    }

    public override void DoEffect(GameObject caster, GameObject target, Transform point) {
        this.point = point;
        //this.caster = caster;
        //this.target = target;
        Effect();
    }

    protected override IEnumerator DuraEffect() {
        //Does nothing
        Debug.Log("DuraEffect method in Projectile does nothing.");
        yield return null;
    }

    protected override void Finish() {
        _rigidbody.velocity *= 0;
        isDone = true;
        Destroy(_rigidbody.gameObject);
    }

    protected override void Effect() {
        Debug.Log("Moving");

        //If point is null, then the point is actually at the max range point in the given direction
        if(point == null) {
            Debug.Log("Point is null");
            point = new GameObject().transform;
            point.position = origin.position + (direction.normalized * _range);
        }
        else { //Else just move towards the given point
            direction = (point.position - origin.position).normalized;
        }
        Debug.Log("POINT: " + point.position);
        Debug.Log("ORIGIN: " + origin.position);
        Vector3 force = direction.normalized * _speed;
        Debug.Log("Force: " + force);
        _rigidbody.velocity = force;
        Debug.Log("Target: " + point.position);
    }

    public void Init() {
        //_rigidbody = GetComponent<Rigidbody>();
        collided = false;
        outOfRange = false;
        origin = this.transform;
        //targets = new Queue<GameObject>();

        _areaType = Types.Area.LINEAR;
        collidedTarget = null;
        collidedLoc = null;
    }
}
