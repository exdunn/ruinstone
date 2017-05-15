using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WizardWars;

namespace SpellSystem {
    public abstract class Status : MonoBehaviour {
        public int _id;
        public float _duration;

        protected int where { get; set; }
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
                target.GetComponent<PlayerManager>().RemoveStatus(where);
            }
        }

        public abstract void Activate(GameObject target, int where);
        public abstract void Deactivate(GameObject target);

        protected abstract IEnumerator Run();
    }
}

