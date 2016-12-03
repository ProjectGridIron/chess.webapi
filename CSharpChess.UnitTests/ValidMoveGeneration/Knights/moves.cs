﻿using System.Linq;
using CSharpChess.TheBoard;
using CSharpChess.UnitTests.Helpers;
using CSharpChess.UnitTests.TheBoard;
using CSharpChess.ValidMoves;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace CSharpChess.UnitTests.ValidMoveGeneration.Knights
{
    [TestFixture]
    public class moves : BoardAssertions
    {
        private KnightValidMoveGenerator _knightValidMoveGenerator;

        [SetUp]
        public void SetUp()
        {
            _knightValidMoveGenerator = new KnightValidMoveGenerator();
        }
        [Test]
        public void all_moves_generated()
        {
            var asOneChar =
                    "........" +
                    "........" +
                    "........" +
                    "........" +
                    "...N...." +
                    "........" +
                    "........" +
                    "........";
            var expected = BoardLocation.List("E6", "F5", "F3", "E2", "C2", "B3", "B5", "C6");
            var board = BoardBuilder.CustomBoard(asOneChar, Chess.Colours.White);

            var moves = _knightValidMoveGenerator.For(board, "D4").ToList();

            AssertMovesContainsExpectedWithType(moves, expected, MoveType.Move);
            AssertAllMovesAreOfType(moves, MoveType.Move);
        }
    }
}