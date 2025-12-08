using Chess_Console_Project.Board;
using Chess_Console_Project.Board.Pieces;

namespace Chess_Console_Project.Chess.ChessPieces;

public class King : Piece
{
    
    public King(ChessBoard board, PieceColor pieceColor) : base(board, pieceColor)
    {
        Value = 999;
        Name = "King";
        ChessNotation = 'K';
        PieceType = PieceType.King;
    }
    protected override void CalculatePossibleMoves()
    {
        ClearPossibleMoves();

        //Posição de Cima
        var pos = new Position(Position.Row -1, Position.Column);
        CheckIfCanMoveToPosition(pos);
        
        //Posição de Cima e Esquerda
        pos = new Position(Position.Row -1, Position.Column - 1);
        CheckIfCanMoveToPosition(pos);

        //Posição de Cima e Direita
        pos = new Position(Position.Row -1, Position.Column + 1);
        CheckIfCanMoveToPosition(pos);
        
        //Posição de Cima e Esquerda
        pos = new Position(Position.Row, Position.Column - 1);
        CheckIfCanMoveToPosition(pos);

        //Posição de Cima e Direita
        pos = new Position(Position.Row, Position.Column + 1);
        CheckIfCanMoveToPosition(pos);

        //Posição de Baixo
        pos = new Position(Position.Row + 1, Position.Column);
        CheckIfCanMoveToPosition(pos);
        
        //Posição de Baixo e Esquerda
        pos = new Position(Position.Row + 1, Position.Column - 1);
        CheckIfCanMoveToPosition(pos);

        //Posição de Baixo e Direita
        pos = new Position(Position.Row + 1, Position.Column + 1);
        CheckIfCanMoveToPosition(pos);
        
    }
    
    
}