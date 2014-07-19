
namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service.Registration
{
    public static class Key
    {
        private static string For(string connectionId, string registrar, string entity)
        {
            return string.Concat(connectionId, "-", registrar, "-", entity);
        }

        public static string For(string connectionId, Common.Dto.Identity registrar, Common.Dto.Identity entity)
        {
            return For(connectionId, registrar.Value, entity.Value);
        }

        internal static string For(string connectionId, With.Component.IIdentity registrar, With.Component.IIdentity entity)
        {
            return For(connectionId, registrar.Value, entity.Value);
        }
    }
}
