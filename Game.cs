using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections;
using System.IO;
using HelloWorld;

namespace Hangman
{
	[Activity (Label = "Hangman")]			
	public class Game : Activity
	{

        public static String[] allPlayers;
        public TextView[] charView;
        public static int highscore;
        public static String currentPlayer;
        public static String log;
        public String[] QuestionsArray;
        public String[] AnswersArray;
        int i = 0;
        int score;
        int WrongCounter = 0;
        ImageView[] BodyElementsArray;
        GridView gridView;
        ArrayAdapter adapter;
        ArrayList AlphabetsArray;
        String CurrentAnswer;
        TextView ScoreView;
        TextView QuestionView;
        Button ButtonSubmit;
        LinearLayout CurrentAnswerLayout;


        protected override void OnCreate(Bundle bundle)
        {
            Data data = new Data();
            QuestionsArray = Data.QuestionsArr;
            AnswersArray = Data.AnswersArr;

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GameLayout);


            gridView = FindViewById<GridView>(Resource.Id.gridView);
            MakeKeyboard();
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,
                         AlphabetsArray);
            CurrentAnswerLayout = (LinearLayout)FindViewById(Resource.Id.CurrentAnswerLayout);
            QuestionView = (TextView)FindViewById(Resource.Id.question);

            //Setting adapter
            gridView.Adapter = adapter;
            gridView.ItemClick += gridView_ItemClick;
            CharacterSequenceView();

            //Submit button function
            ButtonSubmit = (Button)FindViewById(Resource.Id.ButtonSubmit);
            ButtonSubmit.Click +=delegate {
                SaveHighScoresAndProfiles();
                MainActivity.EditText.Text = "";
                Toast.MakeText(this, "Final Score: " + score, ToastLength.Long).Show();
                this.Finish();
            };

            QuestionView.Text = QuestionsArray[i];
            ScoreView = (TextView)FindViewById(Resource.Id.score);
            ScoreView.SetBackgroundColor(Android.Graphics.Color.Blue);

            HideMan();

        }

        //Function to save highscores and profiles in the local database

        public void SaveHighScoresAndProfiles()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = System.IO.Path.Combine(path, "newlog.txt");


            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(currentPlayer + ": " + highscore);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                allPlayers = new string[5];
                allPlayers = content.Split('\n');

            }
        }

        //Making body parts invisible in a new game
        private void HideMan()
        {
            BodyElementsArray = new ImageView[6];
            BodyElementsArray[0] = (ImageView)FindViewById(Resource.Id.head);
            BodyElementsArray[1] = (ImageView)FindViewById(Resource.Id.body);
            BodyElementsArray[2] = (ImageView)FindViewById(Resource.Id.left_arm);
            BodyElementsArray[3] = (ImageView)FindViewById(Resource.Id.right_arm);
            BodyElementsArray[4] = (ImageView)FindViewById(Resource.Id.left_leg);
            BodyElementsArray[5] = (ImageView)FindViewById(Resource.Id.right_leg);

            for (int element = 0; element < 6; element++)
            {
                BodyElementsArray[element].Visibility = ViewStates.Invisible;
            }

        }

        private void CharacterSequenceView()
        {
            charView = new TextView[AnswersArray[i].Length];

            for (int n = 0; n < AnswersArray[i].Length; n++)
            {
                charView[n] = new TextView(this);
                charView[n].Text = "" + AnswersArray[i][n];
                charView[n].SetTextColor(Android.Graphics.Color.White);
                CurrentAnswerLayout.AddView(charView[n]);
            }
        }

        void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            String c = AlphabetsArray[e.Position].ToString();
            AlphabetsArray[e.Position] = " ";

            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, 
                AlphabetsArray);

            //Setting adapter
            gridView.Adapter = adapter;

            if (AnswersArray[i].Contains(c))
            {
                CurrentAnswer = CurrentAnswer + c;

                for(int n = 0; n < AnswersArray[i].Length; n++)
                {
                    if (charView[n].Text == c)
                    {
                        charView[n].SetTextColor(Android.Graphics.Color.Blue);
                    }
                }

                if (AnswersArray[i].Length == CurrentAnswer.Length)
                {
                    MakeKeyboard();
                    adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, AlphabetsArray);
                    gridView.Adapter = adapter;
                    Toast.MakeText(this, "Well done", ToastLength.Short).Show();
                    Toast.MakeText(this, "The answer: " + AnswersArray[i], ToastLength.Long).Show();
                    i++;
                    score++;
                    ScoreView.Text = MainActivity.PlayerName+" SCORE: " + score.ToString();
                    currentPlayer = MainActivity.PlayerName;
                    highscore = score;
                    QuestionView.Text = QuestionsArray[i];
                    CurrentAnswer = "";
                    CurrentAnswerLayout.RemoveAllViews();
                    CharacterSequenceView();
                    WrongCounter = 0;
                    HideMan();
                }
            }
            else
            {
                BodyElementsArray[WrongCounter].Visibility = ViewStates.Visible;
                WrongCounter++;
                if (WrongCounter == 6)
                {
                    Toast.MakeText(this, "You got it wrong!", ToastLength.Short).Show();
                    Toast.MakeText(this, "Correct answer is: "+ AnswersArray[i], ToastLength.Long).Show();
                    Toast.MakeText(this, MainActivity.PlayerName+"'s"+" SCORE: " + score, ToastLength.Long).Show();
                    currentPlayer = MainActivity.PlayerName;
                    MainActivity.EditText.Text = "";
                    highscore = score;
                    SaveHighScoresAndProfiles();
                    this.Finish();
                    i++;
                }
            }

        }

        private void AddButtons(ArrayList arrayList, char alphabet)
        {
            arrayList.Add(alphabet);
        }

        //Adding data to the AlphabetsArray (GridView items)
        private void MakeKeyboard()
        {
            AlphabetsArray = new ArrayList();
            int l = 65;
            while (l < 91)
            {
                AddButtons(AlphabetsArray, Convert.ToChar(l));
                l++;
            }
        }
    }
}

