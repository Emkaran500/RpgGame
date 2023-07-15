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
public abstract class Character: INotifyPropertyChanged
{
    private LifeStatus lifeStatus = LifeStatus.dead;
    public LifeStatus LifeStatus
    {
        get => this.lifeStatus;
        set => this.lifeStatus = value;
    }
    public const int baseHealth = 100;
    public const int baseAttack = 10;
    public string CharacterName { get; set; }

    private int characterAttack = baseAttack;
    public int CharacterAttack
    {
        get => this.characterAttack;
        set => this.characterAttack = value;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private int characterHealth = baseHealth;

    public int CharacterHealth
    {
        get => this.characterHealth;
        set
        {
            this.characterHealth = value;
        }
    }

    public Image CharacterModel { get; set; }
}
