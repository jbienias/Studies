package Projekt1;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Scanner;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

public class ReservationService {
    public String dbLocation = "src/main/resources/db.txt";
    public String confirmationsOutputLocation = "src/main/resources";
    public boolean saveConfirmations = false;
    private ArrayList<User> users;
    private ArrayList<Restaurant> restaurants;
    private String dateFormat = "dd.MM.yyyy";

    public ReservationService(){
        users = new ArrayList<User>();
        restaurants = new ArrayList<Restaurant>();
    }

    public void loadRestaurants(ArrayList<Restaurant> restaurants) {
        if(restaurants == null || restaurants.isEmpty())
            throw new NullPointerException("ArrayList with restaurants is empty or null!");
        this.restaurants = restaurants;
    }

    public int loadRestaurantsFromFile() throws FileNotFoundException {
        Scanner scanner = new Scanner(new File(dbLocation));
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
                if(!Restaurant.validate(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour))
                    continue;
                restaurantsFromFile.add(new Restaurant(name, description, days, openingHour, closingHour, breakStartingHour, breakEndingHour));
            } catch (Exception e) {
                errors++;
                continue;
            }
        }
        scanner.close();
        loadRestaurants(restaurantsFromFile);
        return errors;
    }

    public boolean addUser(String username, String password) {
        if(!User.validate(username, password))
            throw new IllegalArgumentException("User data is not valid!");
        User user = new User(username, password);
        if(users.contains(user))
            return false;
        else {
            users.add(user);
            return true;
        }
    }

    public User logIn(String nickname, String password) {
        return users.stream().filter(user -> user.getNickname().equals(nickname) && user.getPassword().equals(password)).findFirst().orElse(null);
    }

    public boolean bookRestaurant(User user, int restaurantId, String date, int hour) {
        user = getRegisteredUser(user);
        if(user == null)
            throw new SecurityException("User with that data is not registered!");
        if(restaurantId >= restaurants.size())
            throw new ArrayIndexOutOfBoundsException("Restaurant with ID: " + restaurantId + " does not exist!");
        if(hour < 0 || hour > 23)
            throw new IllegalArgumentException("Hour must be between 0 and 23!");
        Date realDate = parseDate(date);
        if(realDate == null)
            throw new IllegalArgumentException("Date format " + dateFormat + " is required!");

        Restaurant restaurant = restaurants.get(restaurantId);
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(realDate);

        if(!restaurant.getDays()[calendar.get(Calendar.DAY_OF_WEEK)-1])
            return false;
        if(restaurant.getOpeningHour() > hour || restaurant.getClosingHour() <= hour)
            return false;
        if(restaurant.getBreakStartingHour() <= hour && restaurant.getBreakEndingHour() > hour)
            return false;

        String generatedCode = generateCode(users.indexOf(user), restaurantId, date, hour);
        //if(!BookedRestaurant.validate(user, restaurant, realDate, hour, generatedCode))
            //return false; <- it is already validated before :|
        BookedRestaurant br = new BookedRestaurant(user, restaurant, realDate, hour, generatedCode);

        if(bookedRestaurantAlreadyExists(br))
            return false;
        else {
            user.getBookedRestaurants().put(generatedCode, br);
            if(saveConfirmations) {
                try {
                    br.saveToFile(confirmationsOutputLocation);
                } catch (FileNotFoundException f) { }
            }
            return true;
        }
    }

    public String generateCode(int userId, int restaurantId, String date, int hour) {
        String dateWithoutDots = date.replaceAll("\\.", "");
        return "" + userId + restaurantId + hour + dateWithoutDots;
    }

    public ArrayList<Restaurant> getRestaurants(){
        return restaurants;
    }

    public ArrayList<User> getUsers() {
        return users;
    }

    public String restaurantsToString() {
        String tmp = "";
        for(int i = 0; i < restaurants.size(); i++) {
            tmp += "[ID: " + i + "] " + restaurants.get(i).toString() + "\n";
        }
        return tmp;
    }

    public String usersToString() {
        String tmp = "";
        for(int i = 0; i < users.size(); i++) {
            tmp += i + "." + users.get(i).toString() + "\n";
        }
        return tmp;
    }

    private boolean bookedRestaurantAlreadyExists(BookedRestaurant br){
        for(User u : users)
            if(u.getBookedRestaurants().containsValue(br))
                return true;
        return false;
    }

    private Date parseDate(String date){
        SimpleDateFormat sdf = new SimpleDateFormat(dateFormat);
        Date dateObj;
        try {
            dateObj = sdf.parse(date);
        } catch(ParseException e) {
            dateObj = null;
        }
        return dateObj;
    }

    private User getRegisteredUser(User user) {
        if(user == null || !users.contains(user))
            return null;
        User registered = users.get(users.indexOf(user));
        if(!registered.getPassword().equals(user.getPassword()))
            return null;
        return registered;
    }
}
