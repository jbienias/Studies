package Projekt2.Repositories.Abstract;

import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;

import java.util.Date;
import java.util.List;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public interface IReservationRepository {
    List getReservations();
    List getReservationsByUser(int id);
    List getReservationsByRestaurant(int id);
    boolean reservationExists(int id);
    boolean reservationExists(Reservation reservation);
    void addReservation(Reservation reservation);
    void deleteReservation(int id);
    void deleteUserReservations(int userId);
    void deleteRestaurantReservations(int restaurantId);
    boolean validateReservation(Restaurant restaurant, Date date, int hour , String reservationId);
}
