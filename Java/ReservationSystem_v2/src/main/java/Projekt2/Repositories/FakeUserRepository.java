package Projekt2.Repositories;

import Projekt2.Models.User;
import Projekt2.Repositories.Abstract.IUserRepository;
import Projekt2.Validators.UserValidator;
import java.util.ArrayList;
import java.util.List;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

public class FakeUserRepository implements IUserRepository {

    private int idCounter = 0;
    private List<User> users = new ArrayList<User>();

    public List getUsers() {
        return users;
    }

    public User getUser(int id) {
        return users.stream().filter(x -> x.getId() == id).findFirst().orElse(null);
    }

    public User getUser(String nickname) {
        return users.stream().filter(x -> x.getNickname().equals(nickname)).findFirst().orElse(null);
    }

    public User getUser(String nickname, String password) {
        return users.stream().filter(x -> x.getNickname().equals(nickname)
                && x.getPassword().equals(password)).findFirst().orElse(null);
    }

    public boolean userExists(int id) {
        return users.stream().anyMatch(x -> x.getId() == id);
    }

    public boolean userExists(String nickname) {
        return users.stream().anyMatch(x -> x.getNickname().equals(nickname));
    }

    public void addUser(User user) {
        users.add(new User(idCounter, user));
        idCounter++;
    }

    public void deleteUser(int id) {
        users.removeIf(x -> x.getId() == id);
    }

    public void updateUser(int id, User user) {
        User u = users.stream().filter(x -> x.getId() == id).findFirst().orElse(null);
        if(u != null){
            users.set(users.indexOf(u), new User(id, user));
        }
    }

    public boolean validateUser(User user) {
       return UserValidator.validate(user);
    }
}
