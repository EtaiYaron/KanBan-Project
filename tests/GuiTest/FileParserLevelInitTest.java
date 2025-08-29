package GuiTest;

import GUI.Parser.FileParser;
import GUI.Parser.TileFactory;
import Game.Board.GameBoard;
import Game.Callbacks.MessageCallback;
import Game.Level;
import Game.Tiles.Tile;
import Game.Tiles.Units.Enemies.Enemy;
import Game.Tiles.Units.Players.Player;
import Game.Tiles.Units.Players.Warrior;
import Game.Tiles.Units.UnitUtils.Health;
import Game.Utils.Position;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.nio.file.Files;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Supplier;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.*;

public class FileParserLevelInitTest {

    private FileParser parser;
    private TileFactory tileFactory;
    private List<String> fileLines;
    private MessageCallback msgCb;
    private Player testPlayer = new Warrior("TestPlayer", new Health(100, 100), 10, 10, 3);
    private final String TEST_LEVEL_PATH = "levels_dir_test/level1.txt";

    @BeforeEach
    void setup() throws Exception {
        tileFactory = new TileFactory();
        List<String> messages = new ArrayList<>();
        msgCb = messages::add;

        parser = new FileParser(tileFactory, msgCb);

        File levelFile = new File(TEST_LEVEL_PATH);
        assertTrue(levelFile.exists(), "Test level file does not exist");

        fileLines = Files.readAllLines(levelFile.toPath())
                .stream()
                .filter(line -> !line.isBlank())
                .collect(Collectors.toList());
        testPlayer.setMessageCallback(msgCb);
    }

    @Test
    void testBoardSizeMatchesLevelFile() {
        File levelFile = new File(TEST_LEVEL_PATH);
        Level level = parser.parseLevel(levelFile);
        level.levelInit(testPlayer);
        GameBoard board = level.getBoard();

        assertEquals(5, board.getRows(), "Expected 5 rows from level1.txt");
        assertEquals(15, board.getCols(), "Expected 15 columns from level1.txt");
    }

    @Test
    void testPlayerInitializedCorrectly() {
        File levelFile = new File(TEST_LEVEL_PATH);
        Level level = parser.parseLevel(levelFile);
        level.levelInit(testPlayer);
        Player player = level.getPlayer();
        assertNotNull(player, "Player should not be null");

        // Player is at (1,3) based on file (column=1, row=3)
        Position expected = new Position(1, 3);
        assertEquals(expected, player.getPosition(), "Player position mismatch");

        Tile tileOnBoard = level.getBoard().getTile(expected);
        assertSame(player, tileOnBoard, "Board should contain the player at the correct position");
    }

    @Test
    void testEnemyCountMatchesLevelFile() {
        File levelFile = new File(TEST_LEVEL_PATH);
        Level level = parser.parseLevel(levelFile);
        level.levelInit(testPlayer);
        // In level1.txt, there's only 1 enemy character 's'
        assertEquals(1, level.getEnemies().size(), "Expected 1 enemy parsed from the file");

        Enemy enemy = level.getEnemies().get(0);
        assertEquals('s', enemy.getTile().charValue(), "Expected enemy character 's'");
        assertEquals(new Position(5, 2), enemy.getPosition(), "Enemy position should be (5, 2)");
    }

    @Test
    void testBoardRendersCorrectly() {
        File levelFile = new File(TEST_LEVEL_PATH);
        Level level = parser.parseLevel(levelFile);
        level.levelInit(testPlayer);
        String expectedBoard = String.join("\n", fileLines) + "\n";
        assertEquals(expectedBoard, level.toString(), "Board rendering mismatch");
    }
}
