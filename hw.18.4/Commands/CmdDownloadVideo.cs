using hw._18._4.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw._18._4.Commands
{
    internal class CmdDownloadVideo : Command, IDisposable
    {
        YoutubeConnector receiver;
        CancellationTokenSource? cts;
        ProgressFileDownload progress = new ProgressFileDownload();

        public CmdDownloadVideo(YoutubeConnector receiver)
        {
            this.receiver = receiver;
        }
        public override void Cancel()
        {
            cts?.Cancel();
            Console.WriteLine("\nЗагрузка файла отменена");
        }

        public void Dispose()
        {
            cts?.Dispose();
        }

        public override Task<bool> Run()
        {
            cts = new CancellationTokenSource();
            return Task.Run(() =>
            {
                Console.WriteLine("\nПытаемся загрузить файл ...");
                receiver?.DownloadVideo(cts.Token, progress);
                return true;
            }).ContinueWith((result) =>
            {
                cts.Dispose();
                cts = null;
                return result.Result;
            });
        }

    }
}
