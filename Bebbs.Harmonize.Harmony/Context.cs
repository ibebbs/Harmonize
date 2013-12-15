
namespace Bebbs.Harmonize.Harmony
{
    public interface IContext 
    {
        string EMail { get; }
        string Password { get; }
    }

    public interface IAuthenticatedContext : IContext
    {
        string AccountId { get; }
        string AuthenticationToken { get; }
    }

    public interface ISessionContext : IContext
    {
        ISessionInfo SessionInfo { get; }
    }

    public interface IActiveContext : IContext
    {
        ISession Session { get; }
        ISessionInfo SessionInfo { get; }
    }

    public static class ContextFactory
    {
        private class PrivateContext : IContext, IAuthenticatedContext, ISessionContext, IActiveContext
        {
            private readonly string _email;
            private readonly string _password;
            private readonly string _accountId;
            private readonly string _authenticationToken;
            private readonly ISessionInfo _sessionInfo;
            private ISession _session;

            public PrivateContext(string email, string password)
            {
                _email = email;
                _password = password;
            }

            public PrivateContext(IContext context, string accountId, string authenticationToken) : this(context.EMail, context.Password)
            {
                _accountId = accountId;
                _authenticationToken = authenticationToken;
            }

            public PrivateContext(IAuthenticatedContext context, ISessionInfo sessionInfo) : this(context, context.AccountId, context.AuthenticationToken)
            {
                _sessionInfo = sessionInfo;
            }

            public PrivateContext(ISessionContext context) : this((IAuthenticatedContext)context, context.SessionInfo)
            {
            }

            public PrivateContext(ISessionContext context, ISessionInfo sessionInfo, ISession session) : this(context)
            {
                _session = session;
            }

            string IContext.EMail
            {
                get { return _email; }
            }

            string IContext.Password
            {
                get { return _password; }
            }

            string IAuthenticatedContext.AccountId
            {
                get { return _accountId; }
            }

            string IAuthenticatedContext.AuthenticationToken
            {
                get { return _authenticationToken; }
            }

            ISessionInfo ISessionContext.SessionInfo
            {
                get { return _sessionInfo; }
            }

            ISession IActiveContext.Session
            {
                get { return _session; }
            }

            ISessionInfo IActiveContext.SessionInfo 
            {
                get { return _sessionInfo; }
            }
        }

        public static IContext Create(string email, string password)
        {
            return new PrivateContext(email, password);
        }

        public static IAuthenticatedContext WithAuthentication(this IContext context, string accountId, string authenticationToken)
        {
            return new PrivateContext(context, accountId, authenticationToken);
        }

        public static ISessionContext ForSession(this IAuthenticatedContext context, ISessionInfo sessionInfo)
        {
            return new PrivateContext(context, sessionInfo);
        }

        public static IActiveContext Activate(this ISessionContext context, ISessionInfo sessionInfo, ISession session)
        {
            return new PrivateContext(context, sessionInfo, session);
        }
    }
}
