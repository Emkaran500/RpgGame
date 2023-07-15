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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        Player player { get; set; }
        ListBoxItem selectedListBoxItem;

        public Inventory()
        {
            InitializeComponent();
            this.player = Player.Instance;
            this.DataContext = this.player;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.MainWindow.IsEnabled = true;
            Application.Current.MainWindow.Activate();
        }

        private void ArmoursListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.ArmoursListBox.SelectedItem;
            this.selectedListBoxItem = this.ArmoursListBox.ItemContainerGenerator.ContainerFromItem(selectedItem) as ListBoxItem;
        }

        private void WeaponsListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.WeaponsListBox.SelectedItem;
            this.selectedListBoxItem = this.WeaponsListBox.ItemContainerGenerator.ContainerFromItem(selectedItem) as ListBoxItem;
        }

        private void EquipButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedListBoxItem != null)
            {
                if (this.selectedListBoxItem.Content is Weapon)
                {
                    Weapon weapon = this.selectedListBoxItem.Content as Weapon;
                    this.player.currentWeapon = weapon;
                    this.player.WeaponName = weapon.WeaponName;
                    this.player.CharacterAttack = Character.baseAttack + this.player.currentWeapon.WeaponDamage;
                    this.player.CurrentPlayerAttackInfo = this.player.CharacterAttack.ToString();
                }
                else if (this.selectedListBoxItem.Content is Armour)
                {
                    Armour armour = this.selectedListBoxItem.Content as Armour;
                    if (this.player.currentArmour == null)
                    {
                        this.player.currentArmour = armour;
                        this.player.CharacterHealth = this.player.CharacterHealth + this.player.currentArmour.ArmourDefense;
                    }
                    else
                    {
                        this.player.CharacterHealth = this.player.CharacterHealth - this.player.currentArmour.ArmourDefense + armour.ArmourDefense;
                        if (this.player.CharacterHealth <= 0)
                        {
                            this.player.CharacterHealth = 1;
                        }
                    }
                    this.player.ArmourName = armour.ArmourName;
                    this.player.CurrentPlayerHealthInfo = this.player.CharacterHealth.ToString();
                }
            }
        }

        private void UnequipButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedListBoxItem != null)
            {
                if (this.selectedListBoxItem.Content is Weapon)
                {
                    Weapon weapon = this.selectedListBoxItem.Content as Weapon;
                    if (weapon == this.player.currentWeapon)
                    {
                        this.player.currentWeapon = null;
                        this.player.WeaponName = null;
                        this.player.CharacterAttack = Character.baseAttack;
                        this.player.CurrentPlayerAttackInfo = this.player.CharacterAttack.ToString();
                    }
                }
                else if (this.selectedListBoxItem.Content is Armour)
                {
                    Armour armour = this.selectedListBoxItem.Content as Armour;
                    if (armour == this.player.currentArmour)
                    {
                        this.player.currentArmour = null;
                        this.player.ArmourName = null;

                        if (this.player.CharacterHealth - armour.ArmourDefense <= 0)
                        {
                            this.player.CharacterHealth = 1;
                        }
                        else
                        {
                            this.player.CharacterHealth -= armour.ArmourDefense;
                        }
                        this.player.CurrentPlayerHealthInfo = this.player.CharacterHealth.ToString();
                    }
                }
            }
        }
    }
}
