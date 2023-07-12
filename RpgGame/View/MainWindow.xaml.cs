using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
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
using System.IO;
using RpgGame.DataModels;

namespace RpgGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Map map;
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

            string mapTilesPath = "C:\\Users\\PC\\Desktop\\Степит\\RpgGame\\RpgGame\\Assets\\MapTiles.json";
            string mapFromJson = File.ReadAllText(mapTilesPath);
            MapTile[] mapTiles = JsonSerializer.Deserialize<MapTile[]>(mapFromJson);

            map = new Map(this.MapGrid, mapTiles);
            BitmapImage[] tileSource = new BitmapImage[mapTiles.Count()];

            for (int i = 0; i < tileSource.Length; i++)
            {
                tileSource[i] = new BitmapImage(new Uri(mapTiles[i].TileURL, UriKind.Relative));
            }
            map.AddTilesToGrid(map.MapGrid.ColumnDefinitions.Count, map.MapGrid.RowDefinitions.Count, tileSource);
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

            this.KeyDown += MainWindow_KeyDown;
        }

        private void AddPlayerToMap()
        {
            Image newPlayer = Player.CreatePlayer();
            map.MapGrid.Children.Add(newPlayer);
            RegisterName("PlayerModel", newPlayer);
            Grid.SetRow(newPlayer, 0);
            Grid.SetColumn(newPlayer, 0);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S || e.Key == Key.A || e.Key == Key.D)
            {
                map.ChangePlayerPosition(e.Key);
            }
        }
    }
}
