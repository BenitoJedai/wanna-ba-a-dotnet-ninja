﻿using System;
using System.Windows;
using System.Windows.Input;
using DotNetNinjaQuizLib.Domain;
using DotNetNinjaQuizLib.Presenters;
using DotNetNinjaQuiz.Controls;

namespace DotNetNinjaQuiz
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        #region Fields
        private bool _answerCommitted;
        #endregion

        #region Constructors
        public GameWindow()
        {
            InitializeComponent();

            SetANewBackground();
        }
        #endregion

        #region Events
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CommandRelay.DoCommand(this, e);            
        }
        #endregion
        #region Private methods
        public void RandomizeQuestions()
        {
            MessageBox.Show("Going to randimize all questions");

            QuestionRandomizerService randomizer = new QuestionRandomizerService();
            randomizer.ShuffleAllQuestions(ServiceLocator.Game.QuestionRepository);

            MessageBox.Show("Randimization done!");
        }

        public void SetANewBackground()
        {
            this.Background = ServiceLocator.Images.GetNextBackgroundImage();
        }

        public void GetNewQuestion()
        {
            if (ServiceLocator.Game.GameOver)
            {
                GameOverControl.Hide();
                ServiceLocator.CreateNewGame();
                _answerGivenByUser = AnswerCode.AnswerNotGiven;
                _answerCommitted = false;
            }

            if ((ServiceLocator.Game.CurrentQuestion != null
                && _answerGivenByUser == AnswerCode.AnswerNotGiven) 
                || ServiceLocator.Game.GameOver)
            {
                return; // Don't provide new question if a question is already active
                        // But no need to tell the user...
            }

            var game = ServiceLocator.Game;

            _progressLadder.AdvanceLadder();

            var questionPresenter = game.GetNewQuestion(game.CurrentLevel);

            _answerGivenByUser = AnswerCode.AnswerNotGiven;

            SetQuestionAndAnswersTexts(questionPresenter);
            ServiceLocator.Sound.PlayEffect(gfx.SoundEffect.NewQuestion);

            _answerCommitted = false;
        }

        private void SetQuestionAndAnswersTexts(QuestionPresenter questionPresenter)
        {
            _questionBox.QuestionText = questionPresenter.QuestionText;
            _answerAButton
                .SetState(AnswerButtonState.Normal)
                .AnswerText = "A: " + questionPresenter.AnswerA;
            _answerBButton
                .SetState(AnswerButtonState.Normal)
                .AnswerText = "B: " + questionPresenter.AnswerB;
            _answerCButton
                .SetState(AnswerButtonState.Normal)
                .AnswerText = "C: " + questionPresenter.AnswerC;
            _answerDButton
                .SetState(AnswerButtonState.Normal)
                .AnswerText = "D: " + questionPresenter.AnswerD;
        }
        private AnswerButton GetButton(AnswerCode answerCode)
        {
            switch (answerCode)
            {
                case AnswerCode.A:
                    return _answerAButton;
                case AnswerCode.B:
                    return _answerBButton;
                case AnswerCode.C:
                    return _answerCButton;
                case AnswerCode.D:
                    return _answerDButton;                
                default:
                    throw new ApplicationException("Unknown answer code..");
            }
        }

        private AnswerCode _answerGivenByUser = AnswerCode.AnswerNotGiven;

        public void AnswerQuestion(AnswerCode answerCode)
        {
            ResetAllAnswers();

            _answerGivenByUser = answerCode;
            GetButton(answerCode).SetState(AnswerButtonState.Selected);
            ServiceLocator.Sound.PlayEffect(gfx.SoundEffect.ButtonPress);
        }

        private void ResetAllAnswers()
        {
            GetButton(AnswerCode.A).SetState(AnswerButtonState.Normal);
            GetButton(AnswerCode.B).SetState(AnswerButtonState.Normal);
            GetButton(AnswerCode.C).SetState(AnswerButtonState.Normal);
            GetButton(AnswerCode.D).SetState(AnswerButtonState.Normal);
        }

        public void CommitAnswer()
        {
            var currentQuestion = ServiceLocator.Game.CurrentQuestion;
            if (currentQuestion != null
                && _answerGivenByUser != AnswerCode.AnswerNotGiven
                && !_answerCommitted)
            {
                _answerCommitted = true;
                var commitAnswerResult = ServiceLocator.Game.CommitAnswer(_answerGivenByUser);

                if (commitAnswerResult.WasAnswerCorrect)
                {
                    GetButton(commitAnswerResult.UserAnswer).SetState(AnswerButtonState.CorrectAnswer);
                    ServiceLocator.Sound.PlayEffect(gfx.SoundEffect.QuestionCommitted);
                }
                else
                {
                    GetButton(commitAnswerResult.UserAnswer).SetState(AnswerButtonState.WrongAnswer);
                    GetButton(commitAnswerResult.CorrectAnswer).SetState(AnswerButtonState.CorrectAnswer);
                }

                if (ServiceLocator.Game.GameOver)
                {
                    if(ServiceLocator.Game.CurrentLevel.DifficultySelector.GetLevel() == DifficultyLevel.Easy)
                        ServiceLocator.Sound.PlayEffect(gfx.SoundEffect.Laughter);
                    else
                        ServiceLocator.Sound.PlayEffect(gfx.SoundEffect.Applause);

                    GameOverControl.Show(ServiceLocator.Game.CurrentLevel.Label);
                }
            }
        }

        private void GameOverControl_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            GameOverControl.Hide();
        }  
        #endregion
    }
}
