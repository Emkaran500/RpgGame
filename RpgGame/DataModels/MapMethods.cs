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

namespace RpgGame.DataModels;
public static class MapMethods
{
    public static void AddTilesToGrid(this Map map, int gridColumns, int gridRows, BitmapImage[] tileSource)
    {
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                map.mapTiles[(i * gridColumns) + j].tileModel.Source = tileSource[(i * gridColumns) + j];
                map.mapTiles[(i * gridColumns) + j].tileModel.Stretch = Stretch.Fill;
                map.MapGrid.Children.Add(map.mapTiles[(i * gridColumns) + j].tileModel);
                Grid.SetRow(map.mapTiles[(i * gridColumns) + j].tileModel, i);
                Grid.SetColumn(map.mapTiles[(i * gridColumns) + j].tileModel, j);
            }
        }
    }

    public static void ShowTileInfo (this Map map, Player player)
    {
        if (map.MapGrid.FindName(player.PlayerName) != null)
        {
            Image currentPlayer = map.MapGrid.FindName(player.PlayerName) as Image;
            int currentPosition = (Grid.GetRow(currentPlayer) * map.MapGrid.ColumnDefinitions.Count) + Grid.GetColumn(currentPlayer);
            map.CurrentTileName = map.mapTiles[currentPosition].TileName;
            if (Enemy.oldPositionPairs.Any(numPair => numPair == (map.mapTiles[currentPosition].TileRow, map.mapTiles[currentPosition].TileColumn)))
            {
                map.CurrentTileHasEnemy = "Has Enemy in Tile: " + true.ToString();
            }
            else
            {
                map.CurrentTileHasEnemy = "Has Enemy in Tile: " + false.ToString();
            }
        }
    }

    public static void ChangePlayerPosition(this Map map, Player player, Key key)
    {
        switch (key)
        {
            case Key.W:
                {
                    if (Grid.GetRow(player.PlayerModel) > 0)
                    {
                        Grid.SetRow(player.PlayerModel, Grid.GetRow(player.PlayerModel) - 1);
                        map.ShowTileInfo(player);
                    }
                    break;
                }
            case Key.S:
                {
                    if (Grid.GetRow(player.PlayerModel) < map.MapGrid.RowDefinitions.Count - 1)
                    {
                        Grid.SetRow(player.PlayerModel, Grid.GetRow(player.PlayerModel) + 1);
                        map.ShowTileInfo(player);
                    }
                    break;
                }
            case Key.A:
                {
                    if (Grid.GetColumn(player.PlayerModel) > 0)
                    {
                        Grid.SetColumn(player.PlayerModel, Grid.GetColumn(player.PlayerModel) - 1);
                        map.ShowTileInfo(player);
                    }
                    break;
                }
            case Key.D:
                {
                    if (Grid.GetColumn(player.PlayerModel) < map.MapGrid.ColumnDefinitions.Count - 1)
                    {
                        Grid.SetColumn(player.PlayerModel, Grid.GetColumn(player.PlayerModel) + 1);
                        map.ShowTileInfo(player);
                    }
                    break;
                }
        }
    }
}
