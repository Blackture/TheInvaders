using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CardBattle.Core.Controls
{
    [RequireComponent(typeof(PlayerInput))]
    public class ControlMapping : MonoBehaviour
    {
        public enum UNIT_MODE
        {
            NONE,
            SELECT,
            DESELECT
        }

        [SerializeField] private InputActionAsset inputActions;

        private void ChangeInputs(string oldBinding, string newBinding)
        {
            InputBinding mask = new InputBinding(oldBinding);
            inputActions.FindBinding(mask, out InputAction action);
            if (action != null)
            {
                action.ChangeBindingWithPath(oldBinding).WithPath(newBinding);
            }
            else Debug.LogError("Due to an unexpected error, the binding could not be changed.");
        }

        public void GetAxis(Vector2 xy)
        {

        }

        public static bool zoomViaAxis = true;
        public static string ZoomAxis
        {
            get { return zoomViaAxis ? "Mouse ScrollWheel" : null; }
        }

        #region ...
        /// <summary>
        /// for key-combination
        /// </summary>
        #endregion
        public static KeyCode? zoom01 = KeyCode.LeftControl;
        #region ...
        /// <summary>
        /// for plus and minus key
        /// </summary>
        #endregion
        private static KeyCode[] zoom02 = null;
        #region ...
        /// <summary>
        /// for plus and minus key
        /// </summary>
        #endregion
        public static KeyCode[] Zoom02
        {
            get { return zoom02; }
            set { zoom02 = !zoomViaAxis ? value : null; }
        }

        #region ...
        /// <summary>
        /// second key to deselect with the next click
        /// </summary>
        #endregion
        public static KeyCode UnitDeselect = KeyCode.LeftControl;
        #region ...
        /// <summary>
        /// Button name (Fire1 = LeftMouseButton)
        /// </summary>
        #endregion
        public static int UnitAction
        {
            get { return 0; }
        }

        private static int scrollSensivity = 200;
        public static int ScrollSensivity { get { return scrollSensivity; } set { scrollSensivity = value > 0 ? value : 200; } }

        public static bool Zoom(out float z)
        {
            z = 0;
            bool result = true;
            if (zoom01 != null && Input.GetKey((KeyCode)zoom01))
            {
                z = GetZoom();
            }
            else if (zoom01 == null)
            {
                z = GetZoom();
            }

            if (z == 0) result = false;

            return result;
        }
        private static float GetZoom()
        {
            float z = 0;
            if (zoomViaAxis)
            {
                z = Input.GetAxis(ZoomAxis);
            }
            else
            {
                if (Input.GetKeyDown(Zoom02[0])) { z = 0.1f; }
                else if (Input.GetKeyDown(Zoom02[1])) { z = -0.1f; }
            }
            return z;
        }

        //public static bool MoveInput(out float h, out float v)
        //{
        //    bool result = true;
        //    PlayerInput.
        //    if (h == 0 && v == 0) result = false;
        //    return result;
        //}

        public static bool GetUnitMode(out UNIT_MODE mode)
        {
            bool result = false;
            mode = UNIT_MODE.NONE;

            if (Input.GetKey(UnitDeselect) && Input.GetMouseButtonDown(UnitAction))
            {
                mode = UNIT_MODE.DESELECT;
                result = true;
            }
            else if (!Input.GetKey(UnitDeselect) && Input.GetMouseButtonDown(UnitAction))
            {
                mode = UNIT_MODE.SELECT;
                result = true;
            }

            return result;
        }
    }
}