namespace TaskPlanner.App.Commands
{
    using TaskPlanner.App.Commands.Base;
    public class LambdaCommand : Command
    {
        private readonly Delegate? _Execute;
        private readonly Delegate? _CanExecute;

        public LambdaCommand(Action<object?> Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public LambdaCommand(Action<object?> Execute, Func<object?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public LambdaCommand(Action Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public LambdaCommand(Action Execute, Func<object?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        protected override bool CanExecute(object? p)
        {
            if (!base.CanExecute(p)) return false;
            return _CanExecute switch
            {
                null => true,
                Func<bool> can_exec => can_exec(),
                Func<object?, bool> can_exec => can_exec(p),
                _ => throw new InvalidOperationException($"The delegate type is {_CanExecute.GetType()} not supported by the command")
            };
        }

        protected override void Execute(object? p)
        {
            switch (_Execute)
            {
                default: throw new InvalidOperationException($"The delegate type is {_Execute.GetType()} not supported by the command");
                case null: throw new InvalidOperationException("The call delegate for the command is not specified");

                case Action execute: execute(); break;
                case Action<object?> execute: execute(p); break;
            }
        }
    }
    public class LambdaCommand<T> : Command
    {
        private readonly Delegate? _Execute;
        private readonly Delegate? _CanExecute;

        public LambdaCommand(Action<T?> Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public LambdaCommand(Action<T?> Execute, Func<T?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public LambdaCommand(Action Execute, Func<bool>? CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        public LambdaCommand(Action Execute, Func<T?, bool>? CanExecute)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        protected override bool CanExecute(object? p)
        {
            if (!base.CanExecute(p)) return false;
            return _CanExecute switch
            {
                null => true,
                Func<bool> can_exec => can_exec(),
                Func<T?, bool> can_exec => can_exec((T?)Convert.ChangeType(p, typeof(T))),
                _ => throw new InvalidOperationException($"The delegate type is {_CanExecute.GetType()} not supported by the command")
            };
        }

        protected override void Execute(object? p)
        {
            switch (_Execute)
            {
                default: throw new InvalidOperationException($"The delegate type is {_Execute.GetType()} not supported by the command");
                case null: throw new InvalidOperationException("The call delegate for the command is not specified");

                case Action execute: execute(); break;
                case Action<T?> execute: execute((T?)Convert.ChangeType(p, typeof(T))); break;
            }
        }
    }
}
