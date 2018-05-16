package Projekt2.Repositories;

import Projekt2.Models.Restaurant;
import Projekt2.Repositories.Abstract.IRestaurantRepository;
import Projekt2.Validators.RestaurantValidator;
import java.util.ArrayList;
import java.util.List;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class FakeRestaurantRepository implements IRestaurantRepository {

    private int idCounter = 0;
    private List<Restaurant> restaurants = new ArrayList<Restaurant>();

    public List getRestaurants() {
        return restaurants;
    }

    public Restaurant getRestaurant(int id) {
        return restaurants.stream().filter(x -> x.getId() == id).findFirst().orElse(null);
    }

    public Restaurant getRestaurant(String name) {
        return restaurants.stream().filter(x -> x.getName().equals(name)).findFirst().orElse(null);
    }

    public boolean restaurantExists(int id) {
        return restaurants.stream().anyMatch(x -> x.getId() == id);
    }

    public boolean restaurantExists(String name) {
        return restaurants.stream().anyMatch(x -> x.getName().equals(name));
    }

    public void addRestaurant(Restaurant restaurant) {
        restaurants.add(new Restaurant(idCounter, restaurant));
        idCounter++;
    }

    public void deleteRestaurant(int id) {
        restaurants.removeIf(x -> x.getId() == id);
    }

    public void updateRestaurant(int id, Restaurant restaurant) {
        Restaurant u = restaurants.stream().filter(x -> x.getId() == id).findFirst().orElse(null);
        if(u != null){
            restaurants.set(restaurants.indexOf(u), new Restaurant(id, restaurant));
        }
    }

    public boolean validateRestaurant(Restaurant restaurant) {
        return RestaurantValidator.validate(restaurant);
    }
}
