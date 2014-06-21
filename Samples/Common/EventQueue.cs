using System.Collections.Concurrent;

namespace Common {
    public class EventQueue {
        ConcurrentQueue<Event> queue = new ConcurrentQueue<Event>();

        public void Post (Event ev) {
            queue.Enqueue(ev);
        }

        public Event Poll () {
            Event ev;
            if (queue.TryDequeue(out ev))
                return ev;

            return null;
        }
    }

    public enum EventType {
        Exit,
        Key,
        Mouse,
        Size
    }

    public class Event {
        public EventType Type {
            get;
            private set;
        }

        public Event (EventType type) {
            Type = type;
        }
    }

    public class SizeEvent : Event {
        public int Width {
            get;
            private set;
        }

        public int Height {
            get;
            private set;
        }

        public SizeEvent (int width, int height)
            : base(EventType.Size) {

            Width = width;
            Height = height;
        }
    }
}
