using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable objects cannot be attached to anything within the scene
[CreateAssetMenu]
public class FloatValue : ScriptableObject
{
    public float initialValue;
}
