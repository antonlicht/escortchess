public class Board
{
    public Field[] Fields;
    private int _fieldAmountPerPlayer;
    private int _playerAmount;

    public Board(int fieldAmountPerPlayer, int playerAmount)
    {
        Fields = new Field[fieldAmountPerPlayer * playerAmount];
        _fieldAmountPerPlayer = fieldAmountPerPlayer;
        _playerAmount = playerAmount;

        Field lastField = null;
        for (int i = 0; i < Fields.Length; i++)
        {
            var field = new Field(i);
            Fields[i] = field;    
            if(lastField != null)
            {
                lastField.Next = field;
                field.Last = lastField;
            }
            lastField = field;          
        }
        Fields[0].Last = Fields[Fields.Length - 1];
        Fields[Fields.Length - 1].Next = Fields[0];

        //For further initialization all fields need to be connected.
        for (int i = 0; i < Fields.Length; i++)
        {
            var field = Fields[i];
            field.Shortcut = ((i - GameConstants.SHORTCUT_OFFSET) % GameConstants.SHORTCUT_INTERVAL == 0) ? field.GetNext(GameConstants.SHORTCUT_LENGTH) : null;
        }
    }

    public Field GetSpawnField(int playerOrder)
    {
        return Fields[GetSpawnFieldId(playerOrder)];
    }

    public int GetSpawnFieldId(int playerOrder)
    {
        return (playerOrder % _playerAmount) * _fieldAmountPerPlayer;
    }

    public int GetHouseEntryId(int playerOrder)
    {
        return (GetSpawnFieldId(playerOrder)+GameConstants.HOUSE_ENTRY+Fields.Length)%Fields.Length;
    }
}
