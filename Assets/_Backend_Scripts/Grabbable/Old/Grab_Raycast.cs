//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Scripts.Grab
//{
//    public class Grab_Raycast : Grab
//    {
//        //the hit object
//        private RaycastHit hit;

//        //the linerenderer
//        private LineRenderer rayLine;

//        public float sphereRadius = .001f;
//        public float castDistance = .001f;

//        void Start()
//        {
//            //create the line renderer
//            createLineRenderer();
//        }

//        //create line renderer
//        void createLineRenderer()
//        {

//            //add the component
//            rayLine = this.gameObject.AddComponent<LineRenderer>();

//            //size is 2 because we just need start and end
//            rayLine.positionCount = 2;

//            //width is small
//            rayLine.startWidth = rayLine.endWidth = .01f;

//            //set the initial position to the hand position
//            rayLine.SetPosition(0, this.transform.position);
//            rayLine.SetPosition(1, this.transform.position);

//            //set its color
//            rayLine.material = (Material)Resources.Load("LineColor");
//        }

//        public void GrabObject()
//        {
//            currentlyHoldingObject = true;
//            grabbedObject = objectInRangeOfGrab;
//            objectInRangeOfGrab.GetComponent<Grabbable>().isGrabbed = true;
//            objectInRangeOfGrab.GetComponent<Grabbable>().previousParent = objectInRangeOfGrab.transform.parent;
//            objectInRangeOfGrab.transform.SetParent(this.transform);
//            rayLine.enabled = false;

//            if (objectGrabbed != null)
//            {
//                objectGrabbed(objectInRangeOfGrab);
//            }
//        }

//        public override void DropObject()
//        {
//            if (grabbedObject)
//            {
//                grabbedObject.GetComponent<Grabbable>().isGrabbed = false;
//                grabbedObject.transform.SetParent(grabbedObject.GetComponent<Grabbable>().previousParent);
//                grabbedObject.GetComponent<Grabbable>().previousParent = null;
//                if (objectReleased != null)
//                {
//                    objectReleased(grabbedObject);
//                }
//                grabbedObject = null;
//                rayLine.enabled = true;
//                currentlyHoldingObject = false;

//            }
//        }

//        bool isInside;
//        public void OnTriggerEnter(Collider other)
//        {
//            isInside = true;
//            objectInRangeOfGrab = other.gameObject;
//            print("inside");
//        }
//        public void OnTriggerExit(Collider other)
//        {
//            isInside = false;
//            objectInRangeOfGrab = null;
//        }


//        protected override void Update()
//        {
//            base.Update();
//            //if the controller isnt connected then stop
//            if (((OVRInput.GetConnectedControllers() & controller)) != controller)
//            {
//                rayLine.SetPosition(0, this.transform.position);
//                rayLine.SetPosition(1, this.transform.position);
//                return;
//            }

//            //set line renderer default positions
//            rayLine.SetPosition(0, this.transform.position);
//            rayLine.SetPosition(1, this.transform.position + this.transform.forward * 10);


//            if (OVRInput.Get(currentControllerTrigger()) > .5 && !currentlyHoldingObject)
//            {
//                if (Physics.SphereCast(this.transform.position, sphereRadius, transform.forward, out hit)
//                       && hit.collider != null
//                       && (objectInRangeOfGrab = hit.collider.gameObject) != null
//                       && objectInRangeOfGrab.GetComponent<Grabbable>()
//                       && !objectInRangeOfGrab.GetComponent<Grabbable>().isGrabbed
//                       && objectInRangeOfGrab.GetComponent<Grabbable>().canGrab)
//                {
//                    GrabObject();
//                }

//            }
//            else if (OVRInput.Get(currentControllerTrigger()) < .5
//                    && grabbedObject != null && currentlyHoldingObject)
//            {
//                DropObject();
//            }

//            // if(grabbedObject && grabbedObject.GetComponent<Grabbable>().dropAtNextUpdate)
//            //{
//            //     grabbedObject.GetComponent<Grabbable>().dropAtNextUpdate = false;
//            //      DropObject();
//            //  }
//        }

//    }
//}