using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Vigicrues
{

    /// <summary>Client for the Vigicrues API.</summary>
    /// <see href="https://www.vigicrues.gouv.fr/services/1/" />
    public class ApiClient
    {

        internal class TerEntVigiCru
        {

            [JsonPropertyName("count_TerEntVigiCru")]
            public int EntitiesCount { get; set; }
            [JsonPropertyName("vic:TerEntVigiCru")]
            public Entity[] Entities { get; set; } = new Entity[0];
        }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <param name="client"></param>
        public ApiClient(HttpClient client)
        {
            _Client = client;
            _Client.BaseAddress = new Uri("https://www.vigicrues.gouv.fr/services/1/");

            _JsonSerializerOptions = new JsonSerializerOptions {
            };
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

        /// <summary>Gets all the territorial entities concerned by the Vigicrues API.</summary>
        /// <remarks>Every entity is returned with minimal information.</remarks>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>All the territorial entities concerned.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq1" />
        public async Task<IEnumerable<Entity>> GetEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Stream stream = await _Client.GetStreamAsync("TerEntVigiCru.jsonld/");
            TerEntVigiCru result = await JsonSerializer.DeserializeAsync<TerEntVigiCru>(stream, _JsonSerializerOptions, cancellationToken) ?? new TerEntVigiCru();
            Debug.Assert(result.EntitiesCount == result.Entities.Length);

            return result.Entities;
        }

        private readonly HttpClient _Client;
        private readonly JsonSerializerOptions _JsonSerializerOptions;
    }
}
