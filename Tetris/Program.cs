using System;
using System.Runtime.InteropServices;
namespace Tetris
{
    internal class Program
    {
        // Settings
        static int TetrisRows = 20;
        static int TetrisCols = 10;
        static int InfoCols = 10;
        static int ConsoleRows = 1 + TetrisRows + 1;
        static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;

        // State
        static int Score = 0;
        // Structure used by GetWindowRect
        struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        private static void Main(string[] args)
        {
            // Import the necessary functions from user32.dll
            [DllImport("user32.dll")]
            static extern IntPtr GetForegroundWindow();
            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            [DllImport("user32.dll")]
            static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
            [DllImport("user32.dll")]
            static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
            // Constants for the ShowWindow function
            const int SW_MAXIMIZE = 3;
            // Get the handle of the console window
            IntPtr consoleWindowHandle = GetForegroundWindow();
            // Maximize the console window
            ShowWindow(consoleWindowHandle, SW_MAXIMIZE);
            // Get the screen size
            Rect screenRect;
            GetWindowRect(consoleWindowHandle, out screenRect);
            // Resize and reposition the console window to fill the screen
            int width = screenRect.Right - screenRect.Left;
            int height = screenRect.Bottom - screenRect.Top;
            MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
            Console.ReadKey();
            DrawBorder();
            DrawInfo();
            while (true)
            {

            }
        }

        static void DrawInfo()
        {
            Write("Score", 1, 3 + TetrisCols);
            Write(Score.ToString(), 2, 3 + TetrisCols);
        }

        static void DrawBorder()
        {
            string topLine = "╔";
            topLine += new string('═', TetrisCols);
            topLine += "╦";
            topLine += new string('═', InfoCols);
            topLine += "╗";
            Console.WriteLine(topLine);

            for (int n = 0; n < TetrisRows; n++)
            {
                string middleCharacter = "║";
                middleCharacter += new string(' ', TetrisCols);
                middleCharacter += "║";
                middleCharacter += new string(' ', InfoCols);
                middleCharacter += "║";
                Console.WriteLine(middleCharacter);
            }

            string bottomLine = "╚";
            bottomLine += new string('═', TetrisCols);
            bottomLine += "╩";
            bottomLine += new string('═', InfoCols);
            bottomLine += "╝";
            Console.Write(bottomLine);
        }

        static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Blue)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(row, col);
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
