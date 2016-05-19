using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class GameField
    {
        public event EventHandler GameOver;

        private AllStars stars;
        Rectangle Area { get; set; }

        private int score = 0;
        private int lives = 5;
        private int wave = 0;
        private int framesSkipped = 6;
        private int currentFrame = 1;

        private Direction invadersDirection;
        private List<Enemy> invaders;
        private List<Shot> invadersShot;
        private const int invaderXspace = 60;
        private const int invaderYspace = 60;

        private Player player;
        private List<Shot> playerShots;

        private PointF scorePlace;
        private PointF livesPlace;
        private PointF wavePlace;

        Font messageFont = new Font(FontFamily.GenericMonospace, 50, FontStyle.Bold);
        Font statsFont = new Font(FontFamily.GenericMonospace, 15);

        public GameField(Rectangle formArea)
        {
            Area = formArea;
            stars = new AllStars(Area);

            scorePlace = new PointF((formArea.Left + 5.0F), (formArea.Top + 5.0F));
            livesPlace = new PointF((formArea.Right - 120.0F), (formArea.Top + 5.0F));
            wavePlace = new PointF((formArea.Left + 5.0F), (formArea.Top + 25.0F));

            player = new Player(Area, new Point((Area.Width / 2), Area.Height - 50));
            playerShots = new List<Shot>();
            invadersShot = new List<Shot>();
            invaders = new List<Enemy>();

            NextWave();

        }
        public void Update()
        {
            if (player.Alive)
            {
                // Check to see if any shots are off screen, to be removed
                List<Shot> deadPlayerShots = new List<Shot>();
                foreach (Shot shot in playerShots)
                {
                    if (!shot.Move())
                        deadPlayerShots.Add(shot);
                }
                foreach (Shot shot in deadPlayerShots)
                    playerShots.Remove(shot);

                List<Shot> deadInvaderShots = new List<Shot>();
                foreach (Shot shot in invadersShot)
                {
                    if (!shot.Move())
                        deadInvaderShots.Add(shot);
                }
                foreach (Shot shot in deadInvaderShots)
                    invadersShot.Remove(shot);

                moveInvaders();
                OffensiveFire();
                Collisions();
                if (invaders.Count < 1)
                {
                    NextWave();
                }
            }
        }

        public void Draw(Graphics g, int frame, bool over)
        {
            g.FillRectangle(Brushes.Black, Area);
            stars.Draw(g);

            foreach (var invader in invaders)
                invader.Draw(g, frame);
            player.Draw(g);

            foreach (var shot in playerShots)
                shot.Draw(g);
            foreach (var shot in invadersShot)
                shot.Draw(g);

            g.DrawString(("Score: " + score.ToString()),
                statsFont, Brushes.Yellow, scorePlace);
            g.DrawString(("Lives: " + lives.ToString()),
                statsFont, Brushes.Yellow, livesPlace);
            g.DrawString(("Wave: " + wave.ToString()),
                statsFont, Brushes.Yellow, wavePlace);
            if (over)
            {
                g.DrawString("GAME OVER", messageFont, Brushes.Red,
                    (Area.Width / 4), Area.Height / 3);
            }

        }

        public void Twinkle()
        {
            stars.Twinkle();
        }

        public void MovePlayer(Direction direction, bool over)
        {
            if (!over)
                player.Move(direction);
        }

        public void PlayerShot()
        {
            if (playerShots.Count < 2)
            {
                Shot shot = new Shot(new Point(player.Location.X + (player.image.Width / 2), player.Location.Y), Direction.Up, Area);
                playerShots.Add(shot);
            }

        }

        private void OffensiveFire()
        {
            var r = new Random();
            if (invaders.Count == wave)
                return;
            if (r.Next(10) < (10 - wave))
                return;

            var invaderColumn = from invader in invaders
                                group invader by invader.Location.X into columns
                                select columns;
            int randomNumber = r.Next(invaderColumn.Count());
            var randColumn = invaderColumn.ElementAt(randomNumber);

            var invaderRow =
            from invader in randColumn
            orderby invader.Location.Y descending
            select invader;

            Enemy shooter = invaderRow.First();
            Point newPlace = new Point(shooter.Location.X + (shooter.Area.Width / 2), shooter.Area.Height);
            Shot newShot = new Shot(newPlace, Direction.Down, Area);
            invadersShot.Add(newShot);

        }

        private void Collisions()
        {
            var deadPlayerShots = new List<Shot>();
            var deadEnemyShots = new List<Shot>();

            foreach (var shot in invadersShot)
            {
                if (player.Area.Contains(shot.Location))
                {
                    deadEnemyShots.Add(shot);
                    lives--;
                    player.Alive = false;
                    if (lives == 0)
                        GameOver(this, null);

                }
            }
            foreach (var shot in playerShots)
            {
                List<Enemy> deadInvaders = new List<Enemy>();
                foreach (var invader in invaders)
                {
                    if (invader.Area.Contains(shot.Location))
                    {
                        deadInvaders.Add(invader);
                        deadEnemyShots.Add(shot);
                        score += (1 * wave);
                    }
                }

                foreach (var invader in deadInvaders)
                    invaders.Remove(invader);

            }
            foreach (var shot in deadPlayerShots)
                playerShots.Remove(shot);
            foreach (var shot in deadEnemyShots)
                invadersShot.Remove(shot);

        }

        private void NextWave()
        {
            wave++;
            invadersDirection = Direction.Right;

            if (wave < 7)
                framesSkipped = 6 - wave;
            else
                framesSkipped = 0;

            int currentInvaderY = 0;
            for (int x = 0; x < 5; x++)
            {
                InvaderType current = (InvaderType)x;
                currentInvaderY += invaderYspace;
                int currentInvaderX = 0;
                for (int y = 0; y < 5; y++)
                {
                    currentInvaderX += invaderXspace;
                    Point newInvaderPoint = new Point(currentInvaderX, currentInvaderY);
                    Enemy invader = new Enemy(current, newInvaderPoint, 10);
                    invaders.Add(invader);
                }
            }

        }
        private void moveInvaders()
        {
            // if the frame is skipped invaders do not move
            if (currentFrame > framesSkipped)
            {
                // проверка на границах, 
                // менять, если с самого краю
                if (invadersDirection == Direction.Right)
                {
                    var edgeInvaders =
                        from invader in invaders
                        where invader.Location.X > (Area.Width - 100)
                        select invader;
                    if (edgeInvaders.Count() > 0)
                    {
                        invadersDirection = Direction.Left;
                        foreach (var invader in invaders)
                            invader.Move(Direction.Down);
                    }
                    else
                    {
                        foreach (var invader in invaders)
                            invader.Move(Direction.Right);
                    }
                }

                if (invadersDirection == Direction.Left)
                {
                    var edgeInvaders =
                        from invader in invaders
                        where invader.Location.X < 100
                        select invader;
                    if (edgeInvaders.Count() > 0)
                    {
                        invadersDirection = Direction.Right;
                        foreach (var invader in invaders)
                            invader.Move(Direction.Down);
                    }
                    else
                    {
                        foreach (var invader in invaders)
                            invader.Move(Direction.Left);
                    }
                }
                currentFrame++;
                if (currentFrame > 6)
                    currentFrame = 1;

            }
        }
    }
}
