using System;

namespace Bebbs.Harmonize.Common
{
    public static class Actions
    {
        public static Action Combine(params Action[] actions)
        {
            return () =>
            {
                foreach(Action action in actions)
                {
                    action();
                }
            };
        }

        public static void Run(params Action[] actions)
        {
            Action action = Combine(actions);

            action();
        }
    }
}
