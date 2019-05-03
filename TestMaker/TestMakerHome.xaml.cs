using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestMaker
{
    /// <summary>
    /// Interaction logic for TestMakerHome.xaml
    /// </summary>
    public partial class TestMakerHome : Page
    {
        List<Question> questions;
        public TestMakerHome()
        {
            questions = new List<Question>();
            InitializeComponent();
            Int32 numberOfAnswers = 4;
            answersDataGrid.IsReadOnly = false;
            answersDataGrid.CanUserAddRows = false;
            answersDataGrid.CanUserDeleteRows = false;
            answersDataGrid.CanUserReorderColumns = false;
            answersDataGrid.CanUserResizeRows = false;
            this.fillDataGrid(numberOfAnswers);
            
        }
        private void fillDataGrid(Int32 numberOfAnswers)
        {
            List<Answer> answers = new List<Answer>();
            for (int i = 1; i <= numberOfAnswers; i++)
            {
                Answer answer = new Answer();
                answer.Number = i;
                answer.AnswerText = "Answer" + i.ToString();
                answers.Add(answer);
            }
            answersDataGrid.ItemsSource = answers;
        }

        private void RefreshTable(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            if (item != null && item.Content != null) {
                String s = item.Content.ToString();
                if (s != null)
                {
                    Int32 numberOfAnswers = Int32.Parse(s);
                    this.fillDataGrid(numberOfAnswers);
                }
            }
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            Question question = new Question();
            if(questionTextBox.Text == null || questionTextBox.Text.Equals(""))
            {
                questionTextBox.BorderBrush = Brushes.IndianRed;
                MessageBox.Show("Напиши въпрос!");
                return;
            }
            question.Number = questions.Count() + 1;
            question.QuestionText = questionTextBox.Text;
            List<Answer> answers = new List<Answer>();
            for (int i = 0; i < answersDataGrid.Items.Count; i++)
            {
                Answer answer = answersDataGrid.ItemContainerGenerator.Items[i] as Answer;
                answers.Add(answer);
            }
            question.Answers = answers;
            questions.Add(question);
            questionCount.Text = "Добавените въпроси са " + questions.Count().ToString();
        }

        private void ViewAddedQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            if(questions.Count() == 0)
            {
                MessageBox.Show("Добави поне 1 Въпрос");
                return;
            }
            ViewTestQuestions view = new ViewTestQuestions(questions);
            this.NavigationService.Navigate(view);
        }

        private void questionCount_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
