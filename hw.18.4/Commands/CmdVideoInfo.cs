using hw._18._4.model;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw._18._4.Commands
{
    internal class CmdVideoInfo : Command, IDisposable
    {
        YoutubeConnector receiver;
        CancellationTokenSource? cts;

        public CmdVideoInfo(YoutubeConnector receiver)
        {
            this.receiver = receiver;
        }
        public override void Cancel()
        {
            cts?.Cancel(); 
        }

        public void Dispose()
        {
            cts?.Dispose();
        }

        public override Task<bool> Run()
        {
            cts = new CancellationTokenSource();
                return Task.Run(  () =>
                {
                    Console.WriteLine("\nПытаемся получить информацию о файле ...");
                    return receiver.GetVideoInfo(cts.Token);
                 
                }).ContinueWith((result) => 
                {
                    cts.Dispose();
                    cts = null;
                    return result.Result;
                });
        }
    }
}
