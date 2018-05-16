package Projekt2.Models;

import lombok.Getter;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

@Getter
public class Restaurant {
    private int id;
    private String name;
    private String description;
    private boolean days[];
    private int openingHour;
    private int closingHour;
    private int breakStartingHour;
    private int breakEndingHour;
    private String daysString[] = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};

    public Restaurant(){

    }

    public Restaurant(String name, String description, boolean days[], int openingHour,
                      int closingHour, int breakStartingHour, int breakEndingHour) {
        this.name = name;
        this.description = description;
        this.days = days;
        this.openingHour = openingHour;
        this.closingHour = closingHour;
        this.breakStartingHour = breakStartingHour;
        this.breakEndingHour = breakEndingHour;
    }

    public Restaurant(int id, Restaurant restaurant){ //DB MODEL
        this.name = restaurant.getName();
        this.description = restaurant.getDescription();
        this.days = restaurant.getDays();
        this.openingHour = restaurant.getOpeningHour();
        this.closingHour = restaurant.getClosingHour();
        this.breakStartingHour = restaurant.getBreakStartingHour();
        this.breakEndingHour = restaurant.getBreakEndingHour();
        this.id = id;
    }

    @Override
    public String toString() {
        String daysArray = "";
        for(int i = 0; i < days.length; i++)
            if(days[i])
                daysArray += "(" + daysString[i] + ")";
        return "ID: " + id + " | NAME: " + name + " | DESCRIPTION: " + description + " | OPENING HOURS: [" + openingHour + "-" + closingHour
                + "] | UNBOOKABLE HOURS: [" + breakStartingHour + "-" + breakEndingHour + "] | OPENING DAYS: {" + daysArray + "}";
    }
}
