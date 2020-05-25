using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncCompare.Services
{
    public class CalculationService
    {

        public int Double(int num)
        {
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            return num * 2;
        }

        public int Halve(int num)
        {
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();

            return num / 2;
        }

        public async Task<int> DoubleAsync(int num)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            return num * 2;
        }

        public async Task<int> HalveAsync(int num)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return num / 2;
        }
    }
}
