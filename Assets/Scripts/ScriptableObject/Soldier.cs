using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 配置文件
 * */
 [CreateAssetMenu(fileName = "Soldier", menuName = "Data/Soldier")]
public class Soldier : ScriptableObject
{
    public float runSpeed;
    public float walkSpeed;
    public float crouchedSpeed;
    public float sprintSpeed;
}
