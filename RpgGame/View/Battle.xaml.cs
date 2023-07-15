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

namespace RpgGame.View
{
    /// <summary>
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : Window, INotifyPropertyChanged
    {
        private int lostHealth = 0;
        private int round = 1;
        private Player player;
        private Enemy enemy;
        public ObservableCollection<Log> BattleLog { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Battle(Player player, Enemy enemy)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.player = player;
            this.enemy = enemy;
            this.BattleLog = new ObservableCollection<Log>();
            this.EnemyHealthInfoTextBox.DataContext = this.enemy;
            this.EnemyNameInfoTextBox.DataContext = this.enemy;
            this.PlayerHealthInfoTextBox.DataContext = this.player;
            this.PlayerNameInfoTextBox.DataContext = this.player;
            this.BattleLogBox.DataContext = this;

            this.PlayerAvatar.Source = this.player.CharacterModel.Source;
            this.EnemyAvatar.Source = this.enemy.CharacterModel.Source;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (player.CharacterHealth != 0 && enemy.CharacterHealth != 0)
            {
                MessageBox.Show("You can't leave until one of you is dead!");
                e.Cancel = true;
            }
            else
            {
                this.player.CurrentPlayerHealthInfo = this.player.CharacterHealth.ToString();
                Application.Current.MainWindow.IsEnabled = true;
                Application.Current.MainWindow.Activate();
            }
        }

        private void PlayerAttack()
        {
            this.enemy.CharacterHealth -= this.player.CharacterAttack;
            this.enemy.CurrentEnemyHealthInfo = this.enemy.CharacterHealth.ToString();
            BattleLog.Add(new Log(round, "attacked", player, enemy));
        }

        private void EnemyAttack()
        {
            this.player.CharacterHealth -= this.enemy.CharacterAttack;
            this.player.CurrentPlayerHealthInfo = this.player.CharacterHealth.ToString();
            this.lostHealth += this.enemy.CharacterAttack;
            BattleLog.Add(new Log(round, "attacked", enemy, player));
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            this.PlayerAttack();

            if (enemy.CharacterHealth <= 0)
            {
                enemy.LifeStatus = LifeStatus.dead;
                Random rand = new Random();
                int gainedXP = rand.Next(60, 101);
                this.player.Level.XP += gainedXP;
                this.player.CharacterHealth += this.lostHealth / 2;
                this.player.CurrentPlayerHealthInfo = this.player.CharacterHealth.ToString();
                string weaponsPath = "Assets\\Weapons.json";
                string weaponsJson = File.ReadAllText(weaponsPath);
                Weapon[] weapons = JsonSerializer.Deserialize<Weapon[]>(weaponsJson);
                MessageBox.Show($"Enemy died!\nYou gained {gainedXP} XP!\nAlso you restored half of your health.");
                Close();
            }
            else
            {
                this.EnemyAttack();
                round++;
            }
            

            if (player.CharacterHealth <= 0)
            {
                player.LifeStatus = LifeStatus.dead;
                MessageBox.Show("You died!");
                Close();
            }
        }
    }
}
