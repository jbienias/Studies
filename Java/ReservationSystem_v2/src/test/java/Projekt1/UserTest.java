package Projekt1;

import Projekt2.Models.User;
import Projekt2.Validators.UserValidator;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;
import org.junit.runners.Parameterized.Parameters;
import java.util.Arrays;
import java.util.Collection;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.*;
import static org.junit.Assert.*;

//Zadanie 6 - system rezerwacji restauracji
//Jan Bienias 238201

@RunWith(Parameterized.class)
public class UserTest {

    String nickname,password;
    User user,paramUser;

    public UserTest(User user) {
        this.paramUser = user;
    }

    @Parameters
    public static Collection<Object[]> correctUserData(){
        Object[][] u = {
                {new User("Janusz", "Kuso")},
                {new User("Kłusownik", "hada1313")},
                {new User("ZbigniewADa", "eol")}
        };
        return Arrays.asList(u);
    }

    @Test
    public void validateFromCorrectDataReturnsTrue(){
        assertThat(UserValidator.validate(paramUser), is(true));
    }

    @Before
    public void setUp(){
        nickname = "Herek";
        password = "janusz100";
    }

    @Test
    public void defaultConstructorNoArgsReturnsInstanceTypeOfUser(){
        User u = new User();
        assertEquals(User.class, u.getClass());
    }

    @Test
    public void validateCorrectDataReturnsTrue(){
        assertThat(UserValidator.validate(new User(nickname, password)), is(true));
    }

    @Test
    public void validateNullNicknameReturnsFalse() {
       assertThat(UserValidator.validate(new User(null, password)), is(false));
    }

    @Test
    public void validateNullPasswordReturnsFalse(){
        assertThat(UserValidator.validate(new User(nickname, null)), is(false));
    }

    @Test
    public void validateEmptyNicknameReturnsFalse() {
        assertThat(UserValidator.validate(new User("", password)), is(false));
    }

    @Test
    public void validateEmptyPasswordReturnsFalse() {
        assertThat(UserValidator.validate(new User(nickname, "")), is(false));
    }

    @Test
    public void toStringUserObjectExistsReturnsStringWithNickname(){
        user = new User(nickname, password);
        assertThat(user.toString(), containsString(nickname));
    }

    @After
    public void tearDown(){
        nickname = null;
        password = null;
    }
}
