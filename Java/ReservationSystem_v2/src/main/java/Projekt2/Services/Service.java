package Projekt2.Services;

import Projekt2.Helpers.CodeGenerator;
import Projekt2.Helpers.DateHelper;
import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;
import Projekt2.Models.User;
import Projekt2.Repositories.Abstract.IReservationRepository;
import Projekt2.Repositories.Abstract.IRestaurantRepository;
import Projekt2.Repositories.Abstract.IUserRepository;
import Projekt2.Repositories.FakeReservationRepository;
import Projekt2.Repositories.FakeRestaurantRepository;
import Projekt2.Repositories.FakeUserRepository;
import Projekt2.Validators.ReservationValidator;
import Projekt2.Validators.RestaurantValidator;
import Projekt2.Validators.UserValidator;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class Service {
    public String restaurantsFileLocation = "src/main/resources/db.txt";
    public String confirmationsOutputLocation = "src/main/resources";
    public boolean saveConfirmations = false;

    private IUserRepository userRepository;
    private IRestaurantRepository restaurantRepository;
    private IReservationRepository reservationRepository;

    public Service(IUserRepository userRepository, IRestaurantRepository restaurantRepository,
                   IReservationRepository reservationRepository){
        this.userRepository = userRepository;
        this.restaurantRepository = restaurantRepository;
        this.reservationRepository = reservationRepository;
    }

    public User logIn(String nickname, String password) {
        return userRepository.getUser(nickname, password);
    }

    public boolean addUser(String username, String password) {
        if(userRepository.userExists(username))
            return false;
        User u = new User(username, password);
        if(!userRepository.validateUser(u))
            throw new IllegalArgumentException("User is not valid!");
        userRepository.addUser(u);
        return true;
    }

    public boolean deleteUser(int id) {
        if(!userRepository.userExists(id))
            throw new IllegalArgumentException("User with ID " + id + " does not exist!");
        userRepository.deleteUser(id);
        reservationRepository.deleteUserReservations(id); // recursive delete
        return true;
    }

    public boolean updateUser(int id, String username, String password) {
        if(!userRepository.userExists(id))
            throw new IllegalArgumentException("User with ID " + id + " does not exist!");
        User updated = new User(username, password);
        if(!userRepository.validateUser(updated))
            throw new IllegalArgumentException("User is not valid!");
        User uFromId = userRepository.getUser(id);
        User uFromUsername = userRepository.getUser(username);
        if(uFromUsername == null || uFromId == uFromUsername) {
            userRepository.updateUser(id, updated);
            return true;
        }
        else
            throw new IllegalArgumentException("User with that username already exists!");
    }

    public List<Reservation> getReservationsByUser(int id) {
        return reservationRepository.getReservationsByUser(id);
    }

    public boolean addRestaurant(String name, String description, boolean days[], int openingHour,
                                 int closingHour, int breakStartingHour, int breakEndingHour) {
        if(restaurantRepository.restaurantExists(name))
            return false;
        Restaurant r = new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour);
        if(!restaurantRepository.validateRestaurant(r))
            throw new IllegalArgumentException("Restaurant is not valid!");
        restaurantRepository.addRestaurant(r);
        return true;
    }

    public boolean deleteRestaurant(int id) {
        if(!restaurantRepository.restaurantExists(id))
            throw new IllegalArgumentException("Restaurant with ID " + id + " does not exist!");
        restaurantRepository.deleteRestaurant(id);
        reservationRepository.deleteRestaurantReservations(id); // recursive delete
        return true;
    }

    public boolean updateRestaurant(int id, String name, String description, boolean days[], int openingHour,
                                    int closingHour, int breakStartingHour, int breakEndingHour) {
        if(!restaurantRepository.restaurantExists(id))
            throw new IllegalArgumentException("Restaurant with ID " + id + " does not exist!");
        Restaurant updated = new Restaurant(name,description, days, openingHour,closingHour, breakStartingHour, breakEndingHour);
        if(!restaurantRepository.validateRestaurant(updated))
            throw new IllegalArgumentException("Restaurant is not valid!");
        Restaurant rFromId = restaurantRepository.getRestaurant(id);
        Restaurant rFromName = restaurantRepository.getRestaurant(name);
        if(rFromName == null || rFromId == rFromName) {
            restaurantRepository.updateRestaurant(id, updated);
            return true;
        }
        else
            throw new IllegalArgumentException("Restaurant with that username already exists!");
    }

    public List<Reservation> getReservationsByRestaurant(int id) {
        return reservationRepository.getReservationsByRestaurant(id);
    }

    public boolean bookRestaurant(int userId, int restaurantId, String date, int hour) { //addUserReservation
        if(!userRepository.userExists(userId))
            throw new SecurityException("User with ID: " + userId + " does not exist!");
        if(!restaurantRepository.restaurantExists(restaurantId))
            throw new SecurityException("Restaurant with ID: " + restaurantId + " does not exist!");
        Restaurant restaurant = restaurantRepository.getRestaurant(restaurantId);
        String generatedCode = CodeGenerator.generate(userId, restaurantId, date, hour);
        Date realDate = DateHelper.parseDate(date);
        if(!reservationRepository.validateReservation(restaurant, realDate, hour, generatedCode))
            return false;
        Reservation reservation = new Reservation(userId, restaurantId, realDate, hour, generatedCode);
        if(reservationRepository.reservationExists(reservation))
            return false;

        reservationRepository.addReservation(new Reservation(userId, restaurantId, realDate, hour, generatedCode));
        return true;
    }

    public boolean deleteReservation(int id){
        if(!reservationRepository.reservationExists(id))
            throw new IllegalArgumentException("Reservation with ID " + id + " does not exist!");
        reservationRepository.deleteReservation(id);
        return true;
    }

    public List<User> getUsers() {
        return userRepository.getUsers();
    }

    public List<Restaurant> getRestaurants(){
        return restaurantRepository.getRestaurants();
    }

    public List<Reservation> getReservations(){
        return reservationRepository.getReservations();
    }

    public String usersToString() {
        List<User> users = userRepository.getUsers();
        String tmp = "";
        for(int i = 0; i < users.size(); i++) {
            tmp += users.get(i).toString() + "\n";
        }
        return tmp;
    }

    public String restaurantsToString() {
        List<Restaurant> restaurants = restaurantRepository.getRestaurants();
        String tmp = "";
        for(int i = 0; i < restaurants.size(); i++) {
            tmp += restaurants.get(i).toString() + "\n";
        }
        return tmp;
    }

    public String reservationsToString() {
        List<Reservation> reservations = reservationRepository.getReservations();
        String tmp = "";
        for(int i = 0; i < reservations.size(); i++) {
            tmp += reservations.get(i).toString() + "\n";
        }
        return tmp;
    }

    public int loadRestaurantsFromFile() throws FileNotFoundException {
        Scanner scanner = new Scanner(new File(restaurantsFileLocation));
        ArrayList<Restaurant> restaurantsFromFile = new ArrayList<Restaurant>();
        int errors = 0;
        String line[], name, description;
        int openingHour, closingHour, breakStartingHour, breakEndingHour;
        while(scanner.hasNextLine()) {
            try {
                line = scanner.nextLine().split(";");
                name = line[0];
                String tmp[] = line[2].split("-");
                openingHour = Integer.parseInt(tmp[0]);
                closingHour = Integer.parseInt(tmp[1]);
                tmp = line[3].split("-");
                breakStartingHour = Integer.parseInt(tmp[0]);
                breakEndingHour = Integer.parseInt(tmp[1]);
                tmp = line[1].split(" ");
                boolean days[] = new boolean[7];
                for(int i = 0; i < tmp.length; i++)
                    days[Integer.parseInt(tmp[i]) - 1] = true;
                description = line[4];
                Restaurant r = new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour);
                if(!restaurantRepository.validateRestaurant(r))
                    continue;
                restaurantRepository.addRestaurant(r);
            } catch (Exception e) {
                errors++;
                continue;
            }
        }
        scanner.close();
        return errors;
    }
}
