//Trips - traps - trull konsoolil.Esmalt trükitakse välja mänguväli (3 rida ja 3 veergu) ja siis palutakse sisestada esimese või teise mängija (kuvatakse kumma kord parasjagu on) poolt komaga eraldatud koordinaadid (1,3), millisele positsioonile oma märk kirjutada. Peab kontrollima, et sisestatud on korrektne ja peale seda uuesti välja trükkima

using System;
using System.Collections.Generic;

namespace TripsTrapsTrull
{
    class Program

    {
        private static void Board(int[,] moves, string type = "")
        {
            //Mängulaud
            string[,] board = new string[10, 13]
            {
                { " ","_","_","_"," ","_","_","_"," ","_","_","_"," " },
                { "|"," "," "," ","|"," "," "," ","|"," "," "," ","|" },
                { "|"," "," "," ","|"," "," "," ","|"," "," "," ","|" },//1
                { "|","_","_","_","|","_","_","_","|","_","_","_","|" },
                { "|"," "," "," ","|"," "," "," ","|"," "," "," ","|" },
                { "|"," "," "," ","|"," "," "," ","|"," "," "," ","|" },//2
                { "|","_","_","_","|","_","_","_","|","_","_","_","|" },
                { "|"," "," "," ","|"," "," "," ","|"," "," "," ","|" },
                { "|"," "," "," ","|"," "," "," ","|"," "," "," ","|" },//3
                { "|","_","_","_","|","_","_","_","|","_","_","_","|" }
            };

            string[] markers = { "X", "O", "X", "O", "X", "O", "X", "O", "X" };

            for (int i = 0; i < moves.GetLength(0); i++)
            {

                int x = 0;
                int y = 0;
                if (moves[i, 0] != 0)
                {
                    //Console.WriteLine(moves[i,0]);
                    x = moves[i, 0];
                    y = moves[i, 1];
                }
                else
                {
                    break;
                }
                board[x, y] = markers[i];
            }

            //Mängulaua kuvamine
            for (int i = 0; i < board.GetLength(0); i++)
            {
                string[] temp = new string[13];
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    temp[j] = board[i, j];
                }
                string test = string.Join("", temp);
                Console.WriteLine(test);

            }
        }

        private static void Title()
        {
            Console.Clear();
            Console.WriteLine("MÄNG");
            Console.WriteLine("Trips-traps-trull!");
            Console.WriteLine();
            Console.WriteLine("Mängija on X ja arvuti on O.");
        }

        private static int TransX(int x)
        {
            if (x == 1)
            {
                x = 2;
            }
            else if (x == 2)
            {
                x = 5;
            }
            else if (x == 3)
            {
                x = 8;
            }
            return x;
        }

        private static int TransY(int y)
        {
            if (y == 1)
            {
                y = 2;
            }
            else if (y == 2)
            {
                y = 6;
            }
            else if (y == 3)
            {
                y = 10;
            }
            return y;
        }

        private static int[,] Push(int x, int y, int[,]arr)
        {
            for (int i=0;i<arr.GetLength(0);i++)
            {
                if (arr[i,0] == 0)
                {
                    arr[i, 0] = x;
                    arr[i, 1] = y;
                    break;
                }
            }
            return arr;
        }

        private static bool CheckIfMarked(int [] move, int[,]moves)
        {
            int movesum = TransX(move[0]) + TransY(move[1]);
            List<int> sums = new List<int>();
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                sums.Add(moves[i, 0] + moves[i, 1]);
            }
            if (sums.Contains(movesum))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private static bool CheckIfOver(int[,]moves)
        {
            if (moves[8, 0] != 0)
            {
                return true;
            }
            return false;
        }

        private static bool CheckWin(int[,] playermoves)
        {
            List<int> sums = new List<int>();
            for (int i=0; i < playermoves.GetLength(0); i++)
            {;
                sums.Add(playermoves[i,0]+ playermoves[i, 1]);
            }
            if (sums.Count < 3)
            {
                return false;
            }
            if(sums.Contains(4) && sums.Contains(8) && sums.Contains(12))
            {
                return true;
            } else if (sums.Contains(7) && sums.Contains(11) && sums.Contains(15))
            {
                return true;
            } else if (sums.Contains(10) && sums.Contains(14) && sums.Contains(18))
            {
                return true;
            } else if (sums.Contains(10) && sums.Contains(11) && sums.Contains(12))
            {
                return true;
            } else if (sums.Contains(4) && sums.Contains(11) && sums.Contains(18))
            {
                return true;
            } else if (sums.Contains(4) && sums.Contains(7) && sums.Contains(10))
            {
                return true;
            } else if (sums.Contains(12) && sums.Contains(15) && sums.Contains(18))
            {
                return true;
            } else if (sums.Contains(8) && sums.Contains(11) && sums.Contains(14))
            {
                return true;
            } else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            //PEALKIRI
            Title();

            //MÄNGIJAD
            int[,] moves = new int[9, 2];
            int[,] usermoves = new int[9, 2];
            int[,] compmoves = new int[9, 2];
            bool win = false;

            //MÄNGULAUD
            Board(moves);

            //KASUTAJA SISEND
            int[] numuserinput;

            //ARVUTI SISEND
            int[] compinput;
            Random rand = new Random();

            //MÄNG
            while (true)
            {
                numuserinput = new int[2];
                compinput = new int[2];
                //Kontrolli, kas kõik võimalikud käigud on tehtud
                if (CheckIfOver(moves) || win)
                {
                    Console.WriteLine("Mäng läbi! Väljumiseks vajuta suvalist klahvi. Kui soovid uuesti mängida, sisesta 'j': ");
                    string newgame = Console.ReadLine();
                    if (newgame.ToLower() == "j")
                    {
                        moves = new int[9, 2];
                        usermoves = new int[9, 2];
                        compmoves = new int[9,2];
                        win = false;
                        Board(moves);
                        continue;
                    } else {
                        Console.WriteLine("Oli tore sinuga mängida!");
                        break;
                    }
                }
                Console.WriteLine("Sisesta oma valik kujul 'rida,veerg' ja vajuta ENTER. Väljumiseks sisesta 'v': ");
                string answer = Console.ReadLine();

                //Kas mängija soovib lõpetada?
                if (answer.ToLower()=="v")
                {
                    Console.WriteLine("Nägemist!");
                    break;
                }
                string[] userinput = answer.Split(",");

                //Kas mängija on sisestanud kaks komaga eraldatud arvu?
                if (userinput.Length != 2)
                {
                    Console.WriteLine("Sisestasid midagi valesti.");
                    continue;
                }
                
                bool err = false;
                while (!err)
                {
                    int n = 0;
                    foreach (string input in userinput)
                    {
                        //Kas sisestatud arve saab teisendada täisarvudeks?
                        try
                        {
                            int num = int.Parse(input);
                            numuserinput[n] = num;
                            n++;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            err = true;
                            break;
                        }

                    }
                    if(err)
                    {
                        break;
                    }
                    foreach (int num in numuserinput)
                    {
                        //Kas sisestatud arvud jäävad vahemikku 1-3?
                        if (num != 1 && num != 2 && num != 3)
                        {
                            Console.WriteLine(num);
                            Console.WriteLine("Read ja veerud saavad olla ainult vahemikus 1-3.");
                            err = true;
                            break;
                        }
                    }
                    break;
                }
                
                if (err)
                {
                    Console.WriteLine("Midagi läks valesti. Sisesta uuesti.");
                    continue;
                }

                //Kui sisend on korrektne:
                //Kontrolli, kas ruutu on käidud ja joonista mängulaud
                if(CheckIfMarked(numuserinput, moves))
                {
                    Console.WriteLine("Sinna ruutu on juba käidud. Vali uuesti!");
                    continue;
                }

                int x = TransX(numuserinput[0]);
                int y = TransY(numuserinput[1]);
                Push(x, y, usermoves);
                Push(x, y, moves);
                Board(moves);
                if (CheckWin(usermoves)) {
                    Console.WriteLine("SA VÕITSID! JUHHEI! :)");
                    win = true;
                    continue;
                }
                   
                //Oota sekund, sest arvuti 'mõtleb' natuke. ;)
                System.Threading.Thread.Sleep(1000);

                //Kontrolli, kas mängija tegi viimase võimaliku käigu või ruutu on käidud ja joonista mängulaud
                if (CheckIfOver(moves))
                {
                    Console.WriteLine("SEEKORD JÄI VIIKI.");
                    continue;
                }
                Console.WriteLine("Arvuti kord:");
                do
                {
                    compinput[0] = rand.Next(1, 4);
                    compinput[1] = rand.Next(1, 4);
                } while (CheckIfMarked(compinput, moves));

                Push(TransX(compinput[0]), TransY(compinput[1]), compmoves);
                Push(TransX(compinput[0]), TransY(compinput[1]), moves);
                Board(moves);
                if (CheckWin(compmoves)) {
                    Console.WriteLine("SEEKORD VÕITIS ARVUTI..");
                    win = true;
                    continue;
                }
            }
        }
    }
}
