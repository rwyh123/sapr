   M S B A M L   `   `   `  ����?  ;sapr, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:sapr.Views
sapr.Views  5      1clr-namespace:sapr.ViewModelssapr.ViewModels  5      !    sapr.Views.PreProcessorView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:sapr.Views     5      3	viewmodelclr-namespace:sapr.ViewModels     5        � DataContext  5      +   %sapr.ViewModels.PreProcessorViewModel  5   
   .��5      ]� ��. .+�5   
   �� $	��Top=�6    �� Orientation$ Vertical=�6*   $	��100��6A   $
��Left=�6M   .+�5      o� $��Center=�6   .@�5      m� $��Файл��6   .@�5      m� $��Загрузка��6    m� Command 61   �� *Load+m� 5      $��Сохранить как��6    66   �� *Save+{�6O   �� *IsSupportCountNotull+�� 5      m� 5      $��Очистить��6    61   �� *Clear+{�6K   �� *IsSupportCountNotull+�� 5      m� 5      -   5        $)��#Сформировать отчет��5      {�6S   �� *IsSupportCountNotull+5      5      5      �� 5   
   
���   �$��1q�6%   $	��5,5q�69   ._�5      � $
��Left=�6   $
��Left=�6*    � RowDefinitions 5      �� 5      �� 5       .+�5"      �� 
���   �$��1q�6-   $	��5,5q�6A   ._�5#      � $��0��6    5$      �� 5%      $��* �6,   �� 5&      $
��Auto �6,   .+�5(      �� $ Vertical=�6%   $��1��6<   $��Center=�6I   .+�5)      �� $ 
Horizontal=�6)   $	��0,5q�6B   .+�5*   "   �� $'��!Добавить стержень��6)   $
��10,0q�6E   ��6S   �� *
AddSupport+5+   #   �� 5,   "   $%��Удалить стержень��6)   ��6D   �� *RemoveSupport+{�6f   �� *IsSupportCountNotull+5-   #   5.      �� 5/      $ 
Horizontal=�6)   $��Center=�6B   .+�50   "   �� $��Левая опора��6+   $
��10,0q�6A    d� 	IsChecked 6O   �� *
LeftSmth+ �� Mode$ TwoWay=�{�6z   �� *IsSupportCountNotull+51   #   �� 52   "   $��Правая опора��6+    6B   �� *	RightSmth+$ TwoWay=�{�6n   �� *IsSupportCountNotull+53   #   54      55      �� 56      $��0��6!   
���   �$��1q�6E   ._�57      �� .��58   "   �� $ Vertical=�6-   .+�59   &   &   System.Windows.Controls.DataGrid  -   5          Name$ SupportTable��59   /      AutoGenerateColumns .     CanUserAddRows .  ��6a   �� *Shapes+$ TwoWay=�	 �� SelectedItem	 5:   /   �� *SelectedShape+
   Columns
 5;   *   0  *System.Windows.Controls.DataGridTextColumn  5<   .   ,  &System.Windows.Controls.DataGridColumn   Header$ 
Номер��6A      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6P   ��  �� Path$ 	Model.Uid�$ TwoWay=�6�     5=   .   $ 
Длина��6A    6P   �� $ Model.Width�$ TwoWay=�  5>   .   $ Сечение��6A    6R   �� $ Model.Height�$ TwoWay=�  5?   .   $! Распр.Нагрузка��6A    6Y   �� $ PrPower�$ TwoWay=�  5@   .   $/ )Допустимое напряжение��6A    6`   �� $ AdmissibleStress�$ TwoWay=�  5A   .   $% Модуль упрогусти��6A    6[   �� $ E�$ TwoWay=�5C   '   5D   #   5E      5F      5G      5H      � 5I      $��1��6    � ColumnDefinitions 5J      �� 5K      �� 5L      .+�5N      �� $��0��6   
���   �$��1q�6A   $	��5,5q�6U   ._�5O      �� .��5P        -   5        $ 	NodeTable��5P   '    .   .  ��6V   �� *Nodes+$ TwoWay=�	 5Q   '   �� *SelectedNode+
 5R   "     5S   &   $ 
Номер��6j    .  69   �� $ 
NodeNumber�$ TwoWay=�6y     5T   &   $3 -Сосредоточеная нагрузка��6g    69   �� $ PoPower�$ TwoWay=�6g   5V      5W      5X      �� 5Y      $��1��6   
���   �$��1q�6A   $	��5,5q�6U   ._�5Z      �  5[      �� 5\   "   $��* �60   �� 5]   "   $
��auto �60   .+�5_      � $��0��6#    5`   "   �� 5a   &   �� 5b   &   �� 5c   &   �� 5d   &    5f   "   �� 5g   &   $
��auto �64   �� 5h   &   $��* �64   .+�5j   "   �� $��Глобальный X��6(   $��0��6?   y�6O   �� *PositionLableVisability+�� 5k   "   $��1��6*   $��0��67   $��20(�6b    �� 
IsReadOnly . ��6G   �� *	CurentSup+y�6�   �� *PositionLableVisability+�� 5l   "   $��NX           |��6(   $��1��6A   y�6Q   �� *PositionLableVisability+�� 5m   "   $��1��6*   $��1��67   $��20(�6[    . ��6G   �� *NX+y�6{   �� *PositionLableVisability+�� 5n   "   $��UX           |��6(   $��2��6A   y�6Q   �� *PositionLableVisability+�� 5o   "   $��1��6*   $��2��67   $��20(�6[    . ��6G   �� *UX+y�6{   �� *PositionLableVisability+�� 5p   "   $��DX           |��6(   $��3��6A   y�6Q   �� *PositionLableVisability+�� 5q   "   $��1��6*   $��3��67   $��20(�6[    . ��6G   �� *DX+y�6{   �� *PositionLableVisability+5r      � 5s      $��1��6#    5t   "   �� 5u   &   �� 5v   &   .+�5}   "   �� $��1��6)   $��Сохранить��66   ��6J   �� *	Calculate+{�6h   �� *CanBeCalculateed+5~      5      5�      5�      5�      5�      �� 5�   
   
���   �$��1q�6%   $	��5,5q�69   ._�5�      �� -   5         ��Name$ 	scrollbar��5�      ��6.   �� *HscrolVisible+��6f   �� *VscrolVisible+.��5�      �� -   5         ��Name$ 	WorkSpase��5�   n   $
��Left=�6   ��6/   �� *CanvasHenght+��6O   �� *CanvasLenhgt+.+�5�      �� ��6#   �� *CanvasChildrens+��5�      �� 5�      .R�5�   "   �� 5�      5�      5�      5�      5�      5�      5�      