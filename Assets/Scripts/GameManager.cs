using System;
using SVS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   //script that gets information from the input manager and pass it to other classes responsible for placing the structures on the map
    public CameraMovement cameraMovement; //gets Cameramovement script
    public InputManager inputManager; // gets Inputmanager script

    private void Start() {
        inputManager.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position){
        Debug.Log(position);
    }

    private void Update() {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0,inputManager.CameraMovementVector.y));
    }
}
