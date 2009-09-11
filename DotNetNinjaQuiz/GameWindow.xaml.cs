using System;
using System.Windows.Input;
using DotNetNinjaQuizLib.Presenters;
using DotNetNinjaQuiz.Controls;
using System.Windows.Media;

namespace DotNetNinjaQuiz
{
    public partial class GameWindow : System.Windows.Window, GameWindowView
    {
        public GameWindow()
        {
            InitializeComponent();
            new GameWindowController(this, GameOverControl, _progressLadder);
            TriggerChangeBackgroundRequest();
        }

        #region Key down event to view event
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.Control:
                    switch (e.Key)
                    {
                        case Key.B:
                            TriggerChangeBackgroundRequest();
                            break;
                        case Key.N:
                            TriggerNewQuestion();
                            break;
                    }
                    break;
                case ModifierKeys.None:
                    switch (e.Key)
                    {
                        case Key.A:
                            TriggerAnswerSelected(AnswerCode.A);
                            break;
                        case Key.B:
                            TriggerAnswerSelected(AnswerCode.B);
                            break;
                        case Key.C:
                            TriggerAnswerSelected(AnswerCode.C);
                            break;
                        case Key.D:
                            TriggerAnswerSelected(AnswerCode.D);
                            break;
                        case Key.Enter:
                            TriggerAnswerCommitted();
                            break;
                    }
                    break;                
            }
        }
        #endregion

        #region GameWindowView Members

        public event EventHandler NewQuestion;
        public event EventHandler<AnswerSelectedEventArgs> AnswerSelected;
        public event EventHandler AnswerCommitted;
        public event EventHandler ChangeBackgroundRequest;

        public virtual void TriggerNewQuestion()
        {
            if (NewQuestion != null)
                NewQuestion(null, EventArgs.Empty);
        }

        private void TriggerAnswerSelected(AnswerCode answer)
        {
            if (AnswerSelected != null)
                AnswerSelected(null, new AnswerSelectedEventArgs { SelectedAnswer = answer });
        }

        public virtual void TriggerAnswerCommitted()
        {
            if (AnswerCommitted != null)
                AnswerCommitted(null, EventArgs.Empty);
        }
        
        public virtual void TriggerChangeBackgroundRequest()
        {
            if (ChangeBackgroundRequest != null)
                ChangeBackgroundRequest(null, EventArgs.Empty);
        }

        public AnswerButton Button(AnswerCode answerCode)
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

        public QuestionBox QuestionBox
        {
            get { return _questionBox; }
        }

        public Brush BackgroundImage
        {
            set
            {
                Background = value;
            }
        }

        #endregion
    }
}
