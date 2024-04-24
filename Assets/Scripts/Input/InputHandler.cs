using UnityEngine;

namespace Input
{
    public class InputHandler
    {
        public bool GetLMB() => UnityEngine.Input.GetMouseButtonDown(0);
        public bool GetRMB() => UnityEngine.Input.GetMouseButtonDown(1);
        public float GetHorizontalAxis() => UnityEngine.Input.GetAxis("Horizontal");
        public float GetVerticalAxis() => UnityEngine.Input.GetAxis("Vertical");
        public bool GetShiftBtn() => UnityEngine.Input.GetKey(KeyCode.LeftShift);
        public bool GetSpaceBtn() => UnityEngine.Input.GetKey(KeyCode.Space);
        public bool GetInteract() => UnityEngine.Input.GetKeyUp(KeyCode.F);
        public bool GetThrowBtn() => UnityEngine.Input.GetKeyUp(KeyCode.E);
    }
}
