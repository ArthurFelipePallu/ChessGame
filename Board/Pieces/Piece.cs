using System.Drawing;
using Chess_Console_Project.Board.Exceptions;
using Chess_Console_Project.Chess.Enums;

namespace Chess_Console_Project.Board.Pieces;

public abstract class Piece
{
    /// <summary>
    /// PRIVATE
    /// </summary>
    protected int _value;
    protected string _name;
    private bool[,] _possibleMoves;
    
    
    
    /// <summary>
    /// PROTECTED
    /// </summary>
    protected char ChessNotation;
    protected PieceType PieceType;
    protected readonly PieceColor PieceColor;
    protected Position PiecePosition;
    protected readonly ChessBoard Board;
    protected int TimesMoved {get; private set;}
    
    /// <summary>
    /// PUBLIC
    /// </summary>
   
    
    
    
    

    /// <summary>
    /// CONSTRUCTOR
    /// </summary>
    protected Piece(ChessBoard board, PieceColor pieceColor)
    {
        PieceColor = pieceColor;
        Board = board;
        TimesMoved = 0;
        _possibleMoves = new bool[Board.MaxChessBoardSize, Board.MaxChessBoardSize];
    }




    /// <summary>
    /// PIECE POSITION METHODS
    /// </summary>
    public bool IsCurrentlyAtCoordinates(int row, int col)
    {
        return PiecePosition.Row == row && PiecePosition.Column == col;
    }
    public void SetPiecePosition(Position position)
    {
        PiecePosition = position;
    }

    
    
    
    
    
    /// <summary>
    /// PIECE MOVEMENTS
    /// </summary>
    public void IncreaseTimesMoved()
    {
        TimesMoved++;
    }
    protected void ClearPossibleMoves()
    {
        for (int i = 0; i < Board.MaxChessBoardSize; i++)
        {
            for (int j = 0; j < Board.MaxChessBoardSize; j++)
            {
                _possibleMoves[i, j] = false;
            }
        }
    } 
    public bool[,] GetAllPossibleMoves()
    {
        return _possibleMoves;
    }
    public bool HasAtLeastOnePossibleMove()
    {
        for (var i = 0; i < _possibleMoves.GetLength(0) - 1; i++)
        {
            for (var j = 0; j < _possibleMoves.GetLength(1) -1 ; j++)
            {
                if (_possibleMoves[i, j])
                    return true;
            }
        }

        return false;
    }
    public bool PositionIsInPossibleMoves(Position pos)
    {
        return CoordinatesIsInPossibleMoves(pos.Row, pos.Column);
    }
    private bool CoordinatesIsInPossibleMoves(int row,int col)
    {
        return _possibleMoves[row, col];
    }
    
    public abstract void AfterMoveVerification();
    public abstract void CalculatePossibleMoves();
    protected void CheckPossibleMovesInDirection(HorizontalDirections hDirection, VerticalDirections vDirection,int maxDistantMovesToCheck = 99 )
    {
        var countHelper = 0;
        var keepGoing = true;

        var possibleMovePosX = 0;
        var possibleMovePosY = 0;

        var possiblePosition = new Position(possibleMovePosX, possibleMovePosY);
        while (keepGoing)
        {
            countHelper++;
            possibleMovePosX = PiecePosition.Row + (countHelper * (int)vDirection);
            possibleMovePosY = PiecePosition.Column + (countHelper * (int)hDirection);
            possiblePosition.SetPosition(possibleMovePosX, possibleMovePosY);

            var move = CheckMovementTypeAt(possiblePosition);
            switch (move)
            {
                case MovementType.Illegal:
                    keepGoing = false;
                    break;
                case MovementType.Take:
                    SetPositionAsPossibleMove(possiblePosition);
                    keepGoing = false;
                    break;
                default:
                    SetPositionAsPossibleMove(possiblePosition);
                    break;
            }
            if(countHelper >= maxDistantMovesToCheck)
                keepGoing = false;
        }
    }
    protected bool PossibleMoveAtPositionIsLegalAndOfAllowedTypes(Position pos,params MovementType[] allowedMovementTypes)
    {
        var move = CheckMovementTypeAt(pos);
        return move != MovementType.Illegal && allowedMovementTypes.Contains(move);
    }
    protected bool PossibleMoveAtPositionIsLegalAndNotOfNotAllowedTypes(Position pos,params MovementType[] notAllowedMovementTypes)
    {
        var move = CheckMovementTypeAt(pos);
        return move != MovementType.Illegal && !notAllowedMovementTypes.Contains(move);
    }
    private MovementType CheckMovementTypeAt(Position position)
    {
        try
        {
            Board.ValidateBoardPosition(position);
        }
        catch (BoardException e)
        {
            return MovementType.Illegal;
        }

        var pieceAtPosition = Board.AccessPieceAtPosition(position);
        
        if (pieceAtPosition == null)
            return MovementType.Move;

        if (pieceAtPosition.GetPieceColor() == PieceColor)
        {
            if(pieceAtPosition.PieceType == PieceType)
                return MovementType.SamePiece;
            return MovementType.AllyPiece;
        }
        
        return pieceAtPosition.GetPieceType() == PieceType.King ? MovementType.Check : MovementType.Take;
    }
    protected void SetPositionAsPossibleMove(Position pos)
    {
        _possibleMoves[pos.Row,pos.Column] = true;
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
        return PieceColor;
    }

    /// <summary>
    /// PIECE NOTATION
    /// </summary>
    public string GetPieceNotation()
    {
        return $"{ChessNotation.ToString()}";
    }
    
    
    
    public override string ToString()
    {
        return $" {PieceColor.ToString()} {_name} ";
    }
}