using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftInputHandler : MonoBehaviour
{

    public OVRInput.Controller Controller;
    public IRiftActionHandler ActionHandler;

    void Start()
    {
        ActionHandler = GetComponent<IRiftActionHandler>();
    }

    void Update()
    {
        //hand trigger
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger,Controller))
        {
            HandTriggerDown();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, Controller))
        {
            HandTriggerUp();
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, Controller))
        {
            HandTriggerHeld();
        }

        //index trigger
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, Controller) || Input.GetKeyDown(KeyCode.P))
        {
            IndexTriggerDown();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, Controller) || Input.GetKeyUp(KeyCode.P))
        {
            IndexTriggerUp();
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, Controller))
        {
            IndexTriggerHeld();
        }

        //thumbstick press
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, Controller))
        {
            ThumbStickPressBegin(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,Controller));
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, Controller))
        {
            ThumbStickPressEnd();
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, Controller))
        {
            ThumbStickPressing(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, Controller));
        }

        //thumbsticktouch
        if (OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick, Controller))
        {
            ThumbStickTouchBegin(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, Controller));
        }
        if (OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick, Controller))
        {
            ThumbstickTouchEnd();
        }
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, Controller))
        {
            ThumbStickTouching(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, Controller));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IndexTriggerDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            IndexTriggerUp();
        }
    }


    public void HandTriggerDown()
    {

    }
    public void HandTriggerHeld()
    {

    }
    public void HandTriggerUp()
    {

    }

    public void IndexTriggerDown()
    {
        ActionHandler.Raycaster.BeginInteract();
    }
    public void IndexTriggerHeld()
    {

    }
    public void IndexTriggerUp()
    {
        ActionHandler.Raycaster.EndInteract();
    }

    public void ThumbStickTouchBegin(Vector3 position)
    {

    }
    public void ThumbstickTouchEnd()
    {

    }
    public void ThumbStickTouching(Vector3 position)
    {

    }

    public void ThumbStickPressBegin(Vector3 position)
    {

    }
    public void ThumbStickPressEnd()
    {

    }
    public void ThumbStickPressing(Vector3 position)
    {

    }
}