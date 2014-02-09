using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With
{
    public static class ObservableExtentions
    {
        public static IObservable<TResult> Either<TX, TY, TResult>(IObservable<TX> x, IObservable<TY> y, Func<TX, TY, TResult> func) 
            where TX : class
            where TY : class
        {
            return Observable.Create<TResult>(
                observer =>
                {
                    Action<Func<TResult>> handler = action =>
                    {
                        try
                        {
                            TResult result = action();

                            observer.OnNext(result);
                        }
                        catch (Exception e)
                        {
                            observer.OnError(e);
                        }
                    };

                    IDisposable xSubscription = x.Subscribe(value => handler(() => func(value, null)), observer.OnError, observer.OnCompleted);
                    IDisposable ySubscription = y.Subscribe(value => handler(() => func(null, value)), observer.OnError, observer.OnCompleted);

                    return new CompositeDisposable(xSubscription, ySubscription);
                }
            );
        }
    }
}
