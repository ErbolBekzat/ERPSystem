   M S B A M L   `   `   `  ����D  @ERPSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ERPSystem	ERPSystem  5   	       ERPSystem.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ERPSystem     5   	   A =Wpf.Ui, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5   	     � Title$  
MainWindow��5	   	   $	��720��6   $
��1280��6)    � WindowStyle$
 None=�5
   	      LeftMenuBackgroundBrush#��[  5   	    � WindowState$ 	Maximized=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   #��[  6   $��	10,5,10,0q�5       �� CornerRadius$ 5��5      ._�5      � .+�5      �� $��Right=�6    �� Orientation$ 
Horizontal=�69   .+�5      �� -   5        
�������$��25��5      $��25��6%   $��
0, 0, 0, 0q�60     ButtonWithoutBorder#��[ 5      .��5      �� $��/Icons/MinimizeIcon.png��6    5      �� 5       -   5        
�������$��25��5!      $��25��6%   $��10, 0, 0, 0q�60   #��[ 5"      $y�	Collapsed=�5#      .��5$      �� $��/Icons/MaximizeIcon.png��6    5%      �� 5'      -   5        
�������$��25��5(      $��25��6%   $��10, 0, 10, 0q�60   #��[ 5)      .��5*      �� $��/Icons/CloseIcon.png��6    5+      5-      5.      5/      � 52   
   $��1��6    � ColumnDefinitions 53      �� 54      $
��Auto �6#   �� 55      .+�58      �� -   5         ��Name$ NavigationGrid��58      $��0��6-   #��[  6=   $	��100��6s   $��
10,10,0,10q�59      $ 10��5:      ._�5;      � .+�5=      �� -   5        $ 	Navigator��5=      $��30��60   $	��100��6<   $��	0,140,0,0q�6H   
������ $	��Top=�6t   $��Center=�6�     	LineBrush#��[ 5>      $��0q�6>   ._�5?      � .+�5@      ��   BackgroundBrush#��[ 6(   $��20��6P   $��Right=�6[    �� RadiusX$ 10��6w   	 �� RadiusY$	 10��6�   5A      5B      �� 5D      -   5        
 ��Name$
 
UserButton��5D      $��Пользователи��5E      $��80��5F      $��30��5G      ���#CBF7ED$��0, 100, 0, 0q�5I      $	��Top=�5J      $��UserListView��5L      #��[ 5M      ��5K      �� *
NavCommand+5M      �� 5O      -   5        $
 ProjectButton��5O      $��Проекты��5P      $��80��5Q      $��30��5R      ���#CBF7ED$��0, 140, 0, 0q�5T      $	��Top=�5U      $��ProjectListView��5W      #��[ 5X      ��5V      �� *
NavCommand+5X      �� 5Z      -   5        $
 WorkerButton��5Z      $��Рабочие��5[      $��80��5\      $��30��5]      ���#CBF7ED$��0, 180, 0, 0q�5_      $	��Top=�5`      $��WorkerListView��5b      #��[ 5c      ��5a      �� *
NavCommand+5c      5e      5f      � 5i      $��1��6   $��	10,10,0,0q�5j      .+�5k      �� $ 10,0,0,0��6   ._�5l       � -	   5          �Name$ MainContent��5l      $��1��5m        � NavigationUIVisibility$ Hidden=�5n      5o      5q      5r      5s      5t      