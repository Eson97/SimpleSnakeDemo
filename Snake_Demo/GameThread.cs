using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Demo
{
    internal class GameThread
    {
        public Command direction { get; set; }
        public bool GameRunning { get; set; }
        private Snake snake;
        private List<Position> food;
        private Panel Screen;
        private Random random;

        public GameThread(Snake snake, List<Position> food, Panel screen)
        {
            random = new Random();
            direction = Command.right;
            GameRunning = true;
            this.snake = snake;
            this.food = food;
            this.Screen = screen;
        }

        public void StartGame()
        {
            var cicles = 1;
            while (GameRunning)
            {
                snake.MoveTo(direction);
                if (!snake.isSnakeAlive) break;
                snake.HasEaten(food);

                Screen.Invalidate();

                Thread.Sleep(500);
                if (cicles % 10 == 0) GenerateFood();

                cicles++;
            }
            if(GameRunning) MessageBox.Show("Game Over!");
            Application.Exit();
        }

        public void GenerateFood()
        {
            var posX = random.Next(29) * 10;
            var posY = random.Next(29) * 10;
            food.Add(new Position(posX, posY));
        }
    }
}
