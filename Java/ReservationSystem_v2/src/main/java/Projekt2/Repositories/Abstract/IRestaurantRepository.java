package Projekt2.Repositories.Abstract;

import Projekt2.Models.Restaurant;
import java.util.List;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public interface IRestaurantRepository {
    List getRestaurants();
    Restaurant getRestaurant(int id);
    Restaurant getRestaurant(String name);
    boolean restaurantExists(int id);
    boolean restaurantExists(String name);
    void addRestaurant(Restaurant restaurant);
    void deleteRestaurant(int id);
    void updateRestaurant(int id, Restaurant restaurant);
    boolean validateRestaurant(Restaurant restaurant);
}
