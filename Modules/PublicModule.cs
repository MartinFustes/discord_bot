using Discord;
using Discord.Commands;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using TextCommandFramework.Services;

namespace TextCommandFramework.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us
        public PictureService PictureService { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        [Command("help")]
        public async Task HelpAsync()
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + "/help.txt"))
            {
                string text = reader.ReadToEnd();
                await ReplyAsync(text.Replace('!', Program._config["commandchard"][0]));
                // string line;
                // while ((line = reader.ReadLine()) != null)
                // {
                //     list.Add(line); // Add to list.
                //     await ReplyAsync(Program._config["commandchard"][0] + line); // Write command
                // }
            }
        }

        [Command("cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }
        [Command("catsay")]
        public async Task CatSayAsync([Remainder] string message)
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatSayPictureAsync(message);
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }
        [Command("catlike")]
        public async Task CatTagAsync([Remainder] string message)
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatTagPictureAsync(message);
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }
        [Command("catlikesay")]
        public async Task CatTagSayAsync([Remainder] string message)
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatTagSayPictureAsync(message);
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }
        [Command("catgif")]
        public async Task CatGifAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatGifAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.gif");
        }
        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user ??= Context.User;

            await ReplyAsync(user.ToString());
        }

        [Command("insultar")]
        public Task InsultUser(IUser user = null)
        {
            string insulto = "warra";
            return ReplyAsync(user.Mention + " es un/a " + insulto);
        }

        // // Ban a user
        // [Command("ban")]
        // [RequireContext(ContextType.Guild)]
        // // make sure the user invoking the command can ban
        // [RequireUserPermission(GuildPermission.BanMembers)]
        // // make sure the bot itself can ban
        // [RequireBotPermission(GuildPermission.BanMembers)]
        // public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        // {
        //     await user.Guild.AddBanAsync(user, reason: reason);
        //     await ReplyAsync(user.Mention + " a tomar por culo! >:)))");
        // }

        // Disconnect a user
        [Command("atucasa")]
        [RequireContext(ContextType.Guild)]
        // make sure the user invoking the command can kick
        [RequireUserPermission(GuildPermission.KickMembers)]
        // make sure the bot itself can kick
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task OutUserAsync(IGuildUser user, [Remainder] IVoiceChannel channel = null)
        {
            await user.Guild.MoveAsync(user, channel);
            await ReplyAsync(user.Mention + " a tomar por culo! >:)))");
        }


        // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        [Command("echo")]
        public Task EchoAsync([Remainder] string text)
            // Insert a ZWSP before the text to prevent triggering other bots!
            => ReplyAsync('\u200B' + text);

        // 'params' will parse space-separated elements into a list
        [Command("list")]
        public Task ListAsync(params string[] objects)
            => ReplyAsync("You listed: " + string.Join("; ", objects));

        // Setting a custom ErrorMessage property will help clarify the precondition error
        [Command("guild_only")]
        [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
        public Task GuildOnlyCommand()
            => ReplyAsync("Nothing to see here!");

        [Command("penis")]
        public Task PenisLength()
        {
            string penis = "8";
            int rnd = (new System.Random()).Next(1, 10);
            for (int i = 0; i < rnd; i++)
            {
                penis = penis + "=";
            }
            return ReplyAsync(penis + "D");
        }
    }
}
