using System.Linq;
using UnityEngine;

public class CoreGameController : MonoBehaviour
{
    public Board Board;
    public Player[] Players;
    public Dice Dice;
    public int CurrentPlayer { get; private set; }

    void Awake()
    {
        Init();
        //KnightTest();
        //QueenTest();
    }

    private void Init()
    {
        Board = new Board(GameConstants.FIELD_AMOUNT_PER_PLAYER, GameConstants.PLAYER_AMOUNT);
        InitPlayers(Board);
        Dice = new Dice(GameConstants.DICE_SIDES);
    }
 
    private void InitPlayers(Board board)
    {
        Players = new Player[GameConstants.PLAYER_AMOUNT];
        for (int i = 0; i < Players.Length; i++)
        {
            var spawnField = Board.GetSpawnField(i);
            var player = new Player(i, spawnField);
            Players[i] = player;
                  
            spawnField.SpawnFieldOwner = player;
            spawnField.PlayingPiece = player.GetPlayingPiece<King>();
        }
        CurrentPlayer = 0;
    }

    public void SwitchToNextPlayer()
    {
        CurrentPlayer = (CurrentPlayer + 1) % Players.Length;
    }

    void Update()
    {
        //Debug.Log(string.Format("Start turn player {0}.", CurrentPlayer));
        //var currentPlayer = Players[CurrentPlayer];

        //Dice.Reset();
        //do
        //{
        //    Dice.Roll();
        //    Debug.Log(string.Format("Throw {0}: {1}", Dice.Throws, Dice.Result));
        //}
        //while (!currentPlayer.HasPlayingPiecesOnBoard() && Dice.Throws < GameConstants.TRIES_WITHOUT_PLAYING_PIECE && !Dice.CanSpawn);

        //Debug.Log(string.Format("Has piece on board: {0}, threw {1}, can spawn: {2}", currentPlayer.HasPlayingPiecesOnBoard(),  Dice.Result, Dice.CanSpawn));

        //SwitchToNextPlayer();
    }

    public void KnightTest()
    {
        Debug.Log("Start Knight Test");
        var player1 = Players[0];
        var player2 = Players[1];
        var knight1 = player1.GetPlayingPiece<Knight>();
        var pawn1 = player1.GetPlayingPiece<Pawn>();
        var rook2 = player2.GetPlayingPiece<Rook>();

        Board.Fields[1].PlayingPiece = knight1;
        Board.Fields[23].PlayingPiece = pawn1;
        
        var moves = pawn1.GetMoves(5);
        foreach (var moveList in moves)
        {
            Debug.Log(string.Join(">", moveList.Select(f => f.Id.ToString()).ToArray()));
        }
        //knight1.Field = moves.First().Last();
        //Debug.Log(knight1 + " " + knight1.Field);
        //Debug.Log(rook2 + " " + rook2.Field);
    }

    public void QueenTest()
    {
        Debug.Log("Start Queen Test");
        var player1 = Players[0];
        var player2 = Players[1];
        var queen1 = player1.GetPlayingPiece<Queen>();
        var pawn1 = player1.GetPlayingPiece<Pawn>();
        var king1 = player1.GetPlayingPiece<King>();
        var rook2 = player2.GetPlayingPiece<Rook>();

        Board.Fields[0].PlayingPiece = queen1;
        Board.Fields[39].PlayingPiece = rook2;

        foreach(var field in queen1.GetSpawnFieldsForOtherPieces())
        {
            Debug.Log(field);
        }
    }

}
