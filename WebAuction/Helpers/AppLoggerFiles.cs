namespace WebAuction.Helpers
{
    public static class AppLoggerFiles
    {
        public static void UseLoggerFile(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var path = Path.Combine(System.Environment.CurrentDirectory, "Logs");

                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filelog = Path.Combine(path, "log-{Date}.txt");//file for loges
                //var services = scope.ServiceProvider;//object
                var log = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();//create logger
                log.AddFile(filelog);//add log to file
            }
        }
    }
}
