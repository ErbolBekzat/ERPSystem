using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ERPSystem.Models;
using System.Windows.Media.Media3D;

namespace ERPSystem.Views.UserView
{
    public class UserListViewModel : BindableBase
    {
        IUserService _userService;
        IRoleService _roleService;
        ICurrentUser _currentUser;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        readonly Brush background;
        readonly Brush primaryTaskBG;
        readonly Brush secondaryTaskBG;
        readonly Brush primaryRectBG;
        readonly Brush secondaryRectBG;
        readonly Brush lineColor;
        readonly Brush textColor;

        ScrollViewer userSV;
        Grid headerGrid;

        public UserListViewModel(IUserService userService, IRoleService roleService, ICurrentUser currentUser)
        {
            background = new SolidColorBrush((Color)Application.Current.Resources["BackgroundColor"]);
            primaryTaskBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryTaskBGColor"]);
            secondaryTaskBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryTaskBGColor"]);
            primaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryRectBGColor"]);
            secondaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryRectBGColor"]);
            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            _userService = userService;
            _roleService = roleService;
            _currentUser = currentUser;


            AddUserCommand = new RelayCommand(OnAddUser);
            AddRoleCommand = new RelayCommand(OnAddRole);
            ShowRolesCommand = new RelayCommand(OnShowRoles);
            UserInfoCommand = new RelayCommand<User>(OnUserInfo);
        }

        public void LoadUsers()
        {
            Users = new ObservableCollection<User>(_userService.GetAllUsers());

            //DisplayHeader();
            //DisplayUsers();
        }

        private void DisplayHeader()
        {
            headerGrid.Children.Clear();
            headerGrid.ColumnDefinitions.Clear();

            PropertyInfo[] properties = typeof(User).GetProperties().Skip(3).Take(typeof(User).GetProperties().Length - 4).ToArray();


            int i = 0;

            foreach (PropertyInfo property in properties)
            {
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    SharedSizeGroup = property.Name,
                });

                Border cellBorder = new Border();
                cellBorder.BorderBrush = lineColor;
                cellBorder.BorderThickness = new Thickness(.5);
                cellBorder.SetValue(Grid.ColumnProperty, i);

                TextBlock textBlock = new TextBlock();
                textBlock.Name = property.Name;
                textBlock.Text = property.Name;
                textBlock.Height = 29;
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.Foreground = textColor;
                textBlock.Margin = new Thickness(2, 0, 10, 0);
                cellBorder.Child = textBlock;

                headerGrid.Children.Add(cellBorder);

                i++;
            }
        }

        private void DisplayUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>(Users);

            Grid userGrid = new Grid();
            userGrid.Name = "UserGrid";
            userSV.Content = userGrid;

            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, lineColor));
            borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(.5)));

            Style textBlockStyle = new Style(typeof(TextBlock));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeights.Bold));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, 13.0));
            textBlockStyle.Setters.Add(new Setter(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0)));

            userGrid.Resources.Add(typeof(Border), borderStyle);
            userGrid.Resources.Add(typeof(TextBlock), textBlockStyle);

            int rowIndex = 0;

            foreach (User user in users)
            {
                PropertyInfo[] properties = user.GetType().GetProperties().Skip(3).Take(user.GetType().GetProperties().Length - 4).ToArray();

                userGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                int i = 0;

                foreach (PropertyInfo prop in properties)
                {
                    if (rowIndex == 0)
                    {
                        userGrid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            SharedSizeGroup = prop.Name,
                            Width = new GridLength(0, GridUnitType.Auto)
                        });
                    }

                    if (prop.Name == "UserRole")
                    {
                        Role userRole = _roleService.GetRoleById(user.UserRole);
                        Grid titleGrid = new Grid();
                        titleGrid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            SharedSizeGroup = "UserRoleButtonColumn"
                        });
                        titleGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        titleGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Auto);

                        Grid.SetColumn(titleGrid, i);
                        Grid.SetRow(titleGrid, rowIndex);

                        StackPanel titleButtons = new StackPanel
                        {
                            Orientation = Orientation.Horizontal
                        };
                        Grid.SetColumn(titleButtons, 0);
                        titleGrid.Children.Add(titleButtons);

                        Button infoButton = new Button();
                        infoButton.Content = "Info";
                        infoButton.Command = UserInfoCommand;
                        infoButton.CommandParameter = user;
                        infoButton.Foreground = textColor;
                        infoButton.BorderBrush = lineColor;
                        infoButton.Width = 30;
                        infoButton.HorizontalAlignment = HorizontalAlignment.Left;
                        infoButton.SetValue(Grid.ColumnProperty, 1);
                        titleButtons.Children.Add(infoButton);

                        Border border = new Border();
                        Grid.SetColumn(border, 1);

                        TextBlock firstName = new TextBlock();
                        firstName.Text = userRole.RoleName;
                        firstName.Height = 29;
                        firstName.HorizontalAlignment = HorizontalAlignment.Left;
                        firstName.Foreground = textColor;
                        firstName.Margin = new Thickness(5, 0, 15, 0);

                        border.Child = firstName;
                        titleGrid.Children.Add(border);
                        userGrid.Children.Add(titleGrid);

                    }
                    else
                    {
                        if (prop.GetValue(user) != null)
                        {
                            Border cellBorder = new Border();
                            cellBorder.SetValue(Grid.ColumnProperty, i);
                            cellBorder.SetValue(Grid.RowProperty, rowIndex);

                            TextBlock textBlock = new TextBlock();
                            textBlock.Name = prop.Name;
                            textBlock.Text = prop.GetValue(user).ToString();
                            textBlock.Height = 29;
                            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                            textBlock.Foreground = textColor;
                            cellBorder.Child = textBlock;


                            userGrid.Children.Add(cellBorder);
                        }
                        else
                        {
                            Border cellBorder = new Border();
                            cellBorder.SetValue(Grid.ColumnProperty, i);
                            cellBorder.SetValue(Grid.RowProperty, rowIndex);

                            userGrid.Children.Add(cellBorder);
                        }
                        
                    }

                    i++;
                } //foreach prop

                rowIndex++;

            } // foreach material
        }

        public RelayCommand AddUserCommand { get; private set; }
        
        public RelayCommand AddRoleCommand { get; private set; }
        public RelayCommand ShowRolesCommand { get; private set; }

        public RelayCommand<User> UserInfoCommand { get; private set; } 

        public event Action<User> AddUserRequested = delegate { };
        
        public event Action<Role> AddRoleRequested = delegate { };
        public event Action ShowRolesRequested = delegate { };

        public event Action<User> UserInfoRequested = delegate { };

        private void OnAddUser()
        {
            AddUserRequested(new User());
        }

        private void OnAddRole()
        {
            AddRoleRequested(new Role());
        }

        private void OnShowRoles()
        {
            ShowRolesRequested();
        }

        private void OnUserInfo(User user)
        {
            UserInfoRequested(user);
        }
    }
}
