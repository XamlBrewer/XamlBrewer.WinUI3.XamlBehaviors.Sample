using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XamlBrewer.WinUI3.XamlBehaviors.Sample.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            Loaded += HomePage_Loaded;
            Unloaded += HomePage_Unloaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            url.Text = "Please find the blog post at https://xamlbrewer.wordpress.com/ and the source code repo at https://github.com/XamlBrewer .";

            timer.Tick += Timer_Tick;
        }

        private void HomePage_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Tick -= Timer_Tick;
        }

        #region Viewport
        // bool IsInViewport => ViewportBehavior.IsInViewport;
        // bool IsFullyInViewport => ViewportBehavior.IsFullyInViewport;

        private void Element_EnteredViewport(object sender, EventArgs e)
        {
            Status.Text = "I'm in";
        }

        private void Element_EnteringViewport(object sender, EventArgs e)
        {
            Status.Text = "I'm entering";
        }

        private void Element_ExitedViewport(object sender, EventArgs e)
        {
            Status.Text = "I'm out";
        }

        private void Element_ExitingViewport(object sender, EventArgs e)
        {
            Status.Text = "I'm leaving";
        }
        #endregion

        #region UserStoppedTyping
        private readonly List<string> Cats = new()
        {
            "Abyssinian",
            "Aegean",
            "American Bobtail",
            "American Curl",
            "American Ringtail",
            "American Shorthair",
            "American Wirehair",
            "Aphrodite Giant",
            "Arabian Mau",
            "Asian cat",
            "Asian Semi-longhair",
            "Australian Mist",
            "Balinese",
            "Bambino",
            "Bengal",
            "Birman",
            "Brazilian Shorthair",
            "British Longhair",
            "British Shorthair",
            "Burmese",
            "Burmilla",
            "California Spangled",
            "Chantilly-Tiffany",
            "Chartreux",
            "Chausie",
            "Colorpoint Shorthair",
            "Cornish Rex",
            "Cymric",
            "Cyprus",
            "Devon Rex",
            "Donskoy",
            "Dragon Li",
            "Dwelf",
            "Egyptian Mau",
            "European Shorthair",
            "Exotic Shorthair",
            "Foldex",
            "German Rex",
            "Havana Brown",
            "Highlander",
            "Himalayan",
            "Japanese Bobtail",
            "Javanese",
            "Kanaani",
            "Khao Manee",
            "Kinkalow",
            "Korat",
            "Korean Bobtail",
            "Korn Ja",
            "Kurilian Bobtail",
            "Lambkin",
            "LaPerm",
            "Lykoi",
            "Maine Coon",
            "Manx",
            "Mekong Bobtail",
            "Minskin",
            "Napoleon",
            "Munchkin",
            "Nebelung",
            "Norwegian Forest Cat",
            "Ocicat",
            "Ojos Azules",
            "Oregon Rex",
            "Persian (modern)",
            "Persian (traditional)",
            "Peterbald",
            "Pixie-bob",
            "Ragamuffin",
            "Ragdoll",
            "Raas",
            "Russian Blue",
            "Russian White",
            "Sam Sawet",
            "Savannah",
            "Scottish Fold",
            "Selkirk Rex",
            "Serengeti",
            "Serrade Petit",
            "Siamese",
            "Siberian or´Siberian Forest Cat",
            "Singapura",
            "Snowshoe",
            "Sokoke",
            "Somali",
            "Sphynx",
            "Suphalak",
            "Thai",
            "Thai Lilac",
            "Tonkinese",
            "Toyger",
            "Turkish Angora",
            "Turkish Van",
            "Turkish Vankedisi",
            "Ukrainian Levkoy",
            "Wila Krungthep",
            "York Chocolate"
        };

        private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(3) };

        private void Timer_Tick(object sender, object e)
        {
            DisplaySuggestions(ManualSuggestBox);
            timer.Stop();
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if ((args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) && (sender.Text.Length >= 3))
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            DisplaySuggestions(sender);
        }

        private void AutoSuggestBox_UserStoppedTyping(object sender, EventArgs e)
        {
            DisplaySuggestions(sender as AutoSuggestBox);
        }

        private void DisplaySuggestions(AutoSuggestBox sender)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var cat in Cats)
            {
                var found = splitText.All((key) =>
                {
                    return cat.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(cat);
                }
            }

            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }

            sender.ItemsSource = suitableItems;
        }
        #endregion
    }
}
