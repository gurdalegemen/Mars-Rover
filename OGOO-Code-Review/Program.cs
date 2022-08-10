/*
    Egemen Gürdal UYAN
    gurdalegemen@gmail.com
    https://www.linkedin.com/in/egemengurdaluyan/

    10/08/2022

    Code Review: Mars Rover

    Part 1
       A squad of robotic rovers are to be landed by NASA on a plateau on Mars. This plateau, which is
    curiously rectangular, must be navigated by the rovers so that their on board cameras can get a
    complete view of the surrounding terrain to send back to Earth.
    A rover's position and location is represented by a combination of x and y co-ordinates and a letter
    representing one of the four cardinal compass points. The plateau is divided up into a grid to
    simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom
    left corner and facing North.
    In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and
    'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its
    current spot. 'M' means move forward one grid point, and maintain the same heading.
    Assume that the square directly North from (x, y) is (x, y+1).

    Input:
        The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are
    assumed to be 0,0.
    The rest of the input is information pertaining to the rovers that have been deployed. Each rover
    has two lines of input. The first line gives the rover's position, and the
    second line is a series of instructions telling the rover how to explore the
    plateau.
    The position is made up of two integers and a letter separated by spaces, corresponding to the x
    and y co-ordinates and the rover's orientation.
    Each rover will be finished sequentially, which means that the second rover won't start to move
    until the first one has finished moving.

    Output:
        The output for each rover should be its final co-ordinates and heading.
    Input and Output
 
    Test Input:
    5 5
    1 2 N
    LMLMLMLMM
    3 3 E
    MMRMMRMRRM
    
    Expected Output:
    1 3 N
    5 1 E
 
 */
public class Program
{
    public static void Main()
    {
        // Declare Plateau which is rectangular.
        int[,] area;
        Console.WriteLine("Please enter the size of area (x and y): ");
        var str = Console.ReadLine();
        var area_info = str.Split(" ");
        while (true)
        {
            if (Int32.Parse(area_info[0]) < 0 || Int32.Parse(area_info[1]) < 0)
            {
                Console.WriteLine("Size of area can't be negative! Please re-enter the size of area (x and y): ");
                str = Console.ReadLine();
                area_info = str.Split(" ");
            }
            else
            {
                break;
            }
        }
        area = new int[Int32.Parse(area_info[0]), Int32.Parse(area_info[1])];

        // Numbers of Robotic Rovers
        Console.WriteLine("How many robotic rovers will work?");
        var count = Console.ReadLine();

        // Getting coordinate and instructions from user.
        string[,] rovers = new string[Int32.Parse(count), 2];
        bool check = true;
        Console.WriteLine("Please enter the rovers coordinate and then instructions:");
        for (int i = 0; i < rovers.GetLength(0); i++)
        {
            for (int j = 0; j < rovers.GetLength(1); j++)
            {
                rovers[i, j] = Console.ReadLine();
                if (check)
                {
                    var coordinate = rovers[i, j].Split(" ");
                    while (true)
                    {
                        if (Int32.Parse(coordinate[0]) > area.GetLength(0) || Int32.Parse(coordinate[1]) > area.GetLength(1) || Int32.Parse(coordinate[0]) < 0 || Int32.Parse(coordinate[1]) < 0)
                        {
                            Console.WriteLine("Coordinates are out of the plateau. Please re-enter coordinates");
                            rovers[i, j] = Console.ReadLine();
                        }
                        else if (!CheckHeading(coordinate[2]))
                        {
                            Console.WriteLine("Heading must be N,W,S,E. Please re-enter coordinates");
                            rovers[i, j] = Console.ReadLine();
                        }
                        else
                        {
                            break;
                        }
                    }

                    check = false;
                }
            }
            check = true;
        }


        string[] new_coordinates = new string[Int32.Parse(count)];
        for (int k = 0; k < new_coordinates.Length; k++)
        {
            new_coordinates[k] = rovers[k, 0];
        }

        // Walking with Robotic Rovers
        for (int m = 0; m < rovers.GetLength(0); m++)
        {
            for (int n = 0; n < rovers[m, 1].Length; n++)
            {
                if (rovers[m, 1][n] == 'M')
                {
                    new_coordinates[m] = MoveForward(new_coordinates[m],area_info);
                }
                else if (rovers[m, 1][n] == 'L')
                {
                    new_coordinates[m] = SetHeading(new_coordinates[m], true);
                }
                else if (rovers[m, 1][n] == 'R')
                {
                    new_coordinates[m] = SetHeading(new_coordinates[m], false);
                }
            }
        }


        for (int t = 0; t < new_coordinates.Length; t++)
        {
            Console.WriteLine(new_coordinates[t]);
        }

    } // end of main
    public static bool CheckHeading(string heading)
    {
        switch (heading)
        {
            case "N":
                return true;
            case "S":
                return true;
            case "E":
                return true;
            case "W":
                return true;
            default: return false;
        }
    }

    public static string GetHeading(string heading, bool side) // side=true : left , side=false : right
    {
        string face = "";
        if (heading == "N")
        {
            if (side)
            {
                face = "W";
            }
            else
            {
                face = "E";

            }
        }
        else if (heading == "E")
        {
            if (side)
            {
                face = "N";
            }
            else
            {
                face = "S";
            }

        }
        else if (heading == "S")
        {
            if (side)
            {
                face = "E";
            }
            else
            {
                face = "W";
            }
        }
        else if (heading == "W")
        {
            if (side)
            {
                face = "S";
            }
            else
            {
                face = "N";
            }
        }

        return face;
    }

    public static string MoveForward(string coordinate, string[] area_dim)
    {
        var x = coordinate.Split(" ");
        if (x[2] == "N")
        {
            if (Int32.Parse(x[1]) < Int32.Parse(area_dim[1]))
            {
                x[1] = (Int32.Parse(x[1]) + 1).ToString();
            }
           
        }
        else if (x[2] == "S")
        {
            if (Int32.Parse(x[1]) > Int32.Parse(area_dim[1]))
            {
                x[1] = (Int32.Parse(x[1]) - 1).ToString();
            }
        }
        else if (x[2] == "W")
        {
            if (Int32.Parse(x[0]) > Int32.Parse(area_dim[0]))
            {
                x[0] = (Int32.Parse(x[0]) - 1).ToString();
            }
        }
        else if (x[2] == "E")
        {
            if (Int32.Parse(x[0]) < Int32.Parse(area_dim[0]))
            {
                x[0] = (Int32.Parse(x[0]) + 1).ToString();
            }
        }

        string new_coordinate = x[0] + " " + x[1] + " " + x[2];

        return new_coordinate;


    }

    public static string SetHeading(string coordinate, bool side) // just turning head
    {
        var x = coordinate.Split(" ");
        x[2] = GetHeading(x[2], side);

        string new_coordinate = x[0] + " " + x[1] + " " + x[2];

        return new_coordinate;
    }


}
