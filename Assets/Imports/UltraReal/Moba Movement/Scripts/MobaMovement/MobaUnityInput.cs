using UnityEngine;

namespace UltraReal.MobaMovement
{
    public class MobaUnityInput : MobaInput
    {

        [SerializeField]
        private string _mobaMoveButton = "Fire1";

        public override bool GetMobaMoveButton()
        {
            return Input.GetButton(_mobaMoveButton);
        }

        public override bool GetMobaMoveButtonDown()
        {
            return Input.GetButtonDown(_mobaMoveButton);
        }

        public override Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}
