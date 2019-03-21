using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }
    [SerializeField] float speed;
    [SerializeField] MouseInput MouseControl;

    private MoveController moveController;
    public MoveController MoveController
    {
        get
        {
            if(moveController == null)
            {
                moveController = GetComponent<MoveController>();
            }
            return moveController;
        }
    }
    public InputController playerInput;
    Vector2 mouseInput;
    // Awake在MonoBehavior创建后就立刻调用，Start将在MonoBehavior创建后在该帧Update之前
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

    }
}
