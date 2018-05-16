package Projekt1;

import java.io.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import junitparams.FileParameters;
import junitparams.JUnitParamsRunner;
import org.assertj.core.api.Assertions;
import org.junit.After;
import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.TemporaryFolder;
import org.junit.runner.RunWith;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.*;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNull;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

@RunWith(JUnitParamsRunner.class)
public class ReservationServiceTest {

    String nickname,password,name,description;
    boolean days[];
    int openingHour,closingHour,breakStartingHour,breakEndingHour, bookHour, restaurantId;
    String date, closedDate;
    Date dateObj;
    User user;
    Restaurant restaurant;
    ReservationService reservationService;
    ArrayList<Restaurant> db;

    @Rule
    public TemporaryFolder folder = new TemporaryFolder();

    @Before
    public void setUp() throws ParseException {
        //Correct values and objects -> that pass validation
        nickname = "Hergroth";
        password = "janeczek100";
        name = "Przebojowa Pizza PePe";
        description = "Pizza srednia tylko za 9.99zl!";
        days = new boolean[]{true,true,true,true,true,true,false};
        openingHour = 8;
        closingHour = 20;
        breakStartingHour = 8;
        breakEndingHour = 10;
        bookHour = 12;
        user = new User(nickname, password);
        restaurant = new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour);
        reservationService = new ReservationService();
        SimpleDateFormat sdf = new SimpleDateFormat("dd.MM.yyyy");
        date = "08.04.2018"; //Sunday
        closedDate = "07.04.2018"; //Saturday(its closed in var restaurant)
        dateObj = sdf.parse(date);
        db = new ArrayList();
        db.add(restaurant);
        restaurantId = db.size()-1;
    }

    @Test
    @FileParameters("src/test/resources/restaurantsCorrectData.csv")
    public void loadRestaurants_FromFileWithCorrectData_AddsEachOne(String name, String description,
                                                                    int openingHour, int closingHour,
                                                                    int breakStartingHour, int breakEndingHour){
        ArrayList<Restaurant> restaurants = new ArrayList<>();
        Restaurant r = new Restaurant(name, description, days, openingHour,
                closingHour, breakStartingHour, breakEndingHour);
        restaurants.add(r);
        reservationService.loadRestaurants(restaurants);
        Assertions.assertThat(reservationService.getRestaurants()).contains(r).hasSize(1);
    }

    @Test
    public void loadRestaurants_ListWithMultipleRestaurants_ContainsThoseRestaurants(){
        ArrayList<Restaurant> restaurants = new ArrayList<Restaurant>();
        int n = 10;
        for(int i = 0; i < n; i++)
            restaurants.add(restaurant);
        reservationService.loadRestaurants(restaurants);
        assertThat(reservationService.getRestaurants().size(), is(n));
    }

    @Test (expected = NullPointerException.class)
    public void loadRestaurants_NullList_ThrowsNullPointerException(){
        reservationService.loadRestaurants(null);
    }

    @Test
    public void loadRestaurantsFromFile_SomeDataInFileIsCorrupted_ReturnsCorrectCountOfErrors() throws FileNotFoundException {
        reservationService.dbLocation = "src/test/resources/dbTest.txt";
        int expectedErrorsCount = 6;

        assertThat(reservationService.loadRestaurantsFromFile(), is(expectedErrorsCount));
    }

    @Test
    public void loadRestaurantsFromFile_SomeDataInFileIsCorrupted_AddsCorrectNumberOfRestaurantsToService() throws FileNotFoundException {
        reservationService.dbLocation = "src/test/resources/dbTest.txt";
        int expectedRestaurantsAdded = 5;

        reservationService.loadRestaurantsFromFile();
        assertThat(reservationService.getRestaurants().size(), is(expectedRestaurantsAdded));
    }

    @Test
    public void addUser_UniqueValidUser_TrueAndAdded(){
        reservationService.addUser(nickname, password);
        Assertions.assertThat(reservationService.getUsers()).contains(new User(nickname,password))
                .hasSize(1).hasAtLeastOneElementOfType(User.class);
    }

    @Test (expected = IllegalArgumentException.class)
    public void addUser_InvalidUser_False(){
        reservationService.addUser(null, "");
        reservationService.addUser("", null);
    }

    @Test
    public void addUser_UserAlreadyOnTheList_AddedOnlyOnce(){
        int n = 10;
        for(int i = 0; i < n; i++)
            reservationService.addUser(nickname, password);
        Assertions.assertThat(reservationService.getUsers()).hasSize(1);
    }

    @Test
    public void addUser_UniqueUsers_Added(){
        int n = 10;
        for(int i = 0; i < n; i++)
            reservationService.addUser(nickname + i, password);
        Assertions.assertThat(reservationService.getUsers()).hasSize(n);
    }

    @Test
    public void logIn_UserWithSameNicknameAndPasswordExistsOnTheList_ProperUser(){
        reservationService.addUser(nickname, password);
        User u = reservationService.logIn(nickname, password);
        Assertions.assertThat(u).hasFieldOrPropertyWithValue("nickname", nickname)
                .hasFieldOrPropertyWithValue("password", password);
    }

    @Test
    public void logIn_UserDoesNotExists_Null(){
        User u = reservationService.logIn(nickname, password);
        assertThat(u, is(nullValue()));
    }

    @Test
    public void logIn_UserExisButPasswordIsNotValid_Null(){
        reservationService.addUser(nickname, password);
        User u = reservationService.logIn(nickname, password + "different");
        assertNull(u);
    }

    @Test
    public void usersToString_ContainsNicknames_ReturnsProperString(){
        reservationService.addUser(nickname, password);
        String secondNickname = nickname + "1";
        reservationService.addUser(secondNickname, password);
        Assertions.assertThat(reservationService.usersToString()).contains(nickname).contains(secondNickname);
    }

    @Test
    public void usersToString_EmptyUserList_ReturnsEmptyString(){
        assertThat(reservationService.usersToString(), isEmptyOrNullString());
    }

    @Test
    public void restaurantsToString_ContainsNamesAndHours_ReturnsProperString(){
        ArrayList<Restaurant> restaurants = new ArrayList<Restaurant>();
        restaurants.add(restaurant);
        restaurants.add(new Restaurant(name + "second", description,
                new boolean[]{false, true, true, true, true, true, false},
                8,20,10,23));
        reservationService.loadRestaurants(restaurants);
        Assertions.assertThat(reservationService.restaurantsToString()).contains(name)
                    .containsSequence(openingHour + "-" + closingHour)
                    .containsSequence(breakStartingHour + "-" + breakEndingHour)
                    .contains(name + "second").containsSequence("8-20").containsSequence("10-23");
    }

    @Test
    public void restaurantsToString_EmptyRestaurantsList_ReturnsEmptyString(){
        assertThat(reservationService.restaurantsToString(), isEmptyOrNullString());
    }

    @Test
    public void bookRestaurant_CorrectlyBookRestaurantWithAllVaidData_AddsBookedRestaurantToUser(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        User u = reservationService.logIn(name, password);

        reservationService.bookRestaurant(u, restaurantId, date, bookHour);
        assertThat(u.getBookedRestaurants().size(), is(1));
    }

    @Test (expected = ArrayIndexOutOfBoundsException.class)
    public void bookRestaurant_RestaurantDoesNotExist_ThrowsArrayIndexOutOfBoundsException(){
        reservationService.loadRestaurants(db);
        user = new User(name, password);
        reservationService.addUser(name, password);

        assertThat(reservationService.bookRestaurant(user, 1000, date, bookHour), is(false));
    }

    @Test (expected = SecurityException.class)
    public void bookRestaurant_UserDoesNotExistOnTheList_ThrowsSecurityException(){
        reservationService.loadRestaurants(db);
        user = new User(name, password);

        reservationService.bookRestaurant(user, restaurantId, date, bookHour);
    }

    @Test (expected = SecurityException.class)
    public void bookRestaurant_UserProvidedWrontPassword_ThrowsSecurityException(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = new User(name, password + "wrong");

        reservationService.bookRestaurant(user, restaurantId, date, bookHour);
    }

    @Test (expected = SecurityException.class)
    public void bookRestaurant_UserExistsButPasswordIsWrong_ThrowsSecurityException(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = reservationService.logIn(name, password + "other");

        reservationService.bookRestaurant(user, restaurantId, date, bookHour);
    }

    @Test (expected = ArrayIndexOutOfBoundsException.class)
    public void bookRestaurant_RestaurantWithGivenIdDoesNotExist_ThrowsArrayIndexOutOfBoundsException(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = reservationService.logIn(name, password);

        reservationService.bookRestaurant(user, reservationService.getRestaurants().size(), date, bookHour);
    }

    @Test (expected = IllegalArgumentException.class)
    public void bookRestaurant_ProvidedHourIsNotBetween0And23_ThrowsIllegalArgumentException(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = reservationService.logIn(name, password);

        reservationService.bookRestaurant(user, restaurantId, date, 24);
        reservationService.bookRestaurant(user, restaurantId, date, -1);
    }

    @Test (expected = IllegalArgumentException.class)
    public void bookRestaurant_StringDateIsNotInProperFormat_ThrowsIllegalArgumentException() {
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = reservationService.logIn(name, password);

        reservationService.bookRestaurant(user, restaurantId, "34124141", bookHour);
    }

    @Test
    public void bookRestaurant_RestaurantIsClosedOnThatDate_False(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = reservationService.logIn(name, password);

        assertThat(reservationService.bookRestaurant(user, restaurantId, closedDate, bookHour), is(false));
    }

    @Test
    public void bookRestaurant_RestaurantIsClosedOnGivenHour_False(){
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = reservationService.logIn(name, password);

        assertThat(reservationService.bookRestaurant(user, restaurantId, date, closingHour), is(false));
    }

    @Test
    public void bookRestaurant_RestaurantHasABreakOnGivenHour_False() {
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        user = new User(name, password);

        assertThat(reservationService.bookRestaurant(user, restaurantId, date, breakStartingHour), is(false));
    }

    @Test
    public void bookRestaurant_SameUserAlreadyBookedThatRestaurant_False() {
        reservationService.loadRestaurants(db);
        User u = new User(name, password);
        reservationService.addUser(name, password);

        reservationService.bookRestaurant(u, restaurantId, date, bookHour);
        assertThat(reservationService.bookRestaurant(u, restaurantId, date, bookHour), is(false));
    }

    @Test
    public void bookRestaurant_OtherUserAlreadyBookedThatRestaurant_False() {
        reservationService.loadRestaurants(db);
        reservationService.addUser(name, password);
        reservationService.addUser(name + "other", password);
        User u = reservationService.logIn(name, password);
        User other = new User(name + "other", password);

        reservationService.bookRestaurant(u, restaurantId, date, bookHour);
        assertThat(reservationService.bookRestaurant(other, restaurantId, date, bookHour), is(false));
    }

    @Test
    public void generateCode_GivenProperData_SetsUpCorrectString(){
        String dateNoDots = date.replaceAll("\\.", "");
        int userId = 10;
        int restaurantId = 52;
        String actual = reservationService.generateCode(userId, restaurantId, date, bookHour);
        Assertions.assertThat(actual).startsWith(userId + "").endsWith(dateNoDots).contains(restaurantId + "")
                .contains(bookHour + "");
    }


    @After
    public void tearDown(){
        nickname = null;
        password = null;
        name = null;
        description = null;
        days = null;
        openingHour = 0;
        closingHour = 0;
        breakStartingHour = 0;
        breakEndingHour = 0;
        user = null;
        restaurant = null;
        reservationService = null;
        restaurantId = 0;
        date = null;
        closedDate = null;
        dateObj = null;
    }
}
