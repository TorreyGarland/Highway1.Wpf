namespace Highway1.Wpf.Input
{

    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public abstract class DelegateCommandBase
    {

        #region Properties

        protected Func<object, bool> Predicate { get; }

        #endregion

        #region Methods

        [DebuggerStepThrough]
        protected DelegateCommandBase(Func<object, bool> predicate)
        {
            Predicate = predicate;
        }

        [DebuggerStepThrough]
        public virtual bool CanExecute(object parameter)
            => (Predicate?.Invoke(parameter)).GetValueOrDefault(true);

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { 
                CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

    }

}