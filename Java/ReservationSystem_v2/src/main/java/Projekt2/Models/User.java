package Projekt2.Models;

import lombok.Getter;
import lombok.ToString;

//Projekt II Zad. 5 - system rezerwacji restauracji (Mockowanie)
//Jan Bienias 238201

@Getter
@ToString(exclude="password")
public class User {
    private int id;
    private String nickname;
    private String password;

    public User(){

    }

    public User(String nickname, String password) {
        this.nickname = nickname;
        this.password = password;
    }

    public User(int id, User user){ //DB MODEL
        this.nickname = user.getNickname();
        this.password = user.getPassword();
        this.id = id;
    }
}
