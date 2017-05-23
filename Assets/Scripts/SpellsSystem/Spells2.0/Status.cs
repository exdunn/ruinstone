using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WizardWars;

namespace SpellSystem {
    public abstract class Status : Photon.MonoBehaviour {

        public float _duration { get; set; }
        protected int where { get; set; }
        public bool isDone { get; set; }
        public bool isEnd { get; set; }
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
            isEnd = false;
            isStarting = false;
        }

        void Update() {
            //Debug.Log(isDone +", " + isEnd + "," + isStarting);
;            if(!isStarting || isDone) {
                return;
            }
            if(isEnd) {
                //target.GetComponent<PlayerManager>().RemoveStatus(where);
                target.GetComponent<PlayerManager>().RemoveStatus(this);
                isDone = true;
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Set the parent of the status prefab
        /// </summary>
        public void ParentStatus(int playerId)
        {
            Debug.Log("parentstatus called");
            GetComponent<PhotonView>().RPC("OnParent", PhotonTargets.All, playerId);
        }

        /// <summary>
        /// Find player with playerId and parent status to them
        /// </summary>
        /// <param name="playerId"></param>
        [PunRPC]
        public void OnParent(int playerId)
        {
            Debug.Log("onparent called");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (playerId == player.GetComponent<PlayerManager>().playerId)
                {
                    transform.parent = player.transform;
                }
            }
        }

        public abstract void Activate(GameObject target, int where);
        public abstract void Deactivate(GameObject target);

        protected abstract IEnumerator Run();
    }
}

