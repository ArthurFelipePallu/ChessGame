using Chess_Console_Project.Board;
using Chess_Console_Project.Board.Pieces;
using Chess_Console_Project.Chess.Enums;

namespace Chess_Console_Project.Chess.ChessPieces;

public class Knight : Piece
{
    
    public Knight(ChessBoard board, PieceColor pieceColor) : base(board, pieceColor)
    {
        _value = 3;
        _name = "Knight";
        ChessNotation = 'N';
        PieceType = PieceType.Knight;
    }

    public override void AfterMoveVerification()
    {
        throw new NotImplementedException();
    }

    public override void CalculatePossibleMoves()
    {
        ClearPossibleMoves();

        HorizontalLMovements();
        VerticalLMovements();
    }
    
    
    private void HorizontalLMovements()
    {
        // L para Esquerda e para cima
        PossibleMovementAtPositionIsMoveOrTake(-1 , -2);
        
        // L para Esquerda e para baixo
        PossibleMovementAtPositionIsMoveOrTake(1, -2);
        
        // L para Direita e para cima
        PossibleMovementAtPositionIsMoveOrTake(-1 , 2);
        
        // L para Direita e para baixo
        PossibleMovementAtPositionIsMoveOrTake(1 , 2);
    }
    private void VerticalLMovements()
    {
        // L para Cima e para Esquerda
        PossibleMovementAtPositionIsMoveOrTake(-2 , -1);
        
        // L para Baixo e para Esquerda
        PossibleMovementAtPositionIsMoveOrTake(2 , -1);
        
        // L para Cima e para Direita
        PossibleMovementAtPositionIsMoveOrTake(-2 , 1);
        
        // L para Baixo e para Direita
        PossibleMovementAtPositionIsMoveOrTake(2 , 1);
    }

    private void PossibleMovementAtPositionIsMoveOrTake(int rowModifier, int columnModifier)
    {
        
        var pos = new Position(PiecePosition.Row + rowModifier, PiecePosition.Column + columnModifier);
        if (PossibleMoveAtPositionIsLegalAndOfAllowedTypes(pos, MovementType.Move, MovementType.Take))
        {
            SetPositionAsPossibleMove(pos);
        }
    }
}