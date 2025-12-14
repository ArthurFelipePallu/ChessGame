using Chess_Console_Project.Board;
using Chess_Console_Project.Board.Pieces;
using Chess_Console_Project.Chess.Enums;
using Chess_Console_Project.Chess.Exceptions;

namespace Chess_Console_Project.Chess.ChessPieces;

public class King : Piece
{
    
    public King(ChessBoard board, PieceColor pieceColor) : base(board, pieceColor)
    {
        _value = 999;
        _name = "King";
        ChessNotation = 'K';
        PieceType = PieceType.King;
    }
    public override void CalculatePossibleMoves()
    {
        ClearPossibleMoves();

        //Posição de Cima
        CheckPossibleMoveIsNotCheck(VerticalDirections.Up,HorizontalDirections.None);
        
        //Posição de Cima e Esquerda
        CheckPossibleMoveIsNotCheck(VerticalDirections.Up,HorizontalDirections.Left);
        
        //Posição de Cima e Direita
        CheckPossibleMoveIsNotCheck(VerticalDirections.Up,HorizontalDirections.Right);
        
        // //Posição Esquerda
        // CheckPossibleMoveIsNotCheck(HorizontalDirections.Left,VerticalDirections.None);
        //
        // //Posição Direita
        // CheckPossibleMoveIsNotCheck(HorizontalDirections.Right,VerticalDirections.None);
        //
        // //Posição de Baixo
        // CheckPossibleMoveIsNotCheck(HorizontalDirections.None,VerticalDirections.Down);
        //
        // //Posição de Baixo e Esquerda
        // CheckPossibleMoveIsNotCheck(HorizontalDirections.Left,VerticalDirections.Down);
        //
        // //Posição de Baixo e Direita
        // CheckPossibleMoveIsNotCheck(HorizontalDirections.Right,VerticalDirections.Down);
    }

    private void CheckPossibleMoveIsNotCheck(VerticalDirections vDir, HorizontalDirections hDir)
    {
        if (!PossibleMovementAtPositionIsMoveOrTake((int)vDir, (int)hDir)) return;
        var kingOriginalPos = PiecePosition;
            
        try
        { 
            var verifyingPosition = new Position(PiecePosition.Row + (int)vDir, PiecePosition.Column + (int)hDir);
            SetPiecePosition(verifyingPosition);
            if(IsInCheck())
                SetPositionAsNotPossibleMove(verifyingPosition);
        }
        catch (MovementException e)
        {
            Console.WriteLine( "[KING IS IN CHECK VERIFICATION] : " + e.Message);
        }
        SetPiecePosition(kingOriginalPos);
    }

    private bool IsInCheck()
    {
        return IsInCheckFromPawn() ||
               IsInCheckFromKnight() ||
               IsInCheckFromDiagonalsDirections() ||
               IsInCheckFromHorizontalOrVerticalDirection();
    }

    private bool IsInCheckFromHorizontalOrVerticalDirection()
    {
               //Direção para Cima
        return DirectionHasEnemyPieceOfType(HorizontalDirections.None, VerticalDirections.Up, 8, PieceType.Queen, PieceType.Rook) ||
               //Direção para Direita 
               DirectionHasEnemyPieceOfType(HorizontalDirections.Right, VerticalDirections.None, 8, PieceType.Queen, PieceType.Rook) ||
               //Direção para Baixo
               DirectionHasEnemyPieceOfType(HorizontalDirections.None, VerticalDirections.Down, 8, PieceType.Queen, PieceType.Rook) ||
               //Direção para Esquerda
               DirectionHasEnemyPieceOfType(HorizontalDirections.Left, VerticalDirections.None, 8, PieceType.Queen,PieceType.Rook);
    }

    private bool IsInCheckFromDiagonalsDirections()
    {
        
                //Direção Diagonal Esquerda para Cima
        return DirectionHasEnemyPieceOfType(HorizontalDirections.Left,VerticalDirections.Up,8,PieceType.Queen,PieceType.Bishop) ||
                //Direção Diagonal Direita para Cima
                DirectionHasEnemyPieceOfType(HorizontalDirections.Right,VerticalDirections.Up,8,PieceType.Queen,PieceType.Bishop) ||
                //Direção Diagonal Esquerda para Baixo
                DirectionHasEnemyPieceOfType(HorizontalDirections.Left,VerticalDirections.Down,8,PieceType.Queen,PieceType.Bishop) ||
                //Direção Diagonal Direita para Baixo
                DirectionHasEnemyPieceOfType(HorizontalDirections.Right,VerticalDirections.Down,8,PieceType.Queen,PieceType.Bishop);
    }

    private bool IsInCheckFromKnight()
    {
                // L para Esquerda e para cima
        return PositionIsEnemyPieceOfType(-1 , -2,PieceType.Knight) ||
                // L para Esquerda e para baixo
                PositionIsEnemyPieceOfType(1, -2,PieceType.Knight) ||
                // L para Direita e para cima
                PositionIsEnemyPieceOfType(-1 , 2,PieceType.Knight) ||
                // L para Direita e para baixo
                PositionIsEnemyPieceOfType(1 , 2,PieceType.Knight) ||
                // L para Cima e para Esquerda
                PositionIsEnemyPieceOfType(-2 , -1,PieceType.Knight) ||
                // L para Baixo e para Esquerda
                PositionIsEnemyPieceOfType(2 , -1,PieceType.Knight) ||
                // L para Cima e para Direita
                PositionIsEnemyPieceOfType(-2 , 1,PieceType.Knight) ||
                // L para Baixo e para Direita
                PositionIsEnemyPieceOfType(2 , 1,PieceType.Knight);
    }
    
    private bool IsInCheckFromPawn()
    {
        var vDir = GetPieceColor() == PieceColor.White ? VerticalDirections.Up : VerticalDirections.Down;
        return PositionIsEnemyPieceOfType((int)vDir , 1,PieceType.Pawn) ||
                PositionIsEnemyPieceOfType((int)vDir , -1,PieceType.Pawn);
    }

    
    
    public override void AfterMoveVerification()
    {
        throw new NotImplementedException();
    }
}