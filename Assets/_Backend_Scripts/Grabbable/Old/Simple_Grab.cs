//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Scripts.Grab
//{
//    public class Simple_Grab : MonoBehaviour
//    {
//        public delegate void ObjectReleased(GameObject obj);
//        public static ObjectReleased objectReleased;
//        public delegate void ObjectGrabbed(GameObject obj);
//        public static ObjectReleased objectGrabbed;

//        public GameObject grabbedObject;
//        RaycastHit hit;

//        private GameObject objToGrab;
//        private LineRenderer rayLine;

//        private bool currentlyHoldingObject;
//        public OVRInput.Controller c;

//        void Start()
//        {
//            currentlyHoldingObject = false;
//            createLineRenderer();
//            if (c == OVRInput.Controller.LTouch)
//                ControllerEvent.leftController = this.gameObject;
//            if (c == OVRInput.Controller.RTouch)
//                ControllerEvent.rightController = this.gameObject;
//        }

//        void createLineRenderer()
//        {
//            rayLine = this.gameObject.AddComponent<LineRenderer>();
//            rayLine.positionCount = 2;
//            rayLine.startWidth = rayLine.endWidth = .01f;
//            rayLine.SetPosition(0, this.transform.position);
//            rayLine.material = (Material)Resources.Load("LineColor");
//        }

//        void GrabObject()
//        {
//            Haptics.Vibrate(c, HapticStrength.MED);
//            currentlyHoldingObject = true;
//            grabbedObject = objToGrab;
//            objToGrab.GetComponent<Grabbable>().isGrabbed = true;
//            objToGrab.GetComponent<Grabbable>().previousParent = objToGrab.transform.parent;
//            objToGrab.transform.SetParent(this.transform);
//            rayLine.enabled = false;

//            if (objectGrabbed != null)
//            {
//                objectGrabbed(objToGrab);
//            }
//        }

//        public void DropObject()
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


//        void Update()
//        {
//            rayLine.SetPosition(0, this.transform.position);
//            rayLine.SetPosition(1, this.transform.forward * 10);
//            if (OVRInput.Get(currentControllerTrigger()) > .5 && !currentlyHoldingObject)
//            {
//                if (Physics.SphereCast(this.transform.position, .1f, transform.forward, out hit)
//                     && hit.collider != null
//                     && (objToGrab = hit.collider.gameObject) != null
//                     && objToGrab.GetComponent<Grabbable>()
//                     && !objToGrab.GetComponent<Grabbable>().isGrabbed
//                     && objToGrab.GetComponent<Grabbable>().canGrab)
//                {
//                    GrabObject();
//                }
//            }
//            else if (OVRInput.Get(currentControllerTrigger()) < .5
//                    && grabbedObject != null && currentlyHoldingObject)
//            {
//                DropObject();
//            }

//        }

//        OVRInput.Axis1D currentControllerTrigger()
//        {
//            if (c == OVRInput.Controller.LTouch)
//            {
//                return OVRInput.Axis1D.PrimaryIndexTrigger;
//            }
//            else
//            {
//                return OVRInput.Axis1D.SecondaryIndexTrigger;
//            }
//        }

//    }
//}