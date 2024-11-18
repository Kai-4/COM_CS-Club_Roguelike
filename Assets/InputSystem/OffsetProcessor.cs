using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class OffsetProcessor : InputProcessor<Vector2>
{
    #if UNITY_EDITOR
    static OffsetProcessor() {
        Initialize();
    }
    #endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor <OffsetProcessor>();
    }

    [Tooltip("Offsets incoming values by half the screen size")]
    
    public override Vector2 Process(Vector2 value, InputControl control)
    {
        value.x -= Screen.width / 2;
        value.y -= Screen.height / 2;
        return value;
    }
}
