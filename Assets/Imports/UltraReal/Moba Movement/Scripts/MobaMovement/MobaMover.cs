using UnityEngine;

namespace UltraReal.MobaMovement
{
    ///<summary>
    ///NavMeshAgent based movement script.  Requires the MobaMovementManager in the scene.
    ///</summary
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class MobaMover : MobaMoverBase
    {
        #region fields
        ///<summary>
        ///Reference to the NavMeshAgent
        ///</summary>
        UnityEngine.AI.NavMeshAgent agent;
        #endregion

        #region Properties
        public float Speed
        {
            get { return _speed; }
            set
            {
                if (!agent) agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.speed = value;
                _speed = value;
            }
        }
        #endregion

        #region Behavior Overrides
        ///<summary>
        ///Gets a reference to the NavMeshAgent
        ///</summary>
        protected override void Start()
        {
            base.Start();
    
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            Speed = _speed;
        }

        ///<summary>
        ///Sets the max speed for the MobaMover based on the speed set in the NavAgent.
        ///</summary>
        protected override void Update()
        {
            base.Update();
        }
        #endregion

        #region Methods
        ///<summary>
        ///Set desired target position for the MobaMover script.
        ///</summary>
        public override void SetDestination(Vector3 location)
        {
            if (agent != null && gameObject.activeSelf)
                agent.SetDestination(location);
        }
        #endregion
    }
}
