//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Scripts.Grab
//{
//    public class Grab_VirtualHand : Grab
//    {
//        /// <summary>
//        /// When you grab an object, we call this method to tie it to your hand.
//        /// </summary>
//        public override void GrabObject()
//        {
//            //if we are currently holding an object then we cannot hold another.
//            if (currentlyHoldingObject) { return; }

//            //set this boolean to true to make sure we can't have race conditions.
//            currentlyHoldingObject = true;

//            //set the currently grabbed object to the grabbable object.
//            grabbedObject = objectInRangeOfGrab;

//            //set the grabbed object itself to grabbed.
//            objectInRangeOfGrab.GetComponentInChildren<Grabbable>().isGrabbed = true;

//            //set the object's previous parent so that we can set it back when released.
//            objectInRangeOfGrab.GetComponentInChildren<Grabbable>().previousParent = objectInRangeOfGrab.transform.parent;

//            //set the objects current parent to the hand.
//            objectInRangeOfGrab.transform.SetParent(this.transform);

//            //set kinematic state so we can set it back later.
//            kinematicInitialState = objectInRangeOfGrab.GetComponent<Rigidbody>().isKinematic;

//            //make objects with rigid bodies kinematic.
//            if (objectInRangeOfGrab.GetComponent<Rigidbody>() && objectInRangeOfGrab.GetComponent<Rigidbody>().isKinematic == false)
//            {
//                objectInRangeOfGrab.GetComponent<Rigidbody>().isKinematic = true;
//            }

//            //call the objectGrabbed event which can do other things if an object is grabbed
//            if (objectGrabbed != null)
//            {
//                objectGrabbed(objectInRangeOfGrab);
//            }

//            //the object in range of grab is now null to prevent it from being stored.
//            objectInRangeOfGrab = null;

//            //vibrate on grab.
//            Haptics.Vibrate(this.controller, HapticStrength.HIGH);
//        }

//        public override void DropObject()
//        {
//            if (grabbedObject)
//            {

//                //set grabbed to false
//                grabbedObject.GetComponentInChildren<Grabbable>().isGrabbed = false;

//                //reset to previous parent
//                grabbedObject.transform.SetParent(grabbedObject.GetComponentInChildren<Grabbable>().previousParent);

//                //set previous parent to null
//                grabbedObject.GetComponentInChildren<Grabbable>().previousParent = null;

//                //reset the kinematics
//                if (grabbedObject.GetComponent<Rigidbody>())
//                {
//                    grabbedObject.GetComponent<Rigidbody>().isKinematic = kinematicInitialState;
//                }

//                //call released events.
//                if (objectReleased != null)
//                {
//                    objectReleased(grabbedObject);
//                }

//                //reset these to avoid grabbing the same obj agani
//                objectInRangeOfGrab = null;
//                grabbedObject = null;
//                currentlyHoldingObject = false;

//            }
//        }

//        GameObject inrangeofdestroy;
//        bool isInside;
//        public void OnTriggerEnter(Collider other)
//        {
//            if (other.GetComponentInChildren<Grabbable>())
//            {
//                inrangeofdestroy = other.gameObject;
//            }
//            //print(other.name);
//            if (other.GetComponentInChildren<Grabbable>() && other.GetComponentInChildren<Grabbable>().canGrab)
//            {
//                isInside = true;
//                objectInRangeOfGrab = other.gameObject;
//                //  Highlight(other.gameObject, true);
//                //print("inside");
//                print("Object is able to be grabbed: " + objectInRangeOfGrab.name);
//            }
//        }
//        public void OnTriggerExit(Collider other)
//        {
//            if (other == objectInRangeOfGrab)
//            {
//                isInside = false;
//                //  Highlight(other.gameObject, true);

//                print("Object is being dropped: " + objectInRangeOfGrab.name);
//                objectInRangeOfGrab = null;
//            }
//        }


//        protected override void Update()
//        {
//            //call grab's update.
//            base.Update();

//            //if trigger is pressed
//            if (OVRInput.Get(currentControllerTrigger()) > .5 && !currentlyHoldingObject)
//            {
//                //if we can grab an object
//                if (isInside
//                      && objectInRangeOfGrab
//                      && !objectInRangeOfGrab.GetComponentInChildren<Grabbable>().isGrabbed)
//                {

//                    GrabObject();
//                }

//            }
//            //released trigger
//            else if (OVRInput.Get(currentControllerTrigger()) < .5
//                    && grabbedObject != null && currentlyHoldingObject)
//            {
//                DropObject();
//            }

//            //destroy data structures with hand trigger?
//            if (OVRInput.Get(currentHandTrigger()) > .5 && !currentlyHoldingObject && OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
//            {
//                if (isInside && inrangeofdestroy

//                      && !inrangeofdestroy.GetComponentInChildren<Grabbable>().isGrabbed)
//                {

//                    Destroy(inrangeofdestroy.GetComponentInParent<DataStructureOld>().gameObject);
//                }
//            }

//            // if (grabbedObject && grabbedObject.GetComponentInChildren<Grabbable>().dropAtNextUpdate)
//            // {
//            //    grabbedObject.GetComponentInChildren<Grabbable>().dropAtNextUpdate = false;
//            //    DropObject();
//            // }
//        }
//    }
//}