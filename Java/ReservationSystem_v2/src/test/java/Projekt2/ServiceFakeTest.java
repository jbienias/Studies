package Projekt2;

import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;
import Projekt2.Models.User;
import Projekt2.Repositories.Abstract.IReservationRepository;
import Projekt2.Repositories.Abstract.IRestaurantRepository;
import Projekt2.Repositories.Abstract.IUserRepository;
import Projekt2.Repositories.FakeReservationRepository;
import Projekt2.Repositories.FakeRestaurantRepository;
import Projekt2.Repositories.FakeUserRepository;
import Projekt2.Services.Service;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;

import java.util.List;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.assertj.core.api.AssertionsForClassTypes.assertThatThrownBy;
import static org.junit.jupiter.api.Assertions.*;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class ServiceFakeTest {

    IUserRepository userRepository;
    IRestaurantRepository restaurantRepository;
    IReservationRepository reservationRepository;

    Service service;

    @BeforeEach
    public void setUp(){
        userRepository = new FakeUserRepository();
        restaurantRepository = new FakeRestaurantRepository();
        reservationRepository = new FakeReservationRepository();
        service = new Service(userRepository, restaurantRepository, reservationRepository);
    }

    //Testy jedynie pod coverage FakeUserRepository:
    @Test
    public void addUserUserIsValidUserIsAddedToTheList(){
        service.addUser("Username", "Password");

        assertThat(service.getUsers().get(0).getNickname()).startsWith("Username");
    }

    @Test
    public void getUsersReturnsAllUsersFromTheList(){
        service.addUser("User1", "Password1");
        service.addUser("User2", "Password2");
        service.addUser("User3", "Password3");

        List result = service.getUsers();
        assertEquals(3, result.size());
    }

    @Test
    public void addUserUserIsInvalidThrowsInvalidArgumentException(){
        assertThatThrownBy(() -> { service.addUser(null, "");}).isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void updateUserUserExistsOnListAndNewDataIsValidUpdatesUser(){
        service.addUser("Username", "Password");
        service.updateUser(0, "Super", "Password");

        assertThat(service.getUsers().get(0).getNickname()).contains("Super");
    }

    @Test
    public void deleteUserUserExistsOnListRemovesUser(){
        service.addUser("Username", "Password");
        service.deleteUser(0);

        assertThat(service.getUsers().isEmpty()).isTrue();
    }

    @Test
    public void logInUserUserExistsAndProvidedCredentialsAreCorrect(){
        service.addUser("Username", "Password");
        User u = service.logIn("Username", "Password");

        assertThat(u).isNotNull();
    }

    @Test
    public void getUsersToStringReturnsStringWithAllUsers(){
        service.addUser("Username0", "Password");
        service.addUser("Username1", "Password");
        service.addUser("Username2", "Password");

        String result = service.usersToString();
        assertThat(result).hasLineCount(3);
    }

    //Testy jedynie pod coverage FakeRestaurantRepository:
    @Test
    public void addRestaurantRestaurantIsValidRestaurantIsAddedToList(){
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);

        assertThat(service.getRestaurants().size()).isEqualTo(1);
    }

    @Test
    public void addRestaurantRestaurantAlreadyExistsOnTheListRestaurantIsNotAdded(){
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);

        assertEquals(1, service.getRestaurants().size());
    }

    @Test
    public void getRestaurantsReturnsAllElementsFromList() {
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12, 13);
        service.addRestaurant("Name1", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12, 13);
        service.addRestaurant("Name2", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12, 13);

        List<Restaurant> result = service.getRestaurants();
        assertThat(result.size()).isEqualTo(3);
    }

    @Test
    public void updateRestaurantRestaurantExistsButGivenDataIsInvalidThrowsException(){
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        assertThrows(IllegalArgumentException.class, () -> {service.updateRestaurant(0, null, null, null, 0, 0, 0, 0);});
    }

    @Test
    public void updateRestaurantRestaurantExistsAndGivenDataIsValidEditsRestaurant(){
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
       service.updateRestaurant(0, "New Name", "Description", new boolean[]{true, true, true, true, true, true, true},
               8, 20, 12,13);

       assertThat(service.getRestaurants().get(0).getName()).isEqualTo("New Name");
    }

    @Test
    public void deleteRestaurantRestaurantExistsRestaurantIsDeletedFromList(){
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.deleteRestaurant(0);
        assertThat(service.getRestaurants().isEmpty()).isTrue();
    }

    @Test
    public void getRestaurantsToStringReturnsStringWithAllRestaurants(){
        service.addRestaurant("First", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.addRestaurant("Second", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);


        String result = service.restaurantsToString();
        assertThat(result).hasLineCount(2);
    }

    //Testy Rezerwacji //nówki nieśmigane
    @Test
    public void addReservationBothUserAndRestaurantExistsAndGivenDataIsValidReturnsTrue(){
        service.addUser("Username", "Password");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        boolean result = service.bookRestaurant(0, 0, "28.04.2018", 14);

        assertTrue(result);
    }

    @Test
    public void addReservationUserIdDoesNotExistThrowsSecurityException(){
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);

        assertThrows(SecurityException.class, () -> { service.bookRestaurant(0, 0, "28.04.2018", 14);});
    }

    @Test
    public void addReservationRestaurantIdDoesNotExistsThrowsSeurityException(){
        service.addUser("Username", "Password");

        assertThrows(SecurityException.class, () -> { service.bookRestaurant(0, 0, "28.04.2018", 14);});
    }

    @Test
    public void addReservationUserAndRestaurantExistsButGivenDataIsNotValidReturnsFalse(){
        service.addUser("Username", "Password");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);

        boolean result = service.bookRestaurant(0, 0, "nope", 100);
        assertFalse(result);
    }

    @Test
    public void addReservationSameReservationAlreadyExistsReturnsFalse(){
        service.addUser("Username", "Password");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.bookRestaurant(0, 0, "28.04.2018", 14);
        boolean result = service.bookRestaurant(0, 0, "28.04.2018", 14);
        assertFalse(result);
    }

    @Test
    public void deleteReservationReservationExistsReturnsTrue(){
        service.addUser("Username", "Password");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.bookRestaurant(0, 0, "28.04.2018", 14);

        boolean result = service.deleteReservation(0);
        assertTrue(result);
    }

    @Test
    public void deleteReservationReservationDoesNotExistsThrowsIllegalArgumentException(){
        assertThrows(IllegalArgumentException.class, () -> { service.deleteReservation(0);});
    }

    @Test
    public void getReservationsReturnsAllReservationsFromTheList(){
        service.addUser("Username1", "Password1");
        service.addUser("Username2", "Password2");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.bookRestaurant(0, 0, "28.04.2018", 14);
        service.bookRestaurant(0, 0, "29.04.2018", 14);
        service.bookRestaurant(1, 0, "30.04.2018", 15);

        List<Reservation> result = service.getReservations();
        assertThat(result.size()).isEqualTo(3);
    }

    @Test
    public void getReservationsByRestaurantIdReturnsFilteredListWithGivenRestaurantId(){
        service.addUser("Username", "Password");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.addRestaurant("SecondName", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.bookRestaurant(0, 0, "28.04.2018", 14);
        service.bookRestaurant(0, 1, "30.04.2018", 15);
        service.bookRestaurant(0, 0, "29.04.2018", 15);

        List<Reservation> result = service.getReservationsByRestaurant(0);
        assertThat(result.size()).isPositive().isEqualTo(2);
    }

    @Test
    public void getReservationsByRestaurantButItIsNotInAnyReservationOrDoesNotExistsReturnsEmptyList(){
        List<Reservation> result = service.getReservationsByRestaurant(777);
        assertTrue(result.isEmpty());
    }

    @Test
    public void getReservationsByUserIdReturnsFilteredListWithGivenUserId(){
        service.addUser("Username", "Password");
        service.addUser("Other", "Pass");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.bookRestaurant(0, 0, "28.04.2018", 14);
        service.bookRestaurant(0, 0, "29.04.2018", 15);
        service.bookRestaurant(1, 0, "28.04.2018", 13);
        service.bookRestaurant(0, 0, "29.04.2018", 16);

        List<Reservation> result = service.getReservationsByUser(0);
        assertThat(result.size()).isPositive().isGreaterThan(2).isLessThan(4);
    }

    @Test
    public void getReservationsUserIdIsNotInAnyReservationOrDoesNotExistsReturnsEmptyList(){
        List<Reservation> result = service.getReservationsByRestaurant(777);
        assertThat(result.size()).isBetween(-1, 1);
    }

    @Test
    public void getReservationsToStringReturnsStringWithAllReservations(){
        service.addUser("Username", "Password");
        service.addUser("Other", "Pass");
        service.addRestaurant("Name", "Description", new boolean[]{true, true, true, true, true, true, true},
                8, 20, 12,13);
        service.bookRestaurant(0, 0, "28.04.2018", 14);
        service.bookRestaurant(1, 0, "29.04.2018", 13);

        String result = service.reservationsToString();
        assertThat(result).hasLineCount(2);
    }


    @AfterEach
    public void tearDown(){
        userRepository = null;
        restaurantRepository = null;
        reservationRepository = null;
        service = null;
    }
}
