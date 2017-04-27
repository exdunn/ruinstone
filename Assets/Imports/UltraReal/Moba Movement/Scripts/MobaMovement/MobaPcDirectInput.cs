using UnityEngine;

namespace UltraReal.MobaMovement
{
    public class MobaPcDirectInput : MobaInput
    {

        ///<summary>
        ///Defines different mouse buttons.
        ///</summary>
        public enum MouseButton
        {
            Left = 0,
            Right = 1,
            Middle = 2
        }

        ///<summary>
        ///Mouse button intended to moving the player.
        ///</summary>
        [SerializeField]
        MouseButton _moveMouseButton = MouseButton.Right;

        ///<summary>
        ///Mouse button intended to moving the player.
        ///</summary>
        public MouseButton MoveButton
        {
            get { return _moveMouseButton; }
            set { _moveMouseButton = value; }
        }

        public override bool GetMobaMoveButton()
        {
            return Input.GetMouseButton((int)_moveMouseButton);
        }

        public override bool GetMobaMoveButtonDown()
        {
            return Input.GetMouseButtonDown((int)_moveMouseButton);
        }

        public override Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}
