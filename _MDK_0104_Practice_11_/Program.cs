using System;
using System.Runtime.InteropServices;
using System.Threading;
namespace SemaphoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                polyanka[] animals = new polyanka[7];
                for (int i = 0; i < animals.Length; i++)
                {
                    animals[i] = new polyanka(i + 1);
                }
                foreach (polyanka animal in animals)
                {
                    animal.myThread.Join();
                }
                Console.WriteLine("Все животные накушались");
            }
        }
    }
    class polyanka
    {
        // создаем семафор
        static Semaphore sem = new Semaphore(4, 4);
        public Thread myThread;
        int count = 4;// счетчик чтения
        gorging status;
        public polyanka (int i)
        {       
        myThread = new Thread(animals);
        myThread.Name = $"Животное {i.ToString()}";
        myThread.Start();
        status = new gorging(myThread.Name);
        
        }
    public void animals()
    {
        while (count > 0)
        {
            sem.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} Cидит на полянке");
            Console.WriteLine($"{Thread.CurrentThread.Name} Кушает");
            Thread.Sleep(1000);
            Console.WriteLine($"{Thread.CurrentThread.Name} Покидает полянку");

                status.DecreaseHunger();
                status.PrintStatus();

                sem.Release();
            count--;
                Thread.Sleep(1000);
        }
    }
        }
    class gorging 
    {
        public string Name { get; set; }
        public int HungerLevel { get; set; }


        public gorging(string name)
        {
            Name = name;
            HungerLevel = 100;
        }

        public void DecreaseHunger()
        {
            HungerLevel -= 10;
        }

        public void PrintStatus()
        {
            Console.WriteLine($"{Name} - Уровень голода: {HungerLevel}%");
        }
    }
   
}