using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Bool", menuName = "ScriptableObjects/New Bool Variable")]
public class BoolVariable : ScriptableObject
{
    public bool value = false;
}