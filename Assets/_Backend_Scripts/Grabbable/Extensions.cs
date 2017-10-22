using UnityEngine;

public static class Extensions
{
    public static IRiftActionHandler AsRiftActionHandler(this IControllerActionHandler handler)
    {
        return handler as IRiftActionHandler;
    }
    public static IViveActionHandler AsViveActionHandler(this IControllerActionHandler handler)
    {
        return handler as IViveActionHandler;
    }

    public static IGrabInteractibleObject AsGrabInteractibleObject(this IInteractableObject obj)
    {
        return obj as IGrabInteractibleObject;
    }
    public static ItemHolder AsItemHolder(this IInteractionObject obj)
    {
        return obj as ItemHolder;
    }
    public static TeleportLocation AsTeleportLocation(this IInteractableObject obj)
    {
        return obj as TeleportLocation;
    }

    public static Quaternion ToQuaternion(this Vector3 q)
    {
        return Quaternion.Euler(q.x,q.y,q.z);
    }

    public static IItemHolderInteractionObject OverlappingWithHolder(this IInteractableObject obj,LayerMask m)
    {

        Collider[] hitColliders = Physics.OverlapSphere(obj.GetGameObject().transform.position, obj.GetGameObject().transform.Size().GetLargestValue(), m);
        foreach (var item in hitColliders)
        {
            Debug.LogWarning("HOLDER OVERLAP: " + item.gameObject.name);
            return item.GetComponent<IItemHolderInteractionObject>();
        }
        return null;
    }

    public static Vector3 Size(this Transform t)
    {
        Vector3 size = Vector3.zero;
        MeshFilter[] filters = t.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter filter in filters)
        {
            if (filter.sharedMesh == null) continue;
            Vector3 meshSize = Vector3.Scale(filter.sharedMesh.bounds.size, filter.transform.lossyScale);
            size = Vector3.Max(size, meshSize);
        }
        return size;
    }

    public static float GetLargestValue(this Vector3 v)
    {
        float val = Mathf.NegativeInfinity;
        if (v.x > val) val = v.x;
        if (v.y > val) val = v.y;
        if (v.z > val) val = v.z;
        return val;
    }
}