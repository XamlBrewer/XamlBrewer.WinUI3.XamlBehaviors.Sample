// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.WebRequestMethods;

namespace XamlBrewer.WinUI3.XamlBehaviors.Sample.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            Loaded += HomePage_Loaded;
        }

        private List<string> Cats = new List<string>()
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

        private DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            // dinges.Focus(FocusState.Programmatic);
            // dinges.SelectAll();

            url.Text = "Find the blog posts at https://xamlbrewer.wordpress.com/ and the source code repos at https://github.com/XamlBrewer .";

            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            //Propose(ManualSuggestBox);
        }

        // C#
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

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 2)
                {
                    timer.Start();
                }
            }
            else
            {
                timer.Stop();
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Propose(sender);
        }




        private void Propose(AutoSuggestBox sender)
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

        private void AutoSuggestBox_UserStoppedTyping(AutoSuggestBox sender, EventArgs e)
        {
            Propose(sender);
        }

        //bool IsInViewport => ViewportBehavior.IsInViewport;
        //bool IsFullyInViewport => ViewportBehavior.IsFullyInViewport;
    }
}
