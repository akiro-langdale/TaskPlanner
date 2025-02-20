namespace TaskPlanner.App.Commands.Base
{
    using System.Windows.Input;
    public abstract class Command : ICommand
    {
        event EventHandler? ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested += value;
        }

        bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

        void ICommand.Execute(object? parameter)
        {
            if (((ICommand)this).CanExecute(parameter))
                Execute(parameter);
        }

        protected virtual bool CanExecute(object? p) => true;

        protected abstract void Execute(object? parameter);
    }
}
