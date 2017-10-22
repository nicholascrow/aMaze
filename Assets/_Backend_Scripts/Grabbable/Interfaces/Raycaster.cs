using System;
using System.Runtime.CompilerServices;
//using Assets.Scripts.Menu;
using Scripts.Grab;
using UnityEngine;


public class LaserPointer : MonoBehaviour
{
    public LineRenderer Laser;

    void Start()
    {
        Laser = gameObject.AddComponent<LineRenderer>();
        Laser.positionCount = 2;
        Laser.startWidth = Laser.endWidth = .01f;
        Laser.startColor = Laser.endColor = new Color(0, 1, 0, 1);
        Laser.useWorldSpace = false;
    }

    void Update()
    {
        Laser.SetPosition(0, Vector3.zero);
    }

    public void SetHitPosition(Vector3 position)
    {
        if (Laser != null)
            Laser.SetPosition(1, position);
    }
}

public class Raycaster : MonoBehaviour
{
    public Ray ControllerPointRay;
    public LaserPointer Laser;
    private IControllerActionHandler ActionHandler;

    void Update()
    {
        UpdateRay();
        Debug.DrawRay(ControllerPointRay.origin, ControllerPointRay.direction);
    }

    void Start()
    {
        ActionHandler = GetComponent<IControllerActionHandler>();
        Laser = gameObject.AddComponent<LaserPointer>();
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
        print("Begin Interaction!");
        if (CurrentInteractableObject != null)
        {
            Debug.LogError("Currently Holding something! :(");
            return null;
        }
        RaycastHit? h = TryRaycast(1 << LayerMask.NameToLayer("InteractibleObject"));
        if (h == null) {return null;}

        IInteractableObject obj = h.Value.transform.GetComponent<IInteractableObject>();
        print(h.Value.transform.name);
        if (obj == null) {return null;}

        if (obj is IGrabInteractibleObject)
        {
            if (obj.Owner.AsItemHolder() != null)
            {
                obj.Owner.AsItemHolder().TakeObject();
            }

            if (obj.AsGrabInteractibleObject().Owner == null)
                ActionHandler.Grab.BeginInteractObject(ActionHandler);
        }
        else if (obj is TeleportLocation)
        {
            obj.AsTeleportLocation().BeginInteractObject(ActionHandler);
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
        else if (CurrentInteractableObject is TeleportLocation)
        {
            CurrentInteractableObject.EndInteractObject(ActionHandler);
            print("tele");
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