   M S B A M L   `   `   `  ���� D  @vfilename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:vfilename.FontSizeConvertervfilename.FontSizeConverter      vfilename.FileNameWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ;local)clr-namespace:vfilename.FontSizeConverter     -     � Icon$%  pack://application:,,,/Icon.ico�� � Title$2 ,V File Name - 压缩包的文件名的组成��$	��300��$	��300�� � 	Resources 3   -vfilename.FontSizeConverter.FontSizeConverter   fontSizeCon  TextBoxBaseControlTemplate �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property	 P� Value
 �� Value �� 
TargetName �� Property  ButtonBaseControlTemplate ��Name��%?  &	        &	      &	       �� " ��.R��� $ Bd��#��z��
���   �#��z��$ 5��._��� $ PART_ContentHost�� P�  � {�	 .  . ��� #
 � ��$ Bd�� � ���� #
 � �� � ��P�  � ��$
	 Auto��. ���  � ��$
 0��P�  � ��$
	 Auto��. ���  � ��$
 0���� " ��.R��� $ Bd��#��z��
���   �#��z��$ 5��._��� $ PART_ContentHost��#��z��#��z�� P�  � {�	 .  . ��� #
 � ��$ Bd�� � ���� #
 � �� � ��P�  � ��$
	 Auto��. ���  � ��$
 0��P�  � ��$
	 Auto��. ���  � ��$
 0��.���  � ColumnDefinitions �� $��1* � � RowDefinitions �� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� -    ��Name$ UserNameTextBox��#��[ $��3q�$��2,2,2,0q�$��微软雅黑)�$��0��$��0��$��Center=�����  �� Path$ ActualHeight� �� RelativeSource ��  �� Mode$
 Self=� �� 	Converter# [  .��	用户名�� -   $ DateTimeTextBox��#��[ $��3q�$��2,2,2,0q� �� 
IsReadOnly . $��微软雅黑)�$��0��$��1��$��Center=����� $ ActualHeight� �� $
 Self=�# [  .��时间�� -   $ DescriptionTextBox��#��[ $��3q�$��2,2,2,0q�$��微软雅黑)�$��0��$��2��$��Center=����� $ ActualHeight� �� $
 Self=�# [  .��说明�� -    ��Name$ CompressButton��
���   �
�������$��生成压缩包��#��[ $��0q�$��2,2,2,2q�$��微软雅黑)�$��0��$��3������ $ ActualHeight� �� $
 Self=�# [  