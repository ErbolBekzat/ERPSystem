   M S B A M L   `   `   `  ����D  @ERPSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ERPSystem.Views.TaskViewERPSystem.Views.TaskView  5      ;"clr-namespace:ERPSystem.ConvertersERPSystem.Converters  5      ,    &ERPSystem.Views.TaskView.TaskGanttView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:ERPSystem.Views.TaskView     5      9
converters"clr-namespace:ERPSystem.Converters     5        � 	Resources  5      @   :ERPSystem.Converters.NegatableBooleanToVisibilityConverter )  %NegatableBooleanToVisibilityConverter��%   &	          5   
   6;   5      .��5      � $��0,0,0,0q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6     BackgroundBrush#��[ 6   $��5,0,5,5q�5       �� CornerRadius$ 5��5      ._�5      �  � ColumnDefinitions 5      �� 5      $	��50* �6'   �� 5      $	��50* �6'   .+�5      � $��0��6    5      �� 5       �� 5!      .+�5$      �� $��0��6!    �� Orientation$ 
Horizontal=�61   .+�5%      �� -   5         ��Name$ BackBtn��5%   !   $��
Назад��5&   !   $��60��61   $��25��6<     	TextBrush#��[ 6H   $��10q�5(   !   $
��Left=�6-   ��5'   !   �� *BackToBlockListCommand+5(   -   �� 5*      -   5        $ 
AddTaskBtn��5*   !   $	��Add��5+   !   $��60��6/   $��25��6:   #��[ 6F   $��10q�5-   !   ��5,   !   �� *AddTaskCommand+5-   !   5.      �� 51      $��1��6!   $ 
Horizontal=�61   $��Right=�6J   .+�52      �� -   5         ��Name$ ProjectTextBlock��52   $   $��Project��6>   #��[ 53   $   $��14(�54   $   $
��Bold"�55   $   $
��10,5q�56   $   $��Center=�57   $   �� 58      -   5        $ BlockTextBlock��58   $   $��Block��6<   #��[ 59   $   $��14(�5:   $   $
��Bold"�5;   $   $
��10,5q�5<   $   $��Center=�5=   $   5>      5?      � 5B      -   5         �Name$ RectangleButtons��5B      $��1��61   $y�	Collapsed=�6A    5C      �� 5D      $	��50* �6+   �� 5E      $	��50* �6+   .+�5H      �� $��0��6   $
��Save��6-   $��60��6<   $��25��6G   #��[ 6S   ��5I      �� * SaveSubtaskRelationshipCommand+�� 5K      $��1��6   $��Cancel��6-   $��60��6>   $��25��6I   #��[ 6U   ��5L      �� *(&CancelSubtaskRelationshipAddingCommand+5M      5N      5O      � 5R   
   -   5        $ ContentGrid��5R      $��1��6    � IsSharedSizeScope$
 True��61   .+�5T      � -   5        $ MainGrid��5T       5U      �� 5V      �� 5W      $��5 �6'   �� 5X      .+�5[      � -   5        $ 	TaskSheet��5[      $��0��6*    5\      �� 5]      $
��Auto �6(   �� 5^      .+�5a      �� $��0��6   #��[ 6*   $��5q�5b      $ 5��5c      ._�5d      �� -	   5        	 ��Name$	 columnSV��5d   '   $��Hidden=�69   $��Disabled=�6`   .��5e      � -
   5        $ 
ColumnGrid��5e   #   5g      5h      5i      �� 5k      $��1��6   $��5q�5l      $ 5��5m      ._�5n      �� -   5        $	 sheetSV��5n   '   $��Visible=�68   $��Hidden=�6`   .��5p      � .+�5q   "   �� -   5        
 ��Name$
 sheetStackPanel��5q   -   $ Vertical=�6F   $	��Top=�6]   $
��Left=�6u   5r   #   5s      5t      5u      5v      �� 5y      -   5         ��Name$ mainGS��5y      $��1��5z      $��5��6/    �� ResizeBehavior$ PreviousAndNext=�69   #��[ 5{      � 5}      $��2��6   $��0,0,0,0q�6'    5~      �� 5      $
��Auto �6(   �� 5�      .+�5�      �� $��0��6   #��[ 6*   $��5q�5�      $ 5��5�      ._�5�      �� -   5        $	 dateSV��5�   '   $��Hidden=�67   $��Disabled=�6^   .��5�      �� -   5        $
 DateSP��5�   )   $ 
Horizontal=�69   5�      5�      5�      �� 5�      $��1��6   $��5q�5�      $ 5��5�      ._�5�      �� -   5        $	 canvasSV��5�   '   $��Visible=�69   $
��Auto=�6a   .��5�      � -   5        $ 
CanvasGrid��5�   #    5�   "   �� 5�   &   $
��Auto �67   �� 5�   &   .+�5�   "   �� -   5         ��Name$ 
MainCanvas��5�   9   $��0��6)   5�   #   �� 5�   "   -   5        $
 canvasStackPanel��5�   =   $��0��6-   $ Vertical=�6W   $	��Top=�6n   $
��Left=�6�   5�   #   �� 5�   "   $��1��6)   $��72��69   5�      5�      5�      5�      5�      5�      5�      5�      