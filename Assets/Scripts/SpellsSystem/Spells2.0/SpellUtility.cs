using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WizardWars;

namespace SpellSystem {
    public static class SpellUtility {
        public static float TICK = 1f;
        public static float GROUND_HEIGHT = -2.3f;

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
        public static GameObject SpawnProjectile(string prefab, Transform parent, Vector3 position, Quaternion rotation, float projRadius) {
            GameObject projectile = PhotonNetwork.Instantiate(prefab, position, rotation, 0);
            projectile.GetComponent<SphereCollider>().radius = projRadius;
            return projectile;
        }
        public static GameObject SpawnIndicator(string prefab, Transform parent, Vector3 position, Quaternion rotation, float projRadius) {
            GameObject indicator = PhotonNetwork.Instantiate(prefab, position, rotation, 0);
            return indicator;
        }
        public static List<GameObject> GetAll(Types.Target type, Vector3 center, float projRadius) {
            //Debug.Log(center + ", " + radius);
            Collider[] t = Physics.OverlapSphere(center, projRadius);
            List<GameObject> all = new List<GameObject>();
            foreach(Collider c in t) {
                //Debug.Log("c: " + c);
                if(c.gameObject.CompareTag(Types.TargetToString(type))) {
                    all.Add(c.gameObject);
                }
            }
            return all;
        }
        public static Vector3 LevelPoint(Vector3 point, float offset = 0f) {
            return new Vector3(point.x, GROUND_HEIGHT + offset, point.z);
        }


        /******************************************************** DAMAGE ********************************************************/


        public static void Damage(GameObject target, GameObject caster, float damage) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }

            // multiply damage by the targets damage received modifier and the casters damage modifier
            damage = damage * player.damageReceivedModifier * caster.GetComponent<PlayerManager>().damageModifier;

            // update the caster's damage dealt
            caster.GetComponent<PhotonView>().
                RPC("BroadcastDamageDealt",
                PhotonTargets.All,
                caster.GetComponent<PlayerManager>().damageDealt + damage);

            // if target's health is reduced to 0, increment caster's kills
            if (player.UpdateHealth(-damage) <= 0)
            {
                caster.GetComponent<PhotonView>().
                    RPC("BroadcastKills", 
                    PhotonTargets.All, 
                    ++caster.GetComponent<PlayerManager>().kills);
            }
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
        public static void AreaDamage(Types.Target type, GameObject caster, Vector3 center, float areaRadius, float damage) {
            List<GameObject> targets = GetAll(type, center, areaRadius);
            Debug.Log("Targets: ");
            for(int i = 0; i < targets.Count; ++i) {
                Debug.Log(targets[i]);
                Damage(targets[i], caster, damage);
            }
        }
        public static IEnumerator AreaDamageOverTime(Types.Target type, GameObject caster, Vector3 center, float areaRadius, float damage, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaDamage(type, caster, center, areaRadius, damage);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }


        /******************************************************** HEAL ********************************************************/


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
        public static void AreaHeal(Types.Target type, Vector3 center, float areaRadius, float heal) {
            List<GameObject> targets = GetAll(type, center, areaRadius);
            for(int i = 0; i < targets.Count; ++i) {
                Heal(targets[i], heal);
            }
        }
        public static IEnumerator AreaHealOverTime(Types.Target type, Vector3 center, float areaRadius, float heal, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaHeal(type, center, areaRadius, heal);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }


        /******************************************************** Status ********************************************************/


        public static void Status(String prefab, GameObject target) {
            PlayerManager player = CheckAndGetPlayer(target);

            if (!player) {
                return;
            }
            // instantiate the status so that all players can see it
            GameObject newStatus = PhotonNetwork.Instantiate(prefab, target.transform.position, target.transform.rotation, 0);

            // parent the status to the target
            newStatus.transform.parent = target.transform;
            newStatus.GetComponent<Status>().ParentStatus(player.playerId);

            // add the status to the player
            player.AddStatus(newStatus);
        }

        public static IEnumerator StatusOverTime(GameObject target, GameObject status, float duration) {
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
        public static void AreaStatus(Types.Target type, Vector3 center, float areaRadius, string prefab) {
            List<GameObject> targets = GetAll(type, center, areaRadius);
            for(int i = 0; i < targets.Count; ++i) {
                Status(prefab, targets[i]);
            }
        }
        public static IEnumerator AreaStatusOverTime(Types.Target type, Vector3 center, float areaRadius, string prefab, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaStatus(type, center, areaRadius, prefab);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }


        /******************************************************** CONTROL ********************************************************/


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
        public static void AreaControl(Types.Target type, Vector3 center, float areaRadius, int control) {
            List<GameObject> targets = GetAll(type, center, areaRadius);
            for(int i = 0; i < targets.Count; ++i) {
                Control(targets[i], control);
            }
        }
        public static IEnumerator AreaControlOverTime(Types.Target type, Vector3 center, float areaRadius, int control, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaControl(type, center, areaRadius, control);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }


        /******************************************************** DISPLACE ********************************************************/


        public static void Displace(GameObject target, Vector3 impact, float force) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            //Calculate the direction (Push the target AWAY from origin)
            Vector3 relativeDir = impact - target.transform.position; //This is the direction between the target and the point of impact
            relativeDir = relativeDir.normalized;
            // player.ForceMove(force, -relativeDir);
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
                // player.ForceMove(force, -relativeDir);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }
        public static void AreaDisplace(Types.Target type, Vector3 center, float areaRadius, float force) {
            List<GameObject> targets = GetAll(type, center, areaRadius);
            for(int i = 0; i < targets.Count; ++i) {
                Displace(targets[i], center, force);
            }
        }
        public static IEnumerator AreaDisplaceOverTime(Types.Target type, Vector3 center, float areaRadius, float force, float duration) {
            float timer = 0f;
            while(timer < duration) {
                AreaDisplace(type, center, areaRadius, force);
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
        }


        /******************************************************** TELEPORT ********************************************************/


        public static void Teleport(GameObject target, Vector3 point) {
            PlayerManager player = CheckAndGetPlayer(target);
            if(!player) {
                return;
            }
            player.Teleport(point);
        }


        /******************************************************** DELAY ********************************************************/


        public static IEnumerator Delay(float duration, Spell s) {
            float timer = 0f;
            s.delay = false;
            while(timer < duration) {
                yield return new WaitForSeconds(TICK);
                timer += TICK;
            }
            s.delay = true;
        }
    }
}

