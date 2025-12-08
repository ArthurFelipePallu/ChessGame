using System.Drawing;
using Chess_Console_Project.Board.Exceptions;

namespace Chess_Console_Project.Board.Pieces;

public abstract class Piece
{
    /// <summary>
    /// PRIVATE
    /// </summary>
    
    public int Value;
    public string Name;
    protected char ChessNotation;
    public Position Position;
    public ChessBoard Board;
    private PieceColor _pieceColor;
    protected PieceType PieceType;
    public int TimesMoved {get; protected set;}
    protected bool[,] PossibleMoves;


    protected Piece(ChessBoard board, PieceColor pieceColor)
    {
        _pieceColor = pieceColor;
        Board = board;
        TimesMoved = 0;
        PossibleMoves = new bool[Board.MaxChessBoardSize, Board.MaxChessBoardSize];
    }

    public void SetPiecePosition(Position position)
    {
        Position = position;
    }
    
    /// <summary>
    /// PIECE MOVEMENTS
    /// </summary>
    public void IncreaseTimesMoved()
    {
        TimesMoved++;
    }

    protected abstract void CalculatePossibleMoves();

    protected void PrintPossibleMoves()
    {
        for (int i = 0; i < Board.MaxChessBoardSize; i++)
        {
            for (int j = 0; j < Board.MaxChessBoardSize; j++)
            {
                Console.Write(PossibleMoves[i, j]);
            }
            Console.WriteLine();
        }
    }
    protected void ClearPossibleMoves()
    {
        for (int i = 0; i < Board.MaxChessBoardSize; i++)
        {
            for (int j = 0; j < Board.MaxChessBoardSize; j++)
            {
                PossibleMoves[i, j] = false;
            }
        }
    }

    protected void CheckIfCanMoveToPosition(Position pos)
    {
        PossibleMoves[pos.Row,pos.Column] = CanMoveTo(pos);
    }
    private bool CanMoveTo(Position position)
    {
        try
        {
            Board.ValidateBoardPosition(position);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e);
            return false;
        }
        
        if (!Board.HasPieceAtPosition(position)) return true;
        
       return  Board.BoardPositionHasPieceOfColor(position) != _pieceColor;
    }
    
    /// <summary>
    /// PIECE TYPE
    /// </summary>
    /// <returns></returns>
    public PieceType GetPieceType()
    {
        return PieceType;
    }
    public string GetPieceTypeAsString()
    {
        return PieceType.ToString();
    }
    
    /// <summary>
    /// PIECE COLOR
    /// </summary>
    /// <returns></returns>
    public PieceColor GetPieceColor()
    {
        return _pieceColor;
    }
    public string GetPieceColroAsString()
    {
        return _pieceColor.ToString();
    }

    /// <summary>
    /// PIECE NOTATION
    /// </summary>
    public string GetPieceNotation()
    {
        return $" {ChessNotation.ToString()} ";
    }
    
    
    
    public override string ToString()
    {
        return $" {_pieceColor.ToString()} {Name} ";
    }
}