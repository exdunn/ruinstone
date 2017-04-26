using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {
    public const float TICK = 1f;
    public SpellStats _stats;

    public Delivery _delivery;
    public Behaviour[] _behaviours;
    public GameObject _spawnPrefab;

    public float _recharge = 0f;

    public bool isDone { get; set; }
    public bool isActive { get; set; }
    public bool isCastable { get; set; }

    private float _internal = 0f;

    void Start() {
        //_behaviours = new List<Behaviour>();
        isDone = false;
        isActive = false;
        isCastable = true;
    }

    public abstract void Activate(GameObject caster, GameObject target, Vector3 point);
    public abstract void Finish();
    protected abstract IEnumerator CoActivate(GameObject caster, GameObject target, Transform point);

    protected void GoOnCooldown() {
        isCastable = false;
        StartCoroutine(Cooldown());
    }
    
    private IEnumerator Cooldown() {
        while(_internal <= _recharge) {
            yield return new WaitForSeconds(TICK);
            _internal += TICK;
        }
        isCastable = true;
    }
}
