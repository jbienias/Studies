package Projekt2;

import Extensions.EasyMockExtension;
import Projekt2.Models.User;
import Projekt2.Repositories.Abstract.IReservationRepository;
import Projekt2.Repositories.Abstract.IRestaurantRepository;
import Projekt2.Repositories.Abstract.IUserRepository;
import Projekt2.Services.Service;
import org.easymock.EasyMock;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.extension.ExtendWith;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.easymock.EasyMock.anyObject;
import static org.easymock.EasyMock.expect;
import static org.easymock.EasyMock.replay;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;
import static org.junit.jupiter.api.Assertions.assertEquals;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

//Testy metod CRUD'owych z użytkownikami (w EasyMock)

@ExtendWith(EasyMockExtension.class)
public class ServiceEasyMockTest {

    IUserRepository userRepository;
    IRestaurantRepository restaurantRepository;
    IReservationRepository reservationRepository;

    Service service;

    @BeforeEach
    public void setUp(){
        userRepository = EasyMock.createNiceMock(IUserRepository.class);
        restaurantRepository = EasyMock.createNiceMock(IRestaurantRepository.class);
        reservationRepository = EasyMock.createNiceMock(IReservationRepository.class);
        service = new Service(userRepository, restaurantRepository, reservationRepository);
    }

    @Test
    public void addUserValidReturnsTrue() {
        User user = new User();
        expect(userRepository.validateUser(anyObject(User.class))).andReturn(true);
        expect(userRepository.userExists(null)).andReturn(false);
        replay(userRepository);

        boolean result = service.addUser(user.getNickname(), user.getPassword());
        assertTrue(result);
    }

    @Test
    public void addUserInvalidThrowsInvalidArgumentException(){
        User user = new User();
        expect(userRepository.validateUser(anyObject(User.class))).andReturn(false);
        replay(userRepository);

        assertThrows(IllegalArgumentException.class, () -> { service.addUser(user.getNickname(), user.getPassword());});
    }

    @Test
    public void addUserSimilarUserExistsReturnsFalse(){
        User user = new User();
        expect(userRepository.userExists(user.getNickname())).andReturn(true);
        replay(userRepository);

        boolean result = service.addUser(user.getNickname(), user.getPassword());
        assertThat(result).isFalse();
    }

    @Test
    public void deleteUserUserExistsReturnsTrue(){
        expect(userRepository.userExists(0)).andReturn(true);
        replay(userRepository);

        boolean result = service.deleteUser(0);
        assertThat(result).isTrue();
    }

    @Test
    public void deleteUserThatNotExistsThrowsIllegalArgumentException(){
        expect(userRepository.userExists(0)).andReturn(false);
        replay(userRepository);

        assertThrows(IllegalArgumentException.class, ()->{service.deleteUser(0);});
    }

    @Test
    public void updateUserUserExistsAndProvidedDataWasCorrectReturnsTrue(){
        User u = new User();
        expect(userRepository.userExists(0)).andReturn(true);
        expect(userRepository.validateUser(anyObject(User.class))).andReturn(true);
        expect(userRepository.getUser(0)).andReturn(u);
        expect(userRepository.getUser(u.getNickname())).andReturn(u);
        replay(userRepository);

        boolean result = service.updateUser(0, null, null);
        assertThat(result).isTrue();
    }

    @Test
    public void updateUserUserDoesNotExistThrowsIllegalArgumentException(){ ;
        expect(userRepository.userExists(0)).andReturn(false);
        replay(userRepository);

        assertThrows(IllegalArgumentException.class, () -> { service.updateUser(0 , null, null);});
    }

    @Test
    public void updateUserUserExistsButNewDataIsIncorrectThrowsIllegalArgumentException() {
        User u = new User();
        expect(userRepository.userExists(0)).andReturn(true);
        expect(userRepository.validateUser(anyObject(User.class))).andReturn(false);
        replay(userRepository);

        assertThrows(IllegalArgumentException.class, () -> {service.updateUser(0, null, null);});
    }

    @Test
    public void updateUserUserExistsButUserWithGivenDataAlreadyExistsThrowsIllegalArgumentException(){
        User u1 = new User();
        User u2 = new User();
        expect(userRepository.userExists(0)).andReturn(true);
        expect(userRepository.validateUser(anyObject(User.class))).andReturn(true);
        expect(userRepository.getUser(0)).andReturn(u1);
        expect(userRepository.getUser("Stefan")).andReturn(u2);
        replay(userRepository);

        assertThrows(IllegalArgumentException.class, () -> { service.updateUser(0, "Stefan", "Einstein");});
    }

    @Test
    public void logInUserUserExistsReturnsUser(){
        User u = new User();
        expect(userRepository.getUser(u.getNickname(), u.getPassword())).andReturn(u);
        replay(userRepository);

        User result = service.logIn(u.getNickname(), u.getPassword());
        assertEquals(u, result);
    }

    @Test
    public void logInUserUserDoesNotExistReturnsNull(){
        expect(userRepository.getUser("Przemek", "Kolendra")).andReturn(null);
        replay(userRepository);

        User result = service.logIn("Przemek", "Kolendra");
        assertEquals(null, result);
    }

    @AfterEach
    public void tearDown(){
        userRepository = null;
        restaurantRepository = null;
        reservationRepository = null;
        service = null;
    }
}
