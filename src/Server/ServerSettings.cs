using System.IO;
using System.Text;
using YamlDotNet.RepresentationModel.Serialization;
using YamlDotNet.RepresentationModel.Serialization.NamingConventions;

namespace TeamCityNotifier.NotificationServer
{
    class ServerSettings
    {
        [YamlAlias("port")]
        public int Port { get; set; }

        [YamlAlias("teamCity")]
        public TeamCitySettings TeamCity { get; set; }

        public static ServerSettings ReadFrom(string filePath)
        {
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            var reader = new StreamReader(filePath, Encoding.UTF8);
            return deserializer.Deserialize<ServerSettings>(reader);
        }
    }

    class TeamCitySettings
    {
        [YamlAlias("url")]
        public string Url { get; set; }

        [YamlAlias("username")]
        public string Username { get; set; }

        [YamlAlias("password")]
        public string Password { get; set; }
    }
}
