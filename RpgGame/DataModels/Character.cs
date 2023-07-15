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
public abstract class Character
{
    private LifeStatus lifeStatus = LifeStatus.dead;
    public LifeStatus LifeStatus
    {
        get => this.lifeStatus;
        set => this.lifeStatus = value;
    }
    protected const int baseHealth = 100;
    protected const int baseAttack = 10;
    public string CharacterName { get; set; }

    private int characterAttack = baseAttack;
    public int CharacterAttack
    {
        get => this.characterAttack;
        set => this.characterAttack = value;
    }

    public string CharacterAttackInfo
    {
        get => "Damage: " + this.characterAttack.ToString();
    }

    public Image CharacterModel { get; set; }
}
