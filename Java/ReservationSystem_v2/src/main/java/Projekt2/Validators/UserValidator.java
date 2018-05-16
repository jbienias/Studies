package Projekt2.Validators;

//Zadanie 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

import Projekt2.Models.User;

public class UserValidator {
    public static boolean validate(User u){
        if(u.getNickname() == null || u.getNickname().isEmpty() || u.getPassword() == null || u.getPassword().isEmpty())
            return false;
        return true;
    }
}
