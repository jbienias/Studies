package Projekt2.Repositories;

import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;
import Projekt2.Repositories.Abstract.IReservationRepository;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

import Projekt2.Validators.ReservationValidator;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class FakeReservationRepository implements IReservationRepository {

    private int idCounter = 0;
    private List<Reservation> reservations = new ArrayList<Reservation>();

    public List getReservations() {
        return reservations;
    }

    public List getReservationsByUser(int id) {
        return reservations.stream().filter(x -> x.getUserId() == id).collect(Collectors.toList());
    }
    public List getReservationsByRestaurant(int id) {
        return reservations.stream().filter(x -> x.getRestaurantId() == id).collect(Collectors.toList());
    }

    public boolean reservationExists(int id) {
        return reservations.stream().anyMatch(x -> x.getId() == id);
    }

    public boolean reservationExists(Reservation reservation) {
        return reservations.stream().anyMatch(x -> x.getId() == reservation.getId()
                && x.getDate().equals(reservation.getDate()) && x.getHour() == reservation.getHour());
    }


    public void addReservation(Reservation reservation) {
        reservations.add(new Reservation(idCounter, reservation));
        idCounter++;
    }

    public void deleteReservation(int id) {
        reservations.removeIf(x -> x.getId() == id);
    }

    public void deleteUserReservations(int userId) {
        reservations.removeIf(x -> x.getUserId() == userId);
    }

    public void deleteRestaurantReservations(int restaurantId) {
        reservations.removeIf(x -> x.getRestaurantId() == restaurantId);
    }

    public boolean validateReservation(Restaurant restaurant, Date date, int hour , String reservationId) {
        return ReservationValidator.validate(restaurant, date, hour , reservationId);
    }
}
