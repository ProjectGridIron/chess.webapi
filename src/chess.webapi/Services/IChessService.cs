﻿using System.Linq;
using System.Text;
using chess.engine.Board;
using chess.engine.Chess;
using chess.engine.Entities;
using chess.engine.Extensions;
using chess.engine.Game;
using Microsoft.Extensions.Logging;

namespace chess.webapi.Services
{
    public class ChessService : IChessService
    {
        private ILogger<ChessService> _logger;
        private readonly ILogger<ChessGame> _chessGameLogger;
        private readonly IBoardEngineProvider<ChessPieceEntity> _boardEngineProvider;

        public ChessService(
            ILogger<ChessService> logger,
            ILogger<ChessGame> chessGameLogger, 
            IBoardEngineProvider<ChessPieceEntity> boardEngineProvider)
        {
            _chessGameLogger = chessGameLogger;
            _boardEngineProvider = boardEngineProvider;
            _logger = logger;
        }
        public string GetNewBoard()
        {
            var game = new ChessGame(
                _chessGameLogger,
                _boardEngineProvider
            );

            return ChessGameConvert.Serialise(game);

        }

        public string PlayMove(string board, string move)
        {
            var chessGame = ChessGameConvert.Deserialise(board);

            var msg = chessGame.Move(move);

            return ChessGameConvert.Serialise(chessGame) + $"\n{msg}";
        }

        public string GetMoves(string board)
        {
            var chessGame = ChessGameConvert.Deserialise(board);
            var locatedItems = chessGame.BoardState.GetAllItems();
            return ToMoveList(locatedItems.ToArray());
        }
        public string GetMovesForPlayer(string board, Colours forPlayer)
        {
            var chessGame = ChessGameConvert.Deserialise(board);
            var locatedItems = chessGame.BoardState.GetItems((int) forPlayer);
            return ToMoveList(locatedItems.ToArray());
        }
        public string GetMovesForLocation(string board, string location)
        {
            var chessGame = ChessGameConvert.Deserialise(board);
            var loc = BoardLocation.At(location);
            var locatedItems = chessGame.BoardState.GetItem(loc);
            return ToMoveList(locatedItems);
        }

        private static string ToMoveList(params LocatedItem<ChessPieceEntity>[] locatedItems)
        {
            var sb = new StringBuilder();

            foreach (var locatedItem in locatedItems)
            {
                var boardLocations = locatedItem.Paths.FlattenMoves()
                    .Select(m => $"{m.From.ToChessCoord()}{m.To.ToChessCoord()}");
                var value = $"{locatedItem.Item} - " + string.Join(", ", boardLocations);
                if (!string.IsNullOrEmpty(value))
                {
                    sb.AppendLine(value);
                }
            }

            return sb.ToString();
        }

    }

    public interface IChessService
    {
        string GetNewBoard();
        string PlayMove(string board, string move);
        string GetMoves(string board);
        string GetMovesForPlayer(string board, Colours forPlayer);
        string GetMovesForLocation(string board, string location);
    }
}