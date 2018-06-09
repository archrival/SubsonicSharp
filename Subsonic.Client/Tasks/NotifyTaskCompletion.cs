using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Subsonic.Client.Tasks
{
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task<TResult> task)
        {
            Task = task;

            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorMessage => InnerException?.Message;

        public AggregateException Exception => Task.Exception;

        public Exception InnerException => Exception?.InnerException;

        public bool IsCanceled => Task.IsCanceled;

        public bool IsCompleted => Task.IsCompleted;

        public bool IsFaulted => Task.IsFaulted;

        public bool IsNotCompleted => !Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        public TResult Result => Task.Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult);

        public TaskStatus Status => Task.Status;

        public Task<TResult> Task { get; private set; }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            {
            }

            var propertyChanged = PropertyChanged;

            if (propertyChanged == null)
                return;

            propertyChanged(this, new PropertyChangedEventArgs("Status"));
            propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));

            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                propertyChanged(this, new PropertyChangedEventArgs("Exception"));
                propertyChanged(this, new PropertyChangedEventArgs("InnerException"));
                propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                propertyChanged(this, new PropertyChangedEventArgs("Result"));
            }
        }
    }
}