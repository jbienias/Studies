package Projekt1;

import java.util.ArrayList;
import java.util.Hashtable;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

public class User {
    private String nickname;
    private String password;
    private Hashtable<String, BookedRestaurant> bookedRestaurants;

    public User(String nickname, String password) {
        this.nickname = nickname;
        this.password = password;
        bookedRestaurants = new Hashtable<String, BookedRestaurant>();
    }

    public static boolean validate(String nickname, String password){
        if(nickname == null || nickname.isEmpty() || password == null || password.isEmpty())
            return false;
        return true;
    }

    public String getNickname(){
        return nickname;
    }

    public String getPassword(){
        return password;
    }

    public Hashtable<String, BookedRestaurant> getBookedRestaurants() {
        return bookedRestaurants;
    }

    public ArrayList<BookedRestaurant> getListOfReservedRestaurants() {
        return new ArrayList<BookedRestaurant>(bookedRestaurants.values());
    }

    @Override
    public boolean equals(Object other) {
        if (other == null) return false;
        if (other == this) return true;
        if (!(other instanceof User)) return false;
        User userOther = (User) other;
        if (this.nickname.equals(userOther.nickname))
            return true;
        else
            return false;
    }

    @Override
    public String toString() {
        return "'" + nickname + "'";
    }
}
