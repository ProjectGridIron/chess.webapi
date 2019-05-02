﻿using System.Collections.Generic;
using chess.engine.Chess;
using chess.engine.Game;
using chess.engine.Movement;
using chess.engine.Movement.Queen;

namespace chess.engine.Entities
{
    public class QueenEntity : ChessPieceEntity
    {
        public QueenEntity(Colours owner) : base(ChessPieceName.Queen, owner)
        {
        }
        public override IEnumerable<IPathGenerator> PathGenerators =>
            new List<IPathGenerator>
            {
                new QueenPathGenerator()
            };

    }
}