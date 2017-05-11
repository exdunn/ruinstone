using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SpellSystem {
    public abstract class Status : MonoBehaviour {
        public int _id;
        public float _power;
        public float _duration;

        public bool isDone { get; set; }
        protected bool isStarting { get; set; }
        public float durationLeft {
            get {
                return _duration - _timer;
            }
        }
        protected float _timer = 0f;
        void Awake() {
            isDone = false;
            isStarting = false;
        }

        public abstract void Activate(GameObject target);
        public abstract void Deactivate(GameObject target);

        protected abstract IEnumerator Run();
    }
}

