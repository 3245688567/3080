using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Threading;


namespace game
{
    public partial class MainWindow : Window
    {
        private Player player = new Player();
        private Sun sun = new Sun();
        private List<Enemy> enemies=new List<Enemy>();

        private DispatcherTimer gameTimer =new DispatcherTimer();
        readonly Random random = new Random();
        private bool up, down, moveLeft, moveRight;

        public MainWindow()
        {
            InitializeComponent();

            player = CreatePlayer(player);
            sun = CreateSun(sun);
            enemies = CreateEnemies(enemies,100);//number of enemies

            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
            
            GameScreen.Focus();
        }
        private void GameLoop(object sender,EventArgs e)
        {
            if(moveLeft==true&&Canvas.GetLeft(player.ellipse) >0)
            {
                Canvas.SetLeft(player.ellipse, Canvas.GetLeft(player.ellipse) -1);
            }
            if (moveRight == true && Canvas.GetLeft(player.ellipse) +25 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player.ellipse, Canvas.GetLeft(player.ellipse) + 1);
            }
            if (up == true && Canvas.GetTop(player.ellipse) >0 )
            {
                Canvas.SetTop(player.ellipse, Canvas.GetTop(player.ellipse) - 1);
            }
            if (down == true && Canvas.GetTop(player.ellipse) + 25 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player.ellipse, Canvas.GetTop(player.ellipse) + 1);
            }
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left) { moveLeft = false; }
            if (e.Key == Key.Right) { moveRight = false; }
            if (e.Key == Key.Up) { up = false; }
            if (e.Key == Key.Down) { down = false; }
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) { moveLeft = true; }
            if (e.Key == Key.Right) { moveRight = true; }
            if (e.Key == Key.Up) { up = true; }
            if (e.Key == Key.Down) { down = true; }
        }
        private Player CreatePlayer(Player player)
        {
            player.ellipse.Width = player.ellipse.Height = player.Size;
            player.ellipse.Fill = Brushes.LightBlue;

            GameScreen.Children.Add(player.ellipse);
            Canvas.SetLeft(player.ellipse, 700);
            Canvas.SetTop(player.ellipse, 300);
            
            
            return player;
        }
        private Sun CreateSun(Sun sun)
        {   

            sun.ellipse.Width = sun.ellipse.Height = sun.Size;
            sun.ellipse.Fill = Brushes.Red;
            GameScreen.Children.Add(sun.ellipse);
            
            Canvas.SetLeft(sun.ellipse, (Application.Current.MainWindow.Width - sun.Size) / 2);
            Canvas.SetTop(sun.ellipse, (Application.Current.MainWindow.Height - sun.Size) / 2);
                
            return sun;
        }
        private List<Enemy> CreateEnemies(List<Enemy> enemyList,int count)
        {

            int randx, randy;
            for (int i = 0; i < count; i++)
            {
                Enemy enemy = new Enemy();
                enemy.ellipse.Width = enemy.ellipse.Height = enemy.Size;
                enemy.ellipse.Fill = Brushes.Blue;

                enemyList.Add(enemy);
                GameScreen.Children.Add(enemy.ellipse);
                randx = random.Next(1, 1020);
                randy = random.Next(1, 760);
                Canvas.SetLeft(enemy.ellipse, randx);
                Canvas.SetTop(enemy.ellipse, randy);
                
            }

            return enemyList;
        }
        public abstract class Orb
        {
            public Ellipse ellipse =new Ellipse();
            public int Size;
            
        }

        public class Player : Orb
        {
            public new int Size = 20;
            
        }

        public class Sun : Orb
        {
            public new int Size = 100;
            
        }

        public class Enemy : Orb
        {
            public new int Size = 10;
            
        }
    }
}

