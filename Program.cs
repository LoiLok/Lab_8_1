using System;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics.SymbolStore;
using System.Timers;


class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("Params.txt");
        int[] start_second = new int[lines.Length];
        int[] stop_second = new int[lines.Length];
        int[] all_seconds = new int[lines.Length];
        string[] position = new string[lines.Length];
        string[] color = new string[lines.Length];
        string[] text = new string[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].ToString();
            string time = Time(line);
            int start = Start(time);
            int stop = Stop(time);
            start_second[i] = start;
            stop_second[i] = stop;
            position[i] = Position(line);
            color[i] = Color(line);
            text[i] = Text(line);

        }
        for (int i = 0; i < lines.Length; i++)
        {
            all_seconds[i] = stop_second[i] - start_second[i] + 1;
        }
        WriteTabl();
        int max = stop_second.Max();
        for (int i = 0; i < max+1; i ++)
        {
            for (int j = 0;j < start_second.Length; j ++)
            {
                if (start_second[j] == i)
                {
                    Write(position[j], color[j], text[j]);
                }
                if (stop_second[j] == i)
                {
                    Delete(position[j]);
                }
            }
            Thread.Sleep(1000);
        }
        Console.WriteLine();
        Console.WriteLine();

    }
    public static void WriteTabl()
    {
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("|                                                                                               |");
        }
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
    }
    public static string Time(string line)
    {
        line = line.Replace(" - ", " ");
        string time = line.Substring(0,11);
        return time;
    }
    public static int Start(string time)
    {
        time = time.Substring(0,6);
        int start;
        int index = time.IndexOf(":");
        int minutes = Convert.ToInt32(time.Substring(0, index));
        int seconds = Convert.ToInt32(time.Substring(3));
        start = minutes * 60 + seconds;
        return start;
    }
    public static int Stop(string time)
    {
        time = time.Substring(6);
        int stop;
        int index = time.IndexOf(":");
        int minutes = Convert.ToInt32(time.Substring(0, index));
        int seconds = Convert.ToInt32(time.Substring(3));
        stop = minutes * 60 + seconds;
        return stop;
    }

    public static string Position(string line)
    {
        int index = line.IndexOf("[");
        if (index == -1)
        {
            return "Bottom";
        }
        int index_second = line.IndexOf(',');
        int pos = index_second - index;
        line = line.Substring(index + 1,pos-1);
        return line;
    }
    public static string Color(string line)
    {
        int index = line.IndexOf("[");
        if (index == -1)
        {
            return "White";
        }
        int index_first = line.IndexOf(",");
        int index_second = line.IndexOf(']');
        int pos = index_second - index_first;
        line = line.Substring(index_first + 2, pos - 2);
        return line;
    }
    public static string Text(string line)
    {
        int index = line.IndexOf("]");
        if (index == -1)
        {
            line = line.Substring(14);
            return line;
        }
        line = line.Substring(index+2);
        return line;
    }
    public static void Write(string position, string color, string text)
    {
        if (color == "Blue")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else if (color == "Green")
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (color == "Red")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (color == "White")
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        if (position == "Top")
        {
            Console.SetCursorPosition(48 - text.Length / 2, 1);
            Console.Write(text);
        }
        else if (position == "Bottom")
        {
            Console.SetCursorPosition(48 - text.Length / 2, 20);
            Console.Write(text);
        }
        else if (position == "Right")
        {
            Console.SetCursorPosition(96 - text.Length, 10);
            Console.Write(text);
        }
        else if (position == "Left")
        {
            Console.SetCursorPosition(1, 10);
            Console.Write(text);
        }
    }
    public static void Delete(string position)
    {
        if (position == "Top")
        {
            Console.SetCursorPosition(2, 1);
            Console.Write("                                                                                              ");
        }
        else if (position == "Bottom")
        {
            Console.SetCursorPosition(2, 20);
            Console.Write("                                                                                              ");
        }
        else if (position == "Right")
        {
            Console.SetCursorPosition(48, 10);
            Console.Write("                                                ");
        }
        else if (position == "Left")
        {
            Console.SetCursorPosition(1, 10);
            Console.Write("                                                                                              ");
        }
    }
}