namespace AdvancedTest.Common.ViewModels.TestPart
{
    /// <summary>
    /// Модель представления для формы задания с ручным вводом ответа
    /// </summary>
    public class CustomTextTestPartViewModel : TestPartViewModelBase
    {
        private string _answer;

        public string CustomAnswer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged(nameof(CustomAnswer));
            }
        }

        public override string GetUserAnswer()
        {
            return _answer;
        }
    }
}