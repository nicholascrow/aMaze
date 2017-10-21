using System;
//using Assets.Scripts.Menu;
using Scripts.Grab;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public Ray ControllerPointRay;
    private IControllerActionHandler ActionHandler;
    void Update()
    {
        UpdateRay();
        Debug.DrawRay(ControllerPointRay.origin, ControllerPointRay.direction);
    }

    void Start()
    {
        ActionHandler = GetComponent<IControllerActionHandler>();
    }

    public RaycastHit? TryRaycast(LayerMask m)
    {
        RaycastHit h;

        if (Physics.Raycast(ControllerPointRay.origin, ControllerPointRay.direction, out h, 1000, m))
        {
            return h;
        }
        return null;
    }


    public IInteractableObject CurrentInteractableObject;
    public IInteractableObject BeginInteract()
    {
        if (CurrentInteractableObject != null)
            return null;

        RaycastHit? h = TryRaycast(-1 << LayerMask.NameToLayer("InteractibleObject"));
        if (h == null) return null;

        IInteractableObject obj = h.Value.transform.GetComponent<IInteractableObject>();
        if (obj == null) return null;

        if (obj is IGrabInteractibleObject)
        {
            ActionHandler.Grab.BeginInteractObject(ActionHandler);
        }

        obj.BeginInteractObject(ActionHandler);

        CurrentInteractableObject = obj;
        return obj;
    }

    public void EndInteract()
    {
        if (CurrentInteractableObject == null) return;
        CurrentInteractableObject.EndInteractObject(ActionHandler);

        if (CurrentInteractableObject is IGrabInteractibleObject)
        {
            ActionHandler.Grab.EndInteractObject(ActionHandler);
        }

        CurrentInteractableObject = null;
    }

    private void UpdateRay()
    {
        ControllerPointRay.origin = this.transform.position;
        ControllerPointRay.direction = this.transform.forward;
    }

    public IInteractionObject FindWithinDistance(Vector3 position, LayerMask m, Type t, float distance = 1f)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, distance, m);
        foreach (var item in hitColliders)
        {
            if (item.GetComponent(t) != null)
            {
                return item.GetComponent(t) as IInteractionObject;
            }
        }
        return null;
    }
}