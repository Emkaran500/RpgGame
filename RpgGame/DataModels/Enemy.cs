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
public class Enemy : Character, INotifyPropertyChanged
{
    private string? currentEnemyHealthInfo;

    public string? CurrentEnemyHealthInfo
    {
        get => currentEnemyHealthInfo;
        set
        {
            if (currentEnemyHealthInfo == null)
            {
                currentEnemyHealthInfo = "\n" + "Health: " + this.CharacterHealth.ToString() + "\n";
            }
            else
            {
                currentEnemyHealthInfo = "\n" + "Health: " + value + "\n";
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentEnemyHealthInfo)));
            }
        }
    }

    private string? enemyAttackInfo;
    public string? EnemyAttackInfo
    {
        get => this.enemyAttackInfo;
        set
        {
            if (this.enemyAttackInfo == null)
            {
                this.enemyAttackInfo = "\n" + "Health: " + this.CharacterAttack.ToString() + "\n";
            }
            else
            {
                this.enemyAttackInfo = "\n" + "Health: " + value + "\n";
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(EnemyAttackInfo)));
            }
        }
    }

    public int enemyRow;
    public int enemyColumn;

    public static string EnemyURL { get; set; } = BaseInfo.enemySource;
    public static (int, int)[] oldPositionPairs = new (int, int)[0];

    public event PropertyChangedEventHandler? PropertyChanged;

    public Enemy(int enemyRow, int enemyColumn)
    {
        this.LifeStatus = LifeStatus.alive;
        this.CharacterName = BaseInfo.enemyName;
        this.CharacterAttack = baseAttack;
        this.CharacterModel = new Image();
        BitmapImage enemySource = new BitmapImage(new Uri(EnemyURL, UriKind.Relative));
        this.CharacterModel.Source = enemySource;
        this.enemyRow = enemyRow;
        this.enemyColumn = enemyColumn;
        this.CharacterHealth = baseHealth + 50;
        this.CurrentEnemyHealthInfo = this.CharacterHealth.ToString();
    }
}
