using Microsoft.AspNet.SignalR.Client;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Hub
{
    public interface IInstance : IDisposable
    {
        IObservable<T1> GetEvent<T1>(string name);
        IObservable<Tuple<T1, T2>> GetEvent<T1, T2>(string name);
        IObservable<Tuple<T1, T2, T3>> GetEvent<T1, T2, T3>(string name);

        Task Start();
        Task Stop();
        Task Register(Common.Dto.Identity registrar, Common.Dto.Entity entity);
        Task Deregister(Common.Dto.Identity registrar, Common.Dto.Entity entity);
        Task Observe(Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Identity source, Common.Dto.Identity observable);
    }

    internal class Instance : IInstance
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;

        public Instance(HubConnection hubConnection, IHubProxy hubProxy)
        {
            _hubConnection = hubConnection;
            _hubProxy = hubProxy;
        }

        public void Dispose()
        {
            _hubConnection.Dispose();
        }

        public IObservable<T1> GetEvent<T1>(string name)
        {
            return Observable.Create<T1>(
                observer =>
                {
                    bool disposed = false;

                    Action<T1> handler = value => { if (!disposed) observer.OnNext(value); };

                    _hubProxy.On<T1>(name, handler);

                    return Disposable.Create(() => disposed = true);
                }
            );
        }

        public IObservable<Tuple<T1, T2>> GetEvent<T1, T2>(string name)
        {
            return Observable.Create<Tuple<T1, T2>>(
                observer =>
                {
                    bool disposed = false;

                    Action<T1, T2> handler = (t1, t2) => { if (!disposed) observer.OnNext(Tuple.Create(t1, t2)); };

                    _hubProxy.On<T1, T2>(name, handler);

                    return Disposable.Create(() => disposed = true);
                }
            );
        }

        public IObservable<Tuple<T1, T2, T3>> GetEvent<T1, T2, T3>(string name)
        {
            return Observable.Create<Tuple<T1, T2, T3>>(
                observer =>
                {
                    bool disposed = false;

                    Action<T1, T2, T3> handler = (t1, t2, t3) => 
                    {
                        if (!disposed) 
                            observer.OnNext(Tuple.Create(t1, t2, t3)); 
                    };

                    _hubProxy.On<T1, T2, T3>(name, handler);

                    return Disposable.Create(() => disposed = true);
                }
            );
        }

        public Task Start()
        {
            return _hubConnection.Start();
        }

        public Task Stop()
        {
            _hubConnection.Stop();

            return TaskEx.Done;
        }

        public Task Register(Common.Dto.Identity registrar, Common.Dto.Entity entity)
        {
            return _hubProxy.Invoke("Register", new object[] { registrar, entity });
        }

        public Task Deregister(Common.Dto.Identity registrar, Common.Dto.Entity entity)
        {
            return _hubProxy.Invoke("Deregister", new object[] { registrar, entity });
        }

        public Task Observe(Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Identity source, Common.Dto.Identity observable)
        {
            return _hubProxy.Invoke("Observe", new object[] { registrar, entity, source, observable });
        }
    }
}
