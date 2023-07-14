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

namespace RpgGame.DataModels;
public class Map: INotifyPropertyChanged
{
    public Grid MapGrid;
    private string? currentTileName = null;
    public string? CurrentTileName
    {
        get => currentTileName;
        set
        {
            currentTileName = value;
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTileName)));
            }
        }
    }

    private string? currentTileHasEnemy = null;
    public string? CurrentTileHasEnemy
    {
        get => currentTileHasEnemy;
        set
        {
            currentTileHasEnemy = value;
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTileHasEnemy)));
            }
        }
    }
    public MapTile[] mapTiles;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Map(Grid mapGrid, MapTile[] tiles)
    {
        this.MapGrid = mapGrid;
        this.mapTiles = tiles;
    }
}
