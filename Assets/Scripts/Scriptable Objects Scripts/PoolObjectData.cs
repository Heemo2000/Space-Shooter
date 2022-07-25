using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Pool Object Data",fileName = "Pool Object Data")]
public class PoolObjectData : ScriptableObject
{
    public PoolObject actualObject;
    public int count;
}
