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
            //Debug.Log("Access origin in DISTANCE");
            //Debug.Log("origin in DISTANCE: " + origin);
            return Vector3.Distance(origin, this.transform.position);
        }
    }

    public Rigidbody _rigidbody;

    private Vector3 dir;

    private bool _start = false;

    void Start() {
        Init();
    }

    void FixedUpdate() {
        //Debug.Log("Fixed Update");
        if(Done()) {
            //Debug.Log("DONE!");
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
        //Debug.Log("Collided with " + other);
        //Debug.Log("Tag: " + other.tag);
        //Debug.Log("First check: " + (Done()));
        if(Done() || !_start) {
            return;
        }
        if(other.tag == Types.TargetToString(_targetType)) {
            if(other.gameObject.GetComponent<WizardWars.PlayerController>().GetId() == caster.GetComponent<WizardWars.PlayerController>().GetId()) {
                return;
            }
            //Debug.Log("Collided with an enemy.");
            collided = true;
            //targets.Enqueue(other.gameObject);
            collidedTarget = other.gameObject;
            collidedLoc = other.transform;
        }
    }

    public bool Done() {
        //Debug.Log("Collided, OutOfRange, AtLoc: " + collided + ", " + outOfRange + ", " + atLoc);
        return collided && !_pass || outOfRange || atLoc;
    }

    public override void DoEffect(GameObject caster, GameObject target, Transform point) {
        this.point = point;
        this.caster = caster;
        this.target = target;
        _start = true;
        Effect();
    }

    protected override IEnumerator DuraEffect() {
        //Does nothing
        Debug.Log("DuraEffect method in Projectile does nothing.");
        yield return null;
    }

    protected override void Finish() {
        //Debug.Log("Finishing...");
        //Debug.Log("Destroying: " + _rigidbody.gameObject);
        _rigidbody.velocity *= 0;
        isDone = true;
        Destroy(_rigidbody.gameObject);
    }

    protected override void Effect() {
        //Debug.Log("Moving");
        //Either the direction is given, but the destination is not
        //OR the destination is given, but the direction is not

        //If point is null, then the point is actually at the max range point in the given direction
        if(point == null) { //Direction is given, go in direction to max range point
            //Debug.Log("Point is null");
            point = Utils.CreateNewTransform(Vector3.zero);
            //Debug.Log("Access origin in EFFECT when setting point position");
            point.position = origin + (direction.normalized * _range);
        }
        else { //Direction is not given, but position is given. The direction is the difference between points
            //Debug.Log("Access origin in EFFECT when setting direction");
            direction = (point.position - origin).normalized;
        }
        //Debug.Log("POINT: " + point.position);
        //Debug.Log("ORIGIN: " + origin);
        Vector3 force = direction.normalized * _speed;
        //Debug.Log("Force: " + force);
        _rigidbody.velocity = force;
        //Debug.Log("Target: " + point.position);
    }

    public void Init() {
        //_rigidbody = GetComponent<Rigidbody>();
        collided = false;
        outOfRange = false;
        //Debug.Log("Access origin in INIT");
        origin = this.transform.position;
        //targets = new Queue<GameObject>();

        _areaType = Types.Area.LINEAR;
        collidedTarget = null;
        collidedLoc = null;
    }
}
