namespace Highway1.Wpf.Input
{

    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public sealed class AsyncDelegateCommand : DelegateCommandBase, ICommand
    {

        #region Fields

        readonly Func<object, Task> _function;

        #endregion

        #region Methods

        [DebuggerStepThrough]
        AsyncDelegateCommand(Func<object, Task> function, Func<object, bool> predicate)
            : base(predicate)
        {
            Contract.Requires<ArgumentNullException>(function != null, nameof(function));
            _function = function;
        }

        [DebuggerStepThrough, Pure]
        public static AsyncDelegateCommand New(Func<Task> function)
        {
            Contract.Requires<ArgumentNullException>(function != null, nameof(function));
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(New));
            return New(_ => function());
        }

        [DebuggerStepThrough, Pure]
        public static AsyncDelegateCommand New(Func<object, Task> function)
        {
            Contract.Requires<ArgumentNullException>(function != null, nameof(function));
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(New));
            return new AsyncDelegateCommand(function, null);
        }

        [DebuggerStepThrough]
        void ICommand.Execute(object parameter)
            => Task.Run(() => ExecuteAsync(parameter));

        [DebuggerStepThrough]
        public Task ExecuteAsync(object parameter = null)
            => _function(parameter);

        [DebuggerStepThrough, Pure]
        public AsyncDelegateCommand WithPredicate(Func<bool> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, nameof(value));
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(WithPredicate));
            return WithPredicate(_ => value());
        }

        [DebuggerStepThrough, Pure]
        public AsyncDelegateCommand WithPredicate(Func<object, bool> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, nameof(value));
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(WithPredicate));
            return value == Predicate ? this : new AsyncDelegateCommand(_function, value);
        }

        #endregion

    }

}