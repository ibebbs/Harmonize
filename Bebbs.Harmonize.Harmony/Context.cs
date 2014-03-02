using System;

namespace Bebbs.Harmonize.With.Harmony
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
        Hub.Session.IInfo SessionInfo { get; }
    }

    public interface IActiveContext : IContext
    {
        Hub.Session.IInstance Session { get; }
        Hub.Session.IInfo SessionInfo { get; }
    }

    public interface IRegistrationContext : IActiveContext
    {
        Hub.Configuration.IValues HarmonyConfiguration { get; }
    }

    public interface IFaultedContext : IContext
    {
        Exception Exception { get; }
    }

    public static class ContextFactory
    {
        private class PrivateContext : IContext, IAuthenticatedContext, ISessionContext, IActiveContext, IRegistrationContext, IFaultedContext
        {
            private readonly string _email;
            private readonly string _password;
            private readonly string _accountId;
            private readonly string _authenticationToken;
            private readonly Hub.Session.IInfo _sessionInfo;
            private readonly Hub.Session.IInstance _session;
            private readonly Exception _exception;
            private readonly Hub.Configuration.IValues _harmonyConfiguration;

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

            public PrivateContext(IAuthenticatedContext context, Hub.Session.IInfo sessionInfo) : this(context, context.AccountId, context.AuthenticationToken)
            {
                _sessionInfo = sessionInfo;
            }

            public PrivateContext(ISessionContext context) : this((IAuthenticatedContext)context, context.SessionInfo)
            {
            }

            public PrivateContext(ISessionContext context, Hub.Session.IInfo sessionInfo, Hub.Session.IInstance session) : this(context)
            {
                _session = session;
            }

            public PrivateContext(IActiveContext context, Hub.Session.IInfo sessionInfo, Hub.Session.IInstance session, Hub.Configuration.IValues harmonyConfiguration) : this((ISessionContext)context, context.SessionInfo, context.Session)
            {
                _harmonyConfiguration = harmonyConfiguration;
            }

            public PrivateContext(IContext context, Exception exception) : this(context.EMail, context.Password)
            {
                _exception = exception;
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

            Hub.Session.IInfo ISessionContext.SessionInfo
            {
                get { return _sessionInfo; }
            }

            Hub.Session.IInstance IActiveContext.Session
            {
                get { return _session; }
            }

            Hub.Session.IInfo IActiveContext.SessionInfo 
            {
                get { return _sessionInfo; }
            }

            Hub.Configuration.IValues IRegistrationContext.HarmonyConfiguration
            {
                get { return _harmonyConfiguration; }
            }

            Exception IFaultedContext.Exception
            {
                get { return _exception; }
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

        public static ISessionContext ForSession(this IAuthenticatedContext context, Hub.Session.IInfo sessionInfo)
        {
            return new PrivateContext(context, sessionInfo);
        }

        public static IActiveContext Activate(this ISessionContext context, Hub.Session.IInfo sessionInfo, Hub.Session.IInstance session)
        {
            return new PrivateContext(context, sessionInfo, session);
        }

        public static IRegistrationContext ForRegistration(this IActiveContext context, Hub.Configuration.IValues harmoneyConfiguration)
        {
            return new PrivateContext(context, context.SessionInfo, context.Session, harmoneyConfiguration);
        }

        public static IFaultedContext Faulted(this IContext context, Exception exception)
        {
            return new PrivateContext(context, exception);
        }
    }
}
