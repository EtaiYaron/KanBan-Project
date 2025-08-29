package InteractionTests;

import Game.Board.GameBoard;
import Game.Tiles.BoardComponents.Empty;
import Game.Tiles.BoardComponents.Wall;
import Game.Tiles.Tile;
import Game.Tiles.Units.Enemies.Enemy;
import Game.Tiles.Units.Enemies.Monster;
import Game.Tiles.Units.Players.Player;
import Game.Tiles.Units.Players.Warrior;
import Game.Tiles.Units.UnitUtils.Health;
import Game.Utils.Position;
import Game.Tiles.Units.Actions.Movement;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class BattleTest {
    private Player player;
    private Enemy enemy1;
    private Enemy enemy2;
    private Enemy enemy3;
    private Enemy enemy4;
    private Enemy enemy5;
    private GameBoard gameBoard;

    @Before
    public void setUp() {
        Tile[][] raw = new Tile[3][3];
        GameBoard board = new GameBoard(raw);
        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                raw[r][c] = new Empty(c, r);
                raw[r][c].addPositionChangedListener(board);
            }
        }
        this.gameBoard = new GameBoard(raw);
        this.player = new Warrior("test player", new Health(0, 0), 99, 10, 3);
        this.player.setPosition(new Position(1, 1));
        this.player.addPositionChangedListener(gameBoard);
        this.player.setMessageCallback(System.out::println);
        this.gameBoard.setTile(player.getPosition(), player);
        this.enemy1 = new Monster('a', "test enemy1", new Health(0, 0), 0, 0, 1, 25);
        this.enemy1.setPosition(new Position(2, 1));
        this.enemy1.addPositionChangedListener(gameBoard);
        this.enemy1.setMessageCallback(System.out::println);
        this.gameBoard.setTile(enemy1.getPosition(), enemy1);
        this.enemy2 = new Monster('b', "test enemy2", new Health(0, 0), 1000, 0, 1, 50);
        this.enemy2.setPosition(new Position(1, 0));
        this.enemy2.addPositionChangedListener(gameBoard);
        this.enemy2.setMessageCallback(System.out::println);
        this.gameBoard.setTile(enemy2.getPosition(), enemy2);
        this.enemy3 = new Monster('c', "test enemy3", new Health(100, 100), 0, 0, 1, 50);
        this.enemy3.setPosition(new Position(1, 2));
        this.enemy3.addPositionChangedListener(gameBoard);
        this.enemy3.setMessageCallback(System.out::println);
        this.gameBoard.setTile(enemy3.getPosition(), enemy3);
        this.enemy4 = new Monster('d', "test enemy4", new Health(0, 0), 1000, 0, 1, 500);
        this.enemy4.setPosition(new Position(0, 1));
        this.enemy4.addPositionChangedListener(gameBoard);
        this.enemy4.setMessageCallback(System.out::println);
        this.gameBoard.setTile(enemy4.getPosition(), enemy4);
        this.enemy5 = new Monster('e', "test enemy5", new Health(0, 0), 1000, 0, 1, 500);
        this.enemy5.setPosition(new Position(0, 0));
        this.enemy5.addPositionChangedListener(gameBoard);
        this.enemy5.setMessageCallback(System.out::println);
        this.gameBoard.setTile(enemy5.getPosition(), enemy5);

        // e b .
        // d @ a
        // . c .

    }

    @Test
    public void playerKillsEnemy(){
        new Movement.Right(this.player).execute(this.gameBoard);
        assertFalse(this.enemy1.isAlive());
    }

    @Test
    public void enemyKillsPlayer(){
        new Movement.Down(this.enemy2).execute(this.gameBoard);
        assertFalse(this.player.isAlive());
    }

    @Test
    public void playerToStringAfterDeath(){
        new Movement.Down(this.enemy2).execute(this.gameBoard);
        assertEquals("X", this.player.toString());
    }

    @Test
    public void playerDamagingEnemy(){
        new Movement.Down(this.player).execute(this.gameBoard);
        assertTrue(this.enemy3.getHealth().getAmount() < 100);
        assertTrue(this.enemy3.isAlive());
    }

    @Test
    public void playerGainingXP(){
        int oldXP = this.player.getExperience();
        new Movement.Right(this.player).execute(this.gameBoard);
        assertEquals( oldXP + this.enemy1.getExperienceValue(), this.player.getExperience());
    }

    @Test
    public void playerLevelsUpAfterKill() {
        new Movement.Up(this.player).execute(this.gameBoard);
        assertEquals(2, this.player.getPlayerLevel());
    }

    @Test
    public void playerLevelsUpMultipleLevelsAfterKill(){
        new Movement.Left(this.player).execute(this.gameBoard);
        assertEquals(5, this.player.getPlayerLevel());
    }

    @Test
    public void enemyStaysInPlaceWhenAttackingEnemy(){
        Position oldP2 = this.enemy2.getPosition();
        Position oldP5 = this.enemy5.getPosition();
        new Movement.Left(this.enemy2).execute(this.gameBoard);
        assertEquals(oldP2, this.enemy2.getPosition());
        assertEquals(oldP5, this.enemy5.getPosition());
    }


    @Test
    public void enemyDoesNotGetDamagedByAnotherEnemy(){
        Integer Enemy2OldHealth = this.enemy2.getHealth().getAmount();
        Integer Enemy5OldHealth = this.enemy5.getHealth().getAmount();
        new Movement.Left(this.enemy2).execute(this.gameBoard);
        assertEquals(Enemy2OldHealth, this.enemy2.getHealth().getAmount());
        assertEquals(Enemy5OldHealth, this.enemy5.getHealth().getAmount());
    }
}
