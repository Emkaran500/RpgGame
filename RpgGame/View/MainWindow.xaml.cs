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
using RpgGame.View;
using System.Collections.ObjectModel;
using System.Threading;

namespace RpgGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Battle battleWindow;
        private Inventory inventoryWindow;

        public Map map;
        public Player player = Player.Instance;
        public ObservableCollection<Enemy> enemies = new ObservableCollection<Enemy>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.WindowState = WindowState.Maximized;

            string mapTilesPath = "Assets\\MapTiles.json";
            string mapFromJson = File.ReadAllText(mapTilesPath);
            MapTile[] mapTiles = JsonSerializer.Deserialize<MapTile[]>(mapFromJson);

            map = new Map(this.MapGrid, mapTiles);
            BitmapImage[] tileSource = new BitmapImage[mapTiles.Count()];

            for (int i = 0; i < tileSource.Length; i++)
            {
                tileSource[i] = new BitmapImage(new Uri(mapTiles[i].TileURL, UriKind.Relative));
            }
            map.AddTilesToGrid(map.MapGrid.ColumnDefinitions.Count, map.MapGrid.RowDefinitions.Count, tileSource);
            this.AddEnemiesToMap(BaseInfo.numOfEnemies);
            this.AddPlayerToMap();
            this.map.ShowTileInfo(this.player);
            this.TileInfo.DataContext = this.map;
            this.HasEnemyInfo.DataContext = this.map;
            this.PlayerHealthInfo.DataContext = this.player;
            this.PlayerLevelInfo.DataContext = this.player.Level;
            this.PlayerXPInfo.DataContext = this.player.Level;
            this.AttackButtonAccessability();
            this.KeyDown += MainWindow_KeyDown;
        }

        private void AddEnemiesToMap(int numOfEnemies)
        {
            for (int i = 0; i < numOfEnemies; i++)
            {
                Random random = new Random();
                int newRowPosition = random.Next(1, this.MapGrid.RowDefinitions.Count);
                int newColumnPosition = random.Next(1, this.MapGrid.ColumnDefinitions.Count);

                if (Enemy.oldPositionPairs.Any(numPair => numPair != (0, 0)))
                {
                    while (Enemy.oldPositionPairs.Any(numPair => numPair == (newRowPosition, newColumnPosition)))
                    {
                        newRowPosition = random.Next(1, this.MapGrid.RowDefinitions.Count);
                        newColumnPosition = random.Next(1, this.MapGrid.ColumnDefinitions.Count);
                    }
                }

                Enemy.oldPositionPairs = Enemy.oldPositionPairs.Append((newRowPosition, newColumnPosition)).ToArray<(int, int)>();
                
                Enemy newEnemy = new Enemy(newRowPosition, newColumnPosition);
                map.MapGrid.Children.Add(newEnemy.CharacterModel);
                enemies.Add(newEnemy);
                RegisterName(newEnemy.CharacterName + $"{i + 1}", newEnemy.CharacterModel);
                Grid.SetRow(newEnemy.CharacterModel, newRowPosition);
                Grid.SetColumn(newEnemy.CharacterModel, newColumnPosition);
            }
        }

        private void DeletePlayer()
        {
            this.map.MapGrid.Children.Remove(this.player.CharacterModel);
            this.map.ShowTileInfo(this.player);
            MessageBox.Show("Please restart the game to start anew.");
            System.Environment.Exit(0);
        }

        private void DeleteEnemy()
        {
            Enemy? enemyForRemove = enemies.FirstOrDefault(enemy => enemy.LifeStatus == LifeStatus.dead, null);
            if (enemyForRemove != null)
            {
                map.MapGrid.Children.Remove(enemyForRemove.CharacterModel);
                (int, int) enemyForRemovePosition = (enemyForRemove.enemyRow, enemyForRemove.enemyColumn);
                Enemy.oldPositionPairs = Enemy.oldPositionPairs.Where(enemyPosition => enemyPosition != enemyForRemovePosition).ToArray();
                this.enemies = new ObservableCollection<Enemy>(enemies.Where(enemy => enemy.LifeStatus != LifeStatus.dead));
                this.AttackButtonAccessability();
                this.map.ShowTileInfo(this.player);
                Window.GetWindow(this).Focus();
                if (this.enemies.Count <= 0)
                {
                    MessageBox.Show("You have won! Congratulations!");
                    System.Environment.Exit(0);
                }
            }
        }

        private void AddPlayerToMap()
        {
            map.MapGrid.Children.Add(this.player.CharacterModel);
            RegisterName(this.player.CharacterName, this.player.CharacterModel);
            this.player.playerRow = 0;
            this.player.playerColumn = 0;
            Grid.SetRow(this.player.CharacterModel, this.player.playerRow);
            Grid.SetColumn(this.player.CharacterModel, this.player.playerColumn);
        }

        private void AttackButtonAccessability()
        {
            int currentPosition = (Grid.GetRow(this.player.CharacterModel) * map.MapGrid.ColumnDefinitions.Count) + Grid.GetColumn(this.player.CharacterModel);
            if (Enemy.oldPositionPairs.Any(numPair => numPair == (map.mapTiles[currentPosition].TileRow, map.mapTiles[currentPosition].TileColumn)))
            {
                this.AttackButton.IsEnabled = true;
            }
            else
            {
                this.AttackButton.IsEnabled = false;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S || e.Key == Key.A || e.Key == Key.D)
            {
                this.player.ChangePlayerPosition(this.map, e.Key);
            }
            this.AttackButtonAccessability();
        }

        private void AttackEnemyClick(object sender, RoutedEventArgs e)
        {
            (int, int) enemyPosition = Enemy.oldPositionPairs.First(numPair => numPair == (this.player.playerRow, this.player.playerColumn));
            Enemy enemy = enemies.First(enemy => enemy.enemyRow == enemyPosition.Item1 && enemy.enemyColumn == enemyPosition.Item2);
            this.battleWindow = new Battle(this.player, enemy);
            this.GameWindow.IsEnabled = false;
            this.battleWindow.ShowInTaskbar = false;
            this.battleWindow.Owner = Application.Current.MainWindow;
            bool? isClosed = this.battleWindow.ShowDialog();

            if (isClosed != null)
            {
                if (player.CheckPlayerAlive() == false)
                {
                    this.DeletePlayer();
                }
                else
                {
                    this.DeleteEnemy();
                }
            }
        }

        private void InventoryOpenClick(object sender, RoutedEventArgs e)
        {
            inventoryWindow = new Inventory();
            this.GameWindow.IsEnabled = false;
            inventoryWindow.ShowInTaskbar = false;
            inventoryWindow.Owner = Application.Current.MainWindow;
            inventoryWindow.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
