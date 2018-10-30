﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MQTTDataProvider.ViewModel
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking
    /// delegates. The default return value for the CanExecute method is 'true'.
    /// </summary>
    ///
    /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        #endregion

        #region Constructors

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new command that can always execute. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="execute">  The execution logic. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new command. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="execute">      The execution logic. </param>
        /// <param name="canExecute">   The execution status logic. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="parameters">   Data used by the command.  If the command does not require data
        ///                             to be passed, this object can be set to <see langword="null" />. </param>
        ///
        /// <returns>
        /// <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return canExecute == null ? true : canExecute(parameters);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Defines the method to be called when the command is invoked. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="parameters">   Data used by the command.  If the command does not require data
        ///                             to be passed, this object can be set to <see langword="null" />. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Execute(object parameters)
        {
            if (execute != null)
            {
                execute(parameters);
                return;
            }
        }

        #endregion
    }
}