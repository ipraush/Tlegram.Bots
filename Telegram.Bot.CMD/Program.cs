using System;
using System.Threading.Tasks;
using Bots.Config.lib;
using Telegram.Bot;

namespace Telegram.Bot.CMD
{
    class Program
    {
        private static TelegramBotClient bot;

        static async Task Main(string[] args)
        {
            Config config = new();
            Console.WriteLine("Привет мир!");
            bot = new(config.key);

            var me = await bot.GetMeAsync();
            Console.WriteLine($"Мой ид: {me.Id} меня зовут {me.FirstName}.");

            Console.ReadKey();
        }
    }
}
