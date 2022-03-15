using System.Reflection;

namespace Snake_Demo
{
    public partial class Form1 : Form
    {
        private Snake snake;
        private List<Position> food;
        GameThread gameThread;
        Thread t;

        public Form1()
        {
            InitializeComponent();
            //Active DoubleBuffered to Panel 'GameScreen'
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            GameScreen, new object[] { true });


            snake = new Snake(new Position(150,150), GameScreen.Width, GameScreen.Height);
            food = new List<Position>();
            food.Add(new Position(200, 200));
            gameThread = new GameThread(snake, food, this.GameScreen);
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;

            g.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.WhiteSmoke)), p.DisplayRectangle);

            DrawSnake(g);
            DrawFood(g);
        }

        private void DrawSnake(Graphics g)
        {
            var snake = this.snake.GetSnake();
            Brush brush = new SolidBrush(Color.DarkBlue);
            Size size = new Size(10,10);
            Rectangle[] points = new Rectangle[snake.Count];

            for (int i = 0; i < snake.Count; i++)
            {
                var point = new Point(snake[i].x, snake[i].y);
                points[i] = new Rectangle(point, size);
            }
            g.FillRectangles(brush, points);
        }

        private void DrawFood(Graphics g)
        {
            if (food.Count <= 0) return;

            Brush brush = new SolidBrush(Color.DarkRed);
            Size size = new Size(10, 10);
            Rectangle[] points = new Rectangle[food.Count];

            for (int i = 0; i < food.Count; i++)
            {
                var point = new Point(food[i].x, food[i].y);
                points[i] = new Rectangle(point, size);
            }
            g.FillRectangles(brush, points);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) gameThread.direction = snake.CanMoveTo(Command.right) ? Command.right  : gameThread.direction;
            if (e.KeyCode == Keys.Left)  gameThread.direction = snake.CanMoveTo(Command.left)  ? Command.left   : gameThread.direction;
            if (e.KeyCode == Keys.Up)    gameThread.direction = snake.CanMoveTo(Command.up)    ? Command.up     : gameThread.direction;
            if (e.KeyCode == Keys.Down)  gameThread.direction = snake.CanMoveTo(Command.down)  ? Command.down   : gameThread.direction;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(new ThreadStart(gameThread.StartGame));
            t.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}