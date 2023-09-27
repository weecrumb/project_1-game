using project_1_game.classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace project_1_game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public classes.PersonalInfo Player = new classes.PersonalInfo("crumb", 300, 50, 1, 0, 0, 10); 
        public List<classes.PersonalInfo> Enemies = new List<classes.PersonalInfo>();
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public classes.PersonalInfo Enemy;
        
        public MainWindow()
        {
            InitializeComponent();
            UserInfoPlayer();
            Enemies.Add(new classes.PersonalInfo("pupa", 100, 5, 1, 25, 10, 15));
            Enemies.Add(new classes.PersonalInfo("buba", 60, 7, 1, 25, 2, 5));
            Enemies.Add(new classes.PersonalInfo("mupa", 30, 3, 1, 10, 5, 10));

            // настройки для таймера
            dispatcherTimer.Tick += AttackPlayer;
            // задём интервал с которым выполняется таймер
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            // запуск таймера
            dispatcherTimer.Start();
            // вызываем метод выбора случайного противника
            SelectEnemy();
        }

        public void UserInfoPlayer()
        {
            // если lvl перса > чем 100* lvl перса
            if (Player.Score > 100 * Player.Level)
            {
                Player.Level++;
                Player.Score = 0;
                Player.Health += 100;
                Player.Damage++;
                Player.Armor++;
            }
            playerHealth.Content = "Жизненные показатели: " + Player.Health;
            playerArmor.Content = "Броня: " + Player.Armor;
            playerLevel.Content = "Уровень: " + Player.Level;
            playerScore.Content = "Опыт: " + Player.Score;
            playerMoney.Content = "Монеты: " + Player.Money;
        }

        public void SelectEnemy()
        {
            // выб случ индекс противника
            int Id = new Random().Next(0, Enemies.Count);
            // создаём экземпляр с данными противника
            Enemy = new classes.PersonalInfo(
                Enemies[Id].Name,
                Enemies[Id].Health,
                Enemies[Id].Armor,
                Enemies[Id].Level,
                Enemies[Id].Score,
                Enemies[Id].Money,
                Enemies[Id].Damage);

            // изменение изображений противника
            if (Id  == 0) { enemyImg.Source = new BitmapImage(new Uri("img/monster1.png", UriKind.Relative)); }
            if (Id == 1) { enemyImg.Source = new BitmapImage(new Uri("img/monster2.png", UriKind.Relative)); }
            if (Id == 2) { enemyImg.Source = new BitmapImage(new Uri("img/monster3.png", UriKind.Relative)); }
        }

        private void AttackPlayer(object sender, EventArgs e)
        {
            // наноси урон в процентном соотношении имеющейся броне
            Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
            
            //обновляем характеристики перса
            UserInfoPlayer();
            // добавляем метод окончания игры
            GameOver();
        }

        private void AttackEnemy(object sender, MouseButtonEventArgs e)
        {
            

            int CouterRandom = new Random().Next(0, 100);
            if (CouterRandom <= 20)
            {
                Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
                GameOver();
            }
            

            // наносим урон в процентном соотношении имеющейся брони
            Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));

            // если хп <= 0
            if (Enemy.Health <= 0)
            {
                Player.Score += Enemy.Score;
                Player.Money += Enemy.Money;
                // обновляем инфу на UI
                UserInfoPlayer();
                // выбираем нового врага
                SelectEnemy();
            }
            else
            {
                // обновляем UI перса
                enemyHealth.Content = "Жизненные показатели: " + Enemy.Health;
                enemyArmor.Content = "Броня: " + Enemy.Armor;
                enemyName.Content = "Имя: " + Enemy.Name;
            }
            


        }

        

        public void GameOver()
        {
            if (Player.Health <= 0)
            {
                playerHealth.Content = "Жизненные показатели: 0";
                MessageBox.Show("Game over");

                Player.Health = 300;
                Player.Armor = 50;
                Player.Level = 1;
                Player.Score = 0;
                Player.Money = 0;
                Player.Damage = 10;
                
                UserInfoPlayer();
                SelectEnemy();
            }
        }

        
    }
}
