using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WizardWars;

namespace SpellSystem {
    public static class SpellUtility {
        public static float TICK = 1f;

        public static PlayerManager CheckAndGetPlayer(GameObject target) {
            if(target == null) {
                Debug.Log("No target.");
                return null;
            }
            PlayerManager player = target.GetComponent<PlayerManager>();
            if(player == null) {
                Debug.Log("No player manager module in target.");
                return null;
            }
            return player;
        }
        public static GameObject SpawnProjectile(string prefab, Transform parent, Vector3 position, Quaternion rotation) {
            return PhotonNetwork.Instantiate(prefab, position, rotation, 0);
        }
        /* Damage */
        public static void Damage(GameObject target, float damage) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            player.UpdateHealth(-damage);
        }
        public static IEnumerator DamageOverTime(GameObject target, float damage, float duration) {
            float timer = 0f;
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                timer = duration;
            }
            while(timer < duration) {
                player.UpdateHealth(-damage);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        public static void AreaDamage(Types.Target type, Vector3 center, float radius, float damage) {
            List<GameObject> targets = Utils.GetAll(type, center, radius);
            for(int i = 0; i < targets.Count; ++i) {
                Damage(targets[i], damage);
            }
        }
        public static IEnumerator AreaDamageOverTime(Types.Target type, Vector3 center, float radius, float damage, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaDamage(type, center, radius, damage);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        /* Heal */
        public static void Heal(GameObject target, float heal) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            player.UpdateHealth(heal);
        }
        public static IEnumerator HealOverTime(GameObject target, float heal, float duration) {
            float timer = 0f;
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                timer = duration;
            }
            while(timer < duration) {
                player.UpdateHealth(heal);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        public static void AreaHeal(Types.Target type, Vector3 center, float radius, float heal) {
            List<GameObject> targets = Utils.GetAll(type, center, radius);
            for(int i = 0; i < targets.Count; ++i) {
                Heal(targets[i], heal);
            }
        }
        public static IEnumerator AreaHealOverTime(Types.Target type, Vector3 center, float radius, float heal, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaHeal(type, center, radius, heal);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        /* Status */
        public static void Status(GameObject target, int status) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            player.AddStatus(status);
        }
        public static IEnumerator StatusOverTime(GameObject target, int status, float duration) {
            float timer = 0f;
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                timer = duration;
            }
            while(timer < duration) {
                player.AddStatus(status);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        public static void AreaStatus(Types.Target type, Vector3 center, float radius, int status) {
            List<GameObject> targets = Utils.GetAll(type, center, radius);
            for(int i = 0; i < targets.Count; ++i) {
                Status(targets[i], status);
            }
        }
        public static IEnumerator AreaStatusOverTime(Types.Target type, Vector3 center, float radius, int status, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaStatus(type, center, radius, status);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        /* Control */
        public static void Control(GameObject target, int control) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            player.SetCrowdControl(control, true);
        }
        public static IEnumerator ControlOverTime(GameObject target, int control, float duration) {
            float timer = 0f;
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                timer = duration;
            }
            while(timer < duration) {
                player.SetCrowdControl(control, true);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        public static void AreaControl(Types.Target type, Vector3 center, float radius, int control) {
            List<GameObject> targets = Utils.GetAll(type, center, radius);
            for(int i = 0; i < targets.Count; ++i) {
                Control(targets[i], control);
            }
        }
        public static IEnumerator AreaControlOverTime(Types.Target type, Vector3 center, float radius, int control, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaControl(type, center, radius, control);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        /* Displace */
        public static void Displace(GameObject target, Vector3 impact, float force) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            //Calculate the direction (Push the target AWAY from origin)
            Vector3 relativeDir = impact - target.transform.position; //This is the direction between the target and the point of impact
            relativeDir = relativeDir.normalized;
            player.ForceMove(force, -relativeDir);
        }
        public static IEnumerator DisplaceOverTime(GameObject target, Vector3 impact, float force, float duration) {
            float timer = 0f;
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                timer = duration;
            }
            while(timer < duration) {
                Vector3 relativeDir = impact - target.transform.position; 
                relativeDir = relativeDir.normalized;
                player.ForceMove(force, -relativeDir);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        public static void AreaDisplace(Types.Target type, Vector3 center, float radius, float force) {
            List<GameObject> targets = Utils.GetAll(type, center, radius);
            for(int i = 0; i < targets.Count; ++i) {
                Displace(targets[i], center, force);
            }
        }
        public static IEnumerator AreaDisplaceOverTime(Types.Target type, Vector3 center, float radius, float force, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaDisplace(type, center, radius, force);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        /* Teleport */
        public static void Teleport(GameObject target, Vector3 point) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            player.Teleport(point);
        }
        /* Delay */
        public static IEnumerator Delay(float duration) {
            float timer = 0f;
            while(timer < duration) {
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
    }
}

