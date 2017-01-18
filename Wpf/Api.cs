using System;

namespace Cliver.CisteraNotification {
    public sealed class Api
    {
        static public void Initialize()
        {
            InformWindow.Initialize();
        }

        public static void ShowInform(string title, string text, string image_url, string action_name, Action action)
        {
            InformWindow.AddInform(title, text, image_url, action_name, action);
        }
    }
}
