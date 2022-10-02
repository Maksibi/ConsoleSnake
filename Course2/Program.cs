using System;

internal class Program
{
    static int foodX;
    static int foodY;
    static void SpawnFood()
    {
        Random rnd = new Random();
        foodX = rnd.Next(0, 120);
        if (foodX % 2 != 0) foodX += 1;
        foodY = rnd.Next(0, 40);
    }
    static void Main(string[] args)
    {
        Console.SetWindowSize(120, 40);
        Console.SetBufferSize(120, 40);
        Console.CursorVisible = false;
        bool isGame = true;
        int maxScore = 0;
        int headX = 10;
        int headY = 10;
        int dir = 0;
        int length = 3;
        int[] body_x = new int[100];
        int[] body_y = new int[100];
        for (int i = 0; i < length; i++)
        {
            body_x[i] = headX - i * 2;
            body_y[1] = 10;
        }

        SpawnFood();

        while (true)
        {
            Console.SetCursorPosition(60, 21);
            Console.Write("Нажмите \"R\" для запуска игры");
            Console.SetCursorPosition(60, 22);
            Console.Write("В случае графических багов кнопка \"H\"");
            ConsoleKeyInfo restartKey;
            Console.SetCursorPosition(0, 0);
            restartKey = Console.ReadKey();
            if (restartKey.Key == ConsoleKey.R)
            {
                Console.Clear();
                isGame = true;
                headX = 10;
                headY = 10;
                length = 3;
                dir = 0;
            }
            while (isGame == true)
            {
                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("  ");
                }
                Console.SetCursorPosition(headX, headY);
                Console.Write("  ");
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("  ");

                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo control;
                    Console.SetCursorPosition(0, 0);
                    control = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("  ");
                    if (control.Key == ConsoleKey.D && dir != 2) dir = 0;
                    if (control.Key == ConsoleKey.S && dir != 3) dir = 1;
                    if (control.Key == ConsoleKey.A && dir != 0) dir = 2;
                    if (control.Key == ConsoleKey.W && dir != 1) dir = 3;
                    if (control.Key == ConsoleKey.H) Console.Clear();
                }

                if (dir == 0) headX += 2;
                if (dir == 1) headY += 1;
                if (dir == 2) headX -= 2;
                if (dir == 3) headY -= 1;

                if (headX >= 120) headX = 0;
                if (headY >= 40) headY = 0;
                if (headX < 0) headX = 118;
                if (headY < 0) headY = 39;

                for (int i = length; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }

                body_x[0] = headX;
                body_y[0] = headY;

                for (int i = 1; i < length; i++)
                {
                    if (body_x[i] == headX && body_y[i] == headY)
                    {
                        isGame = false;
                        Console.SetCursorPosition(60, 20);
                        Console.Write("Вы проиграли");
                        Console.SetCursorPosition(60, 21);
                        Console.Write("Нажмите \"R\" для перезапуска");

                    }
                }
                for (int i = 0; i < length; i++)
                {
                    if (headX == foodX && headY == foodY && foodX != body_x[i+2] && foodY != body_y[i+2])
                    {
                        length += 1;
                        SpawnFood();
                    }
                }

                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("██");
                }
                if (maxScore < length - 3) maxScore = length - 3;
                Console.SetCursorPosition(headX, headY);
                Console.Write("██");
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("██");
                Console.SetCursorPosition(1, 0);
                Console.Write("Score: " + (length - 3));
                Console.SetCursorPosition(1, 1);
                Console.Write("Record: " + maxScore);
            System.Threading.Thread.Sleep(50);
            }
        }
    }
}
