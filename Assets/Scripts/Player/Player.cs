using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    /**
     * 鼠标设置阻尼和灵敏度
     * */
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchedSpeed;
    [SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMoveTreshold; // 发出声音最小距离

    Vector3 previousPosition; // 上一帧所处位置

    private MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }

    private Crosshair m_Crosshair;
    private Crosshair Crosshair
    {
        get
        {
            if(m_Crosshair == null)
            {
                m_Crosshair = GetComponentInChildren<Crosshair>();
            }
            return m_Crosshair;
        }
            }

    public InputController playerInput;
    Vector2 mouseInput;
    // Awake在MonoBehavior创建后就立刻调用，Start将在MonoBehavior创建后在该帧Update之前
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
        //if(MouseControl.LockMouse)
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }

    /**
     * 视角控制
     * */
    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        var crosshair = GetComponentInChildren<Crosshair>();

        Crosshair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
    }

    void Move()
    {
        float moveSpeed = runSpeed;

        if(playerInput.IsWalking)
        {
            moveSpeed = walkSpeed;
        } else if (playerInput.IsSprinting)
        {
            moveSpeed = sprintSpeed;
        } else if (playerInput.IsCrouched)
        {
            moveSpeed = crouchedSpeed;
        }

        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);

        MoveController.Move(direction);
        print(Vector3.Distance(transform.position, previousPosition));
        if (Vector3.Distance(transform.position, previousPosition) > minimumMoveTreshold)
        {
            footSteps.Play();
            print("play");
        }
        previousPosition = transform.position;
    }
}
