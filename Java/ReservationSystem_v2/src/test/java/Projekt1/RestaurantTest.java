package Projekt1;

import Projekt2.Models.Restaurant;
import Projekt2.Validators.RestaurantValidator;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import static org.assertj.core.api.Assertions.*;
import static org.junit.Assert.*;
import org.junit.runner.RunWith;
import junitparams.FileParameters;
import junitparams.JUnitParamsRunner;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

@RunWith(JUnitParamsRunner.class)
public class RestaurantTest {

    String name,description;
    boolean days[];
    int openingHour,closingHour,breakStartingHour,breakEndingHour;
    Restaurant restaurant;

    @Before
    public void setUp(){
        name = "TelePizza";
        description = "Super pizzeria!!111!!!";
        days = new boolean[]{false, true, true, true, true, true, false};
        openingHour = 0;
        closingHour = 24;
        breakStartingHour = 16;
        breakEndingHour = 17;
    }

    @Test
    @FileParameters("src/test/resources/restaurantsCorrectData.csv")
    public void validateCorrectParamsFromFileReturnsTrue(String name, String description,
                                                           int openingHour, int closingHour,
                                                           int breakStartingHour, int breakEndingHour){
        boolean daysS[] = new boolean[]{true,true,true,true,true,true,false};
        assertThat(RestaurantValidator.validate(new Restaurant(name, description, daysS, openingHour,
                closingHour, breakStartingHour, breakEndingHour))).isTrue();
    }

    @Test
    @FileParameters("src/test/resources/restaurantsInvalidData.csv")
    public void validateWrongParamsFromFileReturnsFalse(String name, String description,
                                                                int openingHour, int closingHour,
                                                                int breakStartingHour, int breakEndingHour){
        boolean daysS[] = new boolean[]{true,true,true,true,true,true,false};
        assertThat(RestaurantValidator.validate(new Restaurant(name, description, daysS, openingHour,
                closingHour, breakStartingHour, breakEndingHour))).isFalse();
    }

    @Test
    public void defaultConstructorNoArgsReturnsInstanceTypeOfRestaurant(){
        Restaurant r = new Restaurant();
        assertEquals(Restaurant.class, r.getClass());
    }

    @Test
    public void validateCorrectArgumentsReturnsTrue() {
        assertTrue(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateNullNameReturnsFalse() {
        assertFalse(RestaurantValidator.validate(
                new Restaurant(null, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateEmptyNameReturnsFalse() {
        assertFalse(RestaurantValidator.validate(
                new Restaurant("", description, days, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateNullDescriptionReturnsFalse() {
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, null, days, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateEmptyDescriptionReturnsFalse() {
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, "", days, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateNullDaysArrayReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, null, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateDaysArrLengthIsGreaterThen7ReturnsFalse() {
        boolean arr[] = new boolean[8];
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, arr, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateDaysArrLengthIsSmallerThen7ReturnsFalse() {
        boolean arr[] = new boolean[6];
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, arr, openingHour, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateOpeningHourMustBePositiveReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, -1, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateClosingHourMustBePositiveReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, -1, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateOpeningHourMustBeBetween0And24ReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, 25, closingHour, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateClosingHourMustBeBetween0And24ReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, 25, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateOpeningHourIsGreaterThenClosingHourReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, 20, 8, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateOpeningHourAndClosingHourCannotBeTheSameReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, 20, 20, breakStartingHour, breakEndingHour)));
    }

    @Test
    public void validateBreakStartingHourMustBePositiveReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, -1, breakEndingHour)));
    }

    @Test
    public void validateBreakEndingHourMustBePositiveReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, -1)));
    }

    @Test
    public void validateBreakStartingHourMustBeBetween0And24ReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, 25, breakEndingHour)));
    }

    @Test
    public void validateBreakEndingHourMustBeBetween0And24ReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, 25)));
    }

    @Test
    public void validateBreakStartingHourIsGreaterThenBreakEndingHourReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, 20, 8)));
    }

    @Test
    public void validateBreakStartingHourAndBreakEndingHourCannotBeTheSameReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, closingHour, 20, 20)));
    }

    @Test
    public void validateBreakStartingHourIsSmallerThenOpeningHourReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, 8, closingHour, 7, breakEndingHour)));
    }

    @Test
    public void validateBreakEndingHourIsGreaterThenClosingHourReturnsFalse(){
        assertFalse(RestaurantValidator.validate(
                new Restaurant(name, description, days, openingHour, 20, breakStartingHour, 22)));
    }

    @Test
    public void toStringRestaurantObjectExistsReturnsStringWithProperData(){
        restaurant = new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour);
        assertThat(restaurant.toString()).contains(name).contains(description).contains(openingHour + "-" + closingHour)
                .contains(breakStartingHour +"-" +breakEndingHour).containsIgnoringCase("Monday");
    }

    @After
    public void tearDown(){
        name = null;
        description = null;
        days = null;
        openingHour = 0;
        closingHour = 0;
        breakStartingHour = 0;
        breakEndingHour = 0;
    }
}
