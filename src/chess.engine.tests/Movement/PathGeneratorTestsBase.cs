using System.Collections.Generic;
using System.Linq;
using chess.engine.Board;
using chess.engine.Entities;
using chess.engine.Game;
using chess.engine.Movement;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace chess.engine.tests.Movement
{
    public abstract class PathGeneratorTestsBase
    {
        protected Mock<IBoardState<ChessPieceEntity>> BoardStateMock;
        protected Mock<ILogger> LoggerMock;

        protected void AssertPathContains(IEnumerable<Path> paths, Path path, Colours colour)
            => Assert.That(paths.Contains(path), $"{path} not found for {colour}, check MoveType!");

        protected void SetUp()
        {
            BoardStateMock = new Mock<IBoardState<ChessPieceEntity>>();
        }
    }
}