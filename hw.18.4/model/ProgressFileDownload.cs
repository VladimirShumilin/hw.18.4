using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw._18._4.model
{
    internal class ProgressFileDownload : IProgress<double>
    {
        private double prevProgress;
        public void Report(double value)
        {
            if(value < prevProgress+0.1)
                return;

            prevProgress = value;
            Console.WriteLine($"Видео загружено на {value}%");
        }
    }
}
