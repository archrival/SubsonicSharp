using Subsonic.Client.EventArgs;
using Subsonic.Client.Handlers;
using Subsonic.Client.Models;
using System;

namespace Subsonic.Client.Monitors
{
    public class ChatMonitor<T> : IObserver<ChatModel> where T : class, IDisposable
    {
        private IDisposable _cancellation;

        public event Action<ChatMonitor<T>, ChatEventArgs> PropertyChanged;

        public ChatHandler<T> ChatHandler { get; set; }
        public bool Disposed { get; set; }

        public void OnCompleted()
        {
            Disposed = true;
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ChatModel value)
        {
            try
            {
                OnPropertyChanged(value);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void Subscribe(ChatHandler<T> provider)
        {
            ChatHandler = provider;
            _cancellation = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            Disposed = true;
            _cancellation.Dispose();
            OnPropertyChanged(null);
        }

        protected virtual void OnPropertyChanged(ChatModel chatItem)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new ChatEventArgs(chatItem));
        }
    }
}