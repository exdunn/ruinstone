using UnityEngine;

namespace UltraReal.MobaMovement
{
    ///<summary>
    ///Base Class for MoveMover.
    ///</summary>
    public abstract class MobaMoverBase : MonoBehaviour
    {
        public delegate void MoverHandler(MobaMoverBase mobaMover);
        public static event MoverHandler OnMoverAdded;
        public static event MoverHandler OnMoverRemoved;

        #region Inspector Properties
        ///<summary>
        ///Speed for the mover.
        ///</summary>
        [SerializeField]
        protected float _speed = 10f;
        #endregion



        #region Behavior Overrides
        protected virtual void Start()
        {

        }

        protected virtual void OnEnable()
        {
            if(OnMoverAdded != null)
                OnMoverAdded(this);
        }

        protected virtual void OnDisbled()
        {
            if(OnMoverAdded != null)
                OnMoverRemoved(this);
        }

        ///<summary>
        ///Calculates the forward speed factor for use with an animater.
        ///</summary>
        protected virtual void Update()
        {

        }
        #endregion

        #region Methods
        ///<summary>
        ///Set desired target position for the MobaMover script.
        ///</summary>
        public abstract void SetDestination(Vector3 location);
        #endregion
    }
}
