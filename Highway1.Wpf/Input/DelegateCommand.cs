namespace Highway1.Wpf.Input
{

    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public sealed class DelegateCommand : DelegateCommandBase
    {

        #region Fields

        readonly Action<object> _action;

        #endregion

        #region Methods

        [DebuggerStepThrough]
        DelegateCommand(Action<object> action, Func<object, bool> predicate)
            : base(predicate)
        {
            Contract.Requires(action != null, nameof(action));
            _action = action;
        }

        [DebuggerStepThrough, Pure]
        public static DelegateCommand Create(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(Create));
            return Create(_ => action());
        }

        [DebuggerStepThrough, Pure]
        public static DelegateCommand Create(Action<object> action)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(Create));
            return new DelegateCommand(action, null);
        }

        [DebuggerStepThrough]
        public void Execute()
            => Execute(null);

        [DebuggerStepThrough]
        public override void Execute(object parameter)
            => _action(parameter);

        [DebuggerStepThrough, Pure]
        public DelegateCommand WithPredicate(Func<bool> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, nameof(value));
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(WithPredicate));
            return WithPredicate(_ => value());
        }

        [DebuggerStepThrough, Pure]
        public DelegateCommand WithPredicate(Func<object, bool> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, nameof(value));
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(WithPredicate));
            return value == Predicate ? this : new DelegateCommand(_action, value);
        }

        #endregion

    }

}