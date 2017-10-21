using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Grab
{
    public class Grabbable : MonoBehaviour, IGrabInteractibleObject
    {
        public bool IsGrabbed
        {
            get { return Owner != null; }
        }

        public bool HasPhysicsEnabled { get; private set; }

        public IInteractionObject Owner { get; set; }

        public void BeginInteractObject(IControllerActionHandler handler)
        {
            print("here!!!!");
        }

        public void EndInteractObject(IControllerActionHandler handler)
        {
        }
        

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void UpdateTransform()
        {
            if (Owner != null)
                transform.position = Owner.GetGameObject().transform.position;
        }

        private void Update()
        {
            if (IsGrabbed)
            {
                UpdateTransform();
            }
        }
    }
}