package Projekt2.Helpers;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class CodeGenerator {
    public static String generate(int userId, int restaurantId, String date, int hour) {
        String dateOnlyNumbers = date.replaceAll("[^0-9]+", "");
        return "" + userId + restaurantId + hour + dateOnlyNumbers;
    }
}
