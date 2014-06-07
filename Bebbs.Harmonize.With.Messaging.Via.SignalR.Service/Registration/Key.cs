
namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service.Registration
{
    public static class Key
    {
        private static string For(string registrar, string entity)
        {
            return string.Concat(registrar, "-", entity);
        }

        public static string For(With.Component.IIdentity registrar, With.Component.IIdentity entity)
        {
            return For(registrar.Value, entity.Value);
        }

        public static string For(string contextId, Common.Identity entity)
        {
            return For(contextId, entity.Value);
        }
    }
}
