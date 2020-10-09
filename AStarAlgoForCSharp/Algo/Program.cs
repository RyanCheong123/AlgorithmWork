using System;
using System.Globalization;

namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);
            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;
            while (true)
            {
                #region Frame Manage
                int currentTick = System.Environment.TickCount;
                // if elapsedTick < 1/30
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion 

                //Enter

                //Logic
                player.Update(deltaTick);

                //Render
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}
