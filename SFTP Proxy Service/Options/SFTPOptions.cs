using Microsoft.Extensions.Options;

namespace SFTP_Proxy_Service.Options
{
    /// <summary>
    /// Binding class for SFTP configuration section.
    /// <see cref="Startup"/>
    /// </summary>
    public sealed class SFTPOptions
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }
    }
}