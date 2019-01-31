using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kataBattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            int[,] field = new int[10, 10]
                                  {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                                       {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                                       {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                                       {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                       {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                                       {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                                       {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                                       {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                                       {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                                       {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
            //                      { { 1 ,0 ,0 ,0 ,0 ,1 ,1 ,0 ,0 ,0 },
            //{ 1 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,1 ,0 },
            //{ 1 ,0 ,1 ,0 ,1 ,1 ,1 ,0 ,1 ,0},
            //{ 1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0},
            //{ 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0},
            //{ 0 ,0 ,0 ,0 ,1 ,1 ,1 ,0 ,0 ,0},
            //{ 0 ,0 ,0 ,1 ,0 ,0 ,0 ,0 ,1 ,0},
            //{ 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0},
            //{ 0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,0},
            //{ 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0},
            //       };

            Console.WriteLine(ValidateBattlefield(field));
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        public static bool ValidateBattlefield(int[,] field)
        {
            if (field.GetLength(0) != 10 && field.GetLength(1) != 10)
            {
                return false;
            }
            int amountBattleShips = 1;
            int amountCruisers = 2;
            int amountDestroyers = 3;
            int amountSubmarines = 4;

            for (int column = 0; column < field.GetLength(0); column++)
            {
                for (int row = 0; row < field.GetLength(1); row++)
                {
                    var isShip = field[column, row];
                    if (isShip == 1)
                    {
                        int size = 0;
                        size = checkSizeHorizontal(column, row, field);
                        if (size == 0)
                        {
                            size = checkSizeVertical(column, row, field);
                            if (size == -1)
                            {
                                return false;
                            }
                        }
                        else if (size == -1)
                        {
                            return false;
                        }

                        switch (size)
                        {
                            case 1:
                                amountSubmarines--;
                                break;
                            case 2:
                                amountDestroyers--;
                                break;
                            case 3:
                                amountCruisers--;
                                break;
                            case 4:
                                amountBattleShips--;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            if (amountBattleShips == 0 && amountCruisers == 0 && amountDestroyers == 0 && amountSubmarines == 0)
            {
                return true;
            }

            return false;
        }

        private static int checkSizeHorizontal(int column, int row, int[,] field)
        {
            int isShip = 1;
            int size = 0;
            while (isShip == 1)
            {
                isShip = field[column, row + size];
                if (isShip == 1)
                {
                    size++;
                }
            }
            if (size > 1)
            {
                //set to zero so that we dont trigger the count for big ships
                for (int rowCount = row; row < row + size; row++)
                {
                    field[column, rowCount] = 0;
                }
                return isSuroundedHorizontal(column, row, field, size) ? -1 : size;
            }

            return size <= 1 ? 0 : size;
        }

        private static int checkSizeVertical(int column, int row, int[,] field)
        {
            int value = 1;
            int size = 0;
            while (value == 1)
            {
                value = field[column + size, row];
                field[column + size, row] = 0;
                if (value == 1)
                {
                    size++;
                }
            }
            //set to zero so that we dont trigger the count for big ships
            for (int columnCount = column; row < column + size; row++)
            {
                field[columnCount, row] = 0;
            }
            return isSuroundedVertical(column, row, field, size) ? -1 : size;
        }

        private static bool isSuroundedHorizontal(int column, int row, int[,] field, int size)
        {
            //for horizontal it is row-1 and row+size+1, these need to be checked, then column -1 and column + 1 and all the row's between row-1 and row+size+1
            //check for row > 0 as well just to not get error
            //first we check for the two columns next to it
            if (row > 0)
            {
                if (field[column, row - 1] == 1)
                {
                    return true;
                }
            }

            if (field[column, row + size + 1] == 1)
            {
                return true;
            }
            //now we check the row above and below it
            if (column != 0)
            {
                for (int rowCount = row - 1; row < row + size + 1; row++)
                {
                    if (field[column-1, rowCount] == 1)
                    {
                        return true;
                    }
                }
            }

            for (int rowCount = row == 0 ? row : row - 1; rowCount < row + size + 1; row++)
            {
                if (field[column+1, rowCount] == 1)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool isSuroundedVertical(int column, int row, int[,] field, int size)
        {
            //for horizontal it is row-1 and row+size+1, these need to be checked, then column -1 and column + 1 and all the row's between row-1 and row+size+1
            //check for row > 0 as well just to not get error
            int maxRowToCheck = column + size - 1;
            if (maxRowToCheck == field.GetLength(1))
            {
                maxRowToCheck--;
            }
            column = maxRowToCheck;
            //first we check for the two columns next to it
            if (column > 0)
            {
                if (field[column - 1, row] == 1)
                {
                    return true;
                }
            }

            if (field[maxRowToCheck, row] == 1)
            {
                return true;
            }
            //now we check the row above and below it
            if (row != 0)
            {
                for (int columnCount = column - 1; row < column + size + 1; row++)
                {
                    if (field[columnCount, row-1] == 1)
                    {
                        return true;
                    }
                }
            }

            for (int columnCount = column == 0 ? column : row - 1; columnCount < column + size + 1; row++)
            {
                if (field[columnCount, row+1] == 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
