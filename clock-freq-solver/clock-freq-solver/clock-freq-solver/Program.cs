using System;
using System.Collections.Generic;

namespace clock_freq_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve some clock frequencies");

            var crystal = 8000000; //8 MHz crystal

            var desiredFreq = 512 * 44100;

            List<double> divm3 = new List<double>();
            List<double> divn3 = new List<double>();
            List<double> divp3 = new List<double>();
            List<double> divr3 = new List<double>();

            for(int x = 1; x <= 64; x++)
            {
                double var = 1.0 * x;
                divm3.Add(var);
            }

            for (int x = 1; x <= 512; x++)
            {
                double var = 1.0 * x;
                divn3.Add(var);
            }

            for (int x = 1; x <= 128; x++)
            {
                double var = 1.0 * x;
                divp3.Add(var);
            }

            for (int x = 1; x <= 128; x++)
            {
                double var = 1.0 * x;
                divr3.Add(var);
            }

            foreach(var m in divm3)
            {
                double tempFreq = crystal / m;

                foreach(var n in divn3)
                {
                    tempFreq *= n;
                    double secondaryFreq = tempFreq;

                    if(tempFreq < desiredFreq)
                    {
                        continue;
                    }

                    foreach (var p in divp3)
                    {
                        tempFreq /= p;

                        var difference = desiredFreq - tempFreq;
                        var error = Math.Abs(difference / desiredFreq);


                        if(error < 0.001)
                        {
                            Console.WriteLine("found valid combo!");
                            Console.WriteLine(tempFreq);
                            Console.WriteLine($"m: {m}");
                            Console.WriteLine($"n: {n}");
                            Console.WriteLine($"p: {p}");

                            foreach(var r in divr3)
                            {
                                secondaryFreq /= r;

                                var secondaryDiff = secondaryFreq - 2000000;
                                var secondaryError = Math.Abs(secondaryDiff / 2000000);

                                if(secondaryError < 0.01)
                                {
                                    Console.WriteLine("Secondary Good!");
                                    Console.WriteLine($"r: {r}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
