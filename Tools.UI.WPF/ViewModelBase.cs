using System;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;

namespace Tools.UI.WPF
{
    public class ViewModelBase : PropertyHelper
    {
        protected const ListSortDirection Ascend = ListSortDirection.Ascending;
        protected const ListSortDirection Descend = ListSortDirection.Descending;
            
        
        protected ICommand CommandHelper(ref ICommand command, Action method)
        {
            return command ??= new DelegateCommand(method);
        }
        
        
        protected ICommand CommandHelper(ref ICommand command, Action method, Func<bool> canDoAction)
        {
            return command ??= new DelegateCommand(method, canDoAction);
        }
        
        
        protected ICommand CommandHelper<T>(ref ICommand command, Action<T> method)
        {
            return command ??= new DelegateCommand<T>(method);
        }
        
        
        protected ICommand CommandHelper<T>(ref ICommand command, Action<T> method, Func<T,bool> canDoAction)
        {
            return command ??= new DelegateCommand<T>(method, canDoAction);
        }
        
        
        protected ICommand CommandHelper<T>(ICommand command, Action<T> method, Func<T,bool> canDoAction)
        {
            return command ??= new DelegateCommand<T>(method, canDoAction);
        }
        
        protected DelegateCommand CommandHelper(ref DelegateCommand command, Action method)
        {
            return command ??= new DelegateCommand(method);
        }
        
        
        protected DelegateCommand CommandHelper(ref DelegateCommand command, Action method, Func<bool> canDoAction)
        {
            return command ??= new DelegateCommand(method, canDoAction);
        }
        
        
        protected DelegateCommand<T> CommandHelper<T>(ref DelegateCommand<T> command, Action<T> method)
        {
            return command ??= new DelegateCommand<T>(method);
        }
        
        
        protected DelegateCommand<T> CommandHelper<T>(ref DelegateCommand<T> command, Action<T> method, Func<T,bool> canDoAction)
        {
            return command ??= new DelegateCommand<T>(method, canDoAction);
        }
    }
}