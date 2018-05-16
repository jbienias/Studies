package Projekt1;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

public class Restaurant {
    private String name;
    private String description;
    private boolean days[];
    private int openingHour;
    private int closingHour;
    private int breakStartingHour;
    private int breakEndingHour;
    private String daysString[] = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};

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

    public static boolean validate(String name, String description, boolean days[], int openingHour,
                                   int closingHour, int breakStartingHour, int breakEndingHour){
        if(name == null || name.isEmpty() || description == null || description.isEmpty() || days == null)
            return false;
        if(days.length != 7)
            return false;
        if(openingHour < 0 || openingHour > 24 || closingHour < 0 || closingHour > 24)
            return false;
        if(openingHour >= closingHour)
            return false;
        if(breakStartingHour < 0 || breakStartingHour > 24 || breakEndingHour < 0 || breakEndingHour > 24)
            return false;
        if(breakStartingHour >= breakEndingHour)
            return false;
        if(openingHour > breakStartingHour || closingHour < breakEndingHour)
            return false;
        return true;
    }

    public String getName() {
        return name;
    }

    public String getDescription() {
        return description;
    }

    public boolean[] getDays() {
        return days;
    }

    public int getOpeningHour() {
        return openingHour;
    }

    public int getClosingHour() {
        return closingHour;
    }

    public int getBreakStartingHour() {
        return breakStartingHour;
    }

    public int getBreakEndingHour() {
        return breakEndingHour;
    }

    @Override
    public String toString() {
        String daysArray = "";
        for(int i = 0; i < days.length; i++)
            if(days[i])
                daysArray += "(" + daysString[i] + ")";
        return "NAME: " + name + " | DESCRIPTION: " + description + " | OPENING HOURS: [" + openingHour + "-" + closingHour
                + "] | UNBOOKABLE HOURS: [" + breakStartingHour + "-" + breakEndingHour + "] | OPENING DAYS: {" + daysArray + "}";
    }
}
