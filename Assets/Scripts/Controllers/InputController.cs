using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Fire2;
    public bool Reload;
    public bool IsWalking;
    public bool IsSprinting;
    public bool IsCrouched;
    public bool IsJumped;
    public bool MouseWheelUp;
    public bool MouseWheelDown;
    public bool EscDown;

    /**
     * 获取输入
     * */
    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        Reload = Input.GetKey(KeyCode.R);
        IsWalking = Input.GetKey(KeyCode.X);
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
        IsCrouched = Input.GetKey(KeyCode.C);
        IsJumped = Input.GetKeyDown(KeyCode.Space);
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        EscDown = Input.GetKeyDown(KeyCode.Escape);
    }
}
