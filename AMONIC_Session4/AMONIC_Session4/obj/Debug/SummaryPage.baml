   M S B A M L   `   `   `  ����J  FAMONIC_Session4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AMONIC_Session4AMONIC_Session4  5      !    AMONIC_Session4.SummaryPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AMONIC_Session4     5        N� Title$  SummaryPage��5	      $	��900��5
      $	��280��5      .��5      � .+�5   
   �� -   5         ��Name$ field_work_tb��5      $&�� Field work: May 2017 - July 2017��5      $
��Left=�5      $��Center=�5      $��20, 0, 0, 150q�5      �� 5   
   -   5        $ sample_size_tb��5      $��Sample Size: 1727 Adults��5      $��Right=�5      $��Center=�5      $��0, 0, 20, 150q�5      �� 5   
    �� Orientation$ 
Horizontal=�5      $	��Top=�5      $��20, 80, 20, 0q�5      .+�5      �� 
���   �$��1q�5       $	��110��5!      ._�5"      � .+�5#      �� $��Gender��5$      $��Center=�5%      &   System.Windows.Controls.DataGrid  5&      -   5          Name$ 	gender_dg��5'         CanUserResizeColumns .  $��0, 20, 0, 0q�5)         AutoGenerateColumns .     
IsReadOnly . $
��auto��5,         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$
 Male��61   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6?   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6I   ��  �� Path$	 [0]�  5/      $ Female��61   $	 * 6A   
 6K   �� $	 [1]�51      52      53      �� 54      
���   �$��1q�56      $��-1, 0, 0, 0q�57      $	��180��58      ._�59      � .+�5:      �� $	��Age��5;      $��Center=�5<        5=      -   5        $ age_dg��5>      $
��auto��5?       .  $��0, 20, 0, 0q�5A       .   .  5D        5E      $ 18-24��61   $	 * 6@   
 6J   �� $	 [0]�  5F      $ 25-39��61   $	 * 6@   
 6J   �� $	 [1]�  5G      $ 40-59��61   $	 * 6@   
 6J   �� $	 [2]�  5H      $   60+��61   $	 * 6@   
 6J   �� $	 [3]�5J      5K      5L      �� 5M      
���   �$��1q�5O      $��-1, 0, 0, 0q�5P      $	��200��5Q      ._�5R      � .+�5S      �� $��
Cabin Type��5T      $��Center=�5U        5V      -   5        $ cabin_type_dg��5W      $��0, 20, 0, 0q�5X       .   .   .  5\        5]      $ Economy��61   $	 * 6B   
 6L   �� $	 [0]�  5^      $ Business��61   $	 * 6C   
 6M   �� $	 [1]�  5_      $ First��61   $	 * 6@   
 6J   �� $	 [2]�5a      5b      5c      �� 5d      
���   �$��1q�5f      $��-1, 0, 0, 0q�5g      $	��373��5h      ._�5i      � .+�5j      �� $��Destination Airport��5k      $��Center=�5l        5m      -   5        $ destination_dg��5n      $��0, 20, 0, 0q�5o       .   .   .  5s        5t      $	 AUH��61   $	 * 6>   
 6H   �� $	 [0]�  5u      $	 BAH��61   $	 * 6>   
 6H   �� $	 [1]�  5v      $	 DOH��61   $	 * 6>   
 6H   �� $	 [2]�  5w      $	 RUH��61   $	 * 6>   
 6H   �� $	 [3]�  5x      $	 CAI��61   $	 * 6>   
 6H   �� $	 [4]�5z      5{      5|      5}      5~      5      