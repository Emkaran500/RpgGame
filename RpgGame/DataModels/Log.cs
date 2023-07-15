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
public class Log
{
    private Character combatant1;
    private Character combatant2;
    private ImageSource Combatant1Source;
    private ImageSource Combatant2Source;

    private string round;
    public string Round
    {
        get => this.round;
        set => this.round = "Round: " + value;
    }

    private string action;
    public string Action
    {
        get => this.action;
        set => this.action = combatant1.CharacterName + " " + value + " " + combatant2.CharacterName;
    }

    private string damage;
    public string Damage
    {
        get => this.damage;
        set => this.damage = value;
    }

    public BitmapImage ActionImageSource { get; set; }

    public Log(int round, string? Action, Character combatant1, Character combatant2)
    {
        this.Round = round.ToString();
        this.combatant1 = combatant1;
        this.combatant2 = combatant2;
        this.Action = Action;
        this.Damage = this.combatant1.CharacterAttackInfo;

        if (this.combatant1 is Player)
        {
            this.ActionImageSource = new BitmapImage(new Uri(BaseInfo.attackImageSource, UriKind.Relative));
        }
        else
        {
            this.ActionImageSource = new BitmapImage(new Uri(BaseInfo.defenseImageSource, UriKind.Relative));
        }
    }
}
