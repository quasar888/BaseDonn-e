namespace ConsoleApp21
{
    internal class Program
    {
        private static BD[][] basededonnée; // Updated to use BD class
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
                resultRandX = rand.Next(1, 100);
                resultRandY = rand.Next(1, 100);
                Thread.Sleep(1000);
                basededonnée = new BD[resultRandX][];
                try
                {
                    for (int i = 0; i < resultRandX; i++)
                    {
                        basededonnée[i] = new BD[resultRandY];
                        for (int j = 0; j < resultRandY; j++)
                        {
                            // Create BD object using hashed values
                            int id = GenerateId(i, j);
                            string name = GenerateName(i, j);
                            string status = GenerateStatus(i, j);
                            basededonnée[i][j] = new BD(id, name, status);
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
                            BD record = basededonnée[i][j];
                            Console.WriteLine($"ID: {record.Id}, Name: {record.Name}, Status: {record.Status}");
                        }
                    }
                }
                catch
                {
                    testBD();
                }
            }
        }

        private static int GenerateId(int i, int j)
        {
            // Generate a unique ID based on i and j
            return i * 100 + j;
        }

        private static string GenerateName(int i, int j)
        {
            // Generate a pseudo-random name based on hash
            double hashValue = GenerateHashedValue(i, j);
            return $"Name_{hashValue.ToString("F4")}";
        }

        private static string GenerateStatus(int i, int j)
        {
            // Alternate status based on hash
            return GenerateHashedValue(i, j) > 0.5 ? "Active" : "Inactive";
        }

        private static double GenerateHashedValue(int i, int j)
        {
            // Seed value to alter the hash
            int seed = 12345;
            int hash = (i * 31 + j) ^ seed;
            double normalizedHash = (double)(hash & 0x7FFFFFFF) / int.MaxValue; // Normalize to range [0, 1)
            return normalizedHash;
        }
    }
    // Class representing a database model
    public class BD
    {
        public int Id { get; set; }      // Unique identifier
        public string Name { get; set; } // Name of the record
        public string Status { get; set; } // Status of the record (e.g., Active, Inactive)

        public BD(int id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
    }

}
