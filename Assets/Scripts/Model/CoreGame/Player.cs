using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    public readonly int PlayerOrder;
    public readonly Field SpawnField;
    public readonly Dictionary<Type, APlayingPiece> PlayingPieces;

    public Player(int playerOrder, Field spawnField)
    {
        PlayerOrder = playerOrder;
        SpawnField = spawnField;
        PlayingPieces = new Dictionary<Type, APlayingPiece>
        {
            {typeof(King), new King(this) },
            {typeof(Queen), new Queen(this) },
            {typeof(Rook), new Rook(this) },
            {typeof(Knight), new Knight(this) },
            {typeof(Pawn), new Pawn(this) }
        };
    }

    public T GetPlayingPiece<T>() where T : APlayingPiece
    {
        return (T)PlayingPieces[typeof(T)];
    }

    public bool HasPlayingPiecesOnBoard()
    {
        return PlayingPieces.Where(kv => kv.Key != typeof(King)).Any(kv => kv.Value.Field != null);
    }

    public bool HasPlayingPieceOnSpawnField(Board board)
    {
        return !board.GetSpawnField(PlayerOrder).PlayingPiece.IsNullOrDoesntBelongToPlayer(this);
    }

    public override string ToString()
    {
        return string.Format("Player{0}", PlayerOrder+1);
    }
}