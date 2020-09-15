using System;
using System.Collections.Generic;
using System.Text;

namespace PatternTools.Mediator
{
    public class Messenger<TMessage>
    {
        private static Messenger<TMessage> _instance;

        private Action<TMessage> _broadcast;
        private Dictionary<string, Action<TMessage>> _boxes;

        public static Messenger<TMessage> Instance
        {
            get
            {
                return _instance ?? (_instance = new Messenger<TMessage>());
            }
        }

        protected Messenger()
        {
            _boxes = new Dictionary<string, Action<TMessage>>();
        }

        public void Register(Action<TMessage> action)
        {
            _broadcast += action;
        }

        public void Register(string box, Action<TMessage> action)
        {
            if (!_boxes.ContainsKey(box))
                _boxes.Add(box, null);

            _boxes[box] += action;
        }

        public void Unregister(Action<TMessage> action)
        {
            _broadcast -= action;
        }

        public void Unregister(string box, Action<TMessage> action)
        {
            if (!_boxes.ContainsKey(box))
                throw new InvalidOperationException("The box does'nt exists!");

            _boxes[box] -= action;
        }

        public void Send(TMessage message)
        {
            _broadcast?.Invoke(message);
        }

        public void Send(string box, TMessage message)
        {
            if (!_boxes.ContainsKey(box))
                throw new InvalidOperationException("The box does'nt exists!");

            _boxes[box]?.Invoke(message);
        }
    }
}