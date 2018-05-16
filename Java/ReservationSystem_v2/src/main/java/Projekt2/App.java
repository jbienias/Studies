package Projekt2;

import Projekt2.Models.Reservation;
import Projekt2.Models.Restaurant;
import Projekt2.Models.User;
import Projekt2.Repositories.FakeReservationRepository;
import Projekt2.Repositories.FakeRestaurantRepository;
import Projekt2.Repositories.FakeUserRepository;
import Projekt2.Services.Service;
import java.io.FileNotFoundException;
import java.util.List;
import java.util.Scanner;

//Projekt I i II Zad. 6 i 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class App {

    public static void main( String[] args ) throws FileNotFoundException {
        int config; // jesli config == 1, to pokaz nowe funkcje(zaimplementowane z 2'go projektu w serwisie
        config = 0;
        Service rs = new Service(new FakeUserRepository(), new FakeRestaurantRepository(),
                new FakeReservationRepository());
        if(config == 1) {
            rs.loadRestaurantsFromFile();
            System.out.println();
            rs.addUser("Janusz", "Bienias");
            rs.addUser("Herek", "Sand");
            System.out.println(rs.usersToString());
            rs.updateUser(1, "Jeremi", "Super");
            System.out.println(rs.usersToString());
            rs.updateRestaurant(1, "Super!", "Maaaa", new boolean[]{true, true, true, true, true, true, true},
                    10, 20, 12,13);
            System.out.println(rs.restaurantsToString());
            rs.bookRestaurant(0, 1, "27.04.2018", 14);
            rs.bookRestaurant(0, 1, "27.04.2018", 15);
            rs.bookRestaurant(0, 1, "27.04.2018", 12);
            List l = rs.getReservationsByUser(0);
            for(int i = 0; i < l.size(); i++) {
                System.out.println(l.get(i));
            }
            return;
        }
        rs.restaurantsFileLocation = "src/main/resources/db.txt";
        rs.confirmationsOutputLocation = "src/main/resources";
        rs.saveConfirmations = true;
        int errors = rs.loadRestaurantsFromFile();
        System.out.println("Database loaded with " + errors + " errors.");
        User user = null;
        Scanner scanner = new Scanner(System.in);
        String choice, nickname, password;
        boolean quit = false;
        while(!quit) {
            if(user == null) {
                System.out.println("\n=|RESTAURANT RESERVATION SERVICE|=\n");
                System.out.println("1.Log in\n2.Register\nQ.Quit\n");
                System.out.print("INPUT: ");
                choice = scanner.next();
                switch(choice.toUpperCase()) {
                    case "1":
                        System.out.print("Enter your nickname: ");
                        nickname = scanner.next();
                        System.out.print("Enter your password: "); //Console.readPassword(); -> doesn't work in IDE
                        password = scanner.next();
                        user = rs.logIn(nickname, password);
                        if(user != null)
                            System.out.println("MESSAGE: Successfully logged in as '" + user.getNickname() + "'!");
                        else
                            System.out.println("MESSAGE: Failed to log in! Please make " +
                                    "sure you've entered valid information!");
                        break;

                    case "2":
                        System.out.print("Enter your nickname: ");
                        nickname = scanner.next();
                        System.out.print("Enter your password: ");
                        password = scanner.next(); //Console.readPassword(); -> doesn't work in IDE
                        try {
                            if(rs.addUser(nickname, password))
                                System.out.println("MESSAGE: Successfuly registered new user '" + nickname +"'!");
                            else
                                System.out.println("MESSAGE: Failed to register new user! Username with that " +
                                        "nickname already exists!");
                        } catch (Exception e) { System.out.println("ERROR: Something went terribly wrong! " +
                                "Please try again and beware of your input!"); }
                        break;

                    case "Q":
                        quit = true;
                        break;
                    default:
                        System.out.println("ERROR: Unknown option!");
                        break;
                }
            }
            if(user != null) {
                System.out.println("\n=|RESTAURANT RESERVATION SERVICE|=" + "\nLOGGED AS: '" + user.getNickname() + "'\n");
                System.out.println("1.Restaurants\n2.Your booked restaurants\n3.Book a restaurant\n4.Log out\nQ.Quit\n");
                System.out.print("INPUT: ");
                choice = scanner.next();
                switch(choice.toUpperCase()) {
                    case "1":
                        System.out.println("RESTAURANTS:\n" + rs.restaurantsToString());
                        break;

                    case "2":
                        System.out.println("YOUR BOOKED RESTAURANTS:");
                        List<Reservation> br = rs.getReservationsByUser(user.getId());
                        if(br.size() == 0)
                            System.out.println("-");
                        else
                            for(int i = 0; i < br.size(); i++)
                                System.out.println(br.get(i));
                        break;

                    case "3":
                        System.out.println("RESTAURANTS:\n" + rs.restaurantsToString());
                        try {
                            System.out.print("Enter restaurant ID(check list above): ");
                            int restaurantId = scanner.nextInt();
                            System.out.print("Enter date(dd.MM.yyyy): ");
                            String date = scanner.next();
                            System.out.print("Enter hour(0-23): ");
                            int hour = scanner.nextInt();
                            if(rs.bookRestaurant(user.getId(), restaurantId, date, hour))
                                System.out.println("MESSAGE: Successfully booked restaurant " +
                                        rs.getRestaurants().get(restaurantId).getName() +
                                        " for: " + hour + " o'clock on: " + date);
                            else
                                System.out.println("MESSAGE: Failed to book restaurant! Please make sure it is available"
                                        + " at that day and hour. If that's not the case then it is booked by some other user.");
                        } catch(Exception e) { System.out.println("ERROR: Something went terribly wrong! Please try " +
                                "again and beware of your input!"); }
                        break;

                    case "4":
                        System.out.println("MESSAGE: User '" + user.getNickname() + "' logged out.");
                        user = null;
                        break;

                    case "Q":
                        quit = true;
                        break;

                    default:
                        System.out.println("ERROR: Unknown option!");
                        break;
                }
            }
        }
        scanner.close();
        System.out.println("Goodbye!");
    }
}
