using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SpellSystem {
    public abstract class Status : MonoBehaviour {
        public int _id;
        public float _duration;

        public bool isDone { get; set; }
        protected bool isStarting { get; set; }
        public float durationLeft {
            get {
                return _duration - _timer;
            }
        }
        protected float _timer = 0f;
        protected GameObject target;
        void Awake() {
            isDone = false;
            isStarting = false;
        }

        void Update() {
            if(!isStarting || isDone) {
                return;
            }
            if(isDone) {
                //Call Deactivate
                //Remove self from target
            }
        }

        public abstract void Activate(GameObject target);
        public abstract void Deactivate(GameObject target);

        protected abstract IEnumerator Run();
    }
}

