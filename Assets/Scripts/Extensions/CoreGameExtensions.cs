using System;
using System.Collections.Generic;

public static class CoreGameExtensions
{
    public static bool IsNullOrDoesntBelongToPlayer(this APlayingPiece piece, Player owner)
    {
        return piece == null || piece.Owner != owner;
    }

    public static bool IsEnemyOfType<T>(this APlayingPiece piece, Player owner) where T : APlayingPiece
    {
        return piece != null && piece is T && piece.Owner != owner;
    }

    public static bool IsOwnOfType<T>(this APlayingPiece piece, Player owner) where T : APlayingPiece
    {
        return piece != null && piece is T && piece.Owner == owner;
    }

    public static List<Field> GetNextRange(this Field field, int steps, Func<Field, bool> filter = null, int excludeFilterAtStart = 1, int excludefilterAtEnd = 1, int start = 1, int end = 0)
    {
        var list = new List<Field>();
        var f = field;
        if (steps >= 0)
        {
            for (int i = 0; i <= steps; i++)
            {
                if (i >=excludeFilterAtStart && i<=steps-excludefilterAtEnd && filter != null && !filter(f))
                {
                    list.Clear();
                    break;
                }
                if (i >= start && i <=steps-end)
                {
                    list.Add(f);
                }
                f = f.Next;
            }
        }
        else
        {
            for (int i = 0; i >= steps; i--)
            {
                if (i <= -excludeFilterAtStart && i >= steps + excludefilterAtEnd && filter != null && !filter(f))
                {
                    list.Clear();
                    break;
                }
                if (i <= -start && i >= steps + end)
                {
                    list.Add(f);
                }   
                f = f.Last;
            }
        }
        return list;
    }

    public static Field GetNext(this Field field, int steps, Func<Field, bool> filter = null, int excludeAtStart = 1, int excludeAtEnd = 1)
    {
        var f = field;
        if (steps >= 0)
        {
            for (int i = 0; i <= steps; i++)
            {
                if (i >= excludeAtStart && i <= steps - excludeAtEnd && filter != null && !filter(f))
                {
                    return null;
                }
                f = f.Next;
            }
            f = f.Last;
        }
        else
        {
            for (int i = 0; i >= steps; i--)
            {
                if (i <= -excludeAtStart && i >= steps + excludeAtEnd && filter != null && !filter(f))
                {
                    return null;
                }
                f = f.Last;
            }
            f = f.Next;
        }
        return f;
    }
}