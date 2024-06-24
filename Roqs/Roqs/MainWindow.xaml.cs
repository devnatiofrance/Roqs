using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace Roqs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Entity> entitys = new List<Entity>();
        public static List<GameObject> colliders = new List<GameObject>();

        public Player playerData;
        private Gravity gravity;
        public Gravity Gravity { get => gravity; set => gravity = value; }

        public MainWindow()
        {
            this.Visibility = Visibility.Hidden;
            InitializeComponent();
            this.ShowMainMenu();
            this.InitGame();
            CompositionTarget.Rendering += gameLoop; 
        }
        public void InitGame()
        {
            for (int i = 0; i < 6; i++)
            {
                // Create the rectangle
                Rectangle rec = new Rectangle()
                {
                    Width = 200,
                    Height = 15,
                    Fill = Brushes.Green,
                };

                // Add to a canvas for example
                canvas.Children.Add(rec);
                Canvas.SetTop(rec, i*100 + 100);
                Canvas.SetLeft(rec, 100);
                colliders.Add(new GameObject( 100, i * 100 + 100 , 200,15, rec)); 
            }
            colliders.Add(new GameObject(0, 387, 794,33));
            this.Gravity = new Gravity(); 
            this.playerData = new Player(374,60,Constants.playerWidth,Constants.playerHeight,100,10,10,new Controls(), this.player); 
        }
        public void gameLoop(object sender, EventArgs e)
        {
            playerData.Move();
            

            if (playerData.isGrounded)
            {
                if (playerData.Controls.up)
                {
                    playerData.isJumping = true;
                    playerData.isGrounded = false;
                    playerData.initialYWhenJumping = playerData.YPos;
                }
            }
            else
            {
                playerData.ApplyGravity();
            }
            if (playerData.isJumping) {
                playerData.ApplyJump();            
            }


            int col = 0; 
            playerData.isGrounded = false;
            foreach (GameObject obj in MainWindow.colliders)
            {
                // if the player is colliding obj 
                if (playerData.isColliding(obj))
                {
                    // if we the collision is on the y axis
                    if(new Rect(playerData.XPos - playerData.Velocity.X, playerData.YPos, playerData.Width, playerData.Height).IntersectsWith(obj.CollisionBox))
                    {
                        labelDebug.Content = "Collision Sommets " + playerData.Velocity + " " + playerData.XPos + " " + playerData.YPos;

                        playerData.isJumping = false;
                        if(playerData.XPos != obj.XPos + obj.Width && playerData.XPos + playerData.Width != obj.XPos)
                        {
                            // if the player is on a plateform
                            if (obj.YPos > playerData.YPos)
                            {
                                playerData.YPos = obj.YPos - playerData.Height;
                                playerData.ResetGravity();
                                playerData.isGrounded = true;
                                // The player can't go through the floor
                                playerData.Velocity = new Vector2(playerData.Velocity.X, playerData.Velocity.Y<0 ? playerData.Velocity.Y : 0);

                            }
                            else // ... else the player is down the plateform
                            {
                                playerData.YPos = obj.YPos + obj.Height;

                            }
                        }


                    }
                    // ... else, if the collision is on the x axis

                    else if (new Rect(playerData.XPos , playerData.YPos - playerData.Velocity.Y, playerData.Width, playerData.Height).IntersectsWith(obj.CollisionBox))
                    {
                        labelDebug.Content = "Collision Côtés " + playerData.Velocity + " "+playerData.XPos+" "+playerData.YPos;
                        playerData.Velocity = new Vector2(0, playerData.Velocity.Y);
                        // collision left the player 
                        if (obj.XPos > playerData.XPos)
                        {
                            playerData.XPos = obj.XPos - playerData.Width;
                        }
                        // collision right the player 
                        else
                        {
                            playerData.XPos = obj.XPos + obj.Width;
                        }
                    }


                    col++;
                }

            }
            playerData.UpdatePosition();

            playerData.UpdateVelocity();

            playerData.UpdateSprite();

        }
        public void ShowMainMenu()
        {
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Visibility = Visibility.Visible;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key.ToString() == ("" + ((char)Controls.key.LEFT)))
            {
                playerData.Controls.left = true;

            }
            if (e.Key.ToString() == ("" + ((char)Controls.key.RIGHT)))
            {
                playerData.Controls.right = true;

            }
            if (e.Key.ToString() == ("" + ((char)Controls.key.JUMP)))
            {
                playerData.Controls.up = true;
            }
            if (e.Key.ToString() == ("" + ((char)Controls.key.DOWN)))
            {
                playerData.Controls.down = true;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key.ToString() == ("" + ((char)Controls.key.LEFT)))
            {
                playerData.Controls.left = false;

            }
            if(e.Key.ToString() == ("" + ((char)Controls.key.RIGHT)))
            {
                playerData.Controls.right = false; 

            }
            if(e.Key.ToString() == ("" + ((char)Controls.key.JUMP)))
            {
                playerData.Controls.up = false; 
            }
            if(e.Key.ToString() == ("" + ((char)Controls.key.DOWN)))
            {
                playerData.Controls.down = false; 
            }
        }
    }
}
    