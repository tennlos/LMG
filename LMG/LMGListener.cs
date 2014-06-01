using Leap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMG
{
    public class LMGListener : Listener
    {
        private Object thisLock = new Object();
        public EventHandler<DirectionEventArgs> SwipeDetected;
        private Stopwatch stoper;


        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnInit(Controller controller)
        {
            SafeWriteLine("Initialized");
        }

        public override void OnConnect(Controller controller)
        {
            SafeWriteLine("Connected");
            controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            stoper = Stopwatch.StartNew();
        }

        public override void OnDisconnect(Controller controller)
        {
            SafeWriteLine("Disconnected");
        }

        public override void OnExit(Controller controller)
        {
            SafeWriteLine("Exited");
        }

        public override void OnFrame(Controller controller)
        {
            
            Frame frame = controller.Frame();
            GestureList list = frame.Gestures();
            if (frame.Gestures().Count > 0 && frame.Gestures().First().Type == Gesture.GestureType.TYPE_SWIPE)
            {
                SwipeGesture swipe = new SwipeGesture(frame.Gestures().First());

                var direction = DetermineDirection(swipe);
                //var speed = swipe.Speed;

                stoper.Stop();
                if (SwipeDetected != null && stoper.ElapsedMilliseconds > 500)
                {
                    stoper.Reset();
                    SwipeDetected(null, new DirectionEventArgs() { Direction = direction });
                }
                stoper.Start();
                //SafeWriteLine("Swipe Move detected. Direction: " + DetermineDirection(swipe) + ". Speed " + speed);

            }
        }

        private Direction DetermineDirection(SwipeGesture gesture)
        {
            var direction = gesture.Direction;
            if (Math.Abs(gesture.Direction.y) > Math.Abs(direction.x) && Math.Abs(gesture.Direction.y) > Math.Abs(direction.z) && gesture.Direction.y > 0)
                return Direction.Ignored;
            if (gesture.Direction.x < 0 && Math.Abs(direction.x) > Math.Abs(direction.z))
                return Direction.West;
            if (gesture.Direction.x >= 0 && Math.Abs(direction.x) > Math.Abs(direction.z))
                return Direction.East;
            if (gesture.Direction.z < 0 && Math.Abs(direction.z) > Math.Abs(direction.x))
                return Direction.North;
            if (gesture.Direction.z >= 0 && Math.Abs(direction.z) > Math.Abs(direction.x))
                return Direction.South;
            return Direction.South;
        }
    }

    public class DirectionEventArgs : EventArgs
    {
        public Direction Direction;
    }
}
