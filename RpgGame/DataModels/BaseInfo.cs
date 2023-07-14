using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.DataModels;

public enum LifeStatus
{
    dead = 0,
    alive = 1,
}

public abstract class BaseInfo
{
    public const string playerName = "Player";
    public const string playerSource = "player_texture.png";
    public const string enemyName = "Bandit";
    public const string enemySource = "enemy_texture.png";
    public const int numOfEnemies = 5;
}

