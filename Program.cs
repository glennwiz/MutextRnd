class Program
{
    static int counter = 0;
    static Mutex mutex = new Mutex();
    static Random random = new Random();
    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread newThread = new Thread(DoWork);
            newThread.Start();

            Thread newThread2 = new Thread(DoWorkWithMutex);
            newThread2.Start();
        }
    }

    static void DoWork()
    {

        for (int i = 0; i < 100; i++)
        {
            if (i % 5 == 0)
            {
                Console.ForegroundColor = (ConsoleColor)random.Next(1, 16);

                Console.Write("|Temp is" + counter + "|");
            }

            int temp = counter;
            Thread.Sleep(1);
            counter = temp + 1;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Final counter value: {counter} <--------------------<");
    }

    static void DoWorkWithMutex()
    {
        if (mutex.WaitOne())
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 5 == 0)
                    {
                        Console.ForegroundColor = (ConsoleColor)random.Next(1, 16);

                        Console.Write("|Temp is " + counter + "|");
                    }
                    int temp = counter;
                    Thread.Sleep(1);
                    counter = temp + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                mutex.ReleaseMutex();
            }

        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n\nFinal counter value: {counter} <--------------------<\n");
    }
}
