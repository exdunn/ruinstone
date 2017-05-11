using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpellSystem {
    public abstract class Projectile : MonoBehaviour {
        public const float THRESHOLD = 0.005f;
        public SpellStats _stats;

        public Types.Area _areaType = Types.Area.LINEAR;
        public Types.Target _targetType = Types.Target.ENEMY;

        protected bool isStarting { get; set; }
        protected bool isDone { get; set; }
        protected bool isDoingEffect { get; set; }

        protected bool atLocation { get; set; }
        protected bool collidedWithTarget { get; set; }
        protected bool outOfRange { get; set; }

        protected GameObject caster { get; set; }
        protected Vector3 origin { get; set; }
        protected Vector3 target { get; set; }
        protected Vector3 direction { get; set; }
        protected float distance {
            get {
                return Vector3.Distance(origin, this.transform.position);
            }
        }


        void Awake() {
            Debug.Log("Start");
            isStarting = false;
            isDone = false;
            isDoingEffect = false;
            atLocation = false;
            collidedWithTarget = false;
            outOfRange = false;
            caster = null;
        }

        void Update() {
            Debug.Log("isStarting: " + isStarting);
            Debug.Log("isDone: " + isDone);
            if(!isStarting || isDone) {
                
                return;
            }

            if(IsReadyToFinish()) {
                isDone = true;
                return;
            }

            target = LevelPointToCaster(target, origin);
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, _stats.GetSpeed() * Time.deltaTime);

            if(Vector3.Distance(this.transform.position, target) <= THRESHOLD) {
                atLocation = true;
                if(isDoingEffect) {
                    return;
                }
                isDoingEffect = true;
                OnLocation();

                if (distance >= _stats.GetRange()) {
                    outOfRange = true;
                    if(isDoingEffect) {
                        return;
                    }
                    isDoingEffect = true;
                    OnOutOfRange();
                }
            }
        }

        void OnTriggerEnter(Collider other) {
            if(isDone || !isStarting) {
                return;
            }

            if (other.tag == Types.TargetToString(_targetType)) {
                if(other.gameObject.GetComponent<WizardWars.PlayerManager>().playerId == caster.GetComponent<WizardWars.PlayerManager>().playerId) {
                    return;
                }
                collidedWithTarget = true;
                if(isDoingEffect) {
                    return;
                }
                isDoingEffect = true;
                OnCollide(other.gameObject);
            }
        }

        //Target is where the caster clicked
        public void Move(GameObject caster, Vector3 target) {
            this.target = target;
            this.caster = caster;
            origin = caster.transform.position;
            SetTargetAndDirection();
            Debug.Log("Starting is True now");
            isStarting = true;
            Debug.Log("isStarting: " + isStarting);
        }

        protected bool IsReadyToFinish() {
            return outOfRange || atLocation || collidedWithTarget;
        }

        protected void Die() {
            PhotonNetwork.Destroy(this.gameObject);
        }

        protected Vector3 LevelPointToCaster(Vector3 point, Vector3 caster) {
            return new Vector3(point.x, caster.y, point.z);
        }

        protected void SetTargetAndDirection() {
            //If we only have direction, calculate the max range as the target point
            //If we only have target point, calculate the direction
            Debug.Log("stats: " + _stats);
            if (_areaType == Types.Area.LINEAR) {
                
                direction = target - origin; //The direction is the difference between where the user clicked and where the user is
                target = origin + (direction.normalized * _stats.GetRange()); //The actual target is the max range position in the set direction
            }
            else {
                direction = target - origin; //Since the target is actually where the user clicked. No need for more
            }
        }

        //What to do when we collide with a valid target
        protected abstract void OnCollide(GameObject target);
        //What to do when we get to the target location
        protected abstract void OnLocation();
        //What to do when we go out of range
        protected abstract void OnOutOfRange();
        //What to do when we dissipate, or die
        public abstract void Dissipate();
    }
}

