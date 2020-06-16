using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Elevation : MonoBehaviour
{
    public InputActionReference inputAction;
    // Start is called before the first frame update
    void Start()
    {
        InputState.Change<float>(inputAction.action.activeControl, transform.position.x, InputUpdateType.Default);
    }

    // Update is called once per frame
    
}
