﻿using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace chess.webapi.client.csharp
{
    public class ChessGameApiClient : ApiClientBase, IChessGameApiClient
    {
        public ChessGameApiClient(HttpClient httpClient, string baseUrl) : base(httpClient, baseUrl)
        { }

        public async Task<ChessWebApiResult> ChessGameAsync() 
        {
            // TODO: Find out why availableMoves is not deserialised when using GetJsonAsync method from the new .NET Json libraries
            return JsonConvert.DeserializeObject<ChessWebApiResult>(await GetStringAsync("chessgame"));
        }

        public async Task<ChessWebApiResult> ChessGameAsync(string customSerialisedBoard)
        {
            // TODO: Find out why availableMoves is not deserialised when using GetJsonAsync method from the new .NET Json libraries
            return JsonConvert.DeserializeObject<ChessWebApiResult>(await GetStringAsync($"chessgame/{customSerialisedBoard}"));
        }
        public async Task<ChessWebApiResult> PlayMoveAsync(string board, string move) 
        {
            // TODO: Find out why availableMoves is not deserialised when using GetJsonAsync method from the new .NET Json libraries
            return JsonConvert.DeserializeObject<ChessWebApiResult>(await GetStringAsync($"chessgame/{board}/{move}"));
        }

    }

    public class ChessWebApiResult
    {
        public string Message { get; set; }
        public string Board { get; set; }
        public string BoardText { get; set; }
        public Move[] AvailableMoves { get; set; }
        public string WhoseTurn { get; set; }
        
    }

    public class Move
    {
        public string SAN { get; set; }
        public string Coord { get; set; }
    }
}