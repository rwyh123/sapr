   M S B A M L   `   `   `  ���� ?  ;sapr, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:sapr.Views
sapr.Views  1clr-namespace:sapr.ViewModelssapr.ViewModels  !    sapr.Views.PreProcessorView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     *localclr-namespace:sapr.Views     3	viewmodelclr-namespace:sapr.ViewModels       � DataContext  +   %sapr.ViewModels.PreProcessorViewModel  .��]� ��. .+��� $	��Top=� �� Orientation$ Vertical=�$	��100��$
��Left=�.+�o� $��Center=�.@�m� $��Файл��.@�m� $��Загрузка�� m� Command �� *Load+m� $��Сохранить как�� �� *Save+{��� *IsSupportCountNotull+�� m� $��Очистить�� �� *Clear+{��� *IsSupportCountNotull+�� m� -   $)��#Сформировать отчет��{��� *IsSupportCountNotull+�� 
���   �$��1q�$	��5,5q�._�� $
��Left=�$
��Left=� � RowDefinitions �� �� .+��� 
���   �$��1q�$	��5,5q�._�� $��0�� �� $��* ��� $
��Auto �.+��� $ Vertical=�$��1��$��Center=�.+��� $ 
Horizontal=�$	��0,5q�.+��� $'��!Добавить стержень��$
��10,0q����� *
AddSupport+�� $%��Удалить стержень������ *RemoveSupport+{��� *IsSupportCountNotull+�� $ 
Horizontal=�$��Center=�.+��� $��Левая опора��$
��10,0q� d� 	IsChecked �� *
LeftSmth+ �� Mode$ TwoWay=�{��� *IsSupportCountNotull+�� $��Правая опора�� �� *	RightSmth+$ TwoWay=�{��� *IsSupportCountNotull+�� $��0��
���   �$��1q�._��� .���� $ Vertical=�.+�&   System.Windows.Controls.DataGrid  -     Name$ SupportTable��   AutoGenerateColumns .     CanUserAddRows .  ���� *Shapes+$ TwoWay=�	 �� SelectedItem	 �� *SelectedShape+
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Номер��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ 	Model.Uid�$ TwoWay=�  $ 
Длина�� �� $ Model.Width�$ TwoWay=�  $ Сечение�� �� $ Model.Height�$ TwoWay=�  $! Распр.Нагрузка�� �� $ PrPower�$ TwoWay=�  $/ )Допустимое напряжение�� �� $ AdmissibleStress�$ TwoWay=�  $% Модуль упрогусти�� �� $ E�$ TwoWay=�� $��1�� � ColumnDefinitions �� �� .+��� $��0��
���   �$��1q�$	��5,5q�._��� .��  -   $ 	NodeTable�� .   .  ���� *Nodes+$ TwoWay=�	 �� *SelectedNode+
   $ 
Номер�� .  �� $ 
NodeNumber�$ TwoWay=�  $3 -Сосредоточеная нагрузка�� �� $ PoPower�$ TwoWay=��� $��1��
���   �$��1q�$	��5,5q�._��  �� $��* ��� $
��auto �.+�� $��0�� �� �� �� ��  �� $
��auto ��� $��* �.+��� $��Глобальный X��$��0��y��� *PositionLableVisability+�� $��1��$��0��$��20(� �� 
IsReadOnly . ���� *	CurentSup+y��� *PositionLableVisability+�� $��NX           |��$��1��y��� *PositionLableVisability+�� $��1��$��1��$��20(� . ���� *NX+y��� *PositionLableVisability+�� $��UX           |��$��2��y��� *PositionLableVisability+�� $��1��$��2��$��20(� . ���� *UX+y��� *PositionLableVisability+�� $��DX           |��$��3��y��� *PositionLableVisability+�� $��1��$��3��$��20(� . ���� *DX+y��� *PositionLableVisability+� $��1�� �� �� .+��� $��1��$��Сохранить������ *	Calculate+{��� *CanBeCalculateed+�� 
���   �$��1q�$	��5,5q�._��� -    ��Name$ 	scrollbar������ *HscrolVisible+���� *VscrolVisible+.���� -    ��Name$ 	WorkSpase��$
��Left=����� *CanvasHenght+���� *CanvasLenhgt+.+��� ���� *CanvasChildrens+���� .R��� 