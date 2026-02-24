using System.Net.Http.Headers;
using System.Net.Mime;

namespace BunnyDDNS.Core.Utilities;

public enum IpVersion
{
    IPv4,
    IPv6
}

public class IpUtils
{
    private static HttpClient Client;

    public IpUtils(HttpClient client)
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
        Client = client;
    }

    public string GetCurrentIp(IpVersion ipVersion = IpVersion.IPv4)
    {
        var url = ipVersion switch
        {
            IpVersion.IPv4 => "https://ipv4.text.myip.wtf",
            IpVersion.IPv6 => "https://ipv6.text.myip.wtf",
            _ => throw new ArgumentOutOfRangeException(nameof(ipVersion), "Unsupported IP version.")
        };

        var response = Client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result.Trim(Environment.NewLine.ToCharArray());
    }
}
