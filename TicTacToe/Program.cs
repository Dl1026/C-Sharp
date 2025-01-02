namespace TicTacToe;

class Player
{
    private int playerNumber;
    private char playerSign;

    public void setPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public int getPlayerNumber()
    {
        return playerNumber;
    }

    public void setPlayerSign(char playerSign)
    {
        this.playerSign = playerSign;
    }

    public char getPlayerSign()
    {
        return this.playerSign;
    }
}

class Program
{
    //the play field
    static char[,] field =
    {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' },
    };

    //the winner message
    static string[] winner = { "\nPlayer 1 has won!", "\nPlayer 2 has won!", "\nIt's a draw!" };

    //set the field to display in the console
    public static void setField()
    {
        Console.Clear();
        Console.WriteLine("   |   |   ");
        Console.WriteLine(" {0} | {1} | {2} ", field[0, 0], field[0, 1], field[0, 2]);
        Console.WriteLine("___|___|___");
        Console.WriteLine(" {0} | {1} | {2} ", field[1, 0], field[1, 1], field[1, 2]);
        Console.WriteLine("___|___|___");
        Console.WriteLine(" {0} | {1} | {2} ", field[2, 0], field[2, 1], field[2, 2]);
        Console.WriteLine("   |   |   ");
    }

    //the game start function
    public static int gameStart()
    {
        //create the players
        Player player1 = new Player();
        player1.setPlayerNumber(1);
        player1.setPlayerSign('o');
        Player player2 = new Player();
        player2.setPlayerNumber(2);
        player2.setPlayerSign('x');

        int result;
        int turn = 1;
        int position = 0;
        int[] repeatedPositions = new int[9];

        do
        {
            int turnChangeTo = turn % 2 == 1 ? player1.getPlayerNumber() : player2.getPlayerNumber();
            char sign = turnChangeTo == 1 ? player1.getPlayerSign() : player2.getPlayerSign();
            bool checkRepeated = false;

            setField();

            //check if the field is full and the field is not picked before
            while (!checkRepeated)
            {
                Console.Write("\nPlayer {0} : Choose your field! ", turnChangeTo);
                checkRepeated = int.TryParse(Console.ReadLine(), out position);
                if (checkRepeated == false)
                {
                    Console.WriteLine("Please enter a number!");
                    Console.WriteLine("\n Incorrect input! Please use another field!");
                }
                else if (position < 1 || position > 9)
                {
                    checkRepeated = false;
                    Console.WriteLine("\n Out of range! Please enter a correct field!");
                }
                else if (repeatedPositions.Contains(position))
                {
                    checkRepeated = false;
                    Console.WriteLine("\n Incorrect input! Please use another field!");
                }
                else
                {
                    checkRepeated = true;
                    repeatedPositions[turn - 1] = position;
                }
            }

            //set the sign in the field 
            switch (position)
            {
                case 1:
                    field[0, 0] = sign;
                    break;
                case 2:
                    field[0, 1] = sign;
                    break;
                case 3:
                    field[0, 2] = sign;
                    break;
                case 4:
                    field[1, 0] = sign;
                    break;
                case 5:
                    field[1, 1] = sign;
                    break;
                case 6:
                    field[1, 2] = sign;
                    break;
                case 7:
                    field[2, 0] = sign;
                    break;
                case 8:
                    field[2, 1] = sign;
                    break;
                case 9:
                    field[2, 2] = sign;
                    break;
            }

            //check if the player has won || it's a draw
            result = checkingWinner(turnChangeTo, turn, sign);

            turn++;
        } while (result == 0);

        return result;
    }

    //check if the player has won || it's a draw
    public static int checkingWinner(int player, int turn, char sign)
    {
        int winner = 0;
        if (((field[0, 0] == sign) && (field[0, 1] == sign) && (field[0, 2] == sign)) 
            || ((field[1, 0] == sign) && (field[1, 1] == sign) && (field[1, 2] == sign))
            || ((field[2, 0] == sign) && (field[2, 1] == sign) && (field[2, 2] == sign))
            || ((field[0, 0] == sign) && (field[1, 0] == sign) && (field[2, 0] == sign))
            || ((field[0, 1] == sign) && (field[1, 1] == sign) && (field[2, 1] == sign))
            || ((field[0, 2] == sign) && (field[1, 2] == sign) && (field[2, 2] == sign))
            || ((field[0, 0] == sign) && (field[1, 1] == sign) && (field[2, 2] == sign))
            || ((field[0, 2] == sign) && (field[1, 1] == sign) && (field[2, 0] == sign)))
        {
            winner = player;
        }
        else if (turn == 9)
        {
            winner = 3;
        }

        return winner;
    }

    static void Main(string[] args)
    {
        bool reset = true;
        char[,] fieldInitial = new char[3, 3];
        Array.Copy(field, fieldInitial, field.Length);

        //game reset after each game
        while (reset)
        {
            int result = gameStart();

            setField();
            Console.WriteLine(winner[result - 1]);
            Console.WriteLine("\nPress any Key to Reset the Game or enter Q to Exit");
            //quit the game
            if (Console.ReadLine().ToUpper() == "Q")
            {
                reset = false;
            }

            //reset the field
            Array.Copy(fieldInitial, field, fieldInitial.Length);
        }

        Console.Clear();
        Console.WriteLine("\n Quitting the Game...");
    }
}