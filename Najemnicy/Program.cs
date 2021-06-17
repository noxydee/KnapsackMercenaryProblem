using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace loch_v01
{
    
    struct mercenary
    {
        public int number;
        public int strenght;
        public int food;
        public int entertainment;
        
        public mercenary(int strenght,int food,int entertainment,int number)
        {
            this.strenght = strenght;
            this.food = food;
            this.entertainment = entertainment;
            this.number = number;
        }
    }

    class Program
    {

        static void set_edge_to_zero(int[,,] dynamic_array,int number_of_mercenaries,int food,int entertainment)
        {
            for (int i = 0; i < number_of_mercenaries; i++)
            {
                dynamic_array[i, 0, 0] = 0;
            }
            for (int i = 0; i < food; i++)
            {
                dynamic_array[0, i, 0] = 0;
            }
            for (int i = 0; i < entertainment; i++)
            {
                dynamic_array[0, 0, i] = 0;
            }
        }

        static void knapsack(int food, int entertainment, mercenary[] p,int number_of_mercenaries,int[,,] dynamic_array)
        {
            for(int i=1;i<=number_of_mercenaries;i++)
            {
                for(int j=0;j<=food;j++)
                {
                    for(int k=0;k<=entertainment;k++)
                    {
                        if(p[i-1].food>j)
                        {
                            dynamic_array[i, j, k] = dynamic_array[i - 1, j, k];
                        }
                        else
                        {
                            if(p[i-1].entertainment>k)
                            {
                                dynamic_array[i, j, k] = dynamic_array[i - 1, j, k];
                            }
                            else
                            {
                                dynamic_array[i, j, k] = Math.Max(dynamic_array[i - 1, j, k], dynamic_array[i - 1, j - p[i - 1].food, k - p[i - 1].entertainment] + p[i - 1].strenght);
                            }
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            int food;
            int entertainment;
            int number_of_mercenaries;

            string[] word = new string[2];
            string[] word0= new string[3];
            int iix = 0;
            int counter0 = 1;
            

            string Lines;
            //duzy plik to big_big
            //Lines = File.ReadAllLines("big_big.txt");


            Lines = Console.ReadLine();
            word = Lines.Split(" ");



            food = Convert.ToInt32(word[0]);
            entertainment = Convert.ToInt32(word[1]);

            string xo = Console.ReadLine();

            number_of_mercenaries = Convert.ToInt32(xo);

            mercenary[] mercenaries = new mercenary[number_of_mercenaries];

            mercenary[] chosen_mercenaries = new mercenary[number_of_mercenaries];

            int[,,] dynamic_array = new int[number_of_mercenaries + 1, food + 1,entertainment+1];

            set_edge_to_zero(dynamic_array, number_of_mercenaries, food, entertainment);

            for (int i=0; i<number_of_mercenaries; i++)
            {
                string line = Console.ReadLine();
                word0 = line.Split(" ");

                mercenary p = new mercenary(Convert.ToInt32(word0[0]), Convert.ToInt32(word0[1]), Convert.ToInt32(word0[2]),counter0);
                mercenaries[iix] = p;
                iix++;
                counter0++;
            }










            //Console.WriteLine("food -> " + food);
            //Console.WriteLine("entertainment -> "+ entertainment);
            //Console.WriteLine("number of mercenaries -> "+ number_of_mercenaries);
            //Console.WriteLine(" ");

            knapsack(food, entertainment,mercenaries,number_of_mercenaries,dynamic_array);

            int j = food;
            int k = entertainment;
            int finalfood = 0;
            int finalentertainment = 0;
            int finalstrenght = 0;

            int counter = 0;

            for (int i = number_of_mercenaries; i > 0; i--)
            {
                if (dynamic_array[i,j,k] > dynamic_array[i - 1,j,k])
                {
                    chosen_mercenaries[counter] = mercenaries[i - 1];
                    counter++;
                    //Console.WriteLine("mercanary number:"+i+" -> (" + mercenaries[i - 1].strenght + ", " + mercenaries[i - 1].food + ", " + mercenaries[i - 1].entertainment + ") ");
                    finalfood += mercenaries[i - 1].food;
                    finalstrenght += mercenaries[i - 1].strenght;
                    finalentertainment +=mercenaries[i - 1].entertainment;
                    j -= mercenaries[i - 1].food;
                    k -= mercenaries[i - 1].entertainment;
                }
            }

            //Console.WriteLine(" ");
            //Console.WriteLine("left food -> " + (food-finalfood));
            //Console.WriteLine("left entertainment -> " + (entertainment-finalentertainment));
            //Console.WriteLine("final strenght -> " + finalstrenght);

            Console.WriteLine(finalstrenght);

            foreach(mercenary p in chosen_mercenaries.Reverse())
            {
                if(p.number!=0)
                {
                    Console.Write(p.number + " ");
                }
                
            }
            Console.WriteLine(" ");

            /*for (int i = 0; i <= number_of_mercenaries; i++)
            {
                for (int jj = 0; jj <= food; jj++)
                {
                    for (int kk = 0; kk <= entertainment; kk++)
                    {
                        Console.Write(dynamic_array[i,jj,kk]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }*/
        }  
    }
}
