using UnityEngine;

namespace UltraReal.MobaMovement
{
    ///<summary>
    ///This is the manager script for the Moba Movement system.  There should only be one in you scene.
    ///</summary>
    public class MobaMovementManager : MonoBehaviour
    {

        #region Fields
        ///<summary>
        ///Singleton instance to the MobaMovementManager.
        ///</summary>
        static MobaMovementManager _instance;

        private MobaCameraTrack _mobaCameraTrack;

        private bool _init = false;
        #endregion

        #region Event Delagates
        ///<summary>
        ///Delegate used for spawning camera pick events
        ///</summary>
        public delegate void CameraPickEvent(Camera fromCamera, RaycastHit hit);

        ///<summary>
        ///Static event that will be called when the mouse button is down and the raycast is hitting a surface with the appropriate layermask.
        ///</summary>
        public static CameraPickEvent OnCameraPick;

        ///<summary>
        ///Static event that will be called when the mouse button is clicked and the raycast is hitting a surface with the appropriate layermask.
        ///</summary>
        public static CameraPickEvent OnCameraPickDown;

        public MobaInput _mobaInput;
        #endregion

        #region Enums
        ///<summary>
        /// ClickToMove = This mode requires the user to click the mouse on ground to move the player.
        /// ClickOrHoldToMove = Acts the same as ClickToMove plus it will update the move target with the mouse button is held down.
        ///</summary>
        public enum MouseClickType
        {
            ClickToMove,
            ClickOrHoldToMove
        }


        #endregion

        #region Inspector properties
        ///<summary>
        ///Reference to the camera used for the raycasting process.
        ///</summary>
        [SerializeField]
        Camera _pickCamera = null;

        ///<summary>
        ///The type of clicking process used.  These are defined in the MouseClickType enum.
        ///</summary>
        [SerializeField]
        MouseClickType _mouseClickType = MouseClickType.ClickOrHoldToMove;

        ///<summary>
        ///Reference to the NavAgent GameObject that has the MobaMover script attached.
        ///</summary>
        [SerializeField]
        MobaMoverBase _targetMobaMover = null;

        ///<summary>
        ///Reference to a prefab that will be spawned at the mouse click loation.
        ///</summary>
        [SerializeField]
        GameObject _moveToEffectPrefab = null;

        /*
        ///<summary>
        ///Refernce to MobaCameraTrack on camera rig
        ///</summary>
        [SerializeField]
        MobaCameraTrack _mobaCameraTrack = null;
        */

        ///<summary>
        ///Layermask for the raycasting process.
        ///</summary>
        [SerializeField]
        LayerMask _clickLayerMask;

        
        #endregion

        #region Properties


        ///<summary>
        ///Layermask for the raycasting process.
        ///</summary>
        public static LayerMask ClickLayerMask
        {
            get { return _instance._clickLayerMask; }
            set { _instance._clickLayerMask = value; }
        }

        public MobaMoverBase TargetMobaMover
        {
            get { return _targetMobaMover; }
            set { _targetMobaMover = value; }
        }
        #endregion

        #region Behavior Overrides
        ///<summary>
        ///Sets up the Camera for use.
        ///</summary>
        void Awake()
        {
            _instance = this;

            if (!_pickCamera)
                _pickCamera = Camera.main;

            if (!_pickCamera)
                Debug.LogError("MobaMovementManager: Requires a Camera assignment, or a camera tagged as MainCamera");

            if (!_mobaCameraTrack)
                _mobaCameraTrack = FindObjectOfType<MobaCameraTrack>();

            if (!_mobaInput)
                _mobaInput = FindObjectOfType<MobaInput>();

            if (!_mobaInput)
                Debug.Log("MobaMovementManager: There needs to be at least one MobaInput based script in the scene.");

            MobaMover[] _mobaMovers = FindObjectsOfType<MobaMover>();

            if (!_init)
            {
                foreach (MobaMover mm in _mobaMovers)
                {
                    OnMoverAdded(mm);
                }
            }
        }

        void OnEnable()
        {
            MobaMover.OnMoverAdded += OnMoverAdded;
        }

        void OnDisable()
        {
            MobaMover.OnMoverRemoved += OnMoverRemoved;
        }

        ///<summary>
        ///Processes the click events and sets the destinations for the MobaMover script on the player.
        ///</summary>
        void Update()
        {
          //  if(_targetMobaMover && _mobaCameraTrack)
          //      _mobaCameraTrack.SetTarget(_targetMobaMover.transform);

            if (_mobaInput != null && _mobaInput.GetMobaMoveButton())
            {
                Ray ray = _pickCamera.ScreenPointToRay(_mobaInput.GetMousePosition());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10000f, ClickLayerMask, QueryTriggerInteraction.Collide))
                {
                    if (OnCameraPick != null)
                        OnCameraPick(_pickCamera, hit);

                    if (_mobaInput.GetMobaMoveButton() && _targetMobaMover != null && _mouseClickType == MouseClickType.ClickOrHoldToMove)
                        _targetMobaMover.SetDestination(hit.point);



                    if (_mobaInput.GetMobaMoveButtonDown())
                    {
                        if (OnCameraPickDown != null)
                            OnCameraPickDown(_pickCamera, hit);

                        //Selector
                        if (_moveToEffectPrefab != null && _mobaInput.GetMobaMoveButton())
                        {
                            Instantiate(_moveToEffectPrefab, hit.point, Quaternion.identity);
                        }

                        //Set Destination
                        if (_moveToEffectPrefab != null && _mobaInput.GetMobaMoveButton() && _mouseClickType == MouseClickType.ClickToMove)
                        {
                            Instantiate(_moveToEffectPrefab, hit.point, Quaternion.identity);
                            _targetMobaMover.SetDestination(hit.point);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        void OnMoverAdded(MobaMoverBase mobaMover)
        {
            _init = true;
            if(mobaMover.gameObject.tag == "Player")
            {
                _targetMobaMover = mobaMover;
                if (_mobaCameraTrack)
                    _mobaCameraTrack.SetTarget(_targetMobaMover.transform);
            }
        }

        void OnMoverRemoved(MobaMoverBase mobaMover)
        {
            //Debug.Log("Mover Removed");
        }
        #endregion
    }
}
