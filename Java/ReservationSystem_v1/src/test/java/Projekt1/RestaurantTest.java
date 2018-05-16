package Projekt1;

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
    public void validate_CorrectParamsFromFile_True(String name, String description,
                                                           int openingHour, int closingHour,
                                                           int breakStartingHour, int breakEndingHour){
        boolean daysS[] = new boolean[]{true,true,true,true,true,true,false};
        assertThat(Restaurant.validate(name, description, daysS, openingHour,
                closingHour, breakStartingHour, breakEndingHour)).isTrue();
    }

    @Test
    @FileParameters("src/test/resources/restaurantsInvalidData.csv")
    public void validate_WrongParamsFromFile_False(String name, String description,
                                                                int openingHour, int closingHour,
                                                                int breakStartingHour, int breakEndingHour){
        boolean daysS[] = new boolean[]{true,true,true,true,true,true,false};
        assertThat(Restaurant.validate(name, description, daysS, openingHour,
                closingHour, breakStartingHour, breakEndingHour)).isFalse();
    }

    @Test
    public void validate_CorrectArguments_True() {
        assertTrue(Restaurant.validate(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_NullName_False() {
        assertFalse(Restaurant.validate(null, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_EmptyName_False() {
        assertFalse(Restaurant.validate("", description, days, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_NullDescription_False() {
        assertFalse(Restaurant.validate(name, null, days, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_EmptyDescription_False() {
        assertFalse(Restaurant.validate(name, "", days, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_NullDaysArray_False(){
        assertFalse(Restaurant.validate(name, description, null, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_DaysArrLengthIsGreaterThen7_False() {
        boolean arr[] = new boolean[8];
        assertFalse(Restaurant.validate(name, description, arr, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_DaysArrLengthIsSmallerThen7_False() {
        boolean arr[] = new boolean[6];
        assertFalse(Restaurant.validate(name, description, arr, openingHour, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_OpeningHourMustBePositive_False(){
        assertFalse(Restaurant.validate(name, description, days, -1, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_ClosingHourMustBePositive_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, -1, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_OpeningHourMustBeBetween0And24_False(){
        assertFalse(Restaurant.validate(name, description, days, 25, closingHour, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_ClosingHourMustBeBetween0And24_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, 25, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_OpeningHourIsGreaterThenClosingHour_False(){
        assertFalse(Restaurant.validate(name, description, days, 20, 8, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_OpeningHourAndClosingHourCannotBeTheSame_False(){
        assertFalse(Restaurant.validate(name, description, days, 20, 20, breakStartingHour, breakEndingHour));
    }

    @Test
    public void validate_BreakStartingHourMustBePositive_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, closingHour, -1, breakEndingHour));
    }

    @Test
    public void validate_BreakEndingHourMustBePositive_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, closingHour, breakStartingHour, -1));
    }

    @Test
    public void validate_BreakStartingHourMustBeBetween0And24_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, closingHour, 25, breakEndingHour));
    }

    @Test
    public void validate_BreakEndingHourMustBeBetween0And24_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, closingHour, breakStartingHour, 25));
    }

    @Test
    public void validate_BreakStartingHourIsGreaterThenBreakEndingHour_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, closingHour, 20, 8));
    }

    @Test
    public void validate_BreakStartingHourAndBreakEndingHourCannotBeTheSame_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, closingHour, 20, 20));
    }

    @Test
    public void validate_BreakStartingHourIsSmallerThenOpeningHour_False(){
        assertFalse(Restaurant.validate(name, description, days, 8, closingHour, 7, breakEndingHour));
    }

    @Test
    public void validate_BreakEndingHourIsGreaterThenClosingHour_False(){
        assertFalse(Restaurant.validate(name, description, days, openingHour, 20, breakStartingHour, 22));
    }

    @Test
    public void toString_RestaurantObjectExists_ReturnsStringWithProperData(){
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
