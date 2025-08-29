package GameTests.ActionTests;

import GUI.Parser.TileFactory;
import Game.Board.GameBoard;
import Game.Tiles.BoardComponents.Empty;
import Game.Tiles.Tile;
import Game.Tiles.Units.Actions.CastAbility;
import Game.Tiles.Units.Enemies.Boss;
import Game.Tiles.Units.Enemies.Enemy;
import Game.Tiles.Units.Enemies.Monster;
import Game.Tiles.Units.Players.Player;
import Game.Tiles.Units.Players.Warrior;
import Game.Tiles.Units.Players.Mage;
import Game.Tiles.Units.Players.Rogue;
import Game.Tiles.Units.Players.Hunter;
import Game.Tiles.Units.UnitUtils.Health;
import Game.Utils.Position;
import org.junit.Before;
import org.junit.Test;
import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.*;

public class CastAbilityTest {
    private GameBoard board;
    private Monster monster;
    private List<Monster> enemies;
    private static int rows = 3;
    private static int cols = 3;

    @Before
    public void setupBoardAndMonster() {
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
        // create one monster at center (1,1)
        monster = new Monster('M', "Goblin", new Health(1000, 1000), 5, 0, 3, 20);
        monster.setPosition(new Position(1, 1));
        monster.addPositionChangedListener(board);
        monster.setMessageCallback(System.out::println);
        resetEnemy();

        enemies = new ArrayList<>();
        enemies.add(monster);
    }

    /** Helper: reset monster to center with full health and on board */
    private void resetEnemy() {
        monster.getHealth().setAmount(monster.getHealth().getPool());
        Position center = new Position(1, 1);
        monster.setPosition(center);
        board.setTile(center, monster);
    }

    private Player makeHero(int idx) {
        // idx: 0=Warrior,1=The Hound(Warrior),2=Mage,3=Mage,4=Rogue,5=Rogue,6=Hunter
        TileFactory factory = new TileFactory();
        return factory.initPlayers().get(idx).get();
    }

    @Test
    public void testWarriorAbility() {
        Warrior warrior = (Warrior) makeHero(0);
        warrior.setPosition(new Position(0, 0)); // Place warrior on board
        warrior.addPositionChangedListener(board);
        warrior.setMessageCallback(System.out::println);

        this.resetEnemy();
        int before = monster.getHealth().getAmount();
        new CastAbility(warrior, (List) enemies).execute(board);

        assertEquals(((Integer) (before - warrior.getAttackPoints())), monster.getHealth().getAmount());
    }

    @Test
    public void testMageAbility() {
        Mage mage = (Mage) makeHero(2);
        mage.setPosition(new Position(0, 0)); // Place warrior on board
        mage.addPositionChangedListener(board);
        mage.setMessageCallback(System.out::println);

        this.resetEnemy();
        int before = monster.getHealth().getAmount();
        int originalMana = mage.getMana().getAmount();
        new CastAbility(mage, (List) enemies).execute(board);
        mage.onGameTick();

        assertEquals(((Integer) (before - 15 * 5)), monster.getHealth().getAmount());
        assertEquals(((Integer) mage.getMana().getAmount()), ((Integer) ((Integer) originalMana - mage.getManaCost())));
    }

    @Test
    public void testRogueAbility() {
        Rogue rogue = (Rogue) makeHero(4);
        rogue.setPosition(new Position(0, 0)); // Place warrior on board
        rogue.addPositionChangedListener(board);
        rogue.setMessageCallback(System.out::println);

        this.resetEnemy();
        int before = monster.getHealth().getAmount();
        int originalEnergy = rogue.getEnergy().getAmount();
        new CastAbility(rogue, (List) enemies).execute(board);
        rogue.onGameTick();


        assertEquals(((Integer) (before - rogue.getAttackPoints())), monster.getHealth().getAmount());
        assertEquals(((Integer) rogue.getEnergy().getAmount()), ((Integer) ((Integer) originalEnergy - rogue.getCost())));
    }

    @Test
    public void testHunterAbility() {
        Hunter hunter = (Hunter) makeHero(6);
        hunter.setPosition(new Position(0, 0)); // Place warrior on board
        hunter.addPositionChangedListener(board);
        hunter.setMessageCallback(System.out::println);

        this.resetEnemy();
        int before = monster.getHealth().getAmount();
        int originalArrow = hunter.getArrowsCount();
        new CastAbility(hunter, (List) enemies).execute(board);

        assertEquals(((Integer) (before - hunter.getAttackPoints())), monster.getHealth().getAmount());
        assertEquals(((Integer) hunter.getArrowsCount()), ((Integer) (originalArrow - 1)));
    }

    @Test
    public void testBossAbility() {
        Boss boss = (Boss) new TileFactory().initEnemies().get(8).get(); // Boss 'M' - The Mountain
        boss.setPosition(new Position(0, 0)); // Place boss on board
        boss.addPositionChangedListener(board);
        boss.setMessageCallback(System.out::println);

        Player monster = makeHero(0); // Use Warrior for testing
        monster.setDefensePoints(0);
        monster.setPosition(new Position(1, 1)); // Place monster on board
        monster.addPositionChangedListener(board);
        monster.setMessageCallback(System.out::println);
        int before = monster.getHealth().getAmount();
        boss.castAbility(board, new ArrayList<>(), monster);


        assertEquals(((Integer) (before - boss.getAttackPoints())), monster.getHealth().getAmount());
    }
}
