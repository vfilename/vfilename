   M S B A M L   `   `   `  ���� D  @vfilename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:vfilename	vfilename      vfilename.ListWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:vfilename       � Icon$%  pack://application:,,,/Icon.ico�� � Title$ 
ListWindow��$	��300��$	��300�� � 	Resources    TextBoxBaseControlTemplate �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property	 P� Value
 �� Value �� 
TargetName �� Property��%  &	        �� " ��.R��� $ Bd��#��z��
���   �#��z��$ 5��._��� $ PART_ContentHost�� P�  � {�	 .  . ��� #
 � ��$ Bd�� � ���� #
 � �� � ��P�  � ��$
	 Auto��. ���  � ��$
 0��P�  � ��$
	 Auto��. ���  � ��$
 0��.���  � ColumnDefinitions �� $��1* � � RowDefinitions �� $��50 ��� $��1* �.+��� -    ��Name$ FilterTextBox��#��[  $��3q�$��2,2,2,2q�$��微软雅黑)�$��0��$��0��$��Center=�$��25(�.��过滤的关键词�� -   $��lv��$��0��$��1��$��0q�$��微软雅黑)�$��21(� �� SelectionMode$ Single=�����  �� 
TargetType" ��.�-   ��  � ��
 �� " ��.R��� $ Bd��#��z��#��z��#��z�� �� Padding# z�� J� SnapsToDevicePixels . ._��� #��z��# z #��z����  � ��

 �   ���  � ��$
 0,0,0,3q���  � ��$
 2q���  � ��$
 0q� �� Triggers P�  �@J�IsMouseOver	 . . ���  � ��

 � �����  � ��

 �   ���  � ��$
 0,0,0,3q���  � ��$
 2q���  � ��$
 0q�P�  �@��
IsSelected	 . . ���  � ��

 � � ���  � ��

 �   ���  � ��$
 0,0,0,3q���  � ��$
 2q���  � ��$
 0q� �� View �� -   
lvGridView��.P��� $��	项目名��-   col1�� �� Width ��  �� ElementName$ helperField1�� �� Path$ ActualWidth� �� CellTemplate �� .R��� $
��Wrap=�
���   ����� *ProjectName+�� $��	用户名��-   col2�� �� $ helperField2��$ ActualWidth� �� .R��� $
��Wrap=�
���   ����� *
UserName+�� $��时间��-   col3�� �� $ helperField3��$ ActualWidth� �� .R��� $
��Wrap=�
���   ����� *
DateTime+�� $��说明��-   col4�� �� $ helperField4��$ ActualWidth� �� .R��� $
��Wrap=�
���   ����� *
Descrption+� -	   $��
hiddenGrid��$��0��$��1��$y�Hidden=� �� $��0 ��� $��2* ��� $��1* ��� $��2* ��� $��4* �.+�� -
    �Name$ helperField1��$��1��� -   $ helperField2��$��2��� -   $ helperField3��$��3��� -   $ helperField4��$��4��