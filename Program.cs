using DSharpPlus;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FYB
{
    class Program
    {
        private static DiscordClient Client { get; set; }

        static async Task Main(string[] args)
        {
            JSONReader reader = new JSONReader();
            await reader.Read();

            DiscordConfiguration config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.GuildMessages | DiscordIntents.MessageContents,
                Token = reader.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);

            Client.MessageCreated += Client_MessageCreated;

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task Client_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs args)
        {
            if (args.Author.IsBot) return;

            string lower = args.Message.Content.ToLower();

            Match match = Regex.Match(lower, "(.*)(g)(.*)(u)(.*)(s)(.*)");

            if (!match.Success) return;

            StringBuilder builder = new StringBuilder().Append("FUCK YOU! BABIES.\n```ansi\n");

            for (int i = 1; i < match.Groups.Count; i++)
            {
                switch (match.Groups[i].ToString())
                {
                    case "g":
                    case "u":
                    case "s":
                        builder.Append("\u001b[0;41;30m");
                        builder.Append(match.Groups[i]);
                        break;
                    default:
                        builder.Append("\u001b[0;47;30m");
                        builder.Append(match.Groups[i]);
                        break;
                }
            }

            builder.Append("```");

            await args.Message.RespondAsync(builder.ToString());
        }
    }
}
