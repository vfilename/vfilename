   M S B A M L   `   `   `  ����D  @vfilename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:vfilename.FontSizeConvertervfilename.FontSizeConverter  5   	       vfilename.FileNameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:vfilename.FontSizeConverter     5   	   -   5          � Icon$%  pack://application:,,,/Icon.ico��5   	    � Title$2 ,V File Name - 压缩包的文件名的组成��5	   	   $	��300��6*   $	��300��67    � 	Resources 5      3   -vfilename.FontSizeConverter.FontSizeConverter   fontSizeCon  TextBoxBaseControlTemplate �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property	 P� Value
 �� Value �� 
TargetName �� Property  ButtonBaseControlTemplate ��Name��%  &	        &	      &	 �      5   
   6"   �� 5   
   " ��6=   .R�5      �� $ Bd��5      #��z��5      
���   �#��z��5      $ 5��6E   ._�5      �� $ PART_ContentHost��6   5       5      P� 5       � {�	 .  . �5      �� #
 � ��63   $ Bd��6u    � ���� 5      #
 � ��63    � ��5      P� 5       � ��$
	 Auto��6+   . �5      ��  � ��$
 0��61   5      P� 5       � ��$
	 Auto��6,   . �5      ��  � ��$
 0��62   5      5       �� 5"   
   " ��6<   .R�5#      �� $ Bd��5$      #��z��5#      
���   �#��z��5%      $ 5��6E   ._�5&      �� $ PART_ContentHost��6#   #��z��6=   #��z��5'      5(       5)      P� 5*       � {�	 .  . �5+      �� #
 � ��63   $ Bd��6u    � ���� 5,      #
 � ��63    � ��5-      P� 5.       � ��$
	 Auto��6+   . �5/      ��  � ��$
 0��61   50      P� 51       � ��$
	 Auto��6,   . �52      ��  � ��$
 0��62   53      55      57      .��59      �  � ColumnDefinitions 5:   
   �� 5;      $��1* �6    � RowDefinitions 5=   
   �� 5>      $��1* �6   �� 5?      $��1* �6   �� 5@      $��1* �6   �� 5A      $��1* �6   .+�5C   
   �� -   5         ��Name$ UserNameTextBox��5C   n   #��[ 6   $��3q�6I   $��2,2,2,0q�6]   $��微软雅黑)�6�   $��0��6�   $��0��6�   $��Center=�6�   ��5D   
   ��  �� Path$ ActualHeight� �� RelativeSource ��  �� Mode$
 Self=� �� 	Converter# [  .��6�   	用户名5F      �� 5G   
   -   5        $ DateTimeTextBox��5G   n   #��[ 6   $��3q�6I   $��2,2,2,0q�6]    �� 
IsReadOnly . $��微软雅黑)�6�   $��0��6�   $��1��6�   $��Center=�6�   ��5H   
   �� $ ActualHeight� �� $
 Self=�# [  .��6�   时间5J      �� 5K   
   -   5        $ DescriptionTextBox��5K   n   #��[ 6   $��3q�6I   $��2,2,2,0q�6]   $��微软雅黑)�6�   $��0��6�   $��2��6�   $��Center=�6�   ��5L   
   �� $ ActualHeight� �� $
 Self=�# [  .��6�   说明5N      �� 5O   
   -   5         ��Name$ CompressButton��5O   �   
���   �
�������$��生成压缩包��67   #��[ 6G   $��0q�6}   $��2,2,2,2q�6�   $��微软雅黑)�6�   $��0��6F  $��3��6V  ��6�   �� $ ActualHeight� �� $
 Self=�# [  5P      5Q      5R      