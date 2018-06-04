namespace AdvancedTest.ViewModels.Answer
{
    /// <summary>
    /// Модель представления для вариантов ответа тестового задания типа строка
    /// </summary>
    public class TextAnswerViewModel : AnswerViewModel
    {
        // Текст ответа
        public string Text { get; set; }

        public TextAnswerViewModel()
        {
            TextMode = true;
        }
    }
}
