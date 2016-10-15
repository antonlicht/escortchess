using System.Collections.Generic;
using System.Linq;

public class Field
{
    public readonly int Id;
    public Field Next;
    public Field Last;
    public Player SpawnFieldOwner;
    public Field Shortcut;
    private List<APlayingPiece> _blockers = new List<APlayingPiece>();
    private APlayingPiece _playingPiece;

    public APlayingPiece PlayingPiece
    {
        get { return _playingPiece; }
        set
        {
            if (_playingPiece != value)
            {
                var oldPiece = _playingPiece;
                _playingPiece = value;

                if (oldPiece != null && oldPiece.Field == this)
                {
                    oldPiece.Field = null;
                }

                if (_playingPiece != null)
                {
                    _playingPiece.Field = this;
                }
            }
        }
    }

    public bool IsEmpty { get { return PlayingPiece == null; } }

    public Field(int id)
    {
        Id = id;
    }

    public void AddBlocker(APlayingPiece piece)
    {
        if(!_blockers.Contains(piece))
        {
            _blockers.Add(piece);
        }
    }

    public void RemoveBlocker(APlayingPiece piece)
    {
        if (_blockers.Contains(piece))
        {
            _blockers.Remove(piece);
        }
    }

    public bool IsBlocked(Player owner)
    {
        return _blockers.Count(p => p.Owner != owner) > 0;
    }

    public bool IsSpawnField(Player owner)
    {
        return SpawnFieldOwner != null && SpawnFieldOwner == owner;
    }

    public override string ToString()
    {
        return string.Format("Field {0}", Id);
    }
}