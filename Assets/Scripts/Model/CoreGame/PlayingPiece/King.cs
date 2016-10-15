using System.Collections.Generic;
using System.Linq;

public class King : APlayingPiece
{
    public King(Player owner) : base(owner) { }

    public override List<List<Field>> GetMoves(int steps)
    {
        var list = new List<List<Field>>();
        
        if (Field != null)
        {
            var moves = new List<Field>();
            var field = Field.Next;
            while(field.PlayingPiece.IsOwnOfType<Rook>(Owner) || field.PlayingPiece.IsOwnOfType<Pawn>(Owner))
            {
                moves.Add(field);
                field = field.Next;
            }
            if(field.PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner))
            {
                moves.Add(field);
                if (moves.Count <= 1 || moves.All(f => !f.IsSpawnField(Owner)))
                {
                    list.Add(moves);
                }
            }
        }
        return list;
    }

    public override List<Field> GetSpawnFields()
    {
        var list = new List<Field>();

        foreach (var piece in Owner.PlayingPieces.Values)
        {
            list.AddRange(piece.GetSpawnFieldsForOtherPieces(this));
        }

        if (Owner.SpawnField.PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner)
            && !Owner.SpawnField.PlayingPiece.IsEnemyOfType<King>(Owner)
            && !Owner.SpawnField.Next.PlayingPiece.IsEnemyOfType<King>(Owner)
            && !Owner.SpawnField.Last.PlayingPiece.IsEnemyOfType<King>(Owner))
        {
            list.Add(Owner.SpawnField);
        }

        return list;
    }
}