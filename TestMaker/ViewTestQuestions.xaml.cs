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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestMaker
{
    /// <summary>
    /// Interaction logic for ViewTestQuestions.xaml
    /// </summary>
    public partial class ViewTestQuestions : Page
    {
        public ViewTestQuestions()
        {
            InitializeComponent();
        }

        public ViewTestQuestions(object data) : this()
        {
            this.DataContext = data;
            List<Question> questions = this.DataContext as List<Question>;
            int row = 1;
            for(int i = 0; i < questions.Count(); i++) 
            {
                RowDefinition rowD = new RowDefinition();
                RowDefinition rowD2 = new RowDefinition();
                questionsGrid.RowDefinitions.Add(rowD);
                questionsGrid.RowDefinitions.Add(rowD2);
                Question question = questions[i];
                Label label = new Label();
                label.Content = question.Number + ". " + question.QuestionText;
                Grid.SetRow(label, row++);
                Grid.SetColumn(label, 0);
                questionsGrid.Children.Add(label);
                DockPanel dockPanel = new DockPanel();
                Grid.SetRow(dockPanel, row++);
                Grid.SetColumn(dockPanel, 0);
                for (int j = 0; j < question.Answers.Count(); j++)
                {                    
                    CheckBox checkBox = new CheckBox();
                    checkBox.Margin = new Thickness(10, 30, 10, 10);
                    checkBox.Content = question.Answers[j].Number + ") " + question.Answers[j].AnswerText;
                    dockPanel.Children.Add(checkBox);
                }
                questionsGrid.Children.Add(dockPanel);
            }
            RowDefinition rowD3 = new RowDefinition();
            rowD3.Height = new GridLength(100);
            questionsGrid.RowDefinitions.Add(rowD3);
            Button button = new Button();
            button.Click += SaveFile;
            button.Content = "Export To Pdf";
            button.Margin = new Thickness(10, 10, 10, 10);
            Grid.SetRow(button, row);
            Grid.SetColumn(button, 0);
            questionsGrid.Children.Add(button);
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                FixedDocument doc = new FixedDocument();
                doc.DocumentPaginator.PageSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);

                List<Question> questions = this.DataContext as List<Question>;
                for (int p = 0; p <= questions.Count() / 17; p++)
                {
                    FixedPage fxpg = new FixedPage();
                    Grid grid = new Grid();
                    if (p==0)
                    {
                        RowDefinition titleRow = new RowDefinition();
                        grid.RowDefinitions.Add(titleRow);
                        Label label = new Label();
                        label.Content = titleBox.Text;
                        Grid.SetRow(label, 0);
                        Grid.SetColumn(label, 0);
                        grid.Children.Add(label);
                    }
                    fxpg.Width = doc.DocumentPaginator.PageSize.Width;
                    fxpg.Height = doc.DocumentPaginator.PageSize.Height;
                    
                    grid.Margin = new Thickness(20, 20, 20, 20); 
                    int row = 1;
                    for (int i = 0; i < 17; i++)
                    {
                        RowDefinition rowD = new RowDefinition();
                        RowDefinition rowD2 = new RowDefinition();
                        grid.RowDefinitions.Add(rowD);
                        grid.RowDefinitions.Add(rowD2);
                        int currentElement = i + p * 17;
                        if (currentElement >= questions.Count())
                        {
                            break;
                        }
                        Question question = questions[i+p*17];
                        Label label = new Label();
                        label.Content = question.Number + ". " + question.QuestionText;
                        Grid.SetRow(label, row++);
                        Grid.SetColumn(label, 0);
                        grid.Children.Add(label);
                        DockPanel dockPanel = new DockPanel();
                        Grid.SetRow(dockPanel, row++);
                        Grid.SetColumn(dockPanel, 0);
                        for (int j = 0; j < question.Answers.Count(); j++)
                        {
                            CheckBox checkBox = new CheckBox();
                            checkBox.Margin = new Thickness(10, 10, 10, 10);
                            checkBox.Content = question.Answers[j].Number + ") " + question.Answers[j].AnswerText;
                            dockPanel.Children.Add(checkBox);
                        }
                        grid.Children.Add(dockPanel);
                    }
                    fxpg.HorizontalAlignment = HorizontalAlignment.Center;

                    fxpg.Children.Add(grid);
                    PageContent pc = new PageContent();
                    pc.HorizontalAlignment = HorizontalAlignment.Center;
                   
                    ((IAddChild)pc).AddChild(fxpg);
                    doc.Pages.Add(pc);
                }
               
                pd.PrintDocument(doc.DocumentPaginator, "Print Job Name");
            }
        }
    }
}
