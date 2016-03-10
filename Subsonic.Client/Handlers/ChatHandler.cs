using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Items;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Handlers
{
    public class ChatHandler<T> : IObservable<ChatItem>, IDisposable where T : class, IDisposable
    {
        readonly List<IObserver<ChatItem>> _observers;
        readonly HashSet<ChatItem> _chatItems;
        readonly Lazy<Task> _worker;

        public int Interval { get; set; }
        public ISubsonicClient<T> Client { get; set; }
        public CancellationToken CancellationToken;

        double _lastChatItem;

        public ChatHandler()
        {
            _worker = new Lazy<Task>(() => Task.Factory.StartNew(SpawnWorker));
            _observers = new List<IObserver<ChatItem>>();
            _chatItems = new HashSet<ChatItem>();
            Interval = 1000;
        }

        public IDisposable Subscribe(IObserver<ChatItem> observer)
        {
            var worker = _worker.Value;

            // Check whether observer is already registered. If not, add it 
            if (_observers.Contains(observer))
                return new Unsubscriber<ChatItem>(_observers, observer);

            _observers.Add(observer);

            // Provide observer with existing data. 
            foreach (var item in _chatItems.OrderBy(ci => ci.TimeStamp))
                observer.OnNext(item);

            return new Unsubscriber<ChatItem>(_observers, observer);
        }

        async void SpawnWorker()
        {
            bool execute = true;

            while (execute)
            {
                try
                {
                    CancellationToken.ThrowIfCancellationRequested();

                    if (_observers.Any() && Client != null)
                    {
                        await Client.GetChatMessagesAsync(_lastChatItem, CancellationToken).ContinueWith((response) =>
                        {
                            if (response.Status != TaskStatus.RanToCompletion) return;

                            var result = response.Result;

                            foreach (var chatMessage in result.Items.OrderBy(ci => ci.Time))
                            {
                                var item = new ChatItem(chatMessage);

                                _chatItems.Add(item);

                                foreach (var observer in _observers)
                                    observer.OnNext(item);

                                if (chatMessage.Time > _lastChatItem)
                                    _lastChatItem = chatMessage.Time;
                            }
                        }, CancellationToken);
                    }
                    else
                    {
                        _lastChatItem = 0;
                    }

                    CancellationToken.ThrowIfCancellationRequested();

                    await Task.Delay(Interval, CancellationToken);
                }
                catch (TaskCanceledException ex)
                {
                    execute = false;
                }
            }
        }

        public void RemoveObservers()
        {
            foreach (var observer in _observers)
                observer.OnCompleted();

            _observers.Clear();
        }

        public void Dispose()
        {
            RemoveObservers();
        }
    }
}
