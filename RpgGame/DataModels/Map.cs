﻿using System;
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
public class Map
{
    public Grid MapGrid;
    public MapTile[] mapTiles;

    public Map(Grid mapGrid, MapTile[] tiles)
    {
        this.MapGrid = mapGrid;
        this.mapTiles = tiles;
    }
}
