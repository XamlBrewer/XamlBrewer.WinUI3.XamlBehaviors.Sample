using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using System;

namespace XamlBrewer.WinUI3.Behaviors
{
    // Use this if you prefer strongly typed events
    // public delegate void EventHandler<in TSender, in TArgs>(TSender sender, TArgs args);

    public class UserStoppedTypingBehavior : Behavior<AutoSuggestBox>
    {
        private DispatcherTimer timer;

        /// <summary>
        /// Gets or sets the waiting time threshold (in milliseconds).
        /// </summary>
        public int MinimumDelay { get; set; } = 3000;

        /// <summary>
        /// Gets or sets the search string threshold.
        /// </summary>
        public int MinimumCharacters { get; set; } = 3;

        // Use this if you prefer strongly typed events
        // public event EventHandler<AutoSuggestBox, EventArgs> UserStoppedTyping;

        public event EventHandler UserStoppedTyping;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(MinimumDelay) };
            timer.Tick += Timer_Tick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            timer.Tick -= Timer_Tick;
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
