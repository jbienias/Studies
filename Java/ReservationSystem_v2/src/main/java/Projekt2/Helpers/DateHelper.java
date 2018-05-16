package Projekt2.Helpers;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class DateHelper{
    private static String format = "dd.MM.yyyy";

    public static Date parseDate(String date){
        SimpleDateFormat sdf = new SimpleDateFormat(format);
        Date dateObj;
        try {
            dateObj = sdf.parse(date);
        } catch(ParseException e) {
            dateObj = null;
        }
        return dateObj;
    }

    public static String getFormat() {
        return format;
    }
}
