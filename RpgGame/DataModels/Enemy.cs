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

namespace RpgGame.DataModels;
public class Enemy : Character
{
    private int enemyHeath = baseHealth;
    public int EnemyHeath
    {
        get => enemyHeath;
        set => enemyHeath = value;
    }

    private int enemyAttack = baseAttack;
    public int EnemyAttack
    {
        get => enemyAttack;
        set => enemyAttack = value;
    }

    public string EnemyName { get; set; } = "EnemyModel";
    public Image EnemyModel { get; set; }
    public static string EnemyURL { get; set; } = "enemy_texture.png";

    public Enemy()
    {
        this.EnemyModel = new Image();
        BitmapImage enemySource = new BitmapImage(new Uri(EnemyURL, UriKind.Relative));
        this.EnemyModel.Source = enemySource;
    }
}
