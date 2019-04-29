using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using HelloWorld;

namespace Hangman
{
	[Activity (Label = "Hangman", MainLauncher = true)]
	public class MainActivity : Activity
	{
        public static EditText EditText;
        public static String PlayerName;
        Button HighScoreButton;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainMenu);
           
			var buttonNewGame = FindViewById<Button> (Resource.Id.Button_NewGame);
            var buttonExit = FindViewById<Button>(Resource.Id.Button_Exit);
            var HighScoreButton = (Button)FindViewById(Resource.Id.Button_HighScores);

            //new game function
            buttonNewGame.Click += (sender, e) => {
                EditText = (EditText)FindViewById(Resource.Id.player);
                PlayerName = (EditText.Text).ToString();
                StartActivity(new Intent(this, typeof(Game)));
			};

            //Exit button
			buttonExit.Click += (sender, e) => {
				this.Finish();
			};


            //Profiles button           
            HighScoreButton.Click += delegate {
                StartActivity(new Intent(this, typeof(Profiles)));
            };

        }
	}
}


