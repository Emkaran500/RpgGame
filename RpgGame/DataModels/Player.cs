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
public class Player : Character, INotifyPropertyChanged
{
    private static Player player;

    private int playerHealth = baseHealth;
    public int PlayerHealth 
    {
        get => this.playerHealth;
        set
        {
            this.playerHealth = value;
            this.CurrentPlayerHealthInfo = value.ToString();
        }
    }

    public static Player Instance
    {
        get
        {
            if (player == null)
            {
                player = new Player();
            }
            return player;
        }

    }

    public static string PlayerURL { get; set; } = BaseInfo.playerSource;

    public event PropertyChangedEventHandler? PropertyChanged;

    private string? currentPlayerHealthInfo;

    public string? CurrentPlayerHealthInfo
    {
        get => currentPlayerHealthInfo;
        set
        {
            if (currentPlayerHealthInfo == null)
            {
                currentPlayerHealthInfo = "Health: " + this.playerHealth.ToString() + "\n";
            }
            else
            {
                currentPlayerHealthInfo = "Health: " + value + "\n";
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPlayerHealthInfo)));
            }
        }
    }

    public int playerRow;
    public int playerColumn;

    private Player()
    {
        this.LifeStatus = LifeStatus.alive;
        this.CharacterName = BaseInfo.playerName;
        this.CharacterAttack = baseAttack;
        this.CharacterModel = new Image();
        BitmapImage playerSource = new BitmapImage(new Uri(PlayerURL, UriKind.Relative));
        this.CharacterModel.Source = playerSource;
        this.PlayerHealth = baseHealth;
    }

    public bool CheckPlayerAlive()
    {
        if (this.LifeStatus == LifeStatus.dead)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ChangePlayerPosition(Map map, Key key)
    {
        switch (key)
        {
            case Key.W:
                {
                    if (Grid.GetRow(this.CharacterModel) > 0)
                    {
                        Grid.SetRow(this.CharacterModel, Grid.GetRow(this.CharacterModel) - 1);
                        this.playerRow = Grid.GetRow(this.CharacterModel);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
            case Key.S:
                {
                    if (Grid.GetRow(this.CharacterModel) < map.MapGrid.RowDefinitions.Count - 1)
                    {
                        Grid.SetRow(this.CharacterModel, Grid.GetRow(this.CharacterModel) + 1);
                        this.playerRow = Grid.GetRow(this.CharacterModel);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
            case Key.A:
                {
                    if (Grid.GetColumn(this.CharacterModel) > 0)
                    {
                        Grid.SetColumn(this.CharacterModel, Grid.GetColumn(this.CharacterModel) - 1);
                        this.playerColumn = Grid.GetColumn(this.CharacterModel);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
            case Key.D:
                {
                    if (Grid.GetColumn(this.CharacterModel) < map.MapGrid.ColumnDefinitions.Count - 1)
                    {
                        Grid.SetColumn(this.CharacterModel, Grid.GetColumn(this.CharacterModel) + 1);
                        this.playerColumn = Grid.GetColumn(this.CharacterModel);
                        map.ShowTileInfo(this);
                    }
                    break;
                }
        }
    }
}
