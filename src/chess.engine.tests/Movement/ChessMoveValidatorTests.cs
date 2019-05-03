﻿using System.Collections.Generic;
using System.Linq;
using chess.engine.Actions;
using chess.engine.Board;
using chess.engine.Game;
using chess.engine.Movement;
using chess.engine.tests.Builders;
using Moq;
using NUnit.Framework;

namespace chess.engine.tests.Movement
{
    [TestFixture]
    public class ChessMoveValidatorTests : PathGeneratorTestsBase
    {
        private Mock<IReadOnlyDictionary<ChessMoveType, IEnumerable<ChessBoardMovePredicate>>> _factoryMock;
        private IEnumerable<ChessBoardMovePredicate> _moveTests;
        private readonly IBoardActionFactory _boardActionFactory = new BoardActionFactory();


        [SetUp]
        public new void SetUp()
        {
            base.SetUp();
            _factoryMock = new Mock<IReadOnlyDictionary<ChessMoveType, IEnumerable<ChessBoardMovePredicate>>>();

            _moveTests = new List<ChessBoardMovePredicate>
            {
                (move, state) => true
            };
            _factoryMock.Setup(f => f.TryGetValue(It.IsAny<ChessMoveType>(), out _moveTests))
                .Returns(true);

        }

        [Test]
        public void ValidPath_should_return_validPath()
        {
            var validator = new ChessPathValidator(_factoryMock.Object);
            var path = new PathBuilder().Build();

            validator.ValidatePath(path, BoardStateMock.Object);

            Assert.That(path.Any());
        }

        [Test]
        public void ValidPath_should_return_truncated_path_when_move_test_fails()
        {
            var validator = new ChessPathValidator(_factoryMock.Object);
            var path = new PathBuilder().From("D2").To("D3").To("D4").To("D5", ChessMoveType.TakeOnly).Build();

            IEnumerable<ChessBoardMovePredicate> failOnD5 = new List<ChessBoardMovePredicate>
            {
                (move, state) => !move.To.Equals(BoardLocation.At("D5"))
            };

            _factoryMock.Setup(f => f.TryGetValue(
                    It.IsAny<ChessMoveType>(), 
                    out failOnD5))
                .Returns(true);

            var validPath = validator.ValidatePath(path, BoardStateMock.Object);
            
            AssertPathContains(new List<Path>{validPath}, 
                new PathBuilder().From("D2")
                    .To("D3")
                    .To("D4")
                    .Build(), Colours.White);
        }

        [Test]
        public void ValidPath_should_throw_for_unsupported_MoveType()
        {
            _factoryMock.Setup(f => f.TryGetValue(It.IsAny<ChessMoveType>(), out _moveTests))
                .Returns(false);

            var validator = new ChessPathValidator(_factoryMock.Object);
            var path = new PathBuilder().Build();
            Assert.That(() => validator.ValidatePath(path, BoardStateMock.Object), 
                Throws.Exception);
        }
    }
}