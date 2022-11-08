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
