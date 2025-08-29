package GameTests.ActionTests;

import Game.Board.GameBoard;
import Game.Tiles.BoardComponents.Empty;
import Game.Tiles.BoardComponents.Wall;
import Game.Tiles.Tile;
import Game.Tiles.Units.Actions.Movement;
import Game.Tiles.Units.Enemies.Enemy;
import Game.Tiles.Units.Enemies.Monster;
import Game.Tiles.Units.Players.Warrior;
import Game.Tiles.Units.UnitUtils.Energy;
import Game.Tiles.Units.UnitUtils.Health;
import Game.Utils.Position;
import org.junit.Before;
import org.junit.Test;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;

import static org.junit.Assert.assertEquals;

public class MovementTest {
    private GameBoard board;
    private Warrior hero;
    private Enemy enemy;
    private static int rows = 5;
    private static int cols = 5;
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
        // place hero in center
        hero = new Warrior("TestHero", new Health(100, 100), 10, 5, 3);
        this.board.setTile(new Position(1, 1), hero);
        hero.setPosition(new Position(1, 1));
        hero.addPositionChangedListener(board);

        enemy = new Monster('e', "TestMonster", new Health(50, 50), 8, 2, 3, 100);
        this.board.setTile(new Position(3, 3), enemy);
        enemy.setPosition(new Position(3, 3));
        enemy.addPositionChangedListener(board);
    }

    @Test
    public void testHeroMoveUpIntoEmpty() {
        new Movement.Up(hero).execute(board);
        assertEquals(new Position(1, 0), hero.getPosition());
    }

    @Test
    public void testHeroMoveDownIntoEmpty() {
        new Movement.Down(hero).execute(board);
        assertEquals(new Position(1, 2), hero.getPosition());
    }

    @Test
    public void testHeroMoveLeftIntoEmpty() {
        new Movement.Left(hero).execute(board);
        assertEquals(new Position(0, 1), hero.getPosition());
    }

    @Test
    public void testHeroMoveRightIntoEmpty() {
        new Movement.Right(hero).execute(board);
        assertEquals(new Position(2, 1), hero.getPosition());
    }

    @Test
    public void testHeroStayDoesNothing() {
        new Movement.Stay(hero).execute(board);
        assertEquals(new Position(1, 1), hero.getPosition());
    }

    @Test
    public void testHeroMoveBlockedByWall() {
        // place wall above hero
        Position wallPos = new Position(1, 0);
        board.setTile(wallPos, new Wall(wallPos.getX(), wallPos.getY()));
        new Movement.Up(hero).execute(board);
        // hero should remain in center
        assertEquals(new Position(1, 1), hero.getPosition());
    }

    @Test
    public void testHeroMoveOutOfBoundsDoesNothing() {
        // place hero at top-left
        hero.setPosition(new Position(0, 0));
        new Movement.Up(hero).execute(board);
        new Movement.Left(hero).execute(board);
        assertEquals(new Position(0, 0), hero.getPosition());
    }

    @Test
    public void testEnemyMoveUpIntoEmpty() {
        new Movement.Up(enemy).execute(board);
        assertEquals(new Position(3, 2), enemy.getPosition());
    }

    @Test
    public void testEnemyMoveDownIntoEmpty() {
        new Movement.Down(enemy).execute(board);
        assertEquals(new Position(3, 4), enemy.getPosition());
    }

    @Test
    public void testEnemyMoveLeftIntoEmpty() {
        new Movement.Left(enemy).execute(board);
        assertEquals(new Position(2, 3), enemy.getPosition());
    }

    @Test
    public void testEnemyMoveRightIntoEmpty() {
        new Movement.Right(enemy).execute(board);
        assertEquals(new Position(4, 3), enemy.getPosition());
    }

    @Test
    public void testEnemyStayDoesNothing() {
        new Movement.Stay(enemy).execute(board);
        assertEquals(new Position(3, 3), enemy.getPosition());
    }

    @Test
    public void testEnemyMoveBlockedByWall() {
        // place wall below enemy
        Position wallPos = new Position(3, 4);
        board.setTile(wallPos, new Wall(wallPos.getX(), wallPos.getY()));
        new Movement.Down(enemy).execute(board);
        // enemy should remain in original position
        assertEquals(new Position(3, 3), enemy.getPosition());
    }

    @Test
    public void testEnemyMoveOutOfBoundsDoesNothing() {
        // place enemy at bottom-right
        enemy.setPosition(new Position(4, 4));
        new Movement.Down(enemy).execute(board);
        new Movement.Right(enemy).execute(board);
        assertEquals(new Position(4, 4), enemy.getPosition());
    }
}
