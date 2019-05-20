using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 十字准星
 * */
public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D image;
    [SerializeField] int size;
    [SerializeField] float maxAngle;
    [SerializeField] float minAngle;

    float lookHeight;

    /**
     * 限制视角
     * */
    public void LookHeight(float value)
    {
        lookHeight += value;

        if(lookHeight > maxAngle || lookHeight < minAngle)
        {
            lookHeight -= value;
        }
    }

    /**
     * 设置准星位置
     * */
    private void OnGUI()
    {
        // 瞄准时出现准心
        if (GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
            GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size), image);
        }

    }
}
