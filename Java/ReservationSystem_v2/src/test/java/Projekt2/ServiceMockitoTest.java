package Projekt2;

import Extensions.MockitoExtension;
import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;

import Projekt2.Repositories.Abstract.IReservationRepository;
import Projekt2.Repositories.Abstract.IRestaurantRepository;
import Projekt2.Repositories.Abstract.IUserRepository;
import Projekt2.Services.Service;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mockito;

import java.lang.reflect.Array;
import java.util.Arrays;
import java.util.List;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.assertj.core.api.AssertionsForClassTypes.assertThatThrownBy;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.doReturn;


//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

//Testy metod CRUD'owych z restauracjami (W Mockito)

@SuppressWarnings("deprecation")
@ExtendWith(MockitoExtension.class)
public class ServiceMockitoTest {

    IUserRepository userRepository;
    IRestaurantRepository restaurantRepository;
    IReservationRepository reservationRepository;

    Service service;

    @BeforeEach
    public void setUp(){
        userRepository = Mockito.mock(IUserRepository.class);
        restaurantRepository = Mockito.mock(IRestaurantRepository.class);
        reservationRepository = Mockito.mock(IReservationRepository.class);
        service = new Service(userRepository, restaurantRepository, reservationRepository);
    }

    @Test
    public void addRestaurantValidReturnsTrue(){
        Restaurant restaurant = new Restaurant();
        doReturn(false).when(restaurantRepository).restaurantExists(restaurant.getName());
        doReturn(true).when(restaurantRepository).validateRestaurant(any(Restaurant.class));
        //doNothing().when(restaurantRepository).addRestaurant(any(Restaurant.class));

        boolean result = service.addRestaurant(restaurant.getName(), restaurant.getDescription(), restaurant.getDays(), restaurant.getOpeningHour(),
                restaurant.getClosingHour(), restaurant.getBreakStartingHour(), restaurant.getBreakEndingHour());
        assertThat(result).isTrue();
    }

    @Test
    public void addRestaurantInvalidThrowsIllegalArgumentException(){
        Restaurant restaurant = new Restaurant();
        doReturn(false).when(restaurantRepository).restaurantExists(restaurant.getName());
        doReturn(false).when(restaurantRepository).validateRestaurant(any(Restaurant.class));

        assertThrows(IllegalArgumentException.class, () -> {service.addRestaurant(restaurant.getName(), restaurant.getDescription(), restaurant.getDays(), restaurant.getOpeningHour(),
                restaurant.getClosingHour(), restaurant.getBreakStartingHour(), restaurant.getBreakEndingHour());});
    }

    @Test
    public void addRestaurantRestaurantWithSameNameAlreadyExistsReturnsFalse(){
        Restaurant restaurant = new Restaurant();
        doReturn(true).when(restaurantRepository).restaurantExists(restaurant.getName());

        boolean result = service.addRestaurant(restaurant.getName(), restaurant.getDescription(), restaurant.getDays(), restaurant.getOpeningHour(),
                restaurant.getClosingHour(), restaurant.getBreakStartingHour(), restaurant.getBreakEndingHour());
        assertThat(result).isEqualTo(false);
    }

    @Test
    public void deleteRestaurantRestaurantExistsReturnsTrue(){
        doReturn(true).when(restaurantRepository).restaurantExists(0);

        boolean result = service.deleteRestaurant(0);
        assertEquals(true, result);
    }

    @Test
    public void deleteRestaurantRestaurantDoesNotExistThrowsIllegalArgumentException(){
        doReturn(false).when(restaurantRepository).restaurantExists(0);

        assertThatThrownBy(() -> { service.deleteRestaurant(0);}).isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void updateRestaurantRestaurantExistsAndProvidedDataIsValidReturnsTrue(){
        Restaurant r = new Restaurant();
        doReturn(true).when(restaurantRepository).restaurantExists(0);
        doReturn(true).when(restaurantRepository).validateRestaurant(any(Restaurant.class));
        doReturn(r).when(restaurantRepository).getRestaurant(0);
        doReturn(r).when(restaurantRepository).getRestaurant(r.getName());

        boolean result = service.updateRestaurant(0, null, null, null, 0,0,0,0);
        assertThat(result).isTrue();
    }

    @Test
    public void updateRestaurantRestaurantDoesNotExistsThrowsIllegalArgumentException(){
        doReturn(false).when(restaurantRepository).restaurantExists(0);

        assertThatThrownBy(() -> { service.updateRestaurant(0, null, null, null,
                0,0, 0, 0);}).isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void updateRestaurantRestaurantExistsButProvidedDataIsNotValidThrowsIllegalArgumentException(){
        Restaurant r = new Restaurant();
        doReturn(true).when(restaurantRepository).restaurantExists(0);
        doReturn(false).when(restaurantRepository).validateRestaurant(any(Restaurant.class));

        assertThatThrownBy(() -> { service.updateRestaurant(0, null, null, null,
                0,0, 0, 0);}).isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void updateRestaurantRestaurantWithGivenNameAlreadyExistsThrowsIllegalArgumentException(){
        Restaurant r1 = new Restaurant();
        Restaurant r2 = new Restaurant();
        doReturn(true).when(restaurantRepository).restaurantExists(0);
        doReturn(r1).when(restaurantRepository).getRestaurant(0);
        doReturn(r2).when(restaurantRepository).getRestaurant("TelePizza");

        assertThatThrownBy(() -> { service.updateRestaurant(0, "TelePizza", null, null,
                0,0, 0, 0);}).isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void getReservationsByRestaurantReservationsWithGivenRestaurantExistsReturnsListTypeWithCorrectNumberOfElements(){
        Restaurant r = new Restaurant();
        Reservation reservation1 = new Reservation();
        Reservation reservation2 = new Reservation();
        doReturn(Arrays.asList(reservation1, reservation2)).when(reservationRepository).getReservationsByRestaurant(r.getId());

        assertThat(service.getReservationsByRestaurant(r.getId()).size()).isEqualTo(2);
    }

    @Test
    public void getReservationsByRestaurantReservationsWithGivenRestaurantDoesNotExistsReturnsEmptyList(){
        doReturn(Arrays.asList()).when(reservationRepository).getReservationsByRestaurant(0);

        assertThat(service.getReservationsByRestaurant(0)).isInstanceOf(List.class);
    }

    @AfterEach
    public void tearDown(){
        userRepository = null;
        restaurantRepository = null;
        reservationRepository = null;
        service = null;
    }
}
