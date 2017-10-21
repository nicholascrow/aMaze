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
        public bool IsHighlighted { get; set; }

        private Color oldColor;

        public IInteractionObject Owner { get; set; }


        void Start()
        {
            oldColor = GetComponent<Renderer>().material.color;
        }


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
            {
                transform.position = Owner.GetGameObject().transform.position;
                transform.rotation = Owner.GetGameObject().transform.rotation;
            }
        }

        private void Update()
        {
            if (IsGrabbed)
            {
                UpdateTransform();
            }
            if (IsHighlighted)
            {
                GetComponent<Renderer>().material.color = Color.green;
                IsHighlighted = false;
            }
            else
            {
                GetComponent<Renderer>().material.color = oldColor;
            }
        }
    }
}