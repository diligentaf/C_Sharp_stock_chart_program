   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	   7 clr-namespace:Client.Base.ExCtlsClient.Base.ExCtls  5   	       Client.ChartWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	   3exCtls clr-namespace:Client.Base.ExCtls     5   	     � Title$  Sample Program��5	   	   $	��175��5
   	   $	��950��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$ SingleBorderWindow=�5   	    � ShowInTaskbar$
 True��5   	    � 
ResizeMode$ NoResize=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $
��100* �6   �� 5      $��4 �6   .+�5   
   a� $��0��6   $��2��5      .+�5      `� .�5      c� .@�5      �� -   5         ��Name$
 Exit��5      $��Right=�5         ExitToolButtonContent#��[  5      5      5      5       � 5"   
   $��1��6   $��0��6    5#      �� 5$      $
��100* �6     � ColumnDefinitions 5&      �� 5'      $
��100* �6#   �� 5(      $��5 �6#   .+�5*      #   Client.Base.ExCtls.ExDataGrid  -   5          Name$ 	ChartList��5*       $��0��63   $��0��6@   $��2��6P   $��Stretch=�6a   $
��Left=�6   $��Stretch=�6�   $��5, 0, 0, 10q�6�   &   System.Windows.Controls.DataGrid	   AutoGenerateColumns$	 false��6�   
 �� SelectedValuePath$
 DbIndex.DbId��6�     GlobalDataGridStyle#��[ 6     Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ Position Id��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6]     DataGridTextCol_ElmStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 6h      Binding 6>   �� *
PositionId+6h     5-      $ Ticker��6)   $ 90 6T   # [ 6_    69   �� *Ticker+6_     5.      $ 
Spot Price��6)   $	 100 6[   # [ 6g    6=   �� *	SpotPrice+6g     5/      $ 	Qty [T-1]��6)   $ * 6T   # [ 6^    6<   �� *Qt1+6^     50      $ 	Qty [T-0]��6)   $ * 6T   # [ 6^    6<   �� *Qt0+6^     51      $ 
Qty Change��6)   $ * 6X   # [ 6b    6=   �� *Change+6b   53      55      �� 57   
   $��3��6   ��58      �� *StatusBarColor+59      5:      5;      