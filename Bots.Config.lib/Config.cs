using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Config.lib
{
    /// <summary>
    /// Конфигурационный файл и класс.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Приватнвй ключ.
        /// </summary>
        public string key { get; }

        /// <summary>
        /// Публичная переменная.
        /// </summary>
        public string AccessToken;

        public Config()
        {
            key = "6239153127:AAGik2jc-_UDxgTpE-ORtoTP3Ffa6dy2ujM";
            switch (AccessToken)
            {
                case (null):
                    AccessToken = key;
                    break;
                case (""):
                    AccessToken = key;
                    break;
                case (" "):
                    AccessToken = key;
                    break;
            }
        }
    }
}
