using System.ServiceModel;

namespace Signatures.Signant.Client.Proxy
{
    public class PostingServiceFactory
    {
        public static IPostingsService GetPostingsService()
        {
            // TODO: move settings to appsettings.json
            BasicHttpBinding result = new BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;
            result.Security.Mode = BasicHttpSecurityMode.Transport;

            return new PostingsServiceClient(result, new EndpointAddress("https://test3.signant.no/WS/V1/PostingsService.svc"));
        }
    }
}
