using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Demo
{
    internal class Snake
    {
        private Position _head;
        private List<Position> _body;
        private bool isAlive = true;
        private const int step = 10;

        private readonly int _limitX, _limitY;
        public bool isSnakeAlive { get => isAlive; }

        public Snake(Position starterPosition, int limitX, int limitY)
        {
            _head = starterPosition;
            _body = new List<Position>();

            _limitX = limitX - step;
            _limitY = limitY - step;
        }

        public void MoveTo(Command direction)
        {
            if (!CanMoveTo(direction)) return;

            var prevPosition = _head;
            _head = GetNewHeadPosition(_head, direction);

            for(int i=0;i<_body.Count;i++)
            {
                var temp = _body[i];
                _body[i] = prevPosition;
                prevPosition = temp;
            }

            isAlive = UpdateStatus();
        }
        public List<Position> GetSnake()
        {
            var snake = new List<Position>(_body);
            snake.Insert(0, _head);

            return snake;
        }
        public bool HasEaten(List<Position> food)
        {
            foreach(var foodItem in food)
            {
                if(foodItem == _head)
                {
                    _body.Add(foodItem);
                    food.Remove(foodItem);
                    return true;
                }
            }
            return false;
        }
        public bool CanMoveTo(Command direction)
        {
            if (_body.Count <= 0) return true;
            if (direction == Command.left)  return !(_body[0].x == (_head.x - step) && _head.y == _body[0].y);
            if (direction == Command.right) return !(_body[0].x == (_head.x + step) && _head.y == _body[0].y);
            if (direction == Command.up)    return !(_body[0].y == (_head.y - step) && _head.x == _body[0].x);
            if (direction == Command.down)  return !(_body[0].y == (_head.y + step) && _head.x == _body[0].x);

            return true;
        }

        private bool UpdateStatus()
        {
            foreach(var bodyPos in _body)
                if(bodyPos == _head) return false;

            return true;
        }
        private Position GetNewHeadPosition(Position p, Command dir) => dir switch
        {
            Command.right => new Position(((p.x + step) > _limitX) ? 0 : (p.x + step), p.y),
            Command.left  => new Position(((p.x - step) < 0) ? _limitX : (p.x - step), p.y),
            Command.up    => new Position(p.x, ((p.y - step) < 0) ? _limitY : (p.y - step)),
            Command.down  => new Position(p.x, ((p.y + step) > _limitY) ? 0 : (p.y + step)),

            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(dir)),
        };
    }
}
