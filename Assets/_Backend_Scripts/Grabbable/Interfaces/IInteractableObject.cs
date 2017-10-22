using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableObject
{
    GameObject GetGameObject();
    IInteractionObject Owner { get; set; }
    void BeginInteractObject(IControllerActionHandler handler);
    void EndInteractObject(IControllerActionHandler handler);
}

public interface IGrabInteractibleObject : IInteractableObject
{
    bool IsGrabbed { get; }
    bool HasPhysicsEnabled { get; }
    bool IsHighlighted { get; set; }
    IInteractionObject HoldingOwner { get; set; }
    void UpdateTransform();
}

public interface IInteractionObject
{

    void BeginInteractObject(IControllerActionHandler handler);
    void EndInteractObject(IControllerActionHandler handler);

    GameObject GetGameObject();
}

public interface IGrabInteractionObject : IInteractionObject
{
    IInteractableObject CurrentlyGrabbedObject { get; }
    bool CompareToGrabbedObject(IInteractableObject checkObj);
}

public interface IItemHolderInteractionObject : IInteractionObject
{

}

