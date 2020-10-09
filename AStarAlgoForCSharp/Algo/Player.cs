using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Algo
{

    class Pos
    {
        public int Y;
        public int X;
        public Pos(int y, int x)
        {
            Y = y;
            X = x;
        }
    }

    class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;

            AStar();
        }

        struct PQNode : IComparable<PQNode>
        {
            public int F;
            public int G;
            public int Y;
            public int X;

            public int CompareTo(PQNode other)
            {
                if (F == other.F)
                    return 0;
                return F < other.F ? 1 : -1;
            }
        }

        //First Algorithm to find way: Left Hand Rule
        void LeftHandRule()
        {
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));
            // Until the player reaches the destination
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                // 1. Can turn right?
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    // Turn right 
                    _dir = (_dir - 1 + 4) % 4;

                    // then walk by one cell
                    PosX = PosX + frontX[_dir];
                    PosY = PosY + frontY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                // 2. Can walk front?
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    // Walk front by one cell
                    PosX = PosX + frontX[_dir];
                    PosY = PosY + frontY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    // Turn left 
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        void AStar()
        {
            
            int[] deltaY = new int[] { -1, 0, 1, 0 };
            int[] deltaX = new int[] { 0, -1, 0, 1 };
            int[] cost = new int[] { 10, 10, 10, 10 };

            bool[,] closed = new bool[_board.Size, _board.Size]; // CloseList

            int[,] open = new int[_board.Size, _board.Size]; // OpenList
            for (int y = 0; y < _board.Size; y++)
                for (int x = 0; x < _board.Size; x++)
                    open[y, x] = Int32.MaxValue;

            Pos[,] parent = new Pos[_board.Size, _board.Size];

            PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

            open[PosY, PosX] = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX));
            pq.Push(new PQNode() { F = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX)), G = 0, Y = PosY, X = PosX });
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while (pq.Count > 0)
            {
                PQNode node = pq.Pop();
                if (closed[node.Y, node.X])
                    continue;

                closed[node.Y, node.X] = true;
                if (node.Y == _board.DestY && node.X == _board.DestX)
                    break;

                for (int i = 0; i < deltaY.Length; i++)
                {
                    int nextY = node.Y + deltaY[i];
                    int nextX = node.X + deltaX[i];

                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    if (closed[nextY, nextX])
                        continue;

                    int g = node.G + cost[i];
                    int h = 10 * (Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX));
                    if (open[nextY, nextX] < g + h)
                        continue;

                    open[nextY, nextX] = g + h;
                    pq.Push(new PQNode() { F = g + h, G = g, Y = nextY, X = nextX });
                    parent[nextY, nextX] = new Pos(node.Y, node.X);
                }
            }

            CalcPathFromParent(parent);
        }
        void CalcPathFromParent(Pos[,] parent)
        {
            int y = _board.DestY;
            int x = _board.DestX;
            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            _points.Add(new Pos(y, x));
            _points.Reverse();
        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if(_lastIndex >= _points.Count)
            {
                return;
            }
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                //Logic happens every 0.1 seconds
                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
