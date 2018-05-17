namespace AdvancedTest.ViewModels.TestPart
{
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