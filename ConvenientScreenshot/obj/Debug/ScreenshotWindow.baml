   M S B A M L   `   `   `  ����O  KConvenientScreenshot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ConvenientScreenshotConvenientScreenshot  5   	   +    %ConvenientScreenshot.ScreenshotWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ConvenientScreenshot     5   	     � Title$  ScreenshotWindow��5   	   $	��450��6"   $	��800��6/    � 
ResizeMode$ NoResize=�6;    � WindowState$ 	Maximized=�6Q    � WindowStartupLocation$ CenterScreen=�6i    � WindowStyle$
 None=�6�    J� OpacityMask
 �   �.��5
      �� -   5         ��Name$ canvas��5
      
������ .+�5   
   �� -   5         ��Name$ toolbar��5      
���   �$��0q�66    �� Left$
 -100��6J   
	 �� Top$	 0��6]   $��90��6l   $��30��6w   
 �� CornerRadius$
 3��6�   ���	#FFEEEEEE J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#      ShadowDepth$ 0Q�63   ._�5      �� $
 -100��6   $	 0��6,    �� Orientation$ 
Horizontal=�6;   $��0q�6T   $��30��6_   $��90��6k   .+�5      � -   5         �Name$ button_copyToClipboard��5      ���	#00000000 � ToolTip$+ %copy to clipbaord, review and history��6�   .+�5      �� $$��C:\Users\Otis\Desktop\copy.png��6   $��Uniform=�6D   $��5q�6V   5      � 5      -   5        $" button_addToReviewAndHistory��5      ���	#00000000$[ Ucopy the screenshot to review and history. ( this action will not copy to clipboard )��6�   .+�5      �� $-��'C:\Users\Otis\Desktop\forward-arrow.png��6   $��Uniform=�6M   $��5q�6_   5      � 5      -   5        $ button_abandonScreenshot��5      ���	#00000000$ Abandon��6�   .+�5      �� $&�� C:\Users\Otis\Desktop\cancel.png��6   $��Uniform=�6F   $��5q�6X   5      5      5      �� 5   
   -   5        $ infobar��5      $��0q�6"   $
 -100��66   $	 46��6I   $��30��6Y   $
 3��6e   ���	#99000000���	#99000000._�5      �� $
 -100��6   $	 0��6,   $ 
Horizontal=�6;   $��0q�6T   $��30��6_   .+�5      � -   5        ���	#00000000.+�5       �� -   5         ��Name$ label_RectSize��5        $��0 x 0��68   $��5q�6\   |�.  
���������6E   Z� 6y   5!      5"      5#      5$      5%      