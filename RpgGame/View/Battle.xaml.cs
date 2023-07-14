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

namespace RpgGame.View
{
    /// <summary>
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : Window, INotifyPropertyChanged
    {
        Player player;
        Enemy enemy;

        private string? enemyHealth = null;
        public string? EnemyHealthInfo
        {
            get => enemyHealth;
            set
            {
                if (enemyHealth == null)
                {
                    enemyHealth = "Health: " + this.enemy.EnemyHealth.ToString() + "\n";
                }
                else
                {
                    enemyHealth = "Health: " + value + "\n";
                }

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(EnemyHealthInfo)));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public Battle(Player player, Enemy enemy)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.player = player;
            this.enemy = enemy;
            this.EnemyHealthInfo = this.enemy.EnemyHealth.ToString();
            this.EnemyInfoTextBox.DataContext = this;
            this.PlayerInfoTextBox.DataContext = this.player;

            this.PlayerAvatar.Source = this.player.PlayerModel.Source;
            this.EnemyAvatar.Source = this.enemy.EnemyModel.Source;
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
                this.player.CurrentPlayerInfo = this.player.PlayerHealth.ToString();
                Application.Current.MainWindow.IsEnabled = true;
                Application.Current.MainWindow.Activate();
            }
        }

        private void PlayerAttack()
        {
            this.enemy.EnemyHealth -= this.player.PlayerAttack;
            this.EnemyHealthInfo = this.enemy.EnemyHealth.ToString();
        }

        private void EnemyAttack()
        {
            this.player.PlayerHealth -= this.enemy.EnemyAttack;
            this.player.CurrentPlayerInfo = this.player.PlayerHealth.ToString();
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            this.PlayerAttack();

            if (enemy.EnemyHealth <= 0)
            {
                MessageBox.Show("Enemy died!");
                Close();
            }
            else
            {
                this.EnemyAttack();
            }
            

            if (player.PlayerHealth <= 0)
            {
                MessageBox.Show("You died!");
                Close();
            }
        }
    }
}
