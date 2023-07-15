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
public class Armour: INotifyPropertyChanged
{
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

    private string? armourDefenseInfo;
    public string? ArmourDefenseInfo
    {
        get => armourDefenseInfo;
        set
        {
            this.armourDefenseInfo = "Defense: " + value;
        }
    }

    private int armourDefense;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int ArmourDefense
    {
        get => armourDefense;
        set
        {
            this.armourDefense = value;
            this.ArmourDefenseInfo = value.ToString();
        }
    }

    public Armour(string? armourName, int armourDefense)
    {
        this.ArmourName = armourName;
        this.ArmourDefense = armourDefense;
    }
}
