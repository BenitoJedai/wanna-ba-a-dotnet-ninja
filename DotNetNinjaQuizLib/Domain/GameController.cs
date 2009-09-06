﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNinjaQuizLib.Factories;
using DotNetNinjaQuizLib.Persistance;
using DotNetNinjaQuizLib.Presenters;

namespace DotNetNinjaQuizLib.Domain
{
    public class GameController : IDisposable
    {
        #region Fields
        private SortedList<int, GameLevel> _gameLevels;
        private IQuestionRepository _repository;
        private int _activeLevelKey;
        #endregion

        #region Properties
        public SortedList<int, GameLevel> GameLevels
        {
            get
            {
                return _gameLevels;
            }
        }

        public IQuestionRepository QuestionRepository 
        { 
            get 
            { 
                return _repository; 
            } 
        }


        public GameLevel CurrentLevel
        {
            get
            {
                return this.GameLevels[_activeLevelKey];
            }
        }

        public QuestionPresenter CurrentQuestion
        {
            get;
            private set;
        }
        #endregion

        #region Public methods
        public QuestionPresenter GetNewQuestion(GameLevel level)
        {
            CurrentQuestion = new QuestionPresenter(_repository.GetNextQuestion(level));
            return CurrentQuestion;
        }

        public CommitAnswerResult CommitAnswer(AnswerCode answer)
        {
            CommitAnswerResult answerResult = CurrentQuestion.CommitAnswer(answer, this);

            if (answerResult.WasAnswerCorrect)
            {
                SetActiveLevel(_activeLevelKey + 1);
            }
            else
            {
                //TODO: stop the game
            }

            return answerResult;
        }
        #endregion

        #region Constructors
        public GameController(string questionsDatabasePath)
        {
            _repository = QuestionRepositoryFactory.CreateObjectDatabaseRespository(questionsDatabasePath); 
            
            _gameLevels = GameLevelFactory.CreateDefaultGameLevels();
            SetActiveLevel(1);
        }
        #endregion

        #region Private methods
        private void SetActiveLevel(int levelKey)
        {
            _activeLevelKey = levelKey;

            //TODO: make sure key exists...
            // When player reaches the top of the stack, this must be handled somewhere...

            _gameLevels[levelKey].IsActive = true;

            int previousLevelkey = levelKey - 1;

            if (previousLevelkey > 0)
            {
                _gameLevels[previousLevelkey].IsCompleted = true;
            }
        }
        #endregion

        #region IDisposable Members

        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            _isDisposed = true;

            if (disposing)
            {
                if (_repository != null)
                {
                    _repository.Dispose();
                }
            }
        }

        #endregion
    }
}
