using System;
using System.Threading;
using System.Threading.Tasks;
using Bots.Config.lib;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CMD
{
    class Program
    {
        /// <summary>
        /// Подключение бота.
        /// </summary>
        private static TelegramBotClient bot;
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            Config config = new();
            Console.WriteLine(" Привет мир!");
            bot = new(config.key);

            using CancellationTokenSource cts = new();

            //// Переведено с помошью яндекс переводчика ////
            // Начало приема не блокирует поток вызывающего абонента.
            // Прием выполняется в пуле потоков.
            ReceiverOptions receiverOptions = new()
            {
                // получать все типы обновлений
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            await bot.ReceiveAsync(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                );

            cts.Cancel();

            Console.ReadLine();

        }

        /// <summary>
        /// Отправляем сообщение.
        /// </summary>
        /// <param name="botClient"> Подключение </param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Проверка, есть ли новые сообщения
            if (update.Message is not { } message)
                return;
            
            // Текст сообщения проверка на HULL
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($" Текст сообщения:" +
                $" '{messageText}'\n ID отправителя: {chatId}.");

            if (message.Text.ToLower().Contains("привет"))
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Здравствуйте меня зовут Zheka Game",
                    cancellationToken: cancellationToken
                    );
            }

            if (message.Text.ToLower().Contains("здорова"))
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Здоровей видали, не боялись;)",
                    cancellationToken: cancellationToken
                    );
            }
        }

        /// <summary>
        /// Таск для вывода исключений
        /// </summary>
        /// <param name="botClient"> Бот клиент. </param>
        /// <param name="exception"> Исключение. </param>
        /// <param name="cancellationToken"> Отключение. </param>
        /// <returns> Завершаем вывод исключения и выходим. </returns>
        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Код ошибки Телеграм API:" +
                    $"\n[{apiRequestException.ErrorCode}]" +
                    $"\n{apiRequestException.Message}", _ =>
                    exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}
