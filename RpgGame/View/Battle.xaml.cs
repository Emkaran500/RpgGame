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

namespace RpgGame.View
{
    /// <summary>
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : Window, INotifyPropertyChanged
    {
        int round = 1;
        Player player;
        Enemy enemy;
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
            if (player.PlayerHealth != 0 && enemy.EnemyHealth != 0)
            {
                MessageBox.Show("You can't leave until one of you is dead!");
                e.Cancel = true;
            }
            else
            {
                this.player.CurrentPlayerHealthInfo = this.player.PlayerHealth.ToString();
                Application.Current.MainWindow.IsEnabled = true;
                Application.Current.MainWindow.Activate();
            }
        }

        private void PlayerAttack()
        {
            this.enemy.EnemyHealth -= this.player.CharacterAttack;
            BattleLog.Add(new Log(round, "attacked", player, enemy));
        }

        private void EnemyAttack()
        {
            this.player.PlayerHealth -= this.enemy.CharacterAttack;
            BattleLog.Add(new Log(round, "attacked", enemy, player));
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            this.PlayerAttack();

            if (enemy.EnemyHealth <= 0)
            {
                enemy.LifeStatus = LifeStatus.dead;
                MessageBox.Show("Enemy died!");
                Close();
            }
            else
            {
                this.EnemyAttack();
                round++;
            }
            

            if (player.PlayerHealth <= 0)
            {
                player.LifeStatus = LifeStatus.dead;
                MessageBox.Show("You died!");
                Close();
            }
        }
    }
}
