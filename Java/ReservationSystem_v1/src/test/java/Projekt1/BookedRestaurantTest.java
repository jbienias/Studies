package Projekt1;

import org.junit.After;
import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Date;
import org.assertj.core.api.Assertions;
import org.junit.rules.TemporaryFolder;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.contains;
import static org.hamcrest.Matchers.is;
import static org.junit.Assert.*;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

public class BookedRestaurantTest {

    User user;
    Restaurant restaurant;
    BookedRestaurant br;
    String id, nickname, password, directory;
    Date date;
    int hour;
    SimpleDateFormat sdf = new SimpleDateFormat("dd.MM.yyyy");

    @Rule
    public TemporaryFolder folder = new TemporaryFolder();

    @Before
    public void setUp(){
        nickname = "Herek";
        password = "janusz100";
        user = new User(nickname, password);
        restaurant = new Restaurant("SuperPizza", "Bardzo tanio tylko u nas!", new boolean[]
                {true, true, true, true, true, true, true}, 8,20,8,10);
        id = "12345";
        date = new Date();
        hour = 10;
    }

    @Test
    public void validate_CorrectData_True(){
        assertThat(BookedRestaurant.validate(user, restaurant, date, hour, id), is(true));
    }

    @Test
    public void validate_NullUser_False(){
        assertThat(BookedRestaurant.validate(null, restaurant, date, hour, id), is(false));
    }

    @Test
    public void validate_NullRestaurant_False(){
        assertThat(BookedRestaurant.validate(user, null, date, hour, id), is(false));
    }

    @Test
    public void validate_NullDate_False(){
        assertThat(BookedRestaurant.validate(user, restaurant, null, hour, id), is(false));
    }

    @Test
    public void validate_NullId_False(){
        assertThat(BookedRestaurant.validate(user, restaurant, date, hour, null), is(false));
    }

    @Test
    public void validate_EmptyId_False(){
        assertThat(BookedRestaurant.validate(user, restaurant, date, hour, ""), is(false));
    }

    @Test
    public void equals_BookedRestaurantsWithSameRestaurantDateHourAndHour_True(){
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        BookedRestaurant b = new BookedRestaurant(user, restaurant, date, hour, "2");
        assertEquals(true, br.equals(b));
    }

    @Test
    public void equals_BookedRestaurantsWithDifferentRestaurant_False(){
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        Restaurant r = new Restaurant("Super", "Bolo", new boolean[]{true,true,true,true,false,false,false},
                10,24,11,12);
        BookedRestaurant b = new BookedRestaurant(user, r, date, hour, "12341");
        assertEquals(false, br.equals(b));
    }

    @Test
    public void equals_BookedRestaurantsWithDifferentDate_False() throws ParseException{
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        Date date2 = sdf.parse("10.10.2010");
        BookedRestaurant b = new BookedRestaurant(user, restaurant, date2, hour, "12341");
        assertFalse(br.equals(b));
    }

    @Test
    public void equals_BookedRestaurantsWithDifferentUser_True(){
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        BookedRestaurant b = new BookedRestaurant(new User(nickname + "2", password), restaurant, date, hour, "2");
        assertTrue(br.equals(b));
    }

    @Test
    public void equals_BookedRestaurantsWithDifferentHour_False(){
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        BookedRestaurant b = new BookedRestaurant(user, restaurant, date, (hour + 1), "12341");
        assertFalse(br.equals(b));
    }

    @Test
    public void toString_BookedRestaurantObjectExists_ReturnsStringWithProperData(){
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        Assertions.assertThat(br.toString()).contains(user.getNickname()).contains(restaurant.getName()).contains(id);
    }

    @Test
    public void saveToFile_ContainsCorrectString_CreatesFileAndFillsWithToString() throws IOException {
        br = new BookedRestaurant(user, restaurant, date, hour, id);
        File n = folder.newFolder("testFolder");

        br.saveToFile(n.getPath());
        String actual = new BufferedReader(new FileReader(n.getPath() + "/reservation" + id + ".txt")).readLine();
        assertEquals(br.toString(), actual);
    }


    @After
    public void tearDown(){
        user = null;
        restaurant = null;
    }
}
