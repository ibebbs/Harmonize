﻿using Bebbs.Harmonize.Common;
using Bebbs.Harmonize.Harmony.Messages;
using System;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.Harmony.State
{
    public interface IMachine : IInitializeAtStartup, ICleanupAtShutdown
    {

    }

    public class Machine : IMachine
    {
        private readonly IFactory _stateFactory;
        private readonly Common.Settings.IProvider _settingsProvider;
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _transitionSubscription;
        private IState _currentState;
        private IContext _currentContext;
        private Common.Settings.IValues _settings;

        public Machine(IFactory stateFactory, Common.Settings.IProvider settingsProvider, IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _stateFactory = stateFactory;
            _settingsProvider = settingsProvider;
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void EnterState<TContext>(IState<TContext> state, TContext context)
            where TContext : IContext
        {
            state.OnEnter(context);
        }

        private void EnterState()
        {
            dynamic currentState = _currentState;
            dynamic currentContext = _currentContext;

            EnterState(currentState, currentContext);
        }

        private void ExitState<TContext>(IState<TContext> state, TContext context)
            where TContext : IContext
        {
            state.OnExit(context);
        }

        private void ExitState()
        {
            dynamic currentState = _currentState;
            dynamic currentContext = _currentContext;

            ExitState(currentState, currentContext);
        }

        private void ChangeIntoState<TContext>(ITransitionToStateMessage<TContext> message)
            where TContext : IContext
        {
            _currentState = _stateFactory.ConstructState(message.State);
            _currentContext = message.Context;
        }

        private void ChangeState(ITransitionToStateMessage message)
        {
            dynamic stateMessage = message;

            ChangeIntoState(stateMessage);
        }

        private void ProcessTransition(ITransitionToStateMessage message)
        {
            ExitState();

            ChangeState(message);

            EnterState();
        }

        public void Initialize()
        {
            _settings = _settingsProvider.GetValues();

            _currentState = _stateFactory.ConstructState(Name.Stopped);
            _currentContext = ContextFactory.Create(_settings.EMail, _settings.Password);

            EnterState();

            _transitionSubscription = _eventAggregator.GetEvent<ITransitionToStateMessage>().ObserveOn(_asyncHelper.AsyncScheduler).Subscribe(ProcessTransition);
        }

        public void Cleanup()
        {
            if (_transitionSubscription != null)
            {
                _transitionSubscription.Dispose();
                _transitionSubscription = null;
            }

            ExitState();
        }

        public void Start()
        {
            _eventAggregator.Publish(new StartHarmonizingMessage());
        }

        public void Stop()
        {
            _eventAggregator.Publish(new StopHarmonizingMessage());
        }
    }
}
