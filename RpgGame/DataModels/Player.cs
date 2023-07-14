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
public class Player : Character, INotifyPropertyChanged
{
    private int playerHealth = baseHealth;
    public int PlayerHealth 
    {
        get => this.playerHealth;
        set => this.playerHealth = value; 
    }

    private int playerAttack = baseAttack;
    public int PlayerAttack
    {
        get => this.playerAttack;
        set => this.playerAttack = value;
    }

    public static Player Instance
    {
        get => new Player();
    }

    public string PlayerName { get; set; } = BaseInfo.playerName;
    public Image PlayerModel { get; set; }
    public static string PlayerURL { get; set; } = BaseInfo.playerSource;

    private string? currentPlayerInfo;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? CurrentPlayerInfo
    {
        get => currentPlayerInfo;
        set
        {
            if (currentPlayerInfo == null)
            {
                currentPlayerInfo = "Health: " + this.playerHealth.ToString() + "\n";
            }
            else
            {
                currentPlayerInfo = "Health: " + value + "\n";
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPlayerInfo)));
            }
        }
    }

    private Player()
    {
        this.PlayerModel = new Image();
        BitmapImage playerSource = new BitmapImage(new Uri(PlayerURL, UriKind.Relative));
        this.PlayerModel.Source = playerSource;
        this.CurrentPlayerInfo = $"Health: {this.playerHealth}";
    }

    public void ChangePlayerPosition(Map map, Key key)
    {
        switch (key)
        {
            case Key.W:
                {
                    if (Grid.GetRow(this.PlayerModel) > 0)
                    {
                        Grid.SetRow(this.PlayerModel, Grid.GetRow(this.PlayerModel) - 1);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
            case Key.S:
                {
                    if (Grid.GetRow(this.PlayerModel) < map.MapGrid.RowDefinitions.Count - 1)
                    {
                        Grid.SetRow(this.PlayerModel, Grid.GetRow(this.PlayerModel) + 1);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
            case Key.A:
                {
                    if (Grid.GetColumn(this.PlayerModel) > 0)
                    {
                        Grid.SetColumn(this.PlayerModel, Grid.GetColumn(this.PlayerModel) - 1);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
            case Key.D:
                {
                    if (Grid.GetColumn(this.PlayerModel) < map.MapGrid.ColumnDefinitions.Count - 1)
                    {
                        Grid.SetColumn(this.PlayerModel, Grid.GetColumn(this.PlayerModel) + 1);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
        }
    }
}
