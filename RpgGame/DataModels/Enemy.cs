using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
