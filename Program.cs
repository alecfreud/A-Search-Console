using System;
using System.Collections.Generic;


namespace Assignment2
{
    public class Node
    {
        public int xPos, yPos, gCost, hCost, fCost;
        public int[] parent = new int[2];
        public bool blocked = false;



        public Node(int x, int y) { xPos = x; yPos = y; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Generate 15x15 Grid of Nodes
            Node[,] grid = newRandomGrid();

            //Re-enter for multiple attempts on same board

            string quit = null;
            while (quit != "q")
            {
                bool found = false;

                //Display Grid & Take User Input for Start & Goal Node
                PrintHeader();
                PrintBoard(grid);

                Console.Write("\n You will enter a Starting node & Goal node." +
                     "\n \n The shortest path (avoiding blocked nodes) will then be displayed" +
                     "\n \n Enter the node as a coordinate: x,y" +
                     "\n \n 0,0 is the bottom left. 14,14 is the top right" +
                     "\n\n Enter your Starting node x,y:   ");



                string startStr =   Console.ReadLine();

                Console.Write("\n Great. Now Enter your Goal node x,y:   ");
                string goalStr = Console.ReadLine();

                //Parse user input
                string[] startSplit = startStr.Split(",");
                string[] goalSplit = goalStr.Split(",");

                int startX = Convert.ToInt32(startSplit[0]);
                int startY = Convert.ToInt32(startSplit[1]);
                int goalX = Convert.ToInt32(goalSplit[0]);
                int goalY = Convert.ToInt32(goalSplit[1]);


                //Generate Open & Closed list
                List<Node> openList = new List<Node>();
                List<Node> closedList = new List<Node>();

                openList.Add(grid[startX, startY]); //Add starting node to open list

                grid[startX, startY].gCost = 0; // Set starting node G cost to 0
                grid[startX, startY].parent = null; //Set starting node parent to null

                while (openList.Count > 0)
                {

                    Node current = openList[0];

                    current.hCost = HCost(current, goalX, goalY);



                    //Check G Cost
                    //Generate F Cost
                    //Check for Valid Neighbors & Update Current List

                    //up
                    if (current.yPos + 1 < 15 && !grid[current.xPos, current.yPos + 1].blocked && !closedList.Contains(grid[current.xPos, current.yPos + 1]))
                    {

                        if (grid[current.xPos, current.yPos + 1].gCost > 10 + current.gCost)

                        {
                            grid[current.xPos, current.yPos + 1].gCost = 10 + current.gCost; //Update Lower G Cost

                            grid[current.xPos, current.yPos + 1].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos, current.yPos + 1].parent[1] = current.yPos;

                            grid[current.xPos, current.yPos + 1].fCost = grid[current.xPos, current.yPos + 1].gCost + HCost(grid[current.xPos, current.yPos + 1], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos, current.yPos + 1]))
                            {
                                openList.Remove(grid[current.xPos, current.yPos + 1]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos, current.yPos + 1]))
                        {


                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos, current.yPos + 1].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos, current.yPos + 1]);
                                    break;
                                }



                            }


                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos, current.yPos + 1]))
                        {
                            openList.Add(grid[current.xPos, current.yPos + 1]);
                        }


                    }

                    //up right

                    if (current.yPos + 1 < 15 && current.xPos + 1 < 15 && !grid[current.xPos + 1, current.yPos + 1].blocked && !closedList.Contains(grid[current.xPos + 1, current.yPos + 1]))
                    {

                        if (grid[current.xPos + 1, current.yPos + 1].gCost > 14 + current.gCost)

                        {
                            grid[current.xPos + 1, current.yPos + 1].gCost = 14 + current.gCost; //Update Lower G Cost

                            grid[current.xPos + 1, current.yPos + 1].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos + 1, current.yPos + 1].parent[1] = current.yPos;

                            grid[current.xPos + 1, current.yPos + 1].fCost = grid[current.xPos + 1, current.yPos + 1].gCost + HCost(grid[current.xPos + 1, current.yPos + 1], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos + 1, current.yPos + 1]))
                            {
                                openList.Remove(grid[current.xPos + 1, current.yPos + 1]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos + 1, current.yPos + 1]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos + 1, current.yPos + 1].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos + 1, current.yPos + 1]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos + 1, current.yPos + 1]))
                        {
                            openList.Add(grid[current.xPos + 1, current.yPos + 1]);
                        }


                    }

                    //up left

                    if (current.yPos + 1 < 15 && current.xPos - 1 >= 0 && !grid[current.xPos - 1, current.yPos + 1].blocked && !closedList.Contains(grid[current.xPos - 1, current.yPos + 1]))
                    {

                        if (grid[current.xPos - 1, current.yPos + 1].gCost > 14 + current.gCost)

                        {
                            grid[current.xPos - 1, current.yPos + 1].gCost = 14 + current.gCost; //Update Lower G Cost

                            grid[current.xPos - 1, current.yPos + 1].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos - 1, current.yPos + 1].parent[1] = current.yPos;

                            grid[current.xPos - 1, current.yPos + 1].fCost = grid[current.xPos - 1, current.yPos + 1].gCost + HCost(grid[current.xPos - 1, current.yPos + 1], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos - 1, current.yPos + 1]))
                            {
                                openList.Remove(grid[current.xPos - 1, current.yPos + 1]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos - 1, current.yPos + 1]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos - 1, current.yPos + 1].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos - 1, current.yPos + 1]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos - 1, current.yPos + 1]))
                        {
                            openList.Add(grid[current.xPos - 1, current.yPos + 1]);
                        }


                    }

                    //down

                    if (current.yPos - 1 >= 0 && !grid[current.xPos, current.yPos - 1].blocked && !closedList.Contains(grid[current.xPos, current.yPos - 1]))
                    {

                        if (grid[current.xPos, current.yPos - 1].gCost > 10 + current.gCost)

                        {
                            grid[current.xPos, current.yPos - 1].gCost = 10 + current.gCost; //Update Lower G Cost

                            grid[current.xPos, current.yPos - 1].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos, current.yPos - 1].parent[1] = current.yPos;

                            grid[current.xPos, current.yPos - 1].fCost = grid[current.xPos, current.yPos - 1].gCost + HCost(grid[current.xPos, current.yPos - 1], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos, current.yPos - 1]))
                            {
                                openList.Remove(grid[current.xPos, current.yPos - 1]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos, current.yPos - 1]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos, current.yPos - 1].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos, current.yPos - 1]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos, current.yPos - 1]))
                        {
                            openList.Add(grid[current.xPos, current.yPos - 1]);
                        }


                    }

                    //down right

                    if (current.yPos - 1 > 0 && current.xPos + 1 < 15 && !grid[current.xPos + 1, current.yPos - 1].blocked && !closedList.Contains(grid[current.xPos + 1, current.yPos - 1]))
                    {

                        if (grid[current.xPos + 1, current.yPos - 1].gCost > 14 + current.gCost)

                        {
                            grid[current.xPos + 1, current.yPos - 1].gCost = 14 + current.gCost; //Update Lower G Cost

                            grid[current.xPos + 1, current.yPos - 1].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos + 1, current.yPos - 1].parent[1] = current.yPos;

                            grid[current.xPos + 1, current.yPos - 1].fCost = grid[current.xPos + 1, current.yPos - 1].gCost + HCost(grid[current.xPos + 1, current.yPos - 1], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos + 1, current.yPos - 1]))
                            {
                                openList.Remove(grid[current.xPos + 1, current.yPos - 1]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos + 1, current.yPos - 1]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos + 1, current.yPos - 1].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos + 1, current.yPos - 1]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos + 1, current.yPos - 1]))
                        {
                            openList.Add(grid[current.xPos + 1, current.yPos - 1]);
                        }


                    }

                    //down left

                    if (current.yPos - 1 > 0 && current.xPos - 1 >= 0 && !grid[current.xPos - 1, current.yPos - 1].blocked && !closedList.Contains(grid[current.xPos - 1, current.yPos - 1]))
                    {

                        if (grid[current.xPos - 1, current.yPos - 1].gCost > 14 + current.gCost)

                        {
                            grid[current.xPos - 1, current.yPos - 1].gCost = 14 + current.gCost; //Update Lower G Cost

                            grid[current.xPos - 1, current.yPos - 1].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos - 1, current.yPos - 1].parent[1] = current.yPos;

                            grid[current.xPos - 1, current.yPos - 1].fCost = grid[current.xPos - 1, current.yPos - 1].gCost + HCost(grid[current.xPos - 1, current.yPos - 1], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos - 1, current.yPos - 1]))
                            {
                                openList.Remove(grid[current.xPos - 1, current.yPos - 1]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos - 1, current.yPos - 1]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos - 1, current.yPos - 1].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos - 1, current.yPos - 1]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos - 1, current.yPos - 1]))
                        {
                            openList.Add(grid[current.xPos - 1, current.yPos - 1]);
                        }


                    }

                    //left

                    if (current.xPos - 1 >= 0 && !grid[current.xPos - 1, current.yPos].blocked && !closedList.Contains(grid[current.xPos - 1, current.yPos]))
                    {

                        if (grid[current.xPos - 1, current.yPos].gCost > 10 + current.gCost)

                        {
                            grid[current.xPos - 1, current.yPos].gCost = 10 + current.gCost; //Update Lower G Cost

                            grid[current.xPos - 1, current.yPos].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos - 1, current.yPos].parent[1] = current.yPos;

                            grid[current.xPos - 1, current.yPos].fCost = grid[current.xPos - 1, current.yPos].gCost + HCost(grid[current.xPos - 1, current.yPos], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos - 1, current.yPos]))
                            {
                                openList.Remove(grid[current.xPos - 1, current.yPos]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos - 1, current.yPos]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos - 1, current.yPos].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos - 1, current.yPos]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos - 1, current.yPos]))
                        {
                            openList.Add(grid[current.xPos - 1, current.yPos]);
                        }


                    }

                    //right

                    if (current.xPos + 1 < 15 && !grid[current.xPos + 1, current.yPos].blocked && !closedList.Contains(grid[current.xPos + 1, current.yPos]))
                    {

                        if (grid[current.xPos + 1, current.yPos].gCost > 10 + current.gCost)

                        {
                            grid[current.xPos + 1, current.yPos].gCost = 10 + current.gCost; //Update Lower G Cost

                            grid[current.xPos + 1, current.yPos].parent[0] = current.xPos; //Update Parents
                            grid[current.xPos + 1, current.yPos].parent[1] = current.yPos;

                            grid[current.xPos + 1, current.yPos].fCost = grid[current.xPos + 1, current.yPos].gCost + HCost(grid[current.xPos + 1, current.yPos], goalX, goalY); //Update FCost

                            if (openList.Contains(grid[current.xPos + 1, current.yPos]))
                            {
                                openList.Remove(grid[current.xPos + 1, current.yPos]); //Remove from open list if lower G cost found

                            }

                        }

                        //Add to the open list in min-max Fcost Order if not already on the list
                        if (!openList.Contains(grid[current.xPos + 1, current.yPos]))
                        {

                            for (int n = 0; n < openList.Count; n++)
                            {
                                if (grid[current.xPos + 1, current.yPos].fCost < openList[n].fCost)
                                {
                                    openList.Insert(n, grid[current.xPos + 1, current.yPos]);
                                    break;
                                }
                            }
                        }
                        //Adds to end of the list if it has the highest F cost & not already on the list
                        if (!openList.Contains(grid[current.xPos + 1, current.yPos]))
                        {
                            openList.Add(grid[current.xPos + 1, current.yPos]);
                        }


                    }





                    if (current.hCost == 0)
                    {

                        found = true;

                        List<string> successPath = new List<string>();
                        successPath.Insert(0, $"{ current.xPos},{ current.yPos}");
                        while (current.parent != null)
                        {
                            successPath.Insert(0, $"{current.parent[0]},{current.parent[1]}");
                            current = grid[current.parent[0], current.parent[1]];
                        }


                        Console.Clear();
                        PrintHeader();
                        PrintPathBoard(grid, successPath);
                        Console.WriteLine($"\nPath Found: ");
                      




                        foreach (string path in successPath)
                        {
                            Console.Write(path + " : ");
                        }





                        Console.WriteLine("\n");
                        Console.WriteLine("Enter 'q' to quit. Press any other key to find a new path on this board");

                        string restart = Convert.ToString(Console.ReadLine());

                        if (restart == "q")
                        {
                            Console.Clear();
                            Console.WriteLine("Goodbye.");
                            Environment.Exit(0);

                        }

                        Console.Clear();

                        openList.Clear();
                        closedList.Clear();
                       
                        for (int x = 0; x < 15; x++)
                        {
                            for (int y = 0; y < 15; y++)
                            {
                                if (grid[x, y].blocked == false)
                                {
                                    grid[x, y] = new Node(x, y);
                                }
                                grid[x, y].gCost = 1000;
                             
                            }
                        }
                       break;

                    

                    }


                    openList.Remove(current);
                    closedList.Add(current);

                }



                if (found == false)
                {
                    Console.Clear();
                    PrintHeader();
                    PrintBoard(grid);
                    Console.WriteLine("There is no valid path from entered start to goal");

                    Console.WriteLine("\n");
                    Console.WriteLine("Enter 'q' to quit. Press any other key to find a new path on this board");



                    openList.Clear();
                    closedList.Clear();

                    for (int x = 0; x < 15; x++)
                    {
                        for (int y = 0; y < 15; y++)
                        {
                            if (grid[x, y].blocked == false)
                            {
                                grid[x, y] = new Node(x, y);
                            }
                            grid[x, y].gCost = 1000;

                        }
                    }

                    string restart = Convert.ToString(Console.ReadLine());

                    if (restart == "q")
                    {
                        Console.Clear();
                        Console.WriteLine("Goodbye.");
                        Environment.Exit(0);

                    }


                    Console.Clear();


                }



            }

        }

        static int HCost(Node testNode, int gX, int gY)
        {
            return 10 * ((Math.Abs(testNode.xPos - gX)) + (Math.Abs(testNode.yPos - gY)));
        }
        public int GCost(Node testNode)
        {
            return 0;
        }

        static void PrintBoard(Node[,] writeGrid)
        {

            string[,] printNode = new string[15, 15];


            for (int y = 14; y >= 0; y--)
            {
                Console.Write("  ");

                for (int x = 0; x < 15; x++)
                {

                    if (writeGrid[x, y].blocked)
                    {
                        printNode[x, y] = "[X] ";
                    }

                    else
                    {

                        printNode[x, y] = "[ ] ";

                    }

                    Console.Write(printNode[x, y]);
                }

                Console.WriteLine("\n");
            }

        }

        static void PrintPathBoard(Node[,] writeGrid, List<string> successList)
        {

            string[,] printNode = new string[15, 15];


            for (int y = 14; y >= 0; y--)
            {
               

                for (int x = 0; x < 15; x++)
                {

                    if (writeGrid[x, y].blocked)
                    {
                        printNode[x, y] = "[X] ";
                    }

                    else
                    {
                        printNode[x, y] = "[ ] ";
                        
                    }
                }

            }


            for (int p=0; p<successList.Count;p++)
            {
                string[] pathXYSplit = successList[p].Split(",");

                int pathX = Convert.ToInt32(pathXYSplit[0]);
                int pathY = Convert.ToInt32(pathXYSplit[1]);
                if (p == 0)
                {

                    printNode[pathX, pathY] = "[S] ";

                }

                else if (p == successList.Count - 1)
                {
                    printNode[pathX, pathY] = "[G] ";

                }

                else

                {
                    printNode[pathX, pathY] = " .  ";

                }
            }

            for (int y = 14; y >= 0; y--)
            {
                Console.Write("  ");

           

                for (int x = 0; x < 15; x++)
                {
                    Console.Write(printNode[x, y]);

                }
                Console.WriteLine("\n");
            }
        }

        static void PrintHeader()
        {
         
        Console.WriteLine("* *-* *-* *-* *-* *-* *-* *-* *-* *-* *-*-* *-* *-* *-* *-*-*" +
                    "\n*_*-*_*-*_*-*_*-*_*  A* Search Algorithm  *-*_*-*_*-*_*-*_*-*" +
                    "\n============================================================= \n");

        }

        static Node[,] newRandomGrid()
        {

            Node[,] grid = new Node[15, 15];

            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    grid[x, y] = new Node(x, y); // Initialize nodes to (x,y) coordinates
                    grid[x, y].gCost = 1000;
                   Random r = new Random(); // 10% Chance each generated node will be blocked
                   if (r.Next(10) == 0) { grid[x, y].blocked = true; }


                    //Block Test
                    /*
                    if (x ==5)
                    {
                        grid[x, y].blocked = true;
                    }
                    */

                }
            }

            return grid;
        }
  
    }

}
