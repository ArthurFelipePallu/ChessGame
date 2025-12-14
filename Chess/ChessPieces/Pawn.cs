using Chess_Console_Project.Board;
using Chess_Console_Project.Board.Pieces;
using Chess_Console_Project.Chess.Enums;

namespace Chess_Console_Project.Chess.ChessPieces;

public class Pawn : Piece
{
    public Pawn(ChessBoard board, PieceColor pieceColor) : base(board, pieceColor)
    {
        _value = 1;
        _name  = "Pawn";
        ChessNotation = 'P';
        PieceType = PieceType.Pawn;
    }
    
    public override void CalculatePossibleMoves()
    {
        ClearPossibleMoves();

        var firstPawnMove = TimesMoved == 0 ? 2 : 1;

        var vDir = GetPieceColor() == PieceColor.White ? VerticalDirections.Up : VerticalDirections.Down;
        
        for (var i = 1; i <= firstPawnMove; i++)
        {
            // var hasPieceAhead = Board.HasPieceAtCoordinate(PiecePosition.Row + ((int)vDir * i), PiecePosition.Column);
            // if (!hasPieceAhead)
            // {
            var pos = new Position(PiecePosition.Row + ((int)vDir * i), PiecePosition.Column);
            if (PossibleMoveAtPositionIsOfAllowedTypes(pos,MovementType.Move))
                SetPositionAsPossibleMove(pos);
            // }
            // else break;
        }
        //Posição de Cima e Esquerda e precisa ser Movimento de Captura
        PossibleMovementAtPositionWithModifiersIsOfMoveType((int)vDir, (int)HorizontalDirections.Left,MovementType.TakeEnemyPiece);
        
        //Posição de Cima e Direita e precisa ser Movimento de Captura
        PossibleMovementAtPositionWithModifiersIsOfMoveType((int)vDir, (int)HorizontalDirections.Right,MovementType.TakeEnemyPiece);
    }

    public override void AfterMoveVerification()
    {
        throw new NotImplementedException();
    }
    
    private void PossibleMovementAtPositionWithModifiersIsOfMoveType(int rowModifier, int columnModifier,MovementType movementType)
    {
        var pos = new Position(PiecePosition.Row + rowModifier, PiecePosition.Column + columnModifier);
        if (PossibleMoveAtPositionIsOfAllowedTypes(pos, movementType))
        {
            SetPositionAsPossibleMove(pos);
        }
    }
}