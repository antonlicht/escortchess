using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knight : APlayingPiece
{
    public Knight(Player owner) : base(owner) { }
    public override List<List<Field>> GetMoves(int steps)
    {
        var list = GetMoves(Field.Next, new List<Field>(), steps - 1);
        list.AddRange(GetMoves(Field.Shortcut, new List<Field>(), steps-1));
        return list;
    }

    private List<List<Field>> GetMoves(Field nextField, List<Field> history, int stepsLeft)
    {
        var field = nextField;
        var output = new List<List<Field>>();
        for (int i = 0; i < stepsLeft; i++)
        { 
            if(field == null|| field.IsBlocked(Owner)|| field.PlayingPiece.IsEnemyOfType<Rook>(Owner))
            {
                return output;
            }
            history.Add(field);
            if (field.Shortcut != null)
            {
                var newHistory = history.ToList();
                var newOutput = GetMoves(field.Shortcut, newHistory, stepsLeft - 1-i);
                output.AddRange(newOutput);
            }       
            
            field = field.Next;
        }
        if (field != null && !field.IsBlocked(Owner) && field.PlayingPiece.IsNullOrDoesntBelongToPlayer(Owner))
        {
            history.Add(field);
            output.Add(history);
        }      
        return output;
    }
}