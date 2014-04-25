using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace HraZivota
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int sloupce = 50;
        private int radky = 50;

        private int vyska = 700;
        private int sirka = 700;

        private Button[,] policka;

        private Grid mrizka;

        private bool[,] bunky;
        private bool[,] bunky2;
        private bool[,] stareBunky;
        private bool[,] stareBunky2;

        DispatcherTimer casovac;
       
        private int rychlost = 100;
        private int pocetKroku = 0;

        static private Brush barvaPozadi = Brushes.Gray;
        static private Brush barvaZiveBunky = Brushes.Red;
        static private Brush barvaZiveBunky2 = Brushes.Red;
        static private Brush barvaZiveBunky3 = Brushes.Red;
       
        Random nahoda = new Random();

        bool stabilni = false;

        string nazevTvaru;

        bool vybranyTvar = false;

        private int tvarX, tvarY;

        string[,] poleStringu;
        string[] poleStringuRadek;
        private int pocetRadku = 0;
        private int delkaRadku = 0;

        public MainWindow()
        {
            InitializeComponent();
            vykresleni(50);
            hraZivota();
            naplneniComboBoxu();
            inicializace();
        }

        private void vykresleni(int velikostMrizky)
        {
            //radky = mrizka.RowDefinitions.Count;
            //sloupce = mrizka.ColumnDefinitions.Count;

            //Grid.SetRow = sloupce;
            //Grid.SetColumn = radky;
            mrizka = new Grid();
            mrizka.Background = barvaPozadi;

            mrizka.Height = vyska;
            mrizka.Width = sirka;
            mrizka.HorizontalAlignment = HorizontalAlignment.Left;
            mrizka.VerticalAlignment = VerticalAlignment.Top;

            for (int i = 0; i < velikostMrizky; i++)//nejde parallel
            {
                mrizka.ColumnDefinitions.Add(new ColumnDefinition());
                mrizka.RowDefinitions.Add(new RowDefinition());
            }

            platno.Children.Add(mrizka);
            vykresleniTlacitek();

        }

        private void vykresleniTlacitek()
        {
            policka = new Button[sloupce, radky];
            bunky = new bool[sloupce, radky];
            bunky2 = new bool[sloupce, radky];
            stareBunky = new bool[sloupce, radky];
            stareBunky2 = new bool[sloupce, radky];

            for (int i = 0; i < sloupce; i++)//nejde parallel, chyba STA
            {
                for (int j = 0; j < radky; j++)
                {
                    policka[i, j] = new Button();
                    policka[i, j].Background = Brushes.Transparent;
                    policka[i, j].BorderBrush = Brushes.Black;
                    //policka[i, j].ClickMode = ClickMode.Hover;
                    /*if (chbMrizka.IsChecked == true)
                    {
                        policka[i, j].BorderBrush = Brushes.Black;
                        policka[i, j].BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                    }

                    if (chbMrizka.IsChecked == false)
                    {
                        policka[i, j].BorderBrush = Brushes.Transparent;
                        policka[i, j].BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
                    }*/
                    //policka[i, j].PreviewMouseMove += new MouseEventHandler(MainWindow_PreviewMouseMove);
                    policka[i, j].Click += new RoutedEventHandler(MainWindow_Click);
                    //policka[i, j].PreviewMouseMove += new MouseEventHandler(MainWindow_PreviewMouseMove);
                    policka[i, j].MouseRightButtonDown += new MouseButtonEventHandler(MainWindow_MouseRightButtonDown);


                    Grid.SetRow(policka[i, j], j);
                    Grid.SetColumn(policka[i, j], i);
                    mrizka.Children.Add(policka[i, j]);

                    bunky[i, j] = new bool();
                    bunky[i, j] = false;

                    bunky2[i, j] = new bool();
                    bunky2[i, j] = false;

                    stareBunky[i, j] = new bool();
                    stareBunky[i, j] = false;

                    stareBunky2[i, j] = new bool();
                    stareBunky2[i, j] = false;
                }
            }

            try
            {
                if (chbMrizka.IsChecked == true)
                {
                    zobrazeniMrizky(true);
                }

                if (chbMrizka.IsChecked == false)
                {
                    zobrazeniMrizky(false);
                }
            }
            catch (NullReferenceException)
            {

            }
        }

        void MainWindow_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                int x, y;
                Point body = new Point();
                body = Mouse.GetPosition(mrizka);
                x = (int)body.X / (sirka / sloupce);
                y = (int)body.Y / (vyska / radky);
                if (vybranyTvar == true)
                {
                    //nacteniSouboru();
                    vykresleniTvaru(x, y);
                    vybranyTvar = false;
                }
                else
                {
                    zivotBunka(x, y);
                    barvaPolicka();
                }
            }
        }

        void MainWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Brush barva = Brushes.YellowGreen;
            int x, y;
            Point body = new Point();
            body = Mouse.GetPosition(mrizka);
            x = (int)body.X / (sirka / sloupce);
            y = (int)body.Y / (vyska / radky);

            if (vybranyTvar == true)
            {
                try
                {
                    policka[x, y].Background = barva;
                    policka[x + tvarY - 1, y].Background = barva;
                    policka[x, y + tvarX - 1].Background = barva;
                    policka[x + tvarY - 1, y + tvarX - 1].Background = barva;
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
        }

        /*        void MainWindow_PreviewMouseMove(object sender, MouseEventArgs e)
                {
                    int x, y;
                    Point body = new Point();
                    body = Mouse.GetPosition(mrizka);
                    x = (int)body.X / (sirka / sloupce);
                    y = (int)body.Y / (vyska / radky);

                   /* if (vybranyTvar == true)
                    {
                        policka[x, y].Background = Brushes.Blue;
                        policka[x + tvarX, y].Background = Brushes.Blue;
                        policka[x, y + tvarY].Background = Brushes.Blue;
                        policka[x + tvarX, y + tvarY].Background = Brushes.Blue;
                    }
                }*/

        void MainWindow_Click(object sender, RoutedEventArgs e)
        {
            int x, y;
            Point body = new Point();
            body = Mouse.GetPosition(mrizka);
            x = (int)body.X / (sirka / sloupce);
            y = (int)body.Y / (vyska / radky);

            if (vybranyTvar == true)
            {
                vykresleniTvaru(x, y);
                vybranyTvar = false;
            }
            else
            {
                zivotBunka(x, y);
                barvaPolicka();
            }
        }

        private void zivotBunka(int x, int y)
        {
            try
            {
                if (bunky[x, y] == false)
                {
                    bunky[x, y] = true;
                }

                else
                {
                    bunky[x, y] = false;
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Vybraný bod je mimo použitelné pole", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /*private void barvaPolicka()
        {
            Task prvni = Task.Factory.StartNew(
                () => barvaPolickaTask(0, sloupce / 2-1, 0, radky / 2-1));
            Task druhy = Task.Factory.StartNew(
                () => barvaPolickaTask(sloupce / 2, sloupce , 0, radky / 2-1));
            Task treti = Task.Factory.StartNew(
                () => barvaPolickaTask(0, sloupce / 2-1, radky / 2, radky));
            Task ctvrty = Task.Factory.StartNew(
                () => barvaPolickaTask(sloupce / 2, sloupce, radky / 2, radky));

            Task.WaitAll(prvni,druhy,treti,ctvrty);
        }

        private void barvaPolickaTask(int zacatekDeleniSloupce, int konecDeleniSloupce, int zacatekDeleniRadky, int konecDeleniRadky)//vlakna
        {
            for (int i = 0; i < sloupce; i++)
            {
                for (int j = 0; j < radky; j++)
                {
                    if (bunky[i, j] == true)
                    {
                        if (stareBunky[i, j] == true)
                        {
                            if (stareBunky2[i, j] == true)
                            {
                                policka[i, j].Background = barvaZiveBunky;
                            }
                            else
                            {
                                policka[i, j].Background = barvaZiveBunky3;
                            }
                        }
                        else
                        {
                            policka[i, j].Background = barvaZiveBunky2;
                        }
                    }
                    else
                    {
                        policka[i, j].Background = Brushes.Transparent;
                    }
                }
            }
        }*/

        private void barvaPolicka()
        {
            for (int i = 0; i < sloupce; i++)//nejde parallel
            {
                for (int j = 0; j < radky; j++)
                {
                    if (bunky[i, j] == true)
                    {
                        if (stareBunky[i, j] == true)
                        {
                            if (stareBunky2[i, j] == true)
                            {
                                policka[i, j].Background = barvaZiveBunky;
                            }
                            else
                            {
                                policka[i, j].Background = barvaZiveBunky3;
                            }
                        }
                        else
                        {
                            policka[i, j].Background = barvaZiveBunky2;
                        }
                    }
                    else
                    {
                        policka[i, j].Background = Brushes.Transparent;
                    }
                }
            }
        }

        private int pocetSousedu(int x, int y)
        {
            int sousedu = 0;

            if (x - 1 >= 0 && bunky[x - 1, y] == true)
            {
                sousedu++;
            }
            if (x + 1 < radky && bunky[x + 1, y] == true)
            {
                sousedu++;
            }
            if (y - 1 >= 0 && bunky[x, y - 1] == true)
            {
                sousedu++;
            }
            if (y + 1 < sloupce && bunky[x, y + 1] == true)
            {
                sousedu++;
            }

            if (x - 1 >= 0 && y - 1 >= 0 && bunky[x - 1, y - 1] == true)
            {
                sousedu++;
            }
            if (x - 1 >= 0 && y + 1 < sloupce && bunky[x - 1, y + 1] == true)
            {
                sousedu++;
            }
            if (x + 1 < radky && y + 1 < sloupce && bunky[x + 1, y + 1] == true)
            {
                sousedu++;
            }
            if (x + 1 < radky && y - 1 >= 0 && bunky[x + 1, y - 1] == true)
            {
                sousedu++;
            }

            return sousedu;
        }

        private void pravidla()
        {
            pocetKroku++;
            
            for (int i = 0; i < sloupce; i++) // nejde použít parallel.for
            {
                for (int j = 0; j < radky; j++)
                {
                    int pocet = pocetSousedu(i, j);

                    if (chbPravidlo0.IsChecked == true)
                    {
                        if (pocet == 0)
                        {
                            switch (cbPravidla0.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (chbPravidlo1.IsChecked == true)
                    {
                        if (pocet == 1)
                        {
                            switch (cbPravidla1.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (chbPravidlo2.IsChecked == true)
                    {
                        if (pocet == 2)
                        {
                            switch (cbPravidla2.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (chbPravidlo3.IsChecked == true)
                    {
                        if (pocet == 3)
                        {
                            switch (cbPravidla3.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (chbPravidlo4.IsChecked == true)
                    {
                        if (pocet == 4)
                        {
                            switch (cbPravidla4.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (chbPravidlo5.IsChecked == true)
                    {
                        if (pocet == 5)
                        {
                            switch (cbPravidla5.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (chbPravidlo6.IsChecked == true)
                    {
                        if (pocet == 6)
                        {
                            switch (cbPravidla6.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (chbPravidlo7.IsChecked == true)
                    {
                        if (pocet == 7)
                        {
                            switch (cbPravidla7.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (chbPravidlo8.IsChecked == true)
                    {
                        if (pocet == 8)
                        {
                            switch (cbPravidla8.SelectedIndex)
                            {
                                case 0:
                                    bunky2[i, j] = true;
                                    break;
                                case 1:
                                    bunky2[i, j] = bunky[i, j];
                                    break;
                                case 2:
                                    bunky2[i, j] = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            /*for (int i = 0; i < sloupce; i++)
            {
                for (int j = 0; j < radky; j++)
                {
                    int pocet = pocetSousedu(i, j);

                    if (rPravidlo1.IsChecked == true)
                    {
                        if (pocet <= 1)
                        {
                            if (chbPravidlo1.IsChecked == true)
                            {
                                bunky2[i, j] = false;
                            }
                        }
                        if (pocet == 2)
                        {
                            if (chbPravidlo2.IsChecked == true)
                            {
                                bunky2[i, j] = bunky[i, j];
                            }
                        }

                        if (pocet == 3)
                        {
                            if (chbPravidlo3.IsChecked == true)
                            {
                                bunky2[i, j] = true;
                            }
                        }

                        if (pocet > 3)
                        {
                            if (chbPravidlo4.IsChecked == true)
                            {
                                bunky2[i, j] = false;
                            }
                        }
                    }

                    if (rPravidlo2.IsChecked == true)
                    {
                        if (pocet <= 1)
                        {
                            bunky2[i, j] = false;
                        }
                        if (pocet == 3)
                        {
                            bunky2[i, j] = bunky[i, j];
                        }

                        if (pocet == 2)
                        {
                            bunky2[i, j] = true;
                        }

                        if (pocet > 3)
                        {
                            bunky2[i, j] = false;
                        }
                    }
                    if (rPravidlo3.IsChecked == true)
                    {
                        if (pocet <= 1)
                        {
                            bunky2[i, j] = false;
                        }

                        if (pocet == 2)
                        {
                            bunky2[i, j] = false;
                        }

                        if (pocet == 3)
                        {
                            bunky2[i, j] = true;
                        }

                        if (pocet == 4)
                        {
                            bunky2[i, j] = bunky[i, j];
                        }

                        if (pocet > 4)
                        {
                            bunky2[i, j] = false;
                        }
                    }
                    if (rPravidlo4.IsChecked == true)
                    {
                        if (pocet <= 1)
                        {
                            bunky2[i, j] = false;
                        }

                        if (pocet == 2)
                        {
                            bunky2[i, j] = bunky[i, j];
                        }

                        if (pocet == 3)
                        {
                            bunky2[i, j] = true;
                        }

                        if (pocet == 4)
                        {
                            bunky2[i, j] = bunky[i, j];
                        }

                        if (pocet > 4)
                        {
                            bunky2[i, j] = false;
                        }
                    }
                }
            }*/

            stareBunky2 = stareBunky;
            stareBunky = bunky;

            bunky = new bool[sloupce, radky];

            Parallel.For(0, sloupce, i =>
                {
                    for (int j = 0; j < radky; j++)
                    {
                        bunky[i, j] = new bool();
                        bunky[i, j] = bunky2[i, j];
                    }
                }); 
            /*
            for (int i = 0; i < sloupce; i++)
            {
                for (int j = 0; j < radky; j++)
                {
                    bunky[i, j] = new bool();
                    bunky[i, j] = bunky2[i, j];
                }
            }*/

            barvaPolicka();

            if (chbKontrolaStability.IsChecked == true)
            {
                kontrolaRozvoje();
            }
        }

        private void kontrolaRozvoje()
        {
            bool pravda = false;

            for (int i = 0; i < sloupce; i++)
            {
                for (int j = 0; j < radky; j++)
                {
                    if (stareBunky2[i, j] != bunky2[i, j])
                    {
                        pravda = false;
                        return;
                    }
                    else
                    {
                        pravda = true;
                    }
                }
            }

            if (pravda == true)
            {
                stabilni = true;
                btnHraZivota.Content = "Start";
                casovac.Stop();
            }
        }

        private void stop()
        {
            if (pocetKroku == int.Parse(txbMaxPocetKroku.Text))
            {
                stabilni = true;
                btnHraZivota.Content = "Start";
                casovac.Stop();
            }
        }

        private void cbVelikostMrizky_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                platno.Children.Clear();
                mrizka.Children.Clear();
            }
            catch (NullReferenceException)
            {

            }


            if (cbVelikostMrizky.SelectedIndex == 0)
            {
                sloupce = 20;
                radky = 20;
                vykresleni(20);
            }

            if (cbVelikostMrizky.SelectedIndex == 1)
            {
                sloupce = 35;
                radky = 35;
                vykresleni(35);

            }

            if (cbVelikostMrizky.SelectedIndex == 2)
            {
                sloupce = 50;
                radky = 50;
                vykresleni(50);
            }

            if (cbVelikostMrizky.SelectedIndex == 3)
            {
                sloupce = 70;
                radky = 70;
                vykresleni(70);
            }

            if (cbVelikostMrizky.SelectedIndex == 4)
            {
                sloupce = 100;
                radky = 100;
                vykresleni(100);
            }

            if (cbVelikostMrizky.SelectedIndex == 5)
            {
                sloupce = 140;
                radky = 140;
                vykresleni(140);
            }
        }

        private void hraZivota()
        {
            casovac = new DispatcherTimer();
            casovac.Tick += new EventHandler(hraTick);
            casovac.Interval = new TimeSpan(0, 0, 0, 0, rychlost);
            casovac.Stop();
        }

        void hraTick(object sender, EventArgs e)
        {
            pravidla();
            //pocetKroku++;
            lPocetKroku.Content = "Počet kroků: " + pocetKroku;
            casovac.Interval = new TimeSpan(0, 0, 0, 0, rychlost);
            stop();
        }

        private void btnHraZivota_Click(object sender, RoutedEventArgs e)
        {
            if (casovac.IsEnabled)
            {
                casovac.Stop();
                btnHraZivota.Content = "Start";
            }
            else
            {
                casovac.Start();
                btnHraZivota.Content = "Stop";
            }
        }

        /*private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < radky; i++)
            {
                bunky[i, 25] = true;
                barvaPolicka();
            }
        }*/

        private void sRychlost_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lRychlost.Content = "Rychlost: " + (int)sRychlost.Value;
            rychlost = (int)sRychlost.Value;
        }

        private void cbTvary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int bunkaX = (radky / 2 + sloupce / 2) / 2;
            //vyčisti
            if (cbTvary.SelectedIndex == 0)
            {
                vymazat();
            }

            //R-pentomino
            if (cbTvary.SelectedIndex == 1)
            {
                bunky[bunkaX, bunkaX] = true;
                bunky[bunkaX, bunkaX - 1] = true;
                bunky[bunkaX, bunkaX - 2] = true;
                bunky[bunkaX - 1, bunkaX - 1] = true;
                bunky[bunkaX + 1, bunkaX] = true;
            }

            //Vodorovná čára
            if (cbTvary.SelectedIndex == 2)
            {
                for (int i = 0; i < radky; i++)
                {
                    bunky[i, (int)sloupce / 2] = true;
                }
            }

            //Pentadecathlon
            if (cbTvary.SelectedIndex == 3)
            {
                bunky[bunkaX - 4, bunkaX] = true;
                bunky[bunkaX - 3, bunkaX] = true;
                bunky[bunkaX - 2, bunkaX + 1] = true;
                bunky[bunkaX - 2, bunkaX - 1] = true;
                bunky[bunkaX - 1, bunkaX] = true;
                bunky[bunkaX, bunkaX] = true;
                bunky[bunkaX + 1, bunkaX] = true;
                bunky[bunkaX + 2, bunkaX] = true;
                bunky[bunkaX + 3, bunkaX + 1] = true;
                bunky[bunkaX + 3, bunkaX - 1] = true;
                bunky[bunkaX + 4, bunkaX] = true;
                bunky[bunkaX + 5, bunkaX] = true;
            }

            if (cbTvary.SelectedIndex == 4)
            {
                Parallel.For(0, sloupce, i =>

                //for (int i = 0; i < sloupce; i++)
                {
                    for (int j = 0; j < radky; j++)
                    {
                        bunky[i, j] = nahodnyZivot();
                    }
                });
            }

            //pure glider generator
            if (cbTvary.SelectedIndex == 5)
            {
                /*..O............
                  ..O............
                  OOO............
                  ...............
                  ......OOO......
                  .......O.......
                  ............OOO
                  ............O..
                  ............O..*/

                bunky[bunkaX - 5, bunkaX - 5] = true;
                bunky[bunkaX - 5, bunkaX - 4] = true;
                bunky[bunkaX - 7, bunkaX - 3] = true;
                bunky[bunkaX - 6, bunkaX - 3] = true;
                bunky[bunkaX - 5, bunkaX - 3] = true;

                bunky[bunkaX - 1, bunkaX - 1] = true;
                bunky[bunkaX, bunkaX - 1] = true;
                bunky[bunkaX + 1, bunkaX - 1] = true;
                bunky[bunkaX, bunkaX] = true;

                bunky[bunkaX + 5, bunkaX + 1] = true;
                bunky[bunkaX + 6, bunkaX + 1] = true;
                bunky[bunkaX + 7, bunkaX + 1] = true;
                bunky[bunkaX + 5, bunkaX + 2] = true;
                bunky[bunkaX + 5, bunkaX + 3] = true;
            }

            barvaPolicka();
            cbTvary.SelectedIndex = -1;
        }

        private bool nahodnyZivot()
        {
            bool zivot = Convert.ToBoolean(nahoda.Next() % 5);

            if (zivot)
            {
                return false;
            }
            return true;
        }

        private void btnKrokovaní_Click(object sender, RoutedEventArgs e)
        {
            pravidla();
            //pocetKroku++;
            lPocetKroku.Content = "Počet kroků: " + pocetKroku;
        }

        private void btnVymazat_Click(object sender, RoutedEventArgs e)
        {
            vymazat();
        }

        private void vymazat()
        {
            platno.Children.Clear();
            mrizka.Children.Clear();
            cbTvary.SelectedIndex = 0;
            //pocetKroku = 0;
            //lPocetKroku.Content = "Počet kroků: " + pocetKroku;

            if (sloupce == 20 && radky == 20)
            {
                vykresleni(20);
            }

            if (sloupce == 35 && radky == 35)
            {
                vykresleni(35);
            }

            if (sloupce == 50 && radky == 50)
            {
                vykresleni(50);
            }

            if (sloupce == 70 && radky == 70)
            {
                vykresleni(70);
            }

            if (sloupce == 100 && radky == 100)
            {
                vykresleni(100);
            }

            if (sloupce == 140 && radky == 140)
            {
                vykresleni(140);
            }
        }

        private void btnKonec_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult odpoved = MessageBox.Show("Opravdu chcete ukončit program?", "Ukončení", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (odpoved == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btnUlozeniObr_Click(object sender, RoutedEventArgs e)
        {
            string nazev;
            if (txbNazevTXT.Text == "")
            {
                DateTime cas = DateTime.Now;
                nazev = cas.Ticks.ToString();
            }
            else
            {
                nazev = txbNazevTXT.Text;
            }
            ulozeniObrazku(platno, "export_obr\\" + nazev + ".jpg");
        }

        private void ulozeniObrazku(Canvas platno, string nazevSouboru)
        {
            RenderTargetBitmap vykresleniBitmapy = new RenderTargetBitmap((int)platno.Width, (int)platno.Height, 96d, 96d, PixelFormats.Pbgra32);

            platno.Measure(new Size((int)platno.Width, (int)platno.Height));
            platno.Arrange(new Rect(new Size((int)platno.Width, (int)platno.Height)));

            vykresleniBitmapy.Render(platno);

            PngBitmapEncoder enkoder = new PngBitmapEncoder();
            enkoder.Frames.Add(BitmapFrame.Create(vykresleniBitmapy));

            using (FileStream file = File.Create(nazevSouboru))
            {
                enkoder.Save(file);
            }
        }

        private void chbMrizka_Checked(object sender, RoutedEventArgs e)
        {
            zobrazeniMrizky(true);
        }

        private void chbMrizka_Unchecked(object sender, RoutedEventArgs e)
        {
            zobrazeniMrizky(false);
        }

        private void zobrazeniMrizky(bool zobMrizky)
        {
            for (int i = 0; i < sloupce; i++)
            {
                for (int j = 0; j < radky; j++)
                {

                    if (zobMrizky == false)
                    {
                        //policka[i, j].BorderBrush = Brushes.Transparent;
                        policka[i, j].BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
                    }
                    else
                    {
                        //policka[i, j].BorderBrush = Brushes.Black;
                        policka[i, j].BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                    }
                }
            }
        }

        private void btnOkamzityVypocet_Click(object sender, RoutedEventArgs e)
        {
            if (chbKontrolaStability.IsChecked == true)
            {
                MessageBoxResult odpoved = MessageBox.Show("Okamžitý výpočet může chvíli trvat. Chcete pokračovat?", "Upozornění", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (odpoved == MessageBoxResult.Yes)
                {
                    while (stabilni == false)
                    {
                        pravidla();
                        kontrolaRozvoje();

                        lPocetKroku.Content = "Počet kroků:" + pocetKroku;

                        int maxPocetKroku = 0;

                        try
                        {
                            maxPocetKroku = int.Parse(txbMaxPocetKroku.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Mohou být zadané pouze číslice!", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }


                        if (pocetKroku >= maxPocetKroku)
                        {
                            MessageBox.Show("Daný model nebude stabilní po " + maxPocetKroku + " krocích.", "Zastavení", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }
                    }
                    MessageBox.Show("Výpočet se zastavil po " + pocetKroku + " krocích.", "Ukončení výpočtu", MessageBoxButton.OK, MessageBoxImage.Information);
                    stabilni = false;
                }
            }

            else
            {
                MessageBox.Show("Musí být zapnutá kontrola stability.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void btnVynulovatPocetKroku_Click(object sender, RoutedEventArgs e)
        {
            pocetKroku = 0;
            lPocetKroku.Content = "Počet kroků: " + pocetKroku;
        }

        private void nacteniSouboru()
        {
            string s;
            tvarX = 0;
            tvarY = 0;
            delkaRadku = 0;
            pocetRadku = 0;

            try
            {
                using (StreamReader sr = new StreamReader(@"tvary\\" + nazevTvaru, true))
                { }
            }

            catch (FileNotFoundException)
            {
                MessageBox.Show("Nebyl nalezen soubor s tvary.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (StreamReader sr = new StreamReader(@"tvary\\" + nazevTvaru, true))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    pocetRadku++;
                }
                tvarX = pocetRadku;
            }



            using (StreamReader sr = new StreamReader(@"tvary\\" + nazevTvaru, true))
            {
                s = sr.ReadLine();
                delkaRadku = s.Length;
                tvarY = delkaRadku;
            }

            if (pocetRadku > radky || delkaRadku > sloupce)
            {
                MessageBox.Show("Daný model nelze celý vykrelit na tuto velikost mřížky. Vyberte větší mřížku a opakujte znova.", "Uppozornění", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show("Počet řádku potřebných pro vykrelení je " + pocetRadku + " a sloupců " + delkaRadku, "Počet řádku a sloupců", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void vykresleniTvaru(int x, int y)
        {
            int bunkaX = (x / 2 + y / 2) / 2;
            poleStringu = new string[pocetRadku, delkaRadku];
            poleStringuRadek = new string[pocetRadku];

            using (StreamReader sr = new StreamReader(@"tvary\\" + nazevTvaru, true))
            {
                for (int i = 0; i < pocetRadku; i++)
                {
                    poleStringuRadek[i] = sr.ReadLine();
                    int j = 0;

                    try
                    {
                        foreach (char c in poleStringuRadek[i])
                        {
                            poleStringu[i, j] = c.ToString();
                            j++;
                        }
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
            }

            try
            {
                for (int i = 0; i < pocetRadku; i++)
                {
                    for (int j = 0; j < delkaRadku; j++)
                    {
                        if (poleStringu[i, j] == "O")
                        {
                            bunky[x + j, y + i] = true;
                        }

                        else { bunky[x + j, y + i] = false; }
                    }
                }
            }

            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Daný tvar nebude celý vykreslen, protože jeho část je mimo určenou oblast", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            barvaPolicka();
        }

        private void ulozeniTvaru(string nazev)
        {
            try
            {

            }
            catch (DirectoryNotFoundException)
            {
            }
            using (StreamWriter sw = new StreamWriter(@"tvary\\" + nazev))
            {
                for (int i = 0; i < sloupce; i++)
                {
                    for (int j = 0; j < radky; j++)
                    {
                        if (bunky[j, i] == true)
                        {
                            sw.Write("O");
                        }

                        else { sw.Write("."); }
                    }
                    sw.WriteLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(@"zdroj.txt",true))
            {
                sw.WriteLine(nazev);
            }
        }

        private void naplneniComboBoxu()
        {
            cbModely.Items.Clear();
            using (StreamReader sr = new StreamReader(@"zdroj.txt", true))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    cbModely.Items.Add(s);
                }
            }
        }

        private void cbModely_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                nazevTvaru = cbModely.SelectedItem.ToString();
                vybranyTvar = true;
                cbModely.SelectedIndex = -1;
                nacteniSouboru();
            }
            catch (NullReferenceException)
            {

            }
        }

        // 0 - ozije
        // 1 - zustane
        // 2 - umre

        private void rPravidlo1_Checked(object sender, RoutedEventArgs e)
        {
            chbPravidlo0.IsChecked = true;
            chbPravidlo1.IsChecked = true;
            chbPravidlo2.IsChecked = true;
            chbPravidlo3.IsChecked = true;
            chbPravidlo4.IsChecked = true;
            chbPravidlo5.IsChecked = true;
            chbPravidlo6.IsChecked = true;
            chbPravidlo7.IsChecked = true;
            chbPravidlo8.IsChecked = true;

            cbPravidla0.SelectedIndex = 2;
            cbPravidla1.SelectedIndex = 2;
            cbPravidla2.SelectedIndex = 1;
            cbPravidla3.SelectedIndex = 0;
            cbPravidla4.SelectedIndex = 2;
            cbPravidla5.SelectedIndex = 2;
            cbPravidla6.SelectedIndex = 2;
            cbPravidla7.SelectedIndex = 2;
            cbPravidla8.SelectedIndex = 2;
        }

        private void rPravidlo2_Checked(object sender, RoutedEventArgs e)
        {
            chbPravidlo0.IsChecked = true;
            chbPravidlo1.IsChecked = true;
            chbPravidlo2.IsChecked = true;
            chbPravidlo3.IsChecked = true;
            chbPravidlo4.IsChecked = true;
            chbPravidlo5.IsChecked = true;
            chbPravidlo6.IsChecked = true;
            chbPravidlo7.IsChecked = true;
            chbPravidlo8.IsChecked = true;

            cbPravidla0.SelectedIndex = 2;
            cbPravidla1.SelectedIndex = 2;
            cbPravidla2.SelectedIndex = 0;
            cbPravidla3.SelectedIndex = 1;
            cbPravidla4.SelectedIndex = 2;
            cbPravidla5.SelectedIndex = 2;
            cbPravidla6.SelectedIndex = 2;
            cbPravidla7.SelectedIndex = 2;
            cbPravidla8.SelectedIndex = 2;
        }

        private void rPravidlo3_Checked(object sender, RoutedEventArgs e)
        {
            chbPravidlo0.IsChecked = true;
            chbPravidlo1.IsChecked = true;
            chbPravidlo2.IsChecked = true;
            chbPravidlo3.IsChecked = true;
            chbPravidlo4.IsChecked = true;
            chbPravidlo5.IsChecked = true;
            chbPravidlo6.IsChecked = true;
            chbPravidlo7.IsChecked = true;
            chbPravidlo8.IsChecked = true;

            cbPravidla0.SelectedIndex = 2;
            cbPravidla1.SelectedIndex = 2;
            cbPravidla2.SelectedIndex = 2;
            cbPravidla3.SelectedIndex = 0;
            cbPravidla4.SelectedIndex = 1;
            cbPravidla5.SelectedIndex = 2;
            cbPravidla6.SelectedIndex = 2;
            cbPravidla7.SelectedIndex = 2;
            cbPravidla8.SelectedIndex = 2;
        }

        private void rPravidlo4_Checked(object sender, RoutedEventArgs e)
        {
            chbPravidlo0.IsChecked = true;
            chbPravidlo1.IsChecked = true;
            chbPravidlo2.IsChecked = true;
            chbPravidlo3.IsChecked = true;
            chbPravidlo4.IsChecked = true;
            chbPravidlo5.IsChecked = true;
            chbPravidlo6.IsChecked = true;
            chbPravidlo7.IsChecked = true;
            chbPravidlo8.IsChecked = true;

            cbPravidla0.SelectedIndex = 2;
            cbPravidla1.SelectedIndex = 2;
            cbPravidla2.SelectedIndex = 1;
            cbPravidla3.SelectedIndex = 0;
            cbPravidla4.SelectedIndex = 1;
            cbPravidla5.SelectedIndex = 2;
            cbPravidla6.SelectedIndex = 2;
            cbPravidla7.SelectedIndex = 2;
            cbPravidla8.SelectedIndex = 2;
        }

        private void rPravildo5_Checked(object sender, RoutedEventArgs e)
        {
            chbPravidlo0.IsChecked = true;
            chbPravidlo1.IsChecked = true;
            chbPravidlo2.IsChecked = true;
            chbPravidlo3.IsChecked = true;
            chbPravidlo4.IsChecked = true;
            chbPravidlo5.IsChecked = true;
            chbPravidlo6.IsChecked = true;
            chbPravidlo7.IsChecked = true;
            chbPravidlo8.IsChecked = true;

            cbPravidla0.SelectedIndex = nahodaPravidla();
            cbPravidla1.SelectedIndex = nahodaPravidla();
            cbPravidla2.SelectedIndex = nahodaPravidla();
            cbPravidla3.SelectedIndex = nahodaPravidla();
            cbPravidla4.SelectedIndex = nahodaPravidla();
            cbPravidla5.SelectedIndex = nahodaPravidla();
            cbPravidla6.SelectedIndex = nahodaPravidla();
            cbPravidla7.SelectedIndex = nahodaPravidla();
            cbPravidla8.SelectedIndex = nahodaPravidla();
        }

        private void rPravildo6_Checked(object sender, RoutedEventArgs e)
        {
            chbPravidlo0.IsChecked = true;
            chbPravidlo1.IsChecked = true;
            chbPravidlo2.IsChecked = true;
            chbPravidlo3.IsChecked = true;
            chbPravidlo4.IsChecked = true;
            chbPravidlo5.IsChecked = true;
            chbPravidlo6.IsChecked = true;
            chbPravidlo7.IsChecked = true;
            chbPravidlo8.IsChecked = true;

            cbPravidla0.SelectedIndex = -1;
            cbPravidla1.SelectedIndex = -1;
            cbPravidla2.SelectedIndex = -1;
            cbPravidla3.SelectedIndex = -1;
            cbPravidla4.SelectedIndex = -1;
            cbPravidla5.SelectedIndex = -1;
            cbPravidla6.SelectedIndex = -1;
            cbPravidla7.SelectedIndex = -1;
            cbPravidla8.SelectedIndex = -1;
        }

        private int nahodaPravidla()
        {
            int cislo;
            
            cislo = nahoda.Next(0,3);

            return cislo;
        }

        private void inicializace()
        {
            try
            {
                nazevTvaru = "Hra zivota";
                nacteniSouboru();
                vykresleniTvaru(2, 2);
            }
            catch (Exception)
            {
            }
        }

        private void btnNapoveda_Click(object sender, RoutedEventArgs e)
        {
            Napoveda napoveda = new Napoveda();
            napoveda.Show();
        }

        private void cbBarvy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBarvy.SelectedIndex == 0)
            {
                barvaZiveBunky = barvaZiveBunky2 = barvaZiveBunky3 = Brushes.Red;
            }

            if (cbBarvy.SelectedIndex == 1)
            {
                barvaZiveBunky = Brushes.Red;
                barvaZiveBunky2 = Brushes.Blue;
                barvaZiveBunky3 = Brushes.Black;
            }

            if (cbBarvy.SelectedIndex == 2)
            {
                barvaZiveBunky = Brushes.Red;
                barvaZiveBunky2 = Brushes.Red;
                barvaZiveBunky3 = Brushes.Yellow;
            }

            if (cbBarvy.SelectedIndex == 3)
            {
                barvaZiveBunky = barvaZiveBunky2 = barvaZiveBunky3 = Brushes.Black;
            }

            if (cbBarvy.SelectedIndex == 4)
            {
                barvaZiveBunky = Brushes.Black;
                barvaZiveBunky2 = Brushes.Black;
                barvaZiveBunky3 = Brushes.WhiteSmoke;
            }

            barvaPolicka();
        }

        private void btnUlozeniTxt_Click(object sender, RoutedEventArgs e)
        {
            string nazev;
            if (txbNazevTXT.Text == "")
            {
                DateTime cas = DateTime.Now;
                nazev = cas.Ticks.ToString();
            }
            else
            {
                nazev = txbNazevTXT.Text;
            }
            ulozeniTvaru(nazev);
            naplneniComboBoxu();
            txbNazevTXT.Text = "";
        }
    }
}