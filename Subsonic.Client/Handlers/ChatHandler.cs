using Subsonic.Client.Models;
using Subsonic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Handlers
{
    public class ChatHandler<T> : IObservable<ChatModel>, IDisposable where T : class, IDisposable
    {
        public CancellationToken CancellationToken;
        private readonly HashSet<ChatModel> _chatItems;
        private readonly List<IObserver<ChatModel>> _observers;
        private readonly Lazy<Task> _worker;

        private double _lastChatItem;

        public ChatHandler()
        {
            _worker = new Lazy<Task>(() => Task.Factory.StartNew(SpawnWorker));
            _observers = new List<IObserver<ChatModel>>();
            _chatItems = new HashSet<ChatModel>();
            Interval = 1000;
        }

        public ISubsonicClient<T> Client { get; set; }
        public int Interval { get; set; }

        public void Dispose()
        {
            RemoveObservers();
        }

        public void RemoveObservers()
        {
            foreach (var observer in _observers)
                observer.OnCompleted();

            _observers.Clear();
        }

        public IDisposable Subscribe(IObserver<ChatModel> observer)
        {
            var worker = _worker.Value;

            // Check whether observer is already registered. If not, add it
            if (_observers.Contains(observer))
                return new Unsubscriber<ChatModel>(_observers, observer);

            _observers.Add(observer);

            // Provide observer with existing data.
            foreach (var item in _chatItems.OrderBy(ci => ci.TimeStamp))
                observer.OnNext(item);

            return new Unsubscriber<ChatModel>(_observers, observer);
        }

        private async void SpawnWorker()
        {
            var execute = true;

            while (execute)
            {
                try
                {
                    CancellationToken.ThrowIfCancellationRequested();

                    if (_observers.Any() && Client != null)
                    {
                        await Client.GetChatMessagesAsync(_lastChatItem, CancellationToken).ContinueWith(response =>
                        {
                            if (response.Status != TaskStatus.RanToCompletion) return;

                            var result = response.Result;

                            foreach (var chatMessage in result.Items.OrderBy(ci => ci.Time))
                            {
                                var item = new ChatModel(chatMessage);

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
                catch (TaskCanceledException)
                {
                    execute = false;
                }
            }
        }
    }
}