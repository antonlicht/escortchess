using UnityEngine;

public class Rook : APlayingPiece
{
    public Rook(Player owner) : base(owner) { }
    public override bool IsBlocker { get { return true; } }
    public override Field Field
    {
        get { return _field; }
        set
        {
            if (_field != value)
            {
                var oldField = _field;
                _field = value;

                if (oldField != null)
                {
                    if (oldField.PlayingPiece == this)
                    {
                        oldField.PlayingPiece = null;
                    }
                    foreach (var f in oldField.GetNextRange(GameConstants.FIELDS_BLOCKED_BY_ROOK, null, 1,0))
                    {
                        f.RemoveBlocker(this);
                    }
                }

                if (_field != null)
                {
                    _field.PlayingPiece = this;
                    foreach (var f in _field.GetNextRange(GameConstants.FIELDS_BLOCKED_BY_ROOK, null, 1,0))
                    {
                        f.AddBlocker(this);
                    }
                }
            }
        }
    }
}