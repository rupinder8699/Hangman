using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using HelloWorld;

namespace Hangman
{
    [Activity(Label = "HighActivity")]
    public class Profiles : Activity
    {

        public TextView[] textViews;
        public Button[] buttons;

        int k;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Highscores);
            

            Button backButton = (Button)FindViewById(Resource.Id.backBtn);
            backButton.Click += delegate
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };

            //Recent profiles and highscores
            TextView RecentProfile_1 = (TextView)FindViewById(Resource.Id.highscore1);
            Button profilebutton1 = (Button)FindViewById(Resource.Id.profilebutton1);

            TextView RecentProfile_2 = (TextView)FindViewById(Resource.Id.highscore2);
            Button profilebutton2 = (Button)FindViewById(Resource.Id.profilebutton2);

            TextView RecentProfile_3 = (TextView)FindViewById(Resource.Id.highscore3);
            Button profilebutton3 = (Button)FindViewById(Resource.Id.profilebutton3);

            TextView RecentProfile_4 = (TextView)FindViewById(Resource.Id.highscore4);
            Button profilebutton4 = (Button)FindViewById(Resource.Id.profilebutton4);

            TextView RecentProfile_5 = (TextView)FindViewById(Resource.Id.highscore5);
            Button profilebutton5 = (Button)FindViewById(Resource.Id.profilebutton5);

            textViews = new TextView[] { RecentProfile_1, RecentProfile_2, RecentProfile_3, RecentProfile_4, RecentProfile_5 };
            buttons = new Button[] { profilebutton1, profilebutton2, profilebutton3, profilebutton4, profilebutton5 };

            k = 0;
            while (k < 5)
            {
                try
                {
                    textViews[k].Text = Game.allPlayers[k];

                    if (textViews[k].Text != "")
                    {
                        buttons[k].Visibility = Android.Views.ViewStates.Visible;
                    }
                }
                catch
                {

                }


                k++;
            }

            buttons[0].Click += delegate
            {
                ClickEvents(0);
            };

            buttons[1].Click += delegate
            {
                ClickEvents(1);
            };

            buttons[2].Click += delegate
            {
                ClickEvents(2);
            };

            buttons[3].Click += delegate
            {
                ClickEvents(3);
            };

            buttons[4].Click += delegate
            {
                ClickEvents(4);
            };

        }

        private void ClickEvents(int p)
        {
            MainActivity.PlayerName = Game.allPlayers[p];
            Intent newGame = new Intent(this, typeof(Game));
            StartActivity(newGame);
        }

    }
}
