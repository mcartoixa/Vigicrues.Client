using System.Net.Http;

namespace Vigicrues
{
    /// <summary>Client for the Vigicrues API.</summary>
    /// <see href="https://www.vigicrues.gouv.fr/services/1/" />
    public class ApiClient
    {

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <param name="client"></param>
        public ApiClient(HttpClient client)
        {
            _Client = client;
        }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        public ApiClient(HttpMessageHandler messageHandler) :
            this(new HttpClient(messageHandler))
        { }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        public ApiClient(HttpMessageHandler messageHandler, bool disposeHandler) :
            this(new HttpClient(messageHandler, disposeHandler))
        { }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        public ApiClient() :
            this(new HttpClient())
        { }

        private readonly HttpClient _Client;
    }
}
