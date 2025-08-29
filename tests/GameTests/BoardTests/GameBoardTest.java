package GameTests.BoardTests;

import Game.Board.GameBoard;
import Game.Tiles.BoardComponents.Empty;
import Game.Tiles.BoardComponents.Wall;
import Game.Tiles.Tile;
import Game.Utils.Position;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class GameBoardTest {
    private GameBoard board;
    private int rows = 3;
    private int cols = 4;

    @Before
    public void setup() {
        Tile[][] raw = new Tile[rows][cols];
        GameBoard board = new GameBoard(raw);
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                raw[r][c] = new Empty(c, r);
                raw[r][c].addPositionChangedListener(board);
            }
        }
        board = new GameBoard(raw);
        this.board = board;
    }

    @Test
    public void testGetRowsCols() {
        assertEquals(rows, board.getRows());
        assertEquals(cols, board.getCols());
    }

    @Test
    public void testInBoundsTrue() {
        assertTrue(board.inBounds(new Position(0, 0)));
        assertTrue(board.inBounds(new Position(cols - 1, rows - 1)));
    }

    @Test
    public void testInBoundsFalse() {
        assertFalse(board.inBounds(new Position(-1, 0)));
        assertFalse(board.inBounds(new Position(0, -1)));
        assertFalse(board.inBounds(new Position(cols, rows)));
    }

    @Test
    public void testGetSetTile() {
        Position p = new Position(1, 2);
        Wall wall = new Wall(p.getX(), p.getY());
        board.setTile(p, wall);
        assertSame(wall, board.getTile(p));
    }

    @Test
    public void testPositionChangeListenerUpdatesBoard() {
        // Given: an Empty tile at (0,0)
        Position oldPos = new Position(0, 0);
        Empty tile = (Empty) board.getTile(oldPos);
        assertNotNull(tile);

        // When: we move it to (2,1)
        Position newPos = new Position(2, 1);
        tile.setPosition(newPos);
        Wall wall = new Wall(oldPos.getX(), oldPos.getY());
        board.setTile(oldPos, wall); // Set old position to a wall for clarity
        wall.addPositionChangedListener(board);
        // Then: the board must reflect it at the new position
        assertSame("Tile should appear at its new position",
                tile, board.getTile(newPos));

        // And (optionally) the old position should no longer reference it
        assertNotSame("Old position should no longer hold the moving tile",
                tile, board.getTile(oldPos));
    }

    @Test
    public void testToString() {
        // create small 2x2 board for toString
        Tile[][] raw2 = new Tile[2][2];
        raw2[0][0] = new Empty(0, 0);
        raw2[0][1] = new Wall(1, 0);
        raw2[1][0] = new Wall(0, 1);
        raw2[1][1] = new Empty(1, 1);
        GameBoard b2 = new GameBoard(raw2);
        String expected = ".#\n#.\n";
        assertEquals(expected, b2.toString());
    }
}
