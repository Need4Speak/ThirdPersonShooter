using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第三人称摄像机
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    /**
     * 保存摄像机位置
     * */
     [System.Serializable]
    public class CameraRig
    {
        public Vector3 CameraOffset;  // 相机位置
        public float Damping;  // 相机抖动
    }

    [SerializeField] CameraRig defaultCamera;  // 默认相机位置
    [SerializeField] CameraRig aimCamera;  // 瞄准相机位置

    // 0,3,-7
    //4
    //[SerializeField] Vector3 cameraOffset = new Vector3(0, 3, -7);
    //[SerializeField] float damping = 1;

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

    // 更新摄像机位置
    void Update()
    {
        if(localPlayer == null)
        {
            return;
        }

        CameraRig cameraRig = defaultCamera;
        // 瞄准时转为瞄准摄像机
        if(localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING||
            localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING)
        {
            cameraRig = aimCamera;
        }

        Vector3 targetPosition = cameraLookAtTarget.position
            + localPlayer.transform.forward * cameraRig.CameraOffset.z
            + localPlayer.transform.up * cameraRig.CameraOffset.y
            + localPlayer.transform.right * cameraRig.CameraOffset.x;

        Quaternion targetRotation = Quaternion.LookRotation(cameraLookAtTarget.position - targetPosition, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, cameraRig.Damping * Time.deltaTime);
    }
}
