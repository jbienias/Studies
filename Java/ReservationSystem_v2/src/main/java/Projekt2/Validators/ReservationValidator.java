package Projekt2.Validators;

import Projekt2.Models.Restaurant;
import java.util.Calendar;
import java.util.Date;

//Zadanie 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class ReservationValidator {
    public static boolean validate(Restaurant restaurant, Date date, int hour , String reservationId) {
        if (hour < 0 || hour > 23 || date == null || reservationId == null ||
                reservationId.isEmpty() || restaurant == null)
            return false;
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        if (!restaurant.getDays()[calendar.get(Calendar.DAY_OF_WEEK) - 1])
            return false;
        if (restaurant.getOpeningHour() > hour || restaurant.getClosingHour() <= hour)
            return false;
        if (restaurant.getBreakStartingHour() <= hour && restaurant.getBreakEndingHour() > hour)
            return false;
        return true;
    }
}
