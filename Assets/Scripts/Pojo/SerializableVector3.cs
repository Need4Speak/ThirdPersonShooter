using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Unity Vector3 转为可序列化 Vector3
 * */
[System.Serializable]
public class SerializableVector3
{ 
    public float x;
    public float y;
    public float z;

    public SerializableVector3()
    {
        x = 0.0f;
        y = 0.0f;
        z = 0.0f;
    }

    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    public override string ToString()
    {
        return string.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// Automatic conversion from SerializableVector3 to Vector3
    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    /// Automatic conversion from Vector3 to SerializableVector3
    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}
