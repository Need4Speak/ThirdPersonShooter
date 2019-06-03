using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerHealth))]
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
    [SerializeField] Soldier soldierSetting;  // 设置
    [SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMoveTreshold; // 发出声音最小距离

    [SerializeField] PauseController pauseController;

    Vector3 previousPosition; // 上一帧所处位置

    private CharacterController m_CharacterController;
    public CharacterController CharacterController
    {
        get
        {
            if(m_CharacterController == null)
            {
                m_CharacterController = GetComponent<CharacterController>();
            }
            return m_CharacterController;
        }
    }

    private JumpController m_JumpController;
    public JumpController JumpController
    {
        get
        {
            if (m_JumpController == null)
            {
                m_JumpController = GetComponent<JumpController>();
            }
            return m_JumpController;
        }
    }

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if(m_PlayerShoot == null)
            {
                m_PlayerShoot = GetComponent<PlayerShoot>();
            }
            return m_PlayerShoot;
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

    private PlayerState m_PlayerState;
    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
            {
                m_PlayerState = GetComponent<PlayerState>();
            }
            return m_PlayerState;
        }
    }

    private PlayerHealth m_PlayerHealth;
    public PlayerHealth PlayerHealth
    {
        get
        {
            if (m_PlayerHealth == null)
            {
                m_PlayerHealth = GetComponent<PlayerHealth>();
            }
            return m_PlayerHealth;
        }

        set
        {
            m_PlayerHealth = value;
        }
    }

    public InputController playerInput;
    Vector2 mouseInput;
    // Awake在MonoBehavior创建后就立刻调用，Start将在MonoBehavior创建后在该帧Update之前
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;

        GameManager.Instance.LocalPlayer = this;
        if(MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        previousPosition = transform.position;

    }

    //private void Start()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.IsAlive && !pauseController.Paused)
        {
            LookAround();
            Jump();
            Move();
        }
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

    /**
     * 控制角色跳跃
     * */
    void Jump()
    {
        if (playerInput.IsJumped)
        {
            JumpController.jump();
            Debug.Log("is jump");
        }
    }

    /**
     * 控制角色移动
     * */
    void Move()
    {
        float moveSpeed = soldierSetting.runSpeed;

        if(playerInput.IsWalking)
        {
            moveSpeed = soldierSetting.walkSpeed;
        }  else if (playerInput.IsCrouched)
        {
            moveSpeed = soldierSetting.crouchedSpeed;
        }
        if (playerInput.IsJumped)
        {
            JumpController.jump();
            Debug.Log("is jump");
        }

        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);

        CharacterController.SimpleMove(transform.forward* direction.x + transform.right * direction.y);
        //print(Vector3.Distance(transform.position, previousPosition));
        if (Vector3.Distance(transform.position, previousPosition) > minimumMoveTreshold)
        {
            footSteps.Play();
            //print("play");
        }
        previousPosition = transform.position;
    }
}
