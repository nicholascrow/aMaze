using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Based_Grab : ControllerEvent
{/*
    public GameObject grabbedObject;
    RaycastHit hit;

    private GameObject objToGrab;
    private LineRenderer rayLine;

    protected override void Start()
    {
        base.Start();
        if(c == Controller.Left)
        {
            leftTriggerDown += GrabObject;
        }
        else {
            rightTriggerDown += GrabObject;
        }

        createLineRenderer();
    }

    void createLineRenderer()
    {
        rayLine = this.gameObject.AddComponent<LineRenderer>();
        rayLine.numPositions = 2;
        rayLine.startWidth = rayLine.endWidth =  .01f;
        rayLine.SetPosition(0, this.transform.position);
        rayLine.material = (Material)Resources.Load("LineColor");
    }

    void GrabObject()
    {
        print("might grab");
        
        if(Physics.SphereCast(this.transform.position, .1f, transform.forward, out hit)
               && hit.collider != null
               && (objToGrab = hit.collider.gameObject) != null
              &&  objToGrab.GetComponent<Grabbable>()
            && !objToGrab.GetComponent<Grabbable>().isGrabbed
            && objToGrab.GetComponent<Grabbable>().canGrab)
        {
            print("grabing?");
            grabbedObject = objToGrab;


            objToGrab.GetComponent<Grabbable>().isGrabbed = true;
            objToGrab.GetComponent<Grabbable>().previousParent = objToGrab.transform.parent;
            objToGrab.transform.SetParent(this.transform);


            if(c == Controller.Right)
            {
                rightTriggerUp += DropObject;
                rightTriggerDown -= GrabObject;
            }
            else {
                leftTriggerUp += DropObject;
                leftTriggerDown -= GrabObject;
            }
        }
    }

    public void DropObject()
    {
        if(grabbedObject)
        {
            grabbedObject.GetComponent<Grabbable>().isGrabbed = false;
            grabbedObject.transform.SetParent(grabbedObject.GetComponent<Grabbable>().previousParent);
            grabbedObject.GetComponent<Grabbable>().previousParent = null;
            grabbedObject = null;

            if(c == Controller.Right)
            {
                rightTriggerDown += GrabObject;
                rightTriggerUp -= DropObject;
            }
            else
            {
                leftTriggerDown += GrabObject;
                leftTriggerUp -= DropObject;
            }
        }
    }


    protected override void Update()
    {
        base.Update();
        rayLine.SetPosition(0, this.transform.position);
        rayLine.SetPosition(1, this.transform.forward * 10);
    }

    */
}

public enum Controller
{
    Left,
    Right
}