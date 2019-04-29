using System;
namespace HelloWorld
{
    public class Data
    {
        public static String[] QuestionsArr;
        public static String[] AnswersArr;
        public Data()
        {
            QuestionsArr = new String[] { "FACEBOOK'S FOUNDER FIRST NAME?", "CAPITAL OF INDIA?", "LAST NAME OF WIKIPEDIA FOUNDER?", "FOUNDER OF OPENAI?", "DJANGO IS WRITTEN IN?" };

            AnswersArr = new String[]{
                "MARK",
            "DELHI",
            "WALES",
            "ELON",
            "PYTHON"};
        }
    }
}
