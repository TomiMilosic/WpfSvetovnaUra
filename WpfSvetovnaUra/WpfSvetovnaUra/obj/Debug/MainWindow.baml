   M S B A M L   `   `   `  ����I  EWpfSvetovnaUra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:WpfSvetovnaUra.UserControlsWpfSvetovnaUra.UserControls  5   	       WpfSvetovnaUra.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:WpfSvetovnaUra.UserControls     5   	     � Title$  Svetova Ura��5   	   $	��450��6   $	��800��6*   ��5	      �� 5
   
   -   5        
ozadje�� �� Color$ Beige��5
   *   6:    � 	Resources 5       	  nekaj E� 
StartPoint �� 
IsLargeArc �� Size �� Point �� SweepDirection��%�   &	        B� 5   
   .��5      E� $	 0,0&�6   .��5      ��  . $ 120,90��6/   $	 1,1&�6=   $ 	Clockwise=�6I   5      5      5       � Triggers 5      :� 5   
   	 :� RoutedEvent$	 Window.Loaded��6   .[�5      �� .��5      �� .��5      �� 	
 �� To$
 	IndianRedX�6%    g� Duration$ 0:00:10D�64    g� AutoReverse .  g� RepeatBehavior$ Forever��6Z    �� 
TargetName$ ozadje��6s    �� TargetProperty$ Color�6�   5      5      5      .��5       �  � RowDefinitions 5$   
   �� 5%      $	��30* �6   6+   �� 5&      $
��144* �6   6,   �� 5'      $
��247* �6   6,    � ColumnDefinitions 5)   
   �� 5*      $��1* �6    �� 5+      $��1* �6    �� 5,      $��1* �6    .+�5/   
   �� $��1��6   $��1��6    ��60   �� *ActualWidth+ �� ElementName$ 	KrajLabel����6e   �� *ActualHeight+$ 	KrajLabel��.+�50      A� -   5        $	��dot��50      $��10��6!   $��10��6,   
����  �
 �� Top$ 90��6D    �� Left$	 100��6T    51      :� 52      $	 Ellipse.Loaded��6#   .[�53      �� .��54      �� $ Forever��6)   .��55   "   T�  T� Source$ X=�6;   $ 0:00:05D�6F   $ (Canvas.Left)�6Y    T� PathGeometry# [  6�   56      57      58      5:      5;      ]� 5<   
   $��0,0,0.309,5.673q�6   $��3��6-   .+�5=      `� $	��Top=�6   $��0,0,0.309,0q�60   $��51��6E   .�5>      c� .@�5?      �� -   5        $��
Dodaj kraj��5?      $��0,0,0,-0.182q�6G   $��Bottom=�6]   �� 5@      -   5        $��Seznam priljubljenih��5@      6<   5A      5B      �� 5C       �� AcceptsReturn . 6   5D      (� 5F   
   $��Center=�6   $��Center=�6/   $��0��6K   $��1��6[   $	��124��6h   $��10,10,10,10q�6u   .��5G      �� -   5         ��Name$ 	KrajLabel��5G        VelikiNapis#��[ 6'   $��Center=�6{   $��Center=�6�   $
��Kraj��6�   $��0��6�   $��1��6�   $	��124��6�   $��10,10,10,10q�6�   $��Center=�6  $��Center=�6%  5H      �� 5I   
   $��1��6   $��1��6'   $��5��64   $
��Left=�6>   (� 5J   
   $��Center=�6   $��Center=�6/   $��1��6J   $��1��6Z   $��10,10,10,10q�6g   $	��129��6|   .��5K   
   �� -   5        $ uraLabel��5K      #��[ 6"   $	��Ura��6G   $��Center=�6U   $��Center=�6y   $��Center=�6�   $��Center=�6�   $��1��6�   $��1��6�   $��10,10,10,10q�6�   $	��129��6  5L      &   System.Windows.Controls.DataGrid  5M      -   5        $��KrajiSeznam��5M   8   $��2��6   $��3��6$      AutoGenerateColumns .  $��
10,0,10,10q�6�      
IsReadOnly .    Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   MinWidth$	 100Q�6%      Header$
 Kraj��64   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   ��  �� Path$
 kraj�    	CellStyle  5R      �� 5S      ! �� 
TargetType*  $System.Windows.Controls.DataGridCell"!  6    .�5T      �� " �� Property" � ��# �� Value
# ��p��6;   $ �� Triggers$ 5U      �� 5V   "   % �� Binding% 6.   �� $ 	clanicaEU�& �� Value& da6Q   .a�5W   &   �� " � ��
# ����6C   5X   #   5Z      5\        5]      -   5        UraTick��$	 100Q�5]   6   $	 Ura��6E    6R   �� $	 Ura�' �� StringFormat$' HH:mm:ss��6�   4  .System.Windows.Controls.DataGridTemplateColumn  5^      $# Dodaj na seznam priljubljenih��6)   $	 150Q�6P   (   CellTemplate( 5_      �� 5`      .R�5a      �� -	   5          NavadniGumb#��[ 5a   %   $��15(�6`   $��Stretch=�6n   .��6�   +6�   5b      5d        5e      $ Komentar��6)   )   Width5  /System.Windows.Controls.DataGridLengthConverter$	) 390 6;   ( 5f      �� 5g      .R�5h      �� ��6%   �� $ Komentar�* �� Mode$* TwoWay=�+ �� UpdateSourceTrigger$+ PropertyChanged=�6|   5i      5k      5m      .   (WpfSvetovnaUra.UserControls.UserControl1  5o   
   $��1��6   $��3��6*   $��Right=�6>   $	��Top=�6Z   $��26��6r   $��0,1.327,0.309,0q�6~   $	��160��6�   5p      5q      