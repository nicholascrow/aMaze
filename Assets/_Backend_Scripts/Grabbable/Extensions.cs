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
}