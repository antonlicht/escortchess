using System.Collections.Generic;

public class Queen : APlayingPiece
{
    public Queen(Player owner) : base(owner) { }

    public override List<Field> GetSpawnFieldsForOtherPieces(APlayingPiece piece = null)
    {
        var list = new List<Field>();
        if(Field != null)
        {
            var spawn1 = Field.GetNext(GameConstants.QUEEN_SPAWN_OFFSET, f => !f.PlayingPiece.IsEnemyOfType<Rook>(Owner));
            var spawn2 = Field.GetNext(-GameConstants.QUEEN_SPAWN_OFFSET, f => !f.PlayingPiece.IsEnemyOfType<Rook>(Owner) && !(f.IsSpawnField(Owner)&& piece is King),0,1);

            if (spawn1 != null && spawn1.PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner))
            {
                list.Add(spawn1);
            }
            if (spawn2 != null && spawn2.PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner))
            {
                list.Add(spawn2);
            }
        }
        return list;
    }
}