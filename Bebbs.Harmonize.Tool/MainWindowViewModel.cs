using Caliburn.Micro;
using Caliburn.Micro.Reactive.Extensions;
using System;
using System.Reactive.Disposables;
using System.Windows.Input;

namespace Bebbs.Harmonize.Tool
{
    public interface IMainWindowViewModel
    {
        ICommand Register { get; }
        ICommand Action { get; }
    }

    public class MainWindowViewModel : Screen, IMainWindowViewModel
    {
        private Service.IBridge _bridge;
        private ObservableCommand _registerCommand;
        private ObservableCommand _actionCommand;

        private IDisposable _behaviors;

        public MainWindowViewModel(Service.IBridge bootstrapper)
        {
            _bridge = bootstrapper;

            _registerCommand = new ObservableCommand();
            _actionCommand = new ObservableCommand();

            _behaviors = new CompositeDisposable(
                EnsureRegisterCommandRegistersEntity(),
                EnsureActionCommandPerformsAction()
            );
        }

        private IDisposable EnsureRegisterCommandRegistersEntity()
        {
            return _registerCommand.Subscribe(_ => _bridge.Register());
        }

        private IDisposable EnsureActionCommandPerformsAction()
        {
            return _actionCommand.Subscribe(_ => _bridge.Action());
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            await _bridge.Start();
        }

        protected override async void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                await _bridge.Stop();
            }
        }

        public ICommand Register 
        {
            get { return _registerCommand; }
        }

        public ICommand Action 
        {
            get { return _actionCommand; }
        }
    }
}