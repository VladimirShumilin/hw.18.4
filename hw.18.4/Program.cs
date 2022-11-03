using hw._18._4.Commands;
using hw._18._4.model;


if (args.Length == 0)
{
    Console.WriteLine("Ссылка не распознана. Пример запуска hw.18.4.exe https://www.youtube.com/watch?v=tsbg0eiKU1I");
    return 0;
}

YoutubeConnector reciever = new YoutubeConnector(args[0]);

using CmdVideoInfo videoInfo = new CmdVideoInfo(reciever);

using CmdDownloadVideo loadVideo = new CmdDownloadVideo(reciever);

ConsoleKeyInfo key  = new ConsoleKeyInfo();

while (true)
{
   
    Console.WriteLine($"Ссылка на видео: {reciever.Url}\nВведите команду:\n1 - для получения информации о видео.\n2 - для загрузки видео.\n3 - для отмены загрузки\nESC - для выхода\n"); 
   
    switch (key.Key)
    {
        case ConsoleKey.Escape:
            return 0;
        case ConsoleKey.D1:
            RunCommand(videoInfo).ConfigureAwait(false);
            break;
        case ConsoleKey.D2:
            RunCommand(loadVideo).ConfigureAwait(false);
            break;
        case ConsoleKey.D3:
            loadVideo.Cancel();
            break;
    }
    key = Console.ReadKey();
    Console.Clear();
}


static async Task<bool> RunCommand(Command cmd)
{
   return await cmd.Run();
}

