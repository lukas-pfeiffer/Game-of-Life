   M S B A M L   `   `   `  ����  	HraZivota    HraZivota.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$$  Hra života - Lukáš Pfeiffer��5   	   $	��764��6-   $
��1409��6:    � 
ResizeMode$ CanMinimize=�6G    � WindowStartupLocation$ CenterScreen=�6`   .��5      � ��5   
   �� 5       �� EndPoint$ 1,0.5&�6"    �� 
StartPoint$ 0,0.5&�63   .��5      �  � Color$ Black��6    � Offset$ 0Q�6-   � 5	      $ 
DodgerBlue��6   $ 1Q�62   5
      .+�5   
   �� -   5        $��platno��5   L   $	��700��6   $
��Left=�6   $��	12,12,0,0q�69   $	��Top=�6Z   $	��700��6r   6�   �� 5   
   -   5        $��btnHraZivota��5   ]   $��Start��6   $��23��6!   $
��Left=�6-   $��736,202,0,0q�6H   $	��Top=�6q   $	��149��6�   6�   �� 5   
   -   5        $��	lRychlost��5   `   $��	Rychlost:��6   $��28��6$   $
��Left=�60   $��736,384,0,0q�6K   $	��Top=�6q   $	��149��6�   �� 5   
   -   5        $��cbVelikostMrizky��5   O   $��27��6   $
��Left=�6   $��833,419,0,0q�6:   $	��Top=�6g   $��85��6    �� SelectedIndex$ 2��6�   .@�5      �� $��20 x 20��6   �� 5      $��35 x 35��6    �� 
IsSelected .  6-   �� 5      $��50 x 50��6   �� 5      $��70 x 70��6   �� 5      $��	100 x 100��6   �� 5      $��	140 x 140��6   5      �� 5   
   -   5        $��	sRychlost��5   M   $��23��6   $
��Left=�6   $��823,389,0,0q�68   $	��Top=�6^   $	��217��6v   	 � Maximum$		 300Q�6�   
 � Minimum$
 0Q�6�    � Value$	 150Q�6�    � LargeChange$ 10Q�6�   �� 5   
   -   5        $��label1��5   g   $��Velikost mřížky:��6   $��28��6+   $
��Left=�67   $��736,418,0,0q�6R   $	��Top=�6u   $	��149��6�   �� 5   
   -   5        $��cbTvary��5   O   $��23��6   $
��Left=�6   $��941,452,0,0q�6:   $	��Top=�6^   $��99��6v   .@�5      �� $��	Vyčistit��6   �� 5      $��R-pentomino��6   �� 5      $��Vodorovná čára��6   �� 5      $��Pentadecathlon��6   �� 5      $��Nahoda��6   �� 5      $��Generator lodí��6   5       �� 5!   
   -   5        $��lPocetKroku��5!   e   $��Počet Kroků: 0��6   $��28��6)   $
��Left=�65   $��736,168,0,0q�6P   $	��Top=�6x   $	��149��6�   �� 5"   
   -	   5        $��btnKrokovaní��5"   a   $��Krokování��6   $��23��6%   $
��Left=�61   $��891,202,0,0q�6L   $	��Top=�6u   $	��149��6�   6�   �� 5#   
   -
   5        $��
btnVymazat��5#   f   $��Vymazat plochu��6   $��23��6*   $
��Left=�66   $��736,355,0,0q�6Q   $	��Top=�6x   $	��149��6�   6�   �� 5$   
   -   5        $��btnKonec��5$   ]   $��Konec��6   $��23��6!   $
��Left=�6-   $��890,509,0,0q�6H   $	��Top=�6m   $	��149��6�   6�   �� 5%   
   -   5        $��btnUlozeniObr��5%   2   $��
Ulozit JPG��6   $��23��6&   $	��149��6G   $��736,326,0,0q�6S   $
��Left=�6�   $	��Top=�6�   �� 5&   
   -   5        $��label2��5&   !   $��Tvary:��6   $��736,447,0,0q�6/   $
��Left=�6D   $��43��6_   $��28��6j   $	��Top=�6v   �� 5'   
   -   5        $��	chbMrizka��5'   j   $��Zobrazení mřížky��6   $��16��6.   $
��Left=�6:   $��924,424,0,0q�6U   $	��Top=�6{   $	��116��6�    d� 	IsChecked$ FalseY�6�   6�   �� 5(   
   -   5        $��label3��5(   0   $��	Pravidla:��6   $��28��6$   $	��149��6>   $��1065,168,0,0q�6J   $
��Left=�6`   $	��Top=�6{   	� 5)   
   -   5        $��
rPravidlo1��5)   d   $��Conway��6   $��16��6'   $
��Left=�63   $��1085,192,0,0q�6N   $	��Top=�6v   $	��103��6�   6�   	� 5*   
   -   5        $��
rPravidlo2��5*   f   $��! Conway��6   $��16��6)   $
��Left=�65   $��1085,214,0,0q�6P   $	��Top=�6x   $	��103��6�   6�   	� 5+   
   -   5        $��
rPravidlo3��5+   h   $��
Osci-Pento��6   $��16��6+   $
��Left=�67   $��1181,192,0,0q�6R   $	��Top=�6z   $	��102��6�   6�   	� 5,   
   -   5        $��
rPravidlo4��5,   1   $
��Maze��6   $��16��6%   $	��102��6C   $��1181,214,0,0q�6O   $	��Top=�6e   $
��Left=�6}   6�   �� 5-   
   -   5        $��chbKontrolaStability��5-   <   $��Kontrola stability��6   $��16��60   $	��142��6X   $��891,236,0,0q�6d   $	��Top=�6y   $
��Left=�6�   �� 5.   
   -   5        $��btnOkamzityVypocet��5.   h   $��Okamžitý výpočet��6   $��23��6,   $
��Left=�68   $��736,231,0,0q�6S   $	��Top=�6�   $	��149��6�   6�   �� 5/   
   -   5        $��btnVynulovatPocetKroku��5/   m   $��Vynulovat počet kroků��6   $��23��61   $
��Left=�6=   $��891,355,0,0q�6X   $	��Top=�6�   $	��149��6�   6�   �� 50   
   -   5        $��label4��50   m   $ ��Maximální počet kroků:��6   $��28��61   $
��Left=�6=   $��736,258,0,0q�6X   $	��Top=�6{   $	��149��6�   �� 51   
   -   5        $��txbMaxPocetKroku��51   N   $��23��6   $
��Left=�6   $��891,262,0,0q�69   $	��Top=�6f   $	��149��6~   $��0��6�   �� 52   
   -   5        $��cbModely��52      $��23��6   $	��149��6/   $��786,452,0,0q�6;   $	��Top=�6P   $
��Left=�6h   6�   	� 53   
   -   5        $��
rPravidlo5��53   d   $��Náhoda��6   $��16��6'   $
��Left=�63   $��1270,192,0,0q�6N   $	��Top=�6v   $��89��6�   �� 54   
   -   5        $��chbPravidlo1��54   a   $��23��6   $
��Left=�6   $
 TrueY�6:   $��1085,306,0,0q�6K   $	��Top=�6u   $��83��6�   $��1��6�   $��Right=�6�   $��Center=�6�   �� 55   
   -   5        $��chbPravidlo2��55   a   $��23��6   $
��Left=�6   $
 TrueY�6:   $��1085,335,0,0q�6K   $	��Top=�6u   $��83��6�   $��2��6�   $��Right=�6�   $��Center=�6�   �� 56   
   -   5        $��chbPravidlo3��56   a   $��23��6   $
��Left=�6   $
 TrueY�6:   $��1085,364,0,0q�6K   $	��Top=�6u   $��83��6�   $��3��6�   $��Right=�6�   $��Center=�6�   �� 57   
   -   5        $��chbPravidlo4��57   0   $��23��6   $
 TrueY�6   $��83��6D   $��1085,393,0,0q�6O   $	��Top=�6e   $
��Left=�6}   $��4��6�   $��Right=�6�   $��Center=�6�   �� 58   
   -   5        $��chbPravidlo5��58   0   $��23��6   $
 TrueY�6   $��83��6D   $��1085,422,0,0q�6O   $	��Top=�6e   $
��Left=�6}   $��5��6�   $��Right=�6�   $��Center=�6�   �� 59   
   -    5        $��chbPravidlo6��59   0   $��23��6   $
 TrueY�6   $��83��6D   $��1085,451,0,0q�6O   $	��Top=�6e   $
��Left=�6}   $��6��6�   $��Right=�6�   $��Center=�6�   �� 5:   
   -!   5        $��chbPravidlo7��5:   0   $��23��6   $
 TrueY�6   $��83��6D   $��1085,480,0,0q�6O   $	��Top=�6e   $
��Left=�6}   $��7��6�   $��Right=�6�   $��Center=�6�   �� 5;   
   -"   5        $��chbPravidlo8��5;   0   $��23��6   $
 TrueY�6   $��83��6D   $��1085,509,0,0q�6O   $	��Top=�6e   $
��Left=�6}   $��8��6�   $��Right=�6�   $��Center=�6�   �� 5<   
   -#   5        $��label5��5<   �   $A��;Aktivní   |   počet sousedů   |                 činnost��6   $��28��6R   $
��Left=�6^   $��1065,243,0,0q�6y   $	��Top=�6�   $	��303��6�   �� 5=   
   -$   5        $��cbPravidla1��5=   P   $��23��6   $
��Left=�6   $��1230,306,0,0q�6:   $	��Top=�6c   $	��130��6{   .@�5>      �� $��ožije��6   �� 5?      $��zůstane��6   �� 5@      $��umře��6   5A      �� 5B   
   -%   5        $��cbPravidla2��5B   P   $��23��6   $
��Left=�6   $��1230,335,0,0q�6:   $	��Top=�6c   $	��130��6{   .@�5C      �� $��ožije��6   �� 5D      $��zůstane��6   �� 5E      $��umře��6   5F      �� 5G   
   -&   5        $��cbPravidla3��5G      $��23��6   $	��130��62   $��1230,364,0,0q�6>   $
��Left=�6T   $	��Top=�6o   .@�5H      �� $��ožije��6   �� 5I      $��zůstane��6   �� 5J      $��umře��6   5K      �� 5L   
   -'   5        $��cbPravidla4��5L      $��23��6   $	��130��62   $��1230,393,0,0q�6>   $
��Left=�6T   $	��Top=�6o   .@�5M      �� $��ožije��6   �� 5N      $��zůstane��6   �� 5O      $��umře��6   5P      �� 5Q   
   -(   5        $��cbPravidla5��5Q      $��23��6   $	��130��62   $��1230,422,0,0q�6>   $
��Left=�6T   $	��Top=�6o   .@�5R      �� $��ožije��6   �� 5S      $��zůstane��6   �� 5T      $��umře��6   5U      �� 5V   
   -)   5        $��cbPravidla6��5V      $��23��6   $	��130��62   $��1230,451,0,0q�6>   $
��Left=�6T   $	��Top=�6o   .@�5W      �� $��ožije��6   �� 5X      $��zůstane��6   �� 5Y      $��umře��6   5Z      �� 5[   
   -*   5        $��cbPravidla7��5[      $��23��6   $	��130��62   $��1230,480,0,0q�6>   $
��Left=�6T   $	��Top=�6o   .@�5\      �� $��ožije��6   �� 5]      $��zůstane��6   �� 5^      $��umře��6   5_      �� 5`   
   -+   5        $��cbPravidla8��5`      $��23��6   $	��130��62   $��1230,509,0,0q�6>   $
��Left=�6T   $	��Top=�6o   .@�5a      �� $��ožije��6   �� 5b      $��zůstane��6   �� 5c      $��umře��6   5d      �� 5e   
   -,   5        $��chbPravidlo0��5e   �   $��0��6   $��23��6   $
��Left=�6+   $��Right=�6F   $
 TrueY�6i   $��1085,277,0,0q�6z   $	��Top=�6�   $��Center=�6�   $��83��6�   �� 5f   
   --   5        $��cbPravidla0��5f   P   $��23��6   $
��Left=�6   $��1230,277,0,0q�6:   $	��Top=�6c   $	��130��6{   .@�5g      �� $��ožije��6   �� 5h      $��zůstane��6   �� 5i      $��umře��6   5j      �� 5k   
   -.   5        $��border1��5k   x   
�������$��1q�6&   $	��301��6:   $
��Left=�6G   $��1065,243,0,0q�6b   $	��Top=�6�   $	��303��6�   6�   �� 5l   
   -/   5        $��btnNapoveda��5l   0   $��
Nápověda��6   $��23��6$   $	��149��6C   $��736,509,0,0q�6O   $	��Top=�6d   $
��Left=�6|   6�   �� 5m   
   -0   5        $��cbBarvy��5m   O   $��23��6   $
��Left=�6   $��786,480,0,0q�6:   $	��Top=�6^   $	��149��6v   $ 1��6�   .@�5n      �� $��	Červená��6   �� 5o      $ ��Červená, modrá, černá��6   �� 5p      $��Červená, žlutá��6   �� 5q      $��Černá��6   �� 5r      $��Černobílá��6   5s      	� 5t   
   -1   5        $��
rPravidlo6��5t   4   $��Vlastní��6   $��16��6(   $��1269,214,0,0q�6F   $
��Left=�6\   $��85��6w   $	��Top=�6�   6�   �� 5u   
   -2   5        $��border3��5u   F   
�������$��1q�6&   $��69��6:   $	��303��6U   $��1065,168,0,0q�6a   $
��Left=�6w   $	��Top=�6�   6�   �� 5v   
   -3   5        $��label6��5v   -   $��Barvy:��6   $��28��6!   $��43��6;   $��736,475,0,0q�6F   $
��Left=�6[   $	��Top=�6v   �� 5w   
   -4   5        $��border2��5w   w   
�������$��1q�6&   $	��376��6:   $
��Left=�6G   $��728,168,0,0q�6b   $	��Top=�6�   $	��323��6�   �� 5x   
   -5   5        $��image1��5x      $	��150��6   $
��Fill=�6+   $	��450��6:   $*��$/HraZivota;component/Images/logo.png��6F   $��
823,12,0,0q�6t   $
��Left=�6�   $	��Top=�6�   �� 5y   
   -6   5        $��btnUlozeniTxt��5y   b   $��
Ulozit TXT��6   $��23��6&   $
��Left=�62   $��891,326,0,0q�6M   $	��Top=�6w   $	��149��6�   6�   �� 5z   
   -7   5        $��label7��5z   i   $��Název pro uložení:��6   $��28��6-   $
��Left=�69   $��736,292,0,0q�6T   $	��Top=�6w   $	��149��6�   �� 5{   
   -8   5        $��txbNazevTXT��5{   N   $��23��6   $
��Left=�6   $��891,296,0,0q�69   $	��Top=�6a   $	��149��6y   5|      5}      