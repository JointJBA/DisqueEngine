   M S B A M L   `   `   `  ����  DisqueRenderToy$    DisqueRenderToy.NewLightWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  NewLightWindow��5   	   $	��200��6    $	��300��6-    � WindowStartupLocation$ CenterOwner=�69    � WindowStyle$ 
ToolWindow=�6]    � 
ResizeMode$ NoResize=�6v    � ShowInTaskbar .  $��0q�6�   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��25 �6   .+�5
   
   � -   5         �Name$ new_camera_grid��5
       � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $��* �6#   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      �� $��Camera Type:��6   $��20��60   $��15(�6<   5      �� 5      $ Vertical=�6   $��1��60   .+�5      �� $��20��6   .@�5      �� $��Pinhole��6#   5      5      5      � 5   
   -   5        $ new_light_grid��5      $y�	Collapsed=�6'   �� 5   
   -   5        $��cancelbutton��5   B   $��1��6   $��Cancel��6   $��	228,0,0,0q�6/   6V   �� 5   
   -   5        $��okbutton��5   ?   $��Ok��6   $��
163,0,65,0q�6   $��1��62   6O   5      5      