   M S B A M L   `   `   `  ����J  FAMONIC_Session4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AMONIC_Session4AMONIC_Session4  5      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5      m iSystem.Windows.Forms.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�mclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization/System.Windows.Forms.DataVisualization.Charting 5      "    AMONIC_Session4.DetailedPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AMONIC_Session4     5      Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5      �chartsmclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization     5        N� Title$  DetailedPage��5      $
��1000��5      $	��770��5      .��5      � .+�5   
   ��  �� Orientation$ Vertical=�5      $	��Top=�5      $��10, 15, 20, 0q�5      .+�5      �� $��0, 10, 30, 0q�5      $ 
Horizontal=�5      $��Right=�5      .+�5      �� $��
0, 3, 5, 0q�5      $��Time period: ��5      �� 5      -   5         ��Name$ date_cb��5      $��Right=�5      $	��100��5      ��5       �� 5!      .R�5"      �� ��5#   !   ��  �� Path$
 Name�5$      5&      5'      �� 5(      $ 
Horizontal=�5)      $	��Top=�5*      $��Right=�5+      $��190, 5, 0, 0q�5,      .+�5-      �� 
���   �$��1q�5/      $��50��50      ._�51      � .+�52      &   System.Windows.Controls.DataGrid     CanUserResizeColumns .  $��0, 20, 0, 0q�54         AutoGenerateColumns .     
IsReadOnly . $
��auto��57         Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59   "      centre_header,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [  65   	   Header$	 Total��6b   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6q   5;      5<      5=      �� 5>      
���   �$��1q�5@      $	��100��5A      ._�5B      � .+�5C      �� $��Gender��5D      $��Center=�5E        5F      -   5          Name$ 	gender_dg��5G       .  $��0, 20, 0, 0q�5I       .   . $
��auto��5L       5M        5N   "   # [  65   $
	 Male��6b   $
 * 6p     5O   "   # [  65   $	 Female��6b   $
 * 6r   5Q      5R      5S      �� 5T      
���   �$��1q�5V      $��-1, 0, 0, 0q�5W      $	��180��5X      ._�5Y      � .+�5Z      �� $	��Age��5[      $��Center=�5\        5]      -   5        $ age_dg��5^      $
��auto��5_       .  $��0, 20, 0, 0q�5a       .   .  5d        5e   "   # [  65   $	 18-24��6b   $
 * 6q     5f   "   # [  65   $	 25-39��6b   $
 * 6q     5g   "   # [  65   $	 40-59��6b   $
 * 6q     5h   "   # [  65   $	   60+��6b   $
 * 6q   5j      5k      5l      �� 5m      
���   �$��1q�5o      $��-1, 0, 0, 0q�5p      $	��150��5q      ._�5r      � .+�5s      �� $��
Cabin Type��5t      $��Center=�5u        5v      -   5        $ cabin_type_dg��5w      $��0, 20, 0, 0q�5x       .   .   .  5|        5}   "   # [  65   $	 Economy��6b   $
 52 6s     5~   "   # [  65   $	 Business��6b   $
 * 6t     5   "   # [  65   $	 First��6b   $
 * 6q   5�      5�      5�      �� 5�      
���   �$��1q�5�      $��-1, 0, 0, 0q�5�      $	��200��5�      ._�5�      � .+�5�      �� $��Destination Airport��5�      $��Center=�5�        5�      -   5        $ destination_dg��5�      $��0, 20, 0, 0q�5�       .   .   .  5�        5�   "   # [  65   $		 AUH��6b   $
 * 6o     5�   "   # [  65   $		 BAH��6b   $
 * 6o     5�   "   # [  65   $		 DOH��6b   $
 * 6o     5�   "   # [  65   $		 RUH��6b   $
 * 6o     5�   "   # [  65   $		 CAI��6b   $
 * 6o   5�      5�      5�      5�      �� 5�      $��Right=�5�      $ 
Horizontal=�5�      $��
0, 5, 0, 0q�5�      .+�5�      �� $��0, 0, 30, 0q�5�      $7��1Please rate our aircraft flown on AMONIC Airlines��5�      7  1System.Windows.Forms.Integration.WindowsFormsHost  5�      $	��680��5�      $��10��5�         Child. 5�      %  System.Windows.Forms.PictureBox  -   5        
q1_diagram��"  System.Windows.Forms.Control   Dock$
 Fill=�5�         Width$	 730��5�      5�      5�        5�      -   5        $ q1_dg��5�       �� AlternationCount$ 2��5�         AlternatingRowBackground �	#FFCEDDF1   GridLinesVisibility$
 None=�5�      
������    HeadersVisibility$
 None=�5�       5�        5�        
right_cell1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 6)   $	
 290 6T      Binding 6`   �� $	 [0]�  5�        centre_cell# [ 6)   $
 52 6U    6`   �� $	 [1]�  5�      # [ 6)   $
 50 6U    6`   �� $	 [2]�  5�      # [ 6)   $
 50 6U    6`   �� $	 [3]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [4]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [5]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [6]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [7]�  5�      # [ 6)   $
 49 6U    6`   �� $	 [8]�  5�      # [ 6)   $
 49 6U    6`   �� $	 [9]�  5�      # [ 6)   $
 49 6U    6`   �� $
 [10]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [11]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [12]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [13]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [14]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [15]�5�      �� 5�      $��Right=�5�      $ 
Horizontal=�5�      $��
0, 5, 0, 0q�5�      .+�5�      �� $��0, 0, 70, 0q�5�      $/��)How would you rate our flight attendants:��5�        5�      $	��680��5�      $��10��5�      . 5�        -   5        
q2_diagram��$
 Fill=�5�      $	 730��5�      5�      5�        5�      -	   5        $ q2_dg��5�      $ 2��5�       �	#FFCEDDF1$
 None=�5�      
������ $
 None=�5�       5�        5�      # [ 6)   $	
 290 6T    6`   �� $	 [0]�  5�      # [ 6)   $
 52 6U    6`   �� $	 [1]�  5�      # [ 6)   $
 50 6U    6`   �� $	 [2]�  5�      # [ 6)   $
 50 6U    6`   �� $	 [3]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [4]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [5]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [6]�  5�      # [ 6)   $
 45 6U    6`   �� $	 [7]�  5�      # [ 6)   $
 49 6U    6`   �� $	 [8]�  5�      # [ 6)   $
 49 6U    6`   �� $	 [9]�  5�      # [ 6)   $
 49 6U    6`   �� $
 [10]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [11]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [12]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [13]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [14]�  5�      # [ 6)   $
 * 6U    6_   �� $
 [15]�5�      �� 5�      $��Right=�5�      $ 
Horizontal=�5�      $��
0, 5, 0, 0q�5�      .+�5�      �� $��0, 0, 44, 0q�5�      $3��-How would you rate our inflight entertainment��5�        5�      $	��680��5�      $��10��5�      . 5�        -
   5        
q3_diagram��$
 Fill=�5�      $	 730��5�      5      5       5     -   5        $ q3_dg��5     $ 2��5      �	#FFCEDDF1$
 None=�5     
������ $
 None=�5      5	       5
     # [ 6)   $	
 290 6T    6`   �� $	 [0]�  5     # [ 6)   $
 52 6U    6`   �� $	 [1]�  5     # [ 6)   $
 50 6U    6`   �� $	 [2]�  5     # [ 6)   $
 50 6U    6`   �� $	 [3]�  5     # [ 6)   $
 45 6U    6`   �� $	 [4]�  5     # [ 6)   $
 45 6U    6`   �� $	 [5]�  5     # [ 6)   $
 45 6U    6`   �� $	 [6]�  5     # [ 6)   $
 45 6U    6`   �� $	 [7]�  5     # [ 6)   $
 49 6U    6`   �� $	 [8]�  5     # [ 6)   $
 49 6U    6`   �� $	 [9]�  5     # [ 6)   $
 49 6U    6`   �� $
 [10]�  5     # [ 6)   $
 * 6U    6_   �� $
 [11]�  5     # [ 6)   $
 * 6U    6_   �� $
 [12]�  5     # [ 6)   $
 * 6U    6_   �� $
 [13]�  5     # [ 6)   $
 * 6U    6_   �� $
 [14]�  5     # [ 6)   $
 * 6U    6_   �� $
 [15]�5     �� 5     $��Right=�5     $ 
Horizontal=�5     $��
0, 5, 0, 0q�5     .+�5      �� $��0, 0, 10, 0q�5!     $?��9Please rate the ticket price for the trip you are taking:��5"       5#     $	��680��5$     $��10��5%     . 5&       -   5        
q4_diagram��$
 Fill=�5(     $	 730��5)     5*     5+       5,     -   5        $ q4_dg��5-     $ 2��5.      �	#FFCEDDF1$
 None=�50     
������ $
 None=�52      53       54     # [ 6)   $	
 290 6T    6`   �� $	 [0]�  55     # [ 6)   $
 52 6U    6`   �� $	 [1]�  56     # [ 6)   $
 50 6U    6`   �� $	 [2]�  57     # [ 6)   $
 50 6U    6`   �� $	 [3]�  58     # [ 6)   $
 45 6U    6`   �� $	 [4]�  59     # [ 6)   $
 45 6U    6`   �� $	 [5]�  5:     # [ 6)   $
 45 6U    6`   �� $	 [6]�  5;     # [ 6)   $
 45 6U    6`   �� $	 [7]�  5<     # [ 6)   $
 49 6U    6`   �� $	 [8]�  5=     # [ 6)   $
 49 6U    6`   �� $	 [9]�  5>     # [ 6)   $
 49 6U    6`   �� $
 [10]�  5?     # [ 6)   $
 * 6U    6_   �� $
 [11]�  5@     # [ 6)   $
 * 6U    6_   �� $
 [12]�  5A     # [ 6)   $
 * 6U    6_   �� $
 [13]�  5B     # [ 6)   $
 * 6U    6_   �� $
 [14]�  5C     # [ 6)   $
 * 6U    6_   �� $
 [15]�5E     �� 5F     $ 
Horizontal=�5G     $��200, 15, 0, 0q�5H     .+�5I     �� $��8��5J     $��8��5K     ���	#FF52A0685L     �� 5M     $��Outstanding��5N     $��
5, 0, 0, 0q�5O     �� 5P     $��8��5Q     $��8��5R     ���	#FFA6DEA9$��20, 0, 0, 0q�5T     �� 5U     $��	Very Good��5V     $��
5, 0, 0, 0q�5W     �� 5X     $��8��5Y     $��8��5Z     ���	#FFC6E6C8$��20, 0, 0, 0q�5\     �� 5]     $
��Good��5^     $��
5, 0, 0, 0q�5_     �� 5`     $��8��5a     $��8��5b     ���	#FFE0CFBB$��20, 0, 0, 0q�5d     �� 5e     $��Adequate��5f     $��
5, 0, 0, 0q�5g     �� 5h     $��8��5i     $��8��5j     ���	#FFE2C1C1$��20, 0, 0, 0q�5l     �� 5m     $��Needs Improvement��5n     $��
5, 0, 0, 0q�5o     �� 5p     $��8��5q     $��8��5r     ���	#FFE45839$��20, 0, 0, 0q�5t     �� 5u     $
��Poor��5v     $��
5, 0, 0, 0q�5w     �� 5x     $��8��5y     $��8��5z     ���	#FFDADADA$��20, 0, 0, 0q�5|     �� 5}     $��
Don't know��5~     $��
5, 0, 0, 0q�5     5�     �� 5�     $ 
Horizontal=�5�     $��Center=�5�     $��0, 10, 0, 10q�5�     .+�5�     �� -   5         ��Name$ gender_check��5�     $��Gender��5�     $��
0, 5, 0, 0q�5�     �� 5�     -   5        $ 	gender_cb��5�     $��10, 0, 0, 0q�5�     $	��100��5�     ��5�     �� 5�     .R�5�     �� ��5�  !   �� $
 Name�5�     5�     �� 5�     -   5        $ 	age_check��5�     $	��Age��5�     $��90, 5, 0, 0q�5�     �� 5�     -   5        $ age_cb��5�     $��10, 0, 0, 0q�5�     $	��100��5�     ��5�     �� 5�     .R�5�     �� ��5�  !   �� $
 Name�5�     5�     5�     5�     5�     5�     