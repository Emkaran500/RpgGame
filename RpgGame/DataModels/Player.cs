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
public class Player : Character
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

    public string PlayerName { get; set; } = "PlayerModel";
    public Image PlayerModel { get; set; }
    public static string PlayerURL { get; set; } = "player_texture.png";
    public static Image CreatePlayer()
    {
        Image newPlayer = new Image();
        BitmapImage playerSource = new BitmapImage(new Uri(PlayerURL, UriKind.Relative));
        newPlayer.Source = playerSource;

        return newPlayer;
    }
}
