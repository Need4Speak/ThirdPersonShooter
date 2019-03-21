using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 第三人称摄像机
 * */
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float damping;

    Transform cameraLookAtTarget;
    Player localPlayer;
    /**
     * 相机加载时就绑定到player身上
     * */
    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        cameraLookAtTarget = localPlayer.transform.Find("CameraLookAtTarget");
        if(cameraLookAtTarget == null)
        {
            cameraLookAtTarget = localPlayer.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = cameraLookAtTarget.position
            + localPlayer.transform.forward * cameraOffset.z
            + localPlayer.transform.up * cameraOffset.y
            + localPlayer.transform.right * cameraOffset.x;

        Quaternion targetRotation = Quaternion.LookRotation(cameraLookAtTarget.position - targetPosition, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
    }
}
