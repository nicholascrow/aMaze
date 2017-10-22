using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour, IItemHolderInteractionObject
{
    public IInteractableObject HeldInteractableObject;

    private Vector3 originalScale;
    private Vector3 shrinkScale;


    public void SetObject(IInteractableObject obj)
    {
        if (HeldInteractableObject != null)
        {
            UnsetObject();
        }

        HeldInteractableObject = obj;
        originalScale = obj.GetGameObject().transform.localScale;
        ShrinkObject();
        obj.Owner = this;

    }

    public void ShrinkObject()
    {
        if (shrinkScale == Vector3.zero)
        {

            while (HeldInteractableObject.GetGameObject().transform.Size().GetLargestValue() >
                   transform.Size().GetLargestValue())
            {
                HeldInteractableObject.GetGameObject().transform.localScale =
                    HeldInteractableObject.GetGameObject().transform.localScale -
                    HeldInteractableObject.GetGameObject().transform.localScale / 2;
            }
            shrinkScale = HeldInteractableObject.GetGameObject().transform.localScale;
        }
        else
        {
            HeldInteractableObject.GetGameObject().transform.localScale = shrinkScale;
        }
    }


    void ResetObject()
    {
        HeldInteractableObject.GetGameObject().transform.localScale = originalScale;
    }


    public IInteractableObject TakeObject()
    {
        ResetObject();
        HeldInteractableObject.AsGrabInteractibleObject().HoldingOwner = this;
        HeldInteractableObject.AsGrabInteractibleObject().Owner = null;

        return this.HeldInteractableObject;
    }

    public void UnsetObject()
    {
        ResetObject();
        HeldInteractableObject.Owner = null;
        HeldInteractableObject = null;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void BeginInteractObject(IControllerActionHandler handler)
    {

    }

    public void EndInteractObject(IControllerActionHandler handler)
    {

    }
}
