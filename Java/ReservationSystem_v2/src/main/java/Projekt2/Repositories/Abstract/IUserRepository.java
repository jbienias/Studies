package Projekt2.Repositories.Abstract;

import Projekt2.Models.User;
import java.util.List;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public interface IUserRepository {
    List getUsers();
    User getUser(int id);
    User getUser(String nickname);
    User getUser(String nickname, String password);
    boolean userExists(int id);
    boolean userExists(String nickname);
    void addUser(User user);
    void deleteUser(int id);
    void updateUser(int id, User user);
    boolean validateUser(User user);
}
