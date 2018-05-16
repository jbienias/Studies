package Projekt1;

import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Date;
import java.text.SimpleDateFormat;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

public class BookedRestaurant {
    private User user;
    private Restaurant restaurant;
    private Date date;
    private int hour;
    private String id;
    private String dateFormat = "dd.MM.yyyy";

    public BookedRestaurant(User user, Restaurant restaurant, Date date, int hour, String id) {
        this.user = user;
        this.restaurant = restaurant;
        this.date = date;
        this.id = id;
        this.hour = hour;
    }

    public static boolean validate(User user, Restaurant restaurant, Date date, int hour, String id){
        if(user == null || restaurant == null || date == null || id == null || id.isEmpty())
            return false;
        return true;
    }

    public void saveToFile(String dirPath) throws FileNotFoundException {
        PrintWriter pw = new PrintWriter(dirPath + "/reservation" + id + ".txt");
        pw.println(this.toString());
        pw.close();
    }

    @Override
    public boolean equals(Object other){
        if (other == null) return false;
        if (other == this) return true;
        if (!(other instanceof BookedRestaurant))return false;
        BookedRestaurant bookedRestaurantOther = (BookedRestaurant)other;
        if(this.restaurant == bookedRestaurantOther.restaurant && this.date.equals(bookedRestaurantOther.date)
                && this.hour == bookedRestaurantOther.hour)
            return true;
        else
            return false;
    }

    @Override
    public String toString() {
        SimpleDateFormat sdf = new SimpleDateFormat(dateFormat);
        return "[" + id + "] " + restaurant.getName() + " booked by: " + user.getNickname() + " for: " + hour + " o'clock on: " + sdf.format(date);
    }
}
