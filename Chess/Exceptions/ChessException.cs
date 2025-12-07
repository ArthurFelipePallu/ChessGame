namespace Chess_Console_Project.Chess.Exceptions;

public class ChessException(string msg): Exception(msg)
{
    
}

public class MovementException(string msg) : ChessException(msg)
{
    
}