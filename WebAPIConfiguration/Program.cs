using Microsoft.Extensions.Options;
using ConfigurationsInConsoleApp;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
}

var app = builder.Build();

app.MapGet("/get-settings", (IConfiguration config, IOptions<Settings> options)
    =>
{
    var value1 = config["Settings:SettingKey1"];
    var value2 = config.GetSection(nameof(Settings))
                       .Get<Settings>()?.SettingKey2;
    var value3 = options.Value.SettingKey3;

    return new
    {
        Settingkey1 = $"'{value1}'  via IConfiguration",
        Settingkey2 = $"'{value2}'  via IConfiguration and Binding",
        Settingkey3 = $"'{value3}'  via IOptions"
    };
});


app.Run();
