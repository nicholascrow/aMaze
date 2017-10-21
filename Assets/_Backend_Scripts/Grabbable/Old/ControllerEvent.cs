using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEvent : MonoBehaviour
{
    public static ControllerEvent instance;


    public static GameObject leftController, rightController;
    //public Controller c;

    public delegate void LeftTriggerDown();
    public static LeftTriggerDown leftTriggerDown;
    bool leftDown = false;
    public delegate void RightTriggerDown();
    public static RightTriggerDown rightTriggerDown;
    bool rightDown = false;

    public delegate void LeftTriggerUp();
    public static LeftTriggerUp leftTriggerUp;

    public delegate void RightTriggerUp();
    public static RightTriggerUp rightTriggerUp;

    public delegate void AButton();
    public static AButton AButtonPressed;

    protected virtual void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > .5 && leftTriggerDown != null && this.gameObject == leftController)
        {
            leftDown = true;
            leftTriggerDown();

        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > .5 && rightTriggerDown != null && this.gameObject == rightController)
        {
            rightDown = true;
            rightTriggerDown();
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < .5 && leftTriggerUp != null && leftDown)
        {
            leftTriggerUp();
            leftDown = false;
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < .5 && rightTriggerUp != null && rightDown)
        {
            rightTriggerUp();
            rightDown = false;
        }

        bool hasPressed = false;
        if (OVRInput.GetDown(OVRInput.Button.One) && AButtonPressed != null && !hasPressed)
        {
            AButtonPressed();
            hasPressed = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.One) && AButtonPressed != null && hasPressed)
        {
            hasPressed = false;
        }



    }
    protected virtual void Start()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
