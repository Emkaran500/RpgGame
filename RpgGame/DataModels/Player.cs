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

namespace RpgGame.DataModels;
public class Player : Character, INotifyPropertyChanged
{
    public LevelSystem Level { get; set; } = new LevelSystem(level: 1, xp: 0);
    public ObservableCollection<Weapon> Weapons { get; set; } = new ObservableCollection<Weapon>();
    public ObservableCollection<Armour> Armours { get; set; } = new ObservableCollection<Armour>();

    public Weapon currentWeapon;
    public Armour currentArmour;

    private string? weaponName;
    public string? WeaponName
    {
        get => this.weaponName;
        set
        {
            this.weaponName = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(WeaponName)));
            }
        }
    }

    private string? armourName;
    public string? ArmourName
    {
        get => this.armourName;
        set
        {
            this.armourName = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ArmourName)));
            }
        }
    }

    private static Player player;

    private int playerDefense;
    public int PlayerDefense
    {
        get => this.playerDefense;
        set => this.playerDefense = value;
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

    private string? currentPlayerHealthInfo;

    public string? CurrentPlayerHealthInfo
    {
        get => currentPlayerHealthInfo;
        set
        {
            if (currentPlayerHealthInfo == null)
            {
                currentPlayerHealthInfo = "\n" + "Health: " + this.CharacterHealth.ToString() + "\n";
            }
            else
            {
                currentPlayerHealthInfo = "\n" + "Health: " + value + "\n";
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPlayerHealthInfo)));
            }
        }
    }

    private string? currentPlayerAttackInfo;
    public string? CurrentPlayerAttackInfo
    {
        get => this.currentPlayerAttackInfo;
        set
        {
            if (this.currentPlayerAttackInfo == null)
            {
                this.currentPlayerAttackInfo = "Damage: " + this.CharacterAttack.ToString() + "\n";
            }
            else
            {
                this.currentPlayerAttackInfo = "Damage: " + value + "\n";
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPlayerAttackInfo)));
            }
        }
    }

    public static string PlayerURL { get; set; } = BaseInfo.playerSource;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int playerRow;
    public int playerColumn;

    private Player()
    {
        this.Weapons.Add(new Weapon("Stick", 10));
        this.Armours.Add(new Armour("Rubber Armour", 10));
        this.LifeStatus = LifeStatus.alive;
        this.CharacterName = BaseInfo.playerName;

        if (this.currentWeapon == null)
        {
            this.CharacterAttack = baseAttack;
        }
        else
        {
            this.CharacterAttack = baseAttack + this.currentWeapon.WeaponDamage;
        }

        if (this.currentArmour == null)
        {
            this.CharacterHealth = baseHealth + (this.Level.Level - 1) * 10;
        }
        else
        {
            this.CharacterHealth = baseHealth + this.currentArmour.ArmourDefense + (this.Level.Level - 1) * 10;
        }
        
        this.CharacterModel = new Image();
        BitmapImage playerSource = new BitmapImage(new Uri(PlayerURL, UriKind.Relative));
        this.CharacterModel.Source = playerSource;
        this.CurrentPlayerHealthInfo = this.CharacterHealth.ToString();
        this.CurrentPlayerAttackInfo = this.CharacterAttack.ToString();
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
