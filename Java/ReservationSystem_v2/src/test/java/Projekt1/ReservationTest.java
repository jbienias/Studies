package Projekt1;

import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;
import Projekt2.Models.User;
import Projekt2.Validators.ReservationValidator;
import org.assertj.core.api.Assertions;
import org.junit.After;
import org.junit.Before;
import org.junit.Rule;
import java.util.Date;
import org.junit.Test;
import org.junit.rules.TemporaryFolder;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.is;
import static org.junit.Assert.assertEquals;

//Projekt I Zad. 6 - system rezerwacji restauracji
//Jan Bienias 238201

public class ReservationTest {

    User user;
    Restaurant restaurant;
    Reservation br;
    String id, nickname, password;
    Date date;
    int hour;

    @Rule
    public TemporaryFolder folder = new TemporaryFolder();

    @Before
    public void setUp(){
        nickname = "Herek";
        password = "janusz100";
        restaurant = new Restaurant("SuperPizza", "Bardzo tanio tylko u nas!", new boolean[]
                {true, true, true, true, true, true, true}, 8,20,8,10);
        id = "12345";
        date = new Date();
        hour = 10;
    }

    @Test
    public void defaultConstructorNoArgsReturnsInstanceTypeOfReservation(){
        Reservation r = new Reservation();
        assertEquals(Reservation.class, r.getClass());
    }

    @Test
    public void validateCorrectDataReturnsTrue(){
        assertThat(ReservationValidator.validate(restaurant, date, hour, id), is(true));
    }


    @Test
    public void validateNullRestaurantReturnsFalse(){
        assertThat(ReservationValidator.validate( null, date, hour, id), is(false));
    }

    @Test
    public void validateNullDateReturnsFalse(){
        assertThat(ReservationValidator.validate(restaurant, null, hour, id), is(false));
    }

    @Test
    public void validateNullIdReturnsFalse(){
        assertThat(ReservationValidator.validate(restaurant, date, hour, null), is(false));
    }

    @Test
    public void validateEmptyIdReturnsFalse(){
        assertThat(ReservationValidator.validate(restaurant, date, hour, ""), is(false));
    }

    @Test
    public void toStringBookedRestaurantObjectExistsReturnsStringWithProperData(){
        br = new Reservation(0, 0, date, hour, id);
        Assertions.assertThat(br.toString()).contains(id);
    }

    @After
    public void tearDown(){
        user = null;
        restaurant = null;
    }
}
