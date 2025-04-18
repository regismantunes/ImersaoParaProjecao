﻿using System.Windows.Input;

namespace ImmersionToProjection.Utility;

/// <summary>
/// Creates instance of the command handler
/// </summary>
/// <param name="action">Action to be executed by the command</param>
/// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
public class CommandHandler(Action action, Func<bool> canExecute) : ICommand
{
    private readonly Action _action = action;
    private readonly Func<bool> _canExecute = canExecute;

    public CommandHandler(Action action) : this(action, () => true) { }

    /// <summary>
    /// Wires CanExecuteChanged event 
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Forcess checking if execute is allowed
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter) => _canExecute.Invoke();

    public void Execute(object? parameter) => _action();
}
