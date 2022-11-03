using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace hw._18._4.model
{
    internal class YoutubeConnector
    {
        YoutubeClient connector;
        public string Url{get;}
        public YoutubeConnector(string url)
        {
            connector = new YoutubeClient();
            Url = url;  
        }

        public bool GetVideoInfo(CancellationToken ct)
        {
            try
            {
                var video = connector.Videos.GetAsync(Url, ct).Result;
                Console.WriteLine($"Название: {video.Title} Автор: {video.Author.ChannelTitle} Продолжительность: {video.Duration} \nDescription: {video.Description}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool DownloadVideo(CancellationToken ct, IProgress<double> progress)
        {
            try
            {
                var streamManifest = connector.Videos.Streams.GetManifestAsync(Url).Result;
                var streamInfo = streamManifest
                    .GetVideoOnlyStreams()
                    .Where(s => s.Container == Container.Mp4)
                    .GetWithHighestVideoQuality();

                connector.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}", progress,ct).AsTask().Wait();
                
                Console.WriteLine("\nСкачивание завершено.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
