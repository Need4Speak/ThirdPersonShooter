using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Unity Quaternion 转为可序列化 Quaternion
 * */
[System.Serializable]
public struct SerializableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SerializableQuaternion(float rX, float rY, float rZ, float rW)
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }

    public override string ToString()
    {
        return string.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }

    /// Automatic conversion from SerializableQuaternion to Quaternion
    public static implicit operator Quaternion(SerializableQuaternion rValue)
    {
        return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }

    /// Automatic conversion from Quaternion to SerializableQuaternion
    public static implicit operator SerializableQuaternion(Quaternion rValue)
    {
        return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }
}