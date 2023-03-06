using System;
using System.Threading;
using System.Threading.Tasks;
using Bots.Config.lib;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

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

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            var updateReceiver = new QueuedUpdateReceiver(bot, receiverOptions);

            // to cancel
            var cts = new CancellationTokenSource();

            try
            {
                await foreach (Update update in updateReceiver.WithCancellation(cts.Token))
                {
                    if (update.Message is Message message)
                    {
                        await bot.SendTextMessageAsync(
                            message.Chat,
                            $"Still have to process {updateReceiver.PendingUpdates} updates"
                        );
                    }
                }
            }
            catch (OperationCanceledException exception)
            {
            }


            Console.ReadKey();
        }
    }
}
