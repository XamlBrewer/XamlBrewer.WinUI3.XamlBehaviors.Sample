using CommunityToolkit.WinUI.UI.Behaviors;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace XamlBrewer.WinUI3.Behaviors
{
    public delegate void EventHandler<in TSender, in TArgs>(TSender sender, TArgs args);

    public class UserStoppedTypingBehavior : BehaviorBase<AutoSuggestBox>
    {
        private DispatcherTimer timer;

        public int DelayInMilliseconds { get; set; } = 3000;

        public int MinimumCharacters { get; set; } = 3;

        public event EventHandler<AutoSuggestBox, EventArgs> UserStoppedTyping;

        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(DelayInMilliseconds) };
            timer.Tick += Timer_Tick;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            timer.Tick -= Timer_Tick;
            base.OnDetaching();
        }

        private void AssociatedObject_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if ((args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) && (sender.Text.Length >= MinimumCharacters))
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            UserStoppedTyping?.Invoke(AssociatedObject, null);
            timer.Stop();
        }
    }
}
