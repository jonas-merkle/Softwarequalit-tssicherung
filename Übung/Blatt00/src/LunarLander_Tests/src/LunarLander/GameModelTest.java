package LunarLander;

import model0.*;
import java.util.ArrayList;
import javafx.geometry.Dimension2D;
import javafx.geometry.Point2D;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Test class for the GameModel class.
 *
 * This class contains test cases for the GameModel class, focusing on:
 * - Black-box testing of thrust control (Assignment 0.1a)
 * - White-box testing of collision detection (Assignment 0.1b)
 * - Mutation testing of collision detection (Assignment 0.2)
 */
public class GameModelTest {

    private GameModel gameModel;
    private Lander lander;
    private ArrayList<Triangle> obstacles;

    /**
     * Sets up the test environment before each test.
     * Initializes the game model, lander, and obstacles.
     * Sets the initial state of the lander and starts the game.
     */
    @BeforeEach
    public void setUp() {
        // init game with no obstacles
        gameModel = GameFactory.createGame();
        obstacles = new ArrayList<>();
        gameModel.setObstacles(obstacles);

        // init lander at origin with no speed or tilt and no thrust level and size 1x1
        lander = gameModel.getLander();
        lander.setTilt(0);
        lander.setThrustLevel(0);
        lander.setPos(new Point2D(0, 0));
        lander.setSpeed(new Point2D(0, 0));
        lander.setThrustVector(new Point2D(0, 0));
        lander.setSize(new Dimension2D(1,1));

        // start the game
        gameModel.startGame();
    }

    //////////////////////////////////////////////////////////
    // Assignment 0.1a: black-box testing of thrust control //
    //////////////////////////////////////////////////////////

    /**
     * Tests that the thrust level remains unchanged when increased by 0.
     *
     * @param landerBaseThrust the initial thrust level of the lander
     */
    @ParameterizedTest
    @CsvSource({
            "0",
            "11",
            "5"
    })
    public void testChangeTrustLevel_ZeroIncrement(int landerBaseThrust) {
        // Arrange
        lander.setThrustLevel(landerBaseThrust);

        // Act
        gameModel.changeThrustLevel(0);

        // Assert
        assertEquals(landerBaseThrust, lander.getThrustLevel(), "Thrust should remain unchanged when increased by 0");
    }

    /**
     * Tests that the thrust level increases within bounds.
     *
     * @param landerBaseThrustLevel the initial thrust level of the lander
     * @param thrustLevelIncrement the amount by which the thrust level is increased
     * @param expectedThrustLevel the expected thrust level after the increment
     */
    @ParameterizedTest
    @CsvSource({
            "0, 1, 1",
            "0, 12, 11",
            "5, 3, 8",
            "11, 1, 11",
            "3, 20, 11"
    })
    public void testChangeTrustLevel_PositiveIncrement(int landerBaseThrustLevel, int thrustLevelIncrement, int expectedThrustLevel) {
        // Arrange
        lander.setThrustLevel(landerBaseThrustLevel);

        // Act
        gameModel.changeThrustLevel(thrustLevelIncrement);

        // Assert
        assertEquals(expectedThrustLevel, lander.getThrustLevel(), "Thrust should increase to the expected value");
    }

    /**
     * Tests that the thrust level decreases within bounds.
     *
     * @param landerBaseThrustLevel the initial thrust level of the lander
     * @param thrustLevelIncrement the amount by which the thrust level is decreased
     * @param expectedThrustLevel the expected thrust level after the decrement
     */
    @ParameterizedTest
    @CsvSource({
            "11, 1, 10",
            "11, 12, 0",
            "5, 3, 2",
            "0, 1, 0",
            "3, 20, 0"
    })
    public void testChangeTrustLevel_NegativeIncrement(int landerBaseThrustLevel, int thrustLevelIncrement, int expectedThrustLevel) {
        // Arrange
        lander.setThrustLevel(landerBaseThrustLevel);

        // Act
        gameModel.changeThrustLevel(-thrustLevelIncrement);

        // Assert
        assertEquals(expectedThrustLevel, lander.getThrustLevel(), "Thrust should decrease to the expected value");
    }

    /**
     * Tests the acceleration per step for different thrust levels.
     *
     * @param setThrustLevel the thrust level to set
     * @param expectedAcceleration the expected acceleration
     * @param delta the allowable delta for the expected acceleration
     */
    @ParameterizedTest
    @CsvSource({
            "-1, 0.0f, 0.00001f",
            "0, 0.0f, 0.00001f",
            "1, 0.409f, 0.001f",
            "2, 0.81f, 0.01f",
            "3, 1.227f, 0.001f",
            "4, 1.63f, 0.01f",
            "5, 2.045f, 0.001f",
            "6, 2.45f, 0.01f",
            "7, 2.863f, 0.001f",
            "8, 3.27f, 0.01f",
            "9, 3.681f, 0.001f",
            "10, 4.09f, 0.01f",
            "11, 4.5f, 0.00001f",
            "12, 4.5f, 0.00001f",
    })
    public void testChangeTrustLevel_AccelerationPerStep(int setThrustLevel, double expectedAcceleration, double delta) {
        // Arrange

        // Act
        gameModel.changeThrustLevel(setThrustLevel);

        // Assert
        assertEquals(expectedAcceleration, lander.getThrustVector().magnitude(), delta, "Acceleration should remain unchanged when thrust is 0");
    }

    ///////////////////////////////////////////////////////////////
    // Assignment 0.2a: black-box testing of collision detection //
    ///////////////////////////////////////////////////////////////

    /**
     * Helper method to check if the lander has collided with any obstacles.
     *
     * @return true if the lander has collided, false otherwise
     */
    private boolean landerCollided() {
        // run model update
        gameModel.updateElements(0);

        // check for collision
        return gameModel.getGameState() == GameState.GAME_OVER;
    }

    /**
     * Tests that the lander does not collide with the obstacle in test class ok1.
     */
    @Test
    public void testLanderCollision_Class_ok1() {
        // Arrange -> create test class ok1
        obstacles.add(new Triangle(new Point2D(-1, 3), new Point2D(1, 3), new Point2D(0, 4)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(0, 1));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.RUNNING, gameModel.getGameState(), "Lander should not collide with the obstacle");
    }

    /**
     * Tests that the lander does not collide with the obstacle in test class ok2.
     */
    @Test
    public void testLanderCollision_Class_ok2() {
        // Arrange -> create test class ok2
        obstacles.add(new Triangle(new Point2D(-1, 0), new Point2D(1, 0), new Point2D(0, 1)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(3, 1));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.RUNNING, gameModel.getGameState(), "Lander should not collide with the obstacle");
    }

    /**
     * Tests that the lander does not collide with the obstacle in test class ok3.
     */
    @Test
    public void testLanderCollision_Class_ok3() {
        // Arrange -> create test class ok3
        obstacles.add(new Triangle(new Point2D(-1, 0), new Point2D(1, 0), new Point2D(0, 1)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(3, 1));
        lander.setTilt(-30);

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.RUNNING, gameModel.getGameState(), "Lander should not collide with the obstacle");
    }

    /**
     * Tests that the lander does not collide with the obstacle in test class ok4.
     */
    @Test
    public void testLanderCollision_Class_ok4() {
        // Arrange -> create test class ok4
        obstacles.add(new Triangle(new Point2D(0, 1.5), new Point2D(2, 1.5), new Point2D(1, 3.5)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(1, 1));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.RUNNING, gameModel.getGameState(), "Lander should not collide with the obstacle");
    }

    /**
     * Tests that the lander does not collide with the obstacle in test class ok5.
     */
    @Test
    public void testLanderCollision_Class_ok5() {
        // Arrange -> create test class ok5
        obstacles.add(new Triangle(new Point2D(0, - Math.sqrt(2)), new Point2D(4, -5), new Point2D(0.5 * Math.sqrt(2),0)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(0,0));
        lander.setTilt(45);

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.RUNNING, gameModel.getGameState(), "Lander should not collide with the obstacle");
    }

    /**
     * Tests that the lander collides with the obstacle in test class crash1.
     */
    @Test
    public void testLanderCollision_Class_crash1() {
        // Arrange -> create test class crash1
        obstacles.add(new Triangle(new Point2D(0, 0), new Point2D(4, 0), new Point2D(4, 2)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(2,2));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.GAME_OVER, gameModel.getGameState(), "Lander should collide with the obstacle");
    }

    /**
     * Tests that the lander collides with the obstacle in test class crash2.
     */
    @Test
    public void testLanderCollision_Class_crash2() {
        // Arrange -> create test class crash2
        obstacles.add(new Triangle(new Point2D(0, 0), new Point2D(4, 0), new Point2D(4, 2)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(4,2));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.GAME_OVER, gameModel.getGameState(), "Lander should collide with the obstacle");
    }

    /**
     * Tests that the lander collides with the obstacle in test class crash3.
     */
    @Test
    public void testLanderCollision_Class_crash3() {
        // Arrange -> create test class crash3
        obstacles.add(new Triangle(new Point2D(0, 0), new Point2D(4, 0), new Point2D(4, 2)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(3.5f,2));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.GAME_OVER, gameModel.getGameState(), "Lander should collide with the obstacle");
    }

    /**
     * Tests that the lander collides with the obstacle in test class crash4.
     */
    @Test
    public void testLanderCollision_Class_crash4() {
        // Arrange -> create test class crash4
        obstacles.add(new Triangle(new Point2D(0, 0), new Point2D(4, 0), new Point2D(4, 4)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(2,2));

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.GAME_OVER, gameModel.getGameState(), "Lander should collide with the obstacle");
    }

    /**
     * Tests that the lander collides with the obstacle in test class crash5.
     */
    @Test
    public void testLanderCollision_Class_crash5() {
        // Arrange -> create test class crash5
        obstacles.add(new Triangle(new Point2D(0, 0), new Point2D(4, 0), new Point2D(4, 4)));
        lander.setSize(new Dimension2D(1, 1));
        lander.setPos(new Point2D(2,2));
        lander.setTilt(45);

        // Act
        gameModel.updateElements(0);

        // Assert
        assertEquals(GameState.GAME_OVER, gameModel.getGameState(), "Lander should collide with the obstacle");
    }
}