using System.Collections.Generic;
using System.Linq;

public abstract class APlayingPiece
{
    internal Field _field;
    public readonly Player Owner;
    public virtual bool IsBlocker { get { return false; } }
    public virtual Field Field
    {
        get { return _field; }
        set
        {
            if (_field != value)
            {
                var oldField = _field;
                _field = value;

                if (oldField != null && oldField.PlayingPiece == this)
                {
                    oldField.PlayingPiece = null;
                }

                if (_field != null)
                {
                    _field.PlayingPiece = this;
                }
            }
        }
    }

    public APlayingPiece(Player owner)
    {
        Owner = owner;
    }

    public virtual List<Field> GetSpawnFieldsForOtherPieces(APlayingPiece piece = null){ return new List<Field>(); }
    public virtual List<List<Field>> GetMoves(int steps)
    {
        var list = new List<List<Field>>();
        if (Field != null)
        {
            var moves = Field.GetNextRange(steps, f => !f.PlayingPiece.IsEnemyOfType<Rook>(Owner), 1, 1);
            if (!moves.IsNullOrEmpty() && moves.Last().PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner))
            {
                list.Add(moves);
            }
        }
        return list;
    }

    public virtual List<Field> GetSpawnFields()
    {
        var list = new List<Field>();

        foreach(var piece in Owner.PlayingPieces.Values)
        {
            list.AddRange(piece.GetSpawnFieldsForOtherPieces(this));
        }

        if(Owner.SpawnField.PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner))
        {
            list.Add(Owner.SpawnField);
        }

        return list;
    }

    public override string ToString()
    {
        return string.Format("{0}-{1}", GetType(), Owner);
    }
}