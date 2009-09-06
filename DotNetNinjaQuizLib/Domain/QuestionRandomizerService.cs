using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNinjaQuizLib.Persistance;
using System.Collections;

namespace DotNetNinjaQuizLib.Domain
{
    public class QuestionRandomizerService
    {
        public void ShuffleAllQuestions(IQuestionRepository repoitory)
        {
            var allQuestions = repoitory.GetAllQuestions();
            ShuffleQuestions(allQuestions);

            foreach (var question in allQuestions)
            {
                repoitory.SaveQuestion(question);
            }
        }

        private void ShuffleQuestions(IEnumerable<QuizQuestion> questions)
        {
            Random random = new Random();

            foreach (var question in questions)
            {
                question.ShuffleIndex = random.Next(100);                
            }            
        }

        
    }
}
