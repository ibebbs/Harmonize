using agsXMPP;

namespace Bebbs.Harmonize.Harmony
{
    public interface ISession
    {
        XmppClientConnection Connection { get; }
    }

    internal class Session : ISession
    {
        public Session(XmppClientConnection connection)
        {
            Connection = connection;
        }

        public XmppClientConnection Connection { get; private set; }
    }
}
