using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Grab
{
    public class Grab : MonoBehaviour, IGrabInteractionObject
    {
        public IControllerActionHandler ActionHandler;

        public IInteractableObject CurrentlyGrabbedObject { get; private set; }

        public bool CompareToGrabbedObject(IInteractableObject checkObj)
        {
            return CurrentlyGrabbedObject == checkObj;
        }

        public void EndInteractObject(IControllerActionHandler handler)
        {

            if (CurrentlyGrabbedObject == null)
                return;

            CurrentlyGrabbedObject.EndInteractObject(handler);


            var rigidbody = CurrentlyGrabbedObject.GetGameObject().GetComponent<Rigidbody>();
            var device = SteamVR_Controller.Input((int)handler.AsViveActionHandler().Input.trackedObj.index);
            var origin = handler.AsViveActionHandler().Input.trackedObj.origin ?
                handler.AsViveActionHandler().Input.trackedObj.origin :
                handler.AsViveActionHandler().Input.trackedObj.transform.parent;
            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(device.velocity);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
            }
            else
            {
                rigidbody.velocity = device.velocity;
                rigidbody.angularVelocity = device.angularVelocity;
            }

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;



            CurrentlyGrabbedObject.Owner = null;
            CurrentlyGrabbedObject = null;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void BeginInteractObject(IControllerActionHandler handler)
        {
            if (CurrentlyGrabbedObject != null)
                return;
            LayerMask m = -1 << LayerMask.NameToLayer("InteractibleObject");
            RaycastHit? h = handler.Raycaster.TryRaycast(m);
            if (!h.HasValue) return;
            IInteractableObject obj = h.Value.collider.GetComponent<IInteractableObject>();

            if (obj == null) return;

            obj.Owner = this;
            CurrentlyGrabbedObject = obj;

            CurrentlyGrabbedObject.BeginInteractObject(handler);

            print("grabbed " + obj.GetGameObject().name);
        }

        void Start()
        {
            ActionHandler = GetComponent<IControllerActionHandler>();
        }

        void Update()
        {
            if (CurrentlyGrabbedObject == null)
            {
                ActionHandler.Raycaster.Laser.Laser.enabled = true;
                RaycastHit? h = ActionHandler.Raycaster.TryRaycast(-1 << LayerMask.NameToLayer("InteractibleObject"));
                if (h != null)
                {
                    print("highlighting");
                    ActionHandler.Raycaster.Laser.SetHitPosition(ActionHandler.Raycaster.Laser.transform.InverseTransformPoint(h.Value.point));
                    if (h.Value.collider.GetComponent<IInteractableObject>() is IGrabInteractibleObject)
                    {
                        if (h.Value.collider.GetComponent<IInteractableObject>().AsGrabInteractibleObject().Owner ==
                            null)
                            h.Value.collider.GetComponent<IInteractableObject>().AsGrabInteractibleObject()
                                    .IsHighlighted =
                                true;
                    }
                }
                else
                {
                    ActionHandler.Raycaster.Laser.SetHitPosition(Vector3.forward * 1000);
                }
            }
            else
            {
                ActionHandler.Raycaster.Laser.Laser.enabled = false;
            }
        }
    }
}