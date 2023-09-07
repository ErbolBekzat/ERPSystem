using ERPSystem.Models;
using ERPSystem.Views.TaskView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ERPSystem.Views.TaskView
{
    public partial class GanttRectangle : UserControl
    {
        readonly Brush primaryRectBG;
        readonly Brush secondaryRectBG;
        readonly Brush lineColor;

        Subtask subtask;
        

        public GanttRectangle()
        {
            InitializeComponent();

            primaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryRectBGColor"]);
            secondaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryRectBGColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            AddRelationshipAsParentCommand = new RelayCommand<string>(OnAddRelationshipAsParent);
            AddRelationshipAsChildCommand = new RelayCommand<string>(OnAddRelationshipAsChild);

            DataContext = this;
        }

        public void DrawChildRect(Subtask subtask, int topPoint, ObservableCollection<Models.Task> Tasks)
        {
            this.subtask = subtask;

            DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

            double taskStartPoint = (subtask.StartDate.Date - oldestStartDate.Date).Days + .5;

            double taskEndPoint = (subtask.EndDate.Date - subtask.StartDate.Date).Days;

            double taskCompletedPoint;

            leftButton.Margin = new Thickness(taskStartPoint * 30, 7.5, 0, 0);
            //leftButton.Style = Application.Current.FindResource("RoundButtonStyle") as Style;

            rightButton.Margin = new Thickness((taskStartPoint + taskEndPoint) * 30 - 15, 7.5, 0, 0);
            RightPopUp.HorizontalOffset = taskEndPoint;

            //rightButton.Style = Application.Current.FindResource("RoundButtonStyle") as Style;

            // Create the style for the border
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(7.5)));

            // Apply the style to the button's resources
            leftButton.Resources.Add(typeof(Border), borderStyle);
            rightButton.Resources.Add(typeof(Border), borderStyle);



            rectangle.Width = taskEndPoint * 30;
            rectangle.Height = 15;
            rectangle.Margin = new Thickness(taskStartPoint * 30, 17.5, 0, 0);

            if (subtask.CompletedDate != null)
            {
                DateTime completedDate = subtask.CompletedDate.GetValueOrDefault().Date;
                taskCompletedPoint = (completedDate.Date - subtask.StartDate.Date).Days;

                Brush completedMarkBrush;

                if (subtask.CompletedDate < subtask.EndDate) completedMarkBrush = (Brush)new BrushConverter().ConvertFrom("#0A9B3D");
                else if (subtask.CompletedDate > subtask.EndDate) completedMarkBrush = (Brush)new BrushConverter().ConvertFrom("#A30B37");
                else completedMarkBrush = lineColor;

                Line myLine = new Line();

                myLine.Stroke = completedMarkBrush;
                myLine.StrokeThickness = 3;
                myLine.X1 = taskCompletedPoint * 30 + 15;
                myLine.Y1 = topPoint * 30 + 5;
                myLine.X2 = taskCompletedPoint * 30 + 15;
                myLine.Y2 = topPoint * 30 + 25;

                EllipseGeometry ellipseGeometry = new EllipseGeometry();
                ellipseGeometry.Center = new Point(taskCompletedPoint * 30 + 15, topPoint * 30 + 15);
                ellipseGeometry.RadiusX = 6;
                ellipseGeometry.RadiusY = 6;

                Path ellipsePath = new Path();
                ellipsePath.Fill = completedMarkBrush;
                ellipsePath.Stroke = completedMarkBrush;
                ellipsePath.StrokeThickness = 0;
                ellipsePath.Data = ellipseGeometry;

                RectGrid.Children.Add(myLine);
                RectGrid.Children.Add(ellipsePath);
            }
        } // DrawChildRect

        public void DrawChildRectMonth(Subtask subtask, int topPoint, ObservableCollection<Models.Task> Tasks)
        {
            this.subtask = subtask;

            DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

            double taskStartPoint = ((subtask.StartDate.Year - oldestStartDate.Year) * 12) + subtask.StartDate.Month - oldestStartDate.Month;

            double taskEndPoint = ((subtask.EndDate.Year - subtask.StartDate.Year) * 12) + subtask.EndDate.Month - subtask.StartDate.Month;

            double startPointCoefficient = 0;
            double endPointCoefficient = 0;

            if (subtask.StartDate.Day <= 10)
            {
                startPointCoefficient = .25;
            }
            else if (subtask.StartDate.Day > 10 && subtask.StartDate.Day <= 20)
            {
                startPointCoefficient = .5;
            }
            else
            {
                startPointCoefficient = .75;
            }

            taskStartPoint += startPointCoefficient;

            if (subtask.EndDate.Day <= 10)
            {
                endPointCoefficient = .25;
            }
            else if (subtask.EndDate.Day > 10 && subtask.EndDate.Day <= 20)
            {
                endPointCoefficient = .5;
            }
            else
            {
                endPointCoefficient = .75;
            }

            taskEndPoint -= startPointCoefficient;
            taskEndPoint += endPointCoefficient;

            int width = 72;
            double rectEndPoint = taskEndPoint * width;

            

            double taskCompletedPoint;

            leftButton.Margin = new Thickness(taskStartPoint * width, 17.5, 0, 0);
            rightButton.Margin = new Thickness((taskStartPoint + taskEndPoint) * width - 15, 17.5, 0, 0);
            RightPopUp.HorizontalOffset = rectEndPoint;

            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(7.5)));
            leftButton.Resources.Add(typeof(Border), borderStyle);
            rightButton.Resources.Add(typeof(Border), borderStyle);

            rectangle.Width = rectEndPoint;
            rectangle.Height = 15;
            rectangle.Margin = new Thickness(taskStartPoint * width, 17.5, 0, 0);

            //if (subtask.CompletedDate != null)
            //{
            //    int completedMonths = ((subtask.CompletedDate.Value.Year - subtask.StartDate.Year) * 12) + subtask.CompletedDate.Value.Month - subtask.StartDate.Month;
            //    taskCompletedPoint = completedMonths;

            //    Brush completedMarkBrush;

            //    if (subtask.CompletedDate < subtask.EndDate)
            //        completedMarkBrush = (Brush)new BrushConverter().ConvertFrom("#0A9B3D");
            //    else if (subtask.CompletedDate > subtask.EndDate)
            //        completedMarkBrush = (Brush)new BrushConverter().ConvertFrom("#A30B37");
            //    else
            //        completedMarkBrush = lineColor;

            //    Line myLine = new Line();
            //    myLine.Stroke = completedMarkBrush;
            //    myLine.StrokeThickness = 3;
            //    myLine.X1 = taskCompletedPoint * width + 25;
            //    myLine.Y1 = topPoint * 50;
            //    myLine.X2 = taskCompletedPoint * width + 25;
            //    myLine.Y2 = topPoint * 50 + 15;

            //    EllipseGeometry ellipseGeometry = new EllipseGeometry();
            //    ellipseGeometry.Center = new Point(taskCompletedPoint * width + 25, topPoint * 50 + 15);
            //    ellipseGeometry.RadiusX = 6;
            //    ellipseGeometry.RadiusY = 6;

            //    Path ellipsePath = new Path();
            //    ellipsePath.Fill = completedMarkBrush;
            //    ellipsePath.Stroke = completedMarkBrush;
            //    ellipsePath.StrokeThickness = 0;
            //    ellipsePath.Data = ellipseGeometry;

            //    RectGrid.Children.Add(myLine);
            //    RectGrid.Children.Add(ellipsePath);
            //}
        }


        public RelayCommand<string> AddRelationshipAsParentCommand { get; set; }

        public event EventHandler<RelationshipEventArgs> AddRelationshipAsParentRequested;

        public void OnAddRelationshipAsParent(string relationship)
        {
            RightPopUp.IsOpen = !RightPopUp.IsOpen;

            rectangle.Fill = Brushes.Orange;

            SubtaskRelationship subtaskRelationship = new SubtaskRelationship();
            subtaskRelationship.ParentSubtaskId = subtask.Id;

            switch (relationship)
            {
                case "SF":
                    subtaskRelationship.RelationshipType = RelationshipType.SF;
                    break;
                case "FF":
                    subtaskRelationship.RelationshipType = RelationshipType.FF;
                    break;
                case "SS":
                    subtaskRelationship.RelationshipType |= RelationshipType.SS;
                    break;
                default:
                    subtaskRelationship.RelationshipType = RelationshipType.FS;
                    break;
            }

            var args = new RelationshipEventArgs
            {
                Relationship = subtaskRelationship,
                GanttRectangle = this
            };

            AddRelationshipAsParentRequested?.Invoke(this, args);
        }

        public RelayCommand<string> AddRelationshipAsChildCommand { get; set; }

        public event EventHandler<RelationshipEventArgs> AddRelationshipAsChildRequested;

        public void OnAddRelationshipAsChild(string relationship)
        {
            Brush childBrush = (Brush)new BrushConverter().ConvertFrom("#59C9A5");

            LeftPopUp.IsOpen = !LeftPopUp.IsOpen;

            rectangle.Fill = childBrush;

            SubtaskRelationship subtaskRelationship = new SubtaskRelationship();
            subtaskRelationship.ChildSubtaskId = subtask.Id;

            switch (relationship)
            {
                case "SF":
                    subtaskRelationship.RelationshipType = RelationshipType.SF;
                    break;
                case "FF":
                    subtaskRelationship.RelationshipType = RelationshipType.FF;
                    break;
                case "SS":
                    subtaskRelationship.RelationshipType |= RelationshipType.SS;
                    break;
                default:
                    subtaskRelationship.RelationshipType = RelationshipType.FS;
                    break;
            }

            var args = new RelationshipEventArgs
            {
                Relationship = subtaskRelationship,
                GanttRectangle = this
            };

            AddRelationshipAsChildRequested?.Invoke(this, args);
        }


        public event EventHandler<CheckEventArgs> Check;

        private void TogglePopupRightButton_Click(object sender, RoutedEventArgs e)
        {
            var args = new CheckEventArgs
            {
                Button = "Right",
                GanttRectangle = this
            };

            Check?.Invoke(this, args);
            //RightPopUp.IsOpen = !RightPopUp.IsOpen;
        }

        private void TogglePopupLeftButton_Click(object sender, RoutedEventArgs e)
        {
            var args = new CheckEventArgs
            {
                Button = "Left",
                GanttRectangle = this
            };

            Check?.Invoke(this, args);
            //LeftPopUp.IsOpen = !LeftPopUp.IsOpen;
        }

        public void CheckEventResponse(string button, bool openPopUp)
        {
            if (openPopUp)
            {
                if (button == "Right")
                {
                    RightPopUp.IsOpen = !RightPopUp.IsOpen;
                }
                else
                {
                    LeftPopUp.IsOpen = !LeftPopUp.IsOpen;
                }
            }
            else
            {
                if (button == "Left")
                {
                    Brush childBrush = (Brush)new BrushConverter().ConvertFrom("#59C9A5");

                    rectangle.Fill = childBrush;

                    SubtaskRelationship subtaskRelationship = new SubtaskRelationship();
                    subtaskRelationship.ChildSubtaskId = subtask.Id;

                    var args = new RelationshipEventArgs
                    {
                        Relationship = subtaskRelationship,
                        GanttRectangle = this
                    };

                    AddRelationshipAsChildRequested?.Invoke(this, args);
                }
                else
                {
                    rectangle.Fill = Brushes.Orange;

                    SubtaskRelationship subtaskRelationship = new SubtaskRelationship();
                    subtaskRelationship.ParentSubtaskId = subtask.Id;

                    var args = new RelationshipEventArgs
                    {
                        Relationship = subtaskRelationship,
                        GanttRectangle = this
                    };

                    AddRelationshipAsParentRequested?.Invoke(this, args);
                }
            }
        }

        public void ResetRectable()
        {
            rectangle.Fill = secondaryRectBG;
        }

        

        #region ButtonVisibilityControl
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            UpdateButtonVisibility(Visibility.Visible);
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsMouseOverButton())
            {
                UpdateButtonVisibility(Visibility.Hidden);
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            UpdateButtonVisibility(Visibility.Visible);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsMouseOverButton() && !IsMouseOverRectangle())
            {
                UpdateButtonVisibility(Visibility.Hidden);
            }
        }

        private bool IsMouseOverButton()
        {
            return leftButton.IsMouseOver || rightButton.IsMouseOver;
        }

        private bool IsMouseOverRectangle()
        {
            return rectangle.IsMouseOver;
        }

        private void UpdateButtonVisibility(Visibility visibility)
        {
            leftButton.Visibility = visibility;
            rightButton.Visibility = visibility;
        }
        #endregion ButtonVisibilityControl
    }

    public class CheckEventArgs : EventArgs
    {
        public string Button { get; set; }
        public GanttRectangle GanttRectangle { get; set; }
    }

    public class RelationshipEventArgs : EventArgs
    {
        public SubtaskRelationship Relationship { get; set; }
        public GanttRectangle GanttRectangle { get; set; }
    }

}
