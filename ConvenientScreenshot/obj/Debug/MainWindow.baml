   M S B A M L   `   `   `  ����O  KConvenientScreenshot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ConvenientScreenshotConvenientScreenshot  5   	   %    ConvenientScreenshot.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ConvenientScreenshot     5   	      window_style#��[  5   	     � Title$  
MainWindow��5	   	   $	��599��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z   ���	#FF2D2D30 � AllowsTransparency .  � WindowStyle$
 None=�6�   .��5
      � .+�5   
   �� -   5         ��Name$ spnlWindowTopBar��5      $	��Top=�6/   $��28��6G    �� Orientation$ 
Horizontal=�6S   $��RightToLeft=�6l   ���	#00000000.+�5      �� -   5         ��Name$ btnWindowStateClose��5      $	��✕��62     window_state_close_button#��[ 6>   $��Center=�6q   $��Center=�6�   $��Right=�6�   $	��Top=�6�   $��75��6�   $��28��6�   ���	#00000000���	#00000000
�������$��0q�6D   � UseLayoutRounding .  	 � Cursor$	 Arrow��6�  �� 5      -   5        $ btnWindowStateMaxMin��5      $	��☐��63     window_state_maxmin_button#��[ 6?   $��Center=�6s   $��Center=�6�   $��Right=�6�   $	��Top=�6�   $��75��6�   $��28��6�   ���	#00000000���	#00000000
�������$��0q�6F   .  $	 Arrow��6t  6�  �� 5      -   5        $ btnWindowStateMinimun��5      $	��—��64   #��[ 6@   $��Center=�6t   $��Center=�6�   $��Right=�6�   $	��Top=�6�   $��75��6�   $��28��6�   ���	#00000000���	#00000000
�������$��0q�6G   .  $��0,0,80,0q�6u  6�  5      �� 5   
   -   5        $ btnScreenshot��5        dark_default_button#��[ 6(   $��
Screenshot��6U   $
��Left=�6j   $��	10,33,0,0q�6�   $	��Top=�6�   $��75��6�   $��20��6�   ���	#FF302D2D���	#FF252529
�������6  � 5   
   $��92,33,10,167q�6   ���	#FF252529.+�5      �� -   5        
 ��Name$
 
imgPreview��5      $��Disabled=�6(   5      �� 5   
   -   5         ��Name$ svHistoryList��5      $	��152��6.   $
��Auto=�6;    J� RenderTransformOrigin$ 
0.517,0.73&�6`   $��Stretch=�6�   $��Stretch=�6�   $��10,417,10,10q�6�   $��Disabled=�6�   $��Bottom=�6  .��5      �� -   5        $ spnlImgHistoryList��5      $	��152��65   $ 
Horizontal=�6B   ���	#FF252529 �� CanHorizontallyScroll . ��. $��Visible=�6�   $��Disabled=�6�   5      �� 5   
   -	   5         ��Name$ cbTooglePreviewImgSize��5      $��Origin Size��63   $
��Left=�6I   $��	10,58,0,0q�6d   $	��Top=�6w   
�������6�   5      5      