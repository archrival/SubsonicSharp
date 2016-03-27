using System;
using Subsonic.Client.EventArgs;
using Subsonic.Client.Handlers;
using Subsonic.Client.Models;

namespace Subsonic.Client.Monitors
{
    public class ChatMonitor<T> : IObserver<ChatModel> where T : class, IDisposable
    {
        private IDisposable _cancellation;
        public ChatHandler<T> ChatHandler { get; set; }
        public bool Disposed { get; set; }

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

        public void OnError(Exception error)
        {
            
        }

        public void OnCompleted()
        {
            Disposed = true;
        }

        public event Action<ChatMonitor<T>, ChatEventArgs> PropertyChanged;

        protected virtual void OnPropertyChanged(ChatModel chatItem)
        {
            Action<ChatMonitor<T>, ChatEventArgs> handler = PropertyChanged;
            handler?.Invoke(this, new ChatEventArgs(chatItem));
        }
    }
}
