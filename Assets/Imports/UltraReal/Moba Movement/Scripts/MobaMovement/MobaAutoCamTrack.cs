using UnityEngine;
using UltraReal.SharedAssets.UnityStandardAssets;

namespace UltraReal.MobaMovement
{
    public class MobaAutoCamTrack : MobaCameraTrack
    {
        AutoCam _autoCam;

        void Start()
        {
            _autoCam = GetComponent<AutoCam>();
        }

        public override void SetTarget(Transform target)
        {
            if (_autoCam)
                _autoCam.SetTarget(target);
        }
    }
}
