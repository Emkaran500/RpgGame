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

    public static void ChangePlayerPosition(this Map map, Key key)
    {
        switch (key)
        {
            case Key.W:
                {
                    if (Grid.GetRow(map.MapGrid.FindName("PlayerModel") as Image) > 0)
                    {
                        Grid.SetRow(map.MapGrid.FindName("PlayerModel") as Image, Grid.GetRow(map.MapGrid.FindName("PlayerModel") as Image) - 1);
                    }
                    break;
                }
            case Key.S:
                {
                    if (Grid.GetRow(map.MapGrid.FindName("PlayerModel") as Image) < map.MapGrid.RowDefinitions.Count - 1)
                    {
                        Grid.SetRow(map.MapGrid.FindName("PlayerModel") as Image, Grid.GetRow(map.MapGrid.FindName("PlayerModel") as Image) + 1);
                    }
                    break;
                }
            case Key.A:
                {
                    if (Grid.GetColumn(map.MapGrid.FindName("PlayerModel") as Image) > 0)
                    {
                        Grid.SetColumn(map.MapGrid.FindName("PlayerModel") as Image, Grid.GetColumn(map.MapGrid.FindName("PlayerModel") as Image) - 1);
                    }
                    break;
                }
            case Key.D:
                {
                    if (Grid.GetColumn(map.MapGrid.FindName("PlayerModel") as Image) < map.MapGrid.ColumnDefinitions.Count - 1)
                    {
                        Grid.SetColumn(map.MapGrid.FindName("PlayerModel") as Image, Grid.GetColumn(map.MapGrid.FindName("PlayerModel") as Image) + 1);
                    }
                    break;
                }
        }
    }
}
