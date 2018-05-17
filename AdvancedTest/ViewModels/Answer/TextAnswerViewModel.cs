namespace AdvancedTest.ViewModels.Answer
{
    public class TextAnswerViewModel : AnswerViewModel
    {
        public string Text { get; set; }

        public TextAnswerViewModel()
        {
            TextMode = true;
        }
    }
}
