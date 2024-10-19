package LunarLander;

import model0.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Test class for the GameModel class.
 *
 * This class contains (black-box) test cases for the GameModel class.
 *
 * Assignment 0.1a: black-box testing of thrust control
 * Assignment 0.1b: white-box testing of collision detection
 * Assignment 0.2: mutation testing of collision detection
 */
public class GameModelTest {

    private GameModel gameModel;
    private Lander lander;

    @BeforeEach
    public void setUp() {
        gameModel = GameFactory.createGame();
        lander = gameModel.getLander();
    }

    //////////////////////////////////////////////////////////
    // Assignment 0.1a: black-box testing of thrust control //
    //////////////////////////////////////////////////////////


    @ParameterizedTest
    @CsvSource({
            "0",
            "11",
            "5"
    })
    public void testChangeTrustLevel_ZeroIncrement(int landerBaseThrust) {

        //Arrange
        lander.setThrustLevel(landerBaseThrust);

        //Act
        gameModel.changeThrustLevel(0);

        //Assert
        assertEquals(landerBaseThrust, lander.getThrustLevel(), "Thrust should remain unchanged when increased by 0");
    }

    @ParameterizedTest
    @CsvSource({
            "0, 1, 1",
            "0, 12, 11",
            "5, 3, 8",
            "11, 1, 11",
            "3, 20, 11"
    })
    public void testChangeTrustLevel_PositiveIncrement(int landerBaseThrust, int increment, int expectedThrust) {

        //Arrange
        lander.setThrustLevel(landerBaseThrust);

        //Act
        gameModel.changeThrustLevel(increment);

        //Assert
        assertEquals(expectedThrust, lander.getThrustLevel(), "Thrust should increase to 1");
    }


    @ParameterizedTest
    @CsvSource({
            "11, 1, 10",
            "11, 12, 0",
            "5, 3, 2",
            "0, 1, 0",
            "3, 20, 0"
    })
    public void testChangeTrustLevel_NegativeIncrement(int landerBaseThrust, int increment, int expectedThrust) {

        //Arrange
        lander.setThrustLevel(landerBaseThrust);

        //Act
        gameModel.changeThrustLevel(-increment);

        //Assert
        assertEquals(expectedThrust, lander.getThrustLevel(), "Thrust should increase to 1");
    }


    ///////////////////////////////////////////////////////////////
    // Assignment 0.2a: black-box testing of collision detection //
    ///////////////////////////////////////////////////////////////

    
}
