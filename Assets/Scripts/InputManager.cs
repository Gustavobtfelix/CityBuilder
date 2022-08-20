using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;
    //propfull == camera movement vector to know how to use camera
    private Vector3 cameraMovementVector;

    [SerializeField]
    Camera mainCamera;

    public LayerMask groundMask; //if you have not clicked on the map there is no point in sending the OnClick event

    public Vector3 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }
    
    private void Update(){
        //here we will get all the inputs from the player (mouse click, up, hold and arrow input(for camera movement))
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
        
    }

    private Vector3Int? RaycastGround(){ // ? means it can return null
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // will create a ray from the camera towards the point you click with the mouse on the scene
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask)){ //use ray: if hit exist, has lenght infinite and is hitting groundMask
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }//else
        return null;
    }

    private void CheckArrowInput(){
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));  
    }

    private void CheckClickHoldEvent(){
        if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false){ // 0 == left mouse button | this verify if the pointer is not over the Canvas
            var position = RaycastGround();
            if (position != null)
                OnMouseHold?.Invoke(position.Value);
        }
    }

    private void CheckClickUpEvent(){
        if(Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false){ // this verify if the pointer is not over the Canvas
            OnMouseUp?.Invoke();
        }
    }
    
    private void CheckClickDownEvent(){
        if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false){ // this verify if the pointer is not over the Canvas
            var position = RaycastGround();
            if (position != null)
                OnMouseClick?.Invoke(position.Value);
        }
    }
    
    
}
