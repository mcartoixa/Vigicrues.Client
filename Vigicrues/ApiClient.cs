﻿using System;
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
    public sealed class ApiClient:
        IDisposable
    {

        internal class InfoVigiCru
        {

            [JsonPropertyName("vic:InfoVigiCru")]
            public Information Information { get; set; } = new Information();
        }

        internal class StaEntVigiCruList
        {

            [JsonPropertyName("count_StaEntVigiCru")]
            public int StationsCount { get; set; }
            [JsonPropertyName("vic:StaEntVigiCru")]
            public StationEntity[] Stations { get; set; } = new StationEntity[0];
        }

        internal class StaEntVigiCru
        {

            [JsonPropertyName("vic:StaEntVigiCru")]
            public StationEntity Station { get; set; } = new StationEntity();
        }

        internal class TerEntVigiCruList
        {

            [JsonPropertyName("count_TerEntVigiCru")]
            public int TerritoriesCount { get; set; }
            [JsonPropertyName("vic:TerEntVigiCru")]
            public TerritoryEntity[] Territories { get; set; } = new TerritoryEntity[0];
        }

        internal class TerEntVigiCru
        {

            [JsonPropertyName("vic:TerEntVigiCru")]
            public TerritoryEntity Territory { get; set; } = new TerritoryEntity();
        }

        internal class TronEntVigiCruList
        {

            [JsonPropertyName("count_TronEntVigiCru")]
            public int SectionsCount { get; set; }
            [JsonPropertyName("vic:TronEntVigiCru")]
            public SectionEntity[] Sections { get; set; } = new SectionEntity[0];
        }

        internal class TronEntVigiCru
        {

            [JsonPropertyName("vic:TronEntVigiCru")]
            public SectionEntity Section { get; set; } = new SectionEntity();
        }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <param name="client">The HTTP client to be used by the created instance.</param>
        /// <param name="disposeHttpClient">Whether to dispose the specified <paramref name="client"/> along with the created instance. This should usually bet set to <c>false</c>.</param>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests">Use IHttpClientFactory to implement resilient HTTP requests</seealso>
        public ApiClient(HttpClient client, bool disposeHttpClient)
        {
            _Client = client;
            _Client.BaseAddress = new Uri("https://www.vigicrues.gouv.fr/services/1/");

            _DisposeHttpClient = disposeHttpClient;

            _JsonSerializerOptions = new JsonSerializerOptions {
                Converters = {
                    new Serialization.DateTimeOffsetJsonConverter()
                }
            };
        }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <param name="client">The HTTP client to be used by the current instance.</param>
        /// <remarks>The <paramref name="client"/> will not be disposed with the created instance.</remarks>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests">Use IHttpClientFactory to implement resilient HTTP requests</seealso>
        public ApiClient(HttpClient client):
            this(client, false)
        { }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <remarks>This creates a dedicated <see cref="HttpClient" /> that will be disposed with the created instance.</remarks>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests">Use IHttpClientFactory to implement resilient HTTP requests</seealso>
        public ApiClient(HttpMessageHandler messageHandler, bool disposeHandler) :
            this(new HttpClient(messageHandler, disposeHandler), true)
        { }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <remarks>This creates a dedicated <see cref="HttpClient" /> that will be disposed with the created instance.</remarks>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests">Use IHttpClientFactory to implement resilient HTTP requests</seealso>
        public ApiClient(HttpMessageHandler messageHandler) :
            this(new HttpClient(messageHandler), true)
        { }

        /// <summary>Creates an new instance of the <see cref="ApiClient" /> type.</summary>
        /// <remarks>This creates a dedicated <see cref="HttpClient" /> that will be disposed with the created instance.</remarks>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests">Use IHttpClientFactory to implement resilient HTTP requests</seealso>
        public ApiClient() :
            this(new HttpClient(), true)
        { }

        /// <summary>Finalizes the current instance.</summary>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests">Use IHttpClientFactory to implement resilient HTTP requests</seealso>
        ~ApiClient()
        {
            _Dispose(false);
        }

        /// <summary>Releases the unmanaged resources used by the current <see cref="ApiClient" />.</summary>
        public void Dispose()
        {
            _Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Gets the flooding information about the metropolitan area.</summary>
        /// <param name="mode">The mode in which to retrieve the information.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the metropolitan area.</returns>
        public Task<Information> GetMetropolitanAreaInformationAsync(InformationMode mode = InformationMode.Normal, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _GetEntitiesInformationAsync(EntityType.MetropolitanArea, mode, cancellationToken);
        }

        /// <summary>Gets the flooding information about the counties.</summary>
        /// <param name="mode">The mode in which to retrieve the information.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the counties.</returns>
        public Task<Information> GetCountiesInformationAsync(InformationMode mode = InformationMode.Normal, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _GetEntitiesInformationAsync(EntityType.County, mode, cancellationToken);
        }

        /// <summary>Gets all the sections concerned by the Vigicrues API.</summary>
        /// <remarks>Every entity is returned with minimal information.</remarks>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>All the territorial entities concerned.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq5" />
        public async Task<IEnumerable<SectionEntity>> GetSectionsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (Stream stream = await _Client.GetStreamAsync("TronEntVigiCru.jsonld/"))
            {
                TronEntVigiCruList result = await JsonSerializer.DeserializeAsync<TronEntVigiCruList>(stream, _JsonSerializerOptions, cancellationToken) ?? new TronEntVigiCruList();
                Debug.Assert(result.SectionsCount == result.Sections.Length);

                return result.Sections;
            }
        }

        /// <summary>Gets detailed information about the specified <paramref name="section" />.</summary>
        /// <param name="section">The territory to get detailed information about.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="section" />.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq5" />
        public async Task<SectionEntity> GetSectionAsync(SectionEntity section, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (Stream stream = await _Client.GetStreamAsync($"TronEntVigiCru.jsonld/?CdEntVigiCru={section.Reference}&TypEntVigiCru={section.EntityType:d}"))
            {
                TronEntVigiCru result = await JsonSerializer.DeserializeAsync<TronEntVigiCru>(stream, _JsonSerializerOptions, cancellationToken) ?? new TronEntVigiCru();

                return result.Section;
            }
        }

        /// <summary>Gets the flooding information about the specified <paramref name="section"/>.</summary>
        /// <param name="section">The section to get the information about.</param>
        /// <param name="mode">The mode in which to retrieve the information.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="section"/>.</returns>
        public Task<Information> GetSectionInformationAsync(SectionEntity section, InformationMode mode = InformationMode.Normal, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _GetEntityInformationAsync(section, mode, cancellationToken);
        }

        /// <summary>Gets all the sections concerned by the Vigicrues API.</summary>
        /// <remarks>Every entity is returned with minimal information.</remarks>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>All the territorial entities concerned.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq2" />
        public async Task<IEnumerable<StationEntity>> GetStationsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (Stream stream = await _Client.GetStreamAsync("StaEntVigiCru.jsonld/"))
            {
                StaEntVigiCruList result = await JsonSerializer.DeserializeAsync<StaEntVigiCruList>(stream, _JsonSerializerOptions, cancellationToken) ?? new StaEntVigiCruList();
                Debug.Assert(result.StationsCount == result.Stations.Length);

                return result.Stations;
            }
        }

        /// <summary>Gets all the sections contained in the specified <paramref name="territory"/>.</summary>
        /// <remarks>Every entity is returned with minimal information.</remarks>
        /// <param name="territory">The territory to get the sections of.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>All the territorial entities concerned.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq2" />
        public async Task<IEnumerable<StationEntity>> GetStationsAsync(TerritoryEntity territory, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (Stream stream = await _Client.GetStreamAsync($"StaEntVigiCru.jsonld/?CdEntVigiCru={territory.Reference}&TypEntVigiCru={territory.EntityType:d}"))
            {
                StaEntVigiCruList result = await JsonSerializer.DeserializeAsync<StaEntVigiCruList>(stream, _JsonSerializerOptions, cancellationToken) ?? new StaEntVigiCruList();

                return result.Stations;
            }
        }

        /// <summary>Gets detailed information about the specified <paramref name="station" />.</summary>
        /// <param name="station">The territory to get detailed information about.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="station" />.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq2" />
        public async Task<StationEntity> GetStationAsync(StationEntity station, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (Stream stream = await _Client.GetStreamAsync($"StaEntVigiCru.jsonld/?CdEntVigiCru={station.Reference}&TypEntVigiCru={station.EntityType:d}"))
            {
                StaEntVigiCru result = await JsonSerializer.DeserializeAsync<StaEntVigiCru>(stream, _JsonSerializerOptions, cancellationToken) ?? new StaEntVigiCru();

                return result.Station;
            }
        }

        /// <summary>Gets all the territorial entities concerned by the Vigicrues API.</summary>
        /// <remarks>Every entity is returned with minimal information.</remarks>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>All the territorial entities concerned.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq1" />
        public async Task<IEnumerable<TerritoryEntity>> GetTerritoriesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (Stream stream = await _Client.GetStreamAsync("TerEntVigiCru.jsonld/"))
            {
                TerEntVigiCruList result = await JsonSerializer.DeserializeAsync<TerEntVigiCruList>(stream, _JsonSerializerOptions, cancellationToken) ?? new TerEntVigiCruList();
                Debug.Assert(result.TerritoriesCount == result.Territories.Length);

                return result.Territories;
            }
        }

        /// <summary>Gets detailed information about the specified <paramref name="territory" />.</summary>
        /// <param name="territory">The territory to get detailed information about.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="territory" />.</returns>
        /// <seealso href="https://www.vigicrues.gouv.fr/services/1/#faq1" />
        public async Task<TerritoryEntity> GetTerritoryAsync(TerritoryEntity territory, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Wait for it...
            string json = await _Client.GetStringAsync($"TerEntVigiCru.jsonld/?CdEntVigiCru={territory.Reference}&TypEntVigiCru={territory.EntityType:d}");
            // ... and there we go :facepalm:
            json = json.Replace("\"vic.", "\"vic:");
            TerEntVigiCru result = JsonSerializer.Deserialize<TerEntVigiCru>(json, _JsonSerializerOptions) ?? new TerEntVigiCru();

            return result.Territory;
        }

        /// <summary>Gets the flooding information about the specified <paramref name="territory"/>.</summary>
        /// <param name="territory">The territory to get the information about.</param>
        /// <param name="mode">The mode in which to retrieve the information.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="territory"/>.</returns>
        public Task<Information> GetTerritoryInformationAsync(TerritoryEntity territory, InformationMode mode = InformationMode.Normal, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _GetEntityInformationAsync(territory, mode, cancellationToken);
        }

        private void _Dispose(bool disposing)
        {
            if (disposing && _DisposeHttpClient)
                _Client.Dispose();
        }

        /// <summary>Gets the flooding information about the specified <paramref name="entityType" />.</summary>
        /// <param name="entityType">The type of entities to get information about.</param>
        /// <param name="mode">The mode in which to retrieve the information.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="entityType" />.</returns>
        private async Task<Information> _GetEntitiesInformationAsync(EntityType entityType, InformationMode mode = InformationMode.Normal, CancellationToken cancellationToken = default(CancellationToken))
        {
            string modeParameter = (mode == InformationMode.Detailed) ? "&mode=detail" : string.Empty;
            using (Stream stream = await _Client.GetStreamAsync($"InfoVigiCru.jsonld/?TypEntVigiCru={entityType:d}{modeParameter}"))
            {
                InfoVigiCru result = await JsonSerializer.DeserializeAsync<InfoVigiCru>(stream, _JsonSerializerOptions, cancellationToken) ?? new InfoVigiCru();

                return result.Information;
            }
        }

        /// <summary>Gets the flooding information about the specified <paramref name="entity" />.</summary>
        /// <param name="entity">The entity to get information about.</param>
        /// <param name="mode">The mode in which to retrieve the information.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The detailed information about the specified <paramref name="entity" />.</returns>
        private async Task<Information> _GetEntityInformationAsync(Entity entity, InformationMode mode = InformationMode.Normal, CancellationToken cancellationToken = default(CancellationToken))
        {
            string modeParameter = (mode == InformationMode.Detailed) ? "&mode=detail" : string.Empty;
            using (Stream stream = await _Client.GetStreamAsync($"InfoVigiCru.jsonld/?CdEntVigiCru={entity.Reference}&TypEntVigiCru={entity.EntityType:d}{modeParameter}"))
            {
                InfoVigiCru result = await JsonSerializer.DeserializeAsync<InfoVigiCru>(stream, _JsonSerializerOptions, cancellationToken) ?? new InfoVigiCru();

                return result.Information;
            }
        }

        private readonly bool _DisposeHttpClient;
        private readonly HttpClient _Client;
        private readonly JsonSerializerOptions _JsonSerializerOptions;
    }
}
