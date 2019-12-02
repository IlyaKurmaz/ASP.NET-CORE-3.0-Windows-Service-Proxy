using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Renci.SshNet;
using Serilog;
using SFTP_Proxy_Service.Enums;
using SFTP_Proxy_Service.Options;
using System;

namespace SFTP_Proxy_Service.Services
{
    public class SftpSenderService
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;
        private readonly int _port;
        public SftpSenderService(IOptions<SFTPOptions> options)
        {
            _host = options.Value.Host;
            _username = options.Value.UserName;
            _password = options.Value.Password;
            _port = options.Value.Port;
        }

        public UploadStatus Send(IFormFile file)
        {
            var connectionInfo = new Renci.SshNet.ConnectionInfo(host: _host, port: _port, username: _username, new PasswordAuthenticationMethod(_username, _password));

            using (var sftp = new SftpClient(connectionInfo))
            {

                try
                {
                    sftp.Connect();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Connection refused");

                    return UploadStatus.ConnectionRefused;
                }

                //sftp.ChangeDirectory("/DestinyFolder w/e");

                try
                {
                    sftp.UploadFile(file.OpenReadStream(), file.FileName, true);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, $"Upload failed for file {file.FileName}");
                    return UploadStatus.UploadFailed;
                }

                sftp.Disconnect();
            }

            Log.Information($"File {file.Name} was uploaded successfully");
            return UploadStatus.UploadSuccessful;
        }

    }
}
