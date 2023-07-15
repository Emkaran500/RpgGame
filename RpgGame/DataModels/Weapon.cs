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
public class Weapon: INotifyPropertyChanged
{
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

    private string? weaponDamageInfo;
    public string? WeaponDamageInfo
    {
        get => this.weaponDamageInfo;
        set
        {
            this.weaponDamageInfo = "Damage: " + value;
        }
    }

    private int weaponDamage;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int WeaponDamage
    {
        get => weaponDamage;
        set
        {
            this.weaponDamage = value;
            this.WeaponDamageInfo = value.ToString();
        }
    }

    public Weapon(string? weaponName, int weaponDamage)
    {
        this.WeaponName = weaponName;
        this.WeaponDamage = weaponDamage;
    }
}
