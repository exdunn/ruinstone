using UnityEngine;

namespace UltraReal.MobaMovement
{
    ///<summary>
    ///Sets an Animator value to that of the MobaMovers ForwardSpeedFactor.
    ///</summary
    public class MobaAnimate : MonoBehaviour
    {

        #region fields
        ///<summary>
        ///Store position of transform on last frame.
        ///</summary>
        protected Vector3 _lastPos;

        ///<summary>
        ///Normalized value that represents the speed of the mover relative to it's max speed.  Used for setting forward animation.
        ///</summary>
        protected float _forwardSpeedFactor;
        #endregion

        #region Inspector Properties
        ///<summary>
        ///Refence to the animator.
        ///</summary
        [SerializeField]
        Animator _animator;

        [SerializeField]
        float _strideDistance = 1f;

        ///<summary>
        ///Name of the Animater's float properties that will be assigned the ForwardSpeedFactor value.
        ///</summary
        [SerializeField]
        string _forwardName = "Forward";
        #endregion



        #region Behavior Overrides
        // Use this for initialization
        ///<summary>
        ///Gets the required components.
        ///</summary
        void Start()
        {
            if(!_animator)
                _animator = GetComponent<Animator>();

            _lastPos = transform.position;
        }

        ///<summary>
        ///Applies the ForwardSpeedFactor value to the Animator.
        ///</summary
        void Update()
        {
            Vector3 _velocity = (transform.position - _lastPos) / Time.deltaTime;
            _forwardSpeedFactor = Mathf.Lerp(_forwardSpeedFactor, _velocity.magnitude / _strideDistance,Time.deltaTime * 10f);

            _lastPos = transform.position;

            if (_animator != null)
                _animator.SetFloat(_forwardName, _forwardSpeedFactor);
        }
        #endregion
    }
}
