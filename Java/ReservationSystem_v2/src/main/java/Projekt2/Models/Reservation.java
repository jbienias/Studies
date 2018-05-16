package Projekt2.Models;

import lombok.Getter;
import lombok.ToString;
import java.util.Date;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

@Getter
@ToString
public class  Reservation {
    private int id;
    private int userId;
    private int restaurantId;
    private Date date;
    private int hour;
    private String reservationId;

    public Reservation(){

    }

    public Reservation(int userId, int restaurantId, Date date, int hour, String reservationId) {
        this.userId = userId;
        this.restaurantId = restaurantId;
        this.date = date;
        this.reservationId = reservationId;
        this.hour = hour;
    }

    public Reservation(int id, Reservation reservation) { //DB MODEL
        this.userId = reservation.getUserId();
        this.restaurantId = reservation.getRestaurantId();
        this.date = reservation.getDate();
        this.hour = reservation.getHour();
        this.reservationId = reservation.getReservationId();
        this.id = id;
    }
}
