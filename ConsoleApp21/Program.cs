namespace ConsoleApp21
{
    internal class Program
    {
        private static double[][] basededonnée;
        private static readonly object lockObject = new object();

        static int resultRandX { get; set; }
        static int resultRandY { get; set; }

        static Random rand { get; set; } = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ParameterizedThreadStart _params = new ParameterizedThreadStart(testTarin);
            Thread test = new Thread(_params);
            test.Start();

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                testBD();
            }
        }

        private static void testTarin(object? obj)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        static void testBD()
        {
            lock (lockObject)
            {
                resultRandX = rand.Next(1, 10000);
                resultRandY = rand.Next(1, 10000);
                //Thread.Sleep(1000);
                basededonnée = new double[resultRandX][];
                try
                {
                    for (int i = 0; i < resultRandX; i++)
                    {
                        basededonnée[i] = new double[resultRandY];
                        for (int j = 0; j < resultRandY; j++)
                        {
                            basededonnée[i][j] = new double();
                        }
                    }
                }
                catch
                {
                    testBD();
                }

                try
                {
                    for (int i = 0; i < resultRandX; i++)
                    {
                        for (int j = 0; j < resultRandY; j++)
                        {
                            basededonnée[i][j] = rand.NextDouble();
                            //Console.WriteLine(basededonnée[i][j]);
                        }
                    }
                    Console.WriteLine("/********************************************");
                }
                catch
                {
                    testBD();
                }
            }
        }
    }
}
