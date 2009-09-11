using System;
using System.Windows.Input;
using DotNetNinjaQuizLib.Presenters;

namespace DotNetNinjaQuiz
{
    static class CommandRelay
    {
        public static void DoCommand(GameWindow gameWindow, KeyEventArgs e)
        {
            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.Alt:
                    break;
                case ModifierKeys.Control:
                    switch (e.Key)
                    {
                        case Key.B:
                            gameWindow.SetANewBackground();
                            break;   
                        case Key.N:
                            gameWindow.GetNewQuestion();
                            break;
                        case Key.R:
                            gameWindow.RandomizeQuestions();
                            break;
                        default:
                            break;
                    }
                    break;
                case ModifierKeys.None:
                    switch (e.Key)
                    {
                        case Key.A:
                            gameWindow.AnswerQuestion(AnswerCode.A);
                            break;
                        case Key.B:
                            gameWindow.AnswerQuestion(AnswerCode.B);
                            break;
                        case Key.C:
                            gameWindow.AnswerQuestion(AnswerCode.C);
                            break;
                        case Key.D:
                            gameWindow.AnswerQuestion(AnswerCode.D);
                            break;
                        case Key.Enter:
                            gameWindow.CommitAnswer();
                            break;
                        default:
                            break;
                    }
                    break;
                case ModifierKeys.Shift:
                    break;
                case ModifierKeys.Windows:
                    break;
                default:
                    break;
            }

        }
    }
}
