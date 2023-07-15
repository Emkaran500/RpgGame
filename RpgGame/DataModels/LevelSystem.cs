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
public class LevelSystem: INotifyPropertyChanged
{
    private string? levelInfo;
    public string? LevelInfo
    {
        get => this.levelInfo;
        set
        {
            this.levelInfo = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(LevelInfo)));
            }
        }
    }

    private string? xpInfo;
    public string? XPInfo
    {
        get => this.xpInfo;
        set
        {
            this.xpInfo = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(XPInfo)));
            }
        }
    }

    private int level = 1;
    public int Level
    {
        get => this.level;
        set
        {
            this.level = value;
            this.LevelInfo = "Level: " + this.Level.ToString();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private int xp = 0;
    public int XP
    {
        get => this.xp;
        set
        {
            if (this.xp + value >= 100)
            {
                this.xp = this.xp + value - 100;
                this.Level++;
            }
            else
            {
                this.xp += value;
            }
            this.XPInfo = "XP: " + this.xp.ToString();
        }
    }

    public LevelSystem(int level, int xp)
    {
        this.Level = level;
        this.XP = xp;
    }
}
