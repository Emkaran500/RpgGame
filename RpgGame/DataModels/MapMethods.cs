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
    public static void AddTilesToGrid(this Map map, int gridColumns, int gridRows)
    {
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                map.newTile = new Image();
                BitmapImage tileSource = new BitmapImage(new Uri("grass_tile_1.png", UriKind.Relative));
                map.newTile.Source = tileSource;
                map.MapGrid.Children.Add(map.newTile);
                Grid.SetRow(map.newTile, i);
                Grid.SetColumn(map.newTile, j);
            }
        }
    }
}
