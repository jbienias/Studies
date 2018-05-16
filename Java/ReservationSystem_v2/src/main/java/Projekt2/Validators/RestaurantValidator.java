package Projekt2.Validators;

//Zadanie 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

import Projekt2.Models.Restaurant;

public class RestaurantValidator {
    public static boolean validate(Restaurant r){
        if(r.getName() == null || r.getName().isEmpty() || r.getDescription() == null
                || r.getDescription().isEmpty() || r.getDays() == null)
            return false;
        if(r.getDays().length != 7)
            return false;
        if(r.getOpeningHour() < 0 || r.getOpeningHour() > 24 || r.getClosingHour() < 0 || r.getClosingHour() > 24)
            return false;
        if(r.getOpeningHour() >= r.getClosingHour())
            return false;
        if(r.getBreakStartingHour() < 0 || r.getBreakStartingHour() > 24 || r.getBreakEndingHour() < 0 || r.getBreakEndingHour() > 24)
            return false;
        if(r.getBreakStartingHour() >= r.getBreakEndingHour())
            return false;
        if(r.getOpeningHour() > r.getBreakStartingHour() || r.getClosingHour() < r.getBreakEndingHour())
            return false;
        return true;
    }
}
