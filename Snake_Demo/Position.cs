using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Demo
{
    internal struct Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position(int x, int y)
        {
            this.x = x; this.y = y;
        }
        public override string ToString()
        {
            return $"pos: ({x},{y})";
        }
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (!(obj is Position)) return false;
            var other = (Position)obj;
            return (other.x == x && other.y == y);
        }
        public static bool operator ==(Position a, Position b) => a.Equals(b);
        public static bool operator !=(Position a, Position b) => !a.Equals(b);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
