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
    public static TeleportLocation AsTeleportLocation(this IInteractableObject obj)
    {
        return obj as TeleportLocation;
    }
}