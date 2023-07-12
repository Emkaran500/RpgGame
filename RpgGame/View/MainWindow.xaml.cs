using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RpgGame.DataModels;

namespace RpgGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Map map;
        private string column;
        public string Column
        {
            get => this.column;
            set
            {
                this.column = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Column)));
                }
            }
        }

        private string row;
        public string Row
        {
            get => this.row;
            set
            {
                this.row = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Row)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            map.MapGrid = this.MapGrid;
            MapMethods.AddTilesToGrid(map, map.MapGrid.ColumnDefinitions.Count, map.MapGrid.RowDefinitions.Count);
            this.AddPlayerToMap();

            if (this.MapGrid.FindName("PlayerModel") != null)
            {
                Image playerModel = map.MapGrid.FindName("PlayerModel") as Image;
                this.Row = Grid.GetRow(playerModel).ToString();
            }
            if (map.MapGrid.FindName("PlayerModel") != null)
            {
                Image playerModel = map.MapGrid.FindName("PlayerModel") as Image;
                this.Column = Grid.GetColumn(playerModel).ToString();
            }
            //this.Column = Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image).ToString();

            this.KeyDown += MainWindow_KeyDown;
        }

        private void AddPlayerToMap()
        {
            Image newPlayer = new Image();
            BitmapImage playerSource = new BitmapImage(new Uri("player_texture.png", UriKind.Relative));
            newPlayer.Source = playerSource;
            RegisterName("PlayerModel", newPlayer);
            map.MapGrid.Children.Add(newPlayer);
            Grid.SetRow(newPlayer, 0);
            Grid.SetColumn(newPlayer, 0);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S || e.Key == Key.A || e.Key == Key.D)
            {
                this.ChangePlayerPosition(e.Key);
            }
        }

        public void ChangePlayerPosition(Key key)
        {
            switch (key)
            {
                case Key.W:
                    {
                        if (Grid.GetRow(this.MapGrid.FindName("PlayerModel") as Image) > 0)
                        {
                            Grid.SetRow(this.MapGrid.FindName("PlayerModel") as Image, Grid.GetRow(this.MapGrid.FindName("PlayerModel") as Image) - 1);
                            this.Row = Grid.GetRow(this.MapGrid.FindName("PlayerModel") as Image).ToString();
                        }
                        break;
                    }
                case Key.S:
                    {
                        if (Grid.GetRow(this.MapGrid.FindName("PlayerModel") as Image) < this.MapGrid.RowDefinitions.Count - 1)
                        {
                            Grid.SetRow(this.MapGrid.FindName("PlayerModel") as Image, Grid.GetRow(this.MapGrid.FindName("PlayerModel") as Image) + 1);
                            this.Row = Grid.GetRow(this.MapGrid.FindName("PlayerModel") as Image).ToString();
                        }
                        break;
                    }
                case Key.A:
                    {
                        if (Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image) > 0)
                        {
                            Grid.SetColumn(this.MapGrid.FindName("PlayerModel") as Image, Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image) - 1);
                            Column = Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image).ToString();
                        }
                        break;
                    }
                case Key.D:
                    {
                        if (Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image) < this.MapGrid.ColumnDefinitions.Count - 1)
                        {
                            Grid.SetColumn(this.MapGrid.FindName("PlayerModel") as Image, Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image) + 1);
                            Column = Grid.GetColumn(this.MapGrid.FindName("PlayerModel") as Image).ToString();
                        }
                        break;
                    }
            }
        }
    }
}
