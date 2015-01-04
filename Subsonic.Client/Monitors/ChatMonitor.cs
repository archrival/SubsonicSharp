using System;
using Subsonic.Client.EventArgs;
using Subsonic.Client.Handlers;
using Subsonic.Client.Items;

namespace Subsonic.Client.Monitors
{
    public class ChatMonitor<T> : IObserver<ChatItem>
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

        public void OnNext(ChatItem value)
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

        protected virtual void OnPropertyChanged(ChatItem chatItem)
        {
            Action<ChatMonitor<T>, ChatEventArgs> handler = PropertyChanged;
            if (handler != null) handler(this, new ChatEventArgs(chatItem));
        }
    }
}
