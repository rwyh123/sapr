   M S B A M L   `   `   `  ����?  ;sapr, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:sapr.Views
sapr.Views  5      1clr-namespace:sapr.ViewModelssapr.ViewModels  5          sapr.Views.ProccesorView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:sapr.Views     5      3	viewmodelclr-namespace:sapr.ViewModels     5        � DataContext  5      )   #sapr.ViewModels.ProcecssorViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� 
���   �$��1q�61   ._�5      �� $	��100��6>   ��6$   �� *
MyTextNX+6>   5      �� 5       
���   �$��1q�61   ._�5!      �� $	��100��6>   ��6$   �� *
MyTextDX+6>   5"      �� 5#      
���   �$��1q�61   ._�5$      �� $	��100��6>   ��6$   �� *
MyTextUX+6>   5%      5&      5'      �� 5(      $ Vertical=�6   $��1��60   .+�5)      �� $��x��6   $��Center=�6$   �� 5*      ��6   �� *Step+ �� Mode$ TwoWay=�5+      �� 5,      $��2��6   $��Расcчитать��6#   $��Bottom=�68   $��20��6�   ��6S   �� *PreProcessorCalculations+6�   5-      �� 5/   
   $��1��6   .@�50      �� $��Таблицы��6   .��51         sapr.Views.TablesXU  52      53      54      55      