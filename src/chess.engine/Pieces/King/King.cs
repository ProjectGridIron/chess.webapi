﻿using chess.engine.Game;

namespace chess.engine.Pieces.King
{
    public class King
    {
        public static BoardLocation StartPositionFor(Colours player)
            => player == Colours.White ? BoardLocation.At("E1") : BoardLocation.At("E8");


    }
}