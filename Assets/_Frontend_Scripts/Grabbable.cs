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

        public Vector3 HardSetRotation;
        public bool UseSetChild;

        public bool HasPhysicsEnabled { get; private set; }
        public bool IsHighlighted { get; set; }
        public IInteractionObject HoldingOwner { get; set; }

        private Color oldColor;

        public IInteractionObject Owner { get; set; }

        void Start()
        {
            oldColor = GetComponent<Renderer>().material.color;
        }

        private Transform OldParent = null;
        public void BeginInteractObject(IControllerActionHandler handler)
        {
            if (UseSetChild)
            {
                transform.GetComponent<Rigidbody>().isKinematic = true;
                OldParent = transform.parent;
                transform.SetParent(handler.Grab.gameObject.transform);
            }
        }

        public void EndInteractObject(IControllerActionHandler handler)
        {
            if (UseSetChild)
            {
                transform.SetParent(OldParent);
                transform.GetComponent<Rigidbody>().isKinematic = false;
                OldParent = null;
            }

            if (this.GetComponent<CheckBelow>() != null)
            {
                GetComponent<CheckBelow>().DoCheck();
            }

        }


        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void UpdateTransform()
        {
            if (Owner != null && !UseSetChild)
            {
                transform.position = Owner.GetGameObject().transform.position;
                Quaternion old = Owner.GetGameObject().transform.rotation;
                transform.rotation = old;
                // Quaternion.Euler(new Vector3(old.eulerAngles.x + OffsetRotation.x, old.eulerAngles.y + OffsetRotation.y, old.eulerAngles.z + OffsetRotation.z));
            }
            else if(Owner != null && UseSetChild)
            {
                transform.localRotation = Quaternion.Euler(HardSetRotation);
                transform.localPosition = Vector3.zero;
                //  Quaternion old = Owner.GetGameObject().transform.rotation;
                // transform.rotation = HardSetRotation.ToQuaternion();
            }
        }

        IEnumerator ReGrabIn2Seconds()
        {
            for (float i = 0; i < 2f; i += Time.deltaTime)
            {
                if (IsGrabbed)
                {
                    yield break;
                }
                yield return null;
            }
            if (!IsGrabbed && HoldingOwner != null)
            {
                Owner = HoldingOwner;
                HoldingOwner.AsItemHolder().ShrinkObject();
                HoldingOwner = null;
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

            if (HoldingOwner != null && !IsGrabbed)
            {
                //   StartCoroutine(ReGrabIn2Seconds());
            }
        }
    }
}