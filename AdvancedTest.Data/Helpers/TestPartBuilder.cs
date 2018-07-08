using System.Collections.Generic;
using AdvancedTest.Data.Enum;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Data.Helpers
{
    public class TestPartBuilder
    {

        private TheoryTestPart _source;

        public TestPartBuilder()
        {
            Intialize();
        }

        public static TestPartBuilder New(TestPartType testPartType = TestPartType.SingleChoice)
        {
            TestPartBuilder builder = new TestPartBuilder();
            return builder.CurrentType(testPartType);
        }

        public TestPartBuilder Description(string description)
        {
            _source.Description = description;
            return this;
        }

        public TestPartBuilder AddAnswer(string text, int seq, string options = null)
        {
            _source.Answers.Add(new TheoryTestPartAnswer
            {
                AnswerNumber = seq,
                Text = text,
                TheoryTestPart = _source,
                Options = options
            });

            return this;
        }

        public TestPartBuilder Answers(params string[] answers)
        {
            int seq = 1;
            foreach (string answer in answers)
            {
                _source.Answers.Add(new TheoryTestPartAnswer
                {
                    AnswerNumber = seq,
                    Text = answer,
                    TheoryTestPart = _source
                });
            }

            return this;
        }

        public TestPartBuilder CurrentType(TestPartType testPartType)
        {
            _source.TestType = testPartType;
            return this;
        }

        public TestPartBuilder Seq(int seq)
        {
            _source.Seq = seq;
            return this;
        }

        public TestPartBuilder CorrectAnswer(string answer)
        {
            _source.CorrectAnswer = answer;
            return this;
        }

        public TheoryTestPart Build(TheoryPart theory)
        {
            var theoryTestPart = CopyHelper.DeepCopy(_source);
            theoryTestPart.Seq = theory.TheoryTestParts.Count + 1;
            theoryTestPart.TheoryPart = theory;
            theory.TheoryTestParts.Add(theoryTestPart);
            return theoryTestPart;
        }

        private void Intialize()
        {
            _source = null;
            _source = new TheoryTestPart
            {
                Answers = new List<TheoryTestPartAnswer>()
            };
        }
    }
}
