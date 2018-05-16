package Projekt1;

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
    public void validate_FromCorrectData_True(){
        assertThat(User.validate( paramUser.getNickname(),  paramUser.getPassword()), is(true));
    }

    @Before
    public void setUp(){
        nickname = "Herek";
        password = "janusz100";
    }

    @Test
    public void validate_CorrectData_True(){
        assertThat(User.validate(nickname, password), is(true));
    }

    @Test
    public void validate_NullNickname_False() {
       assertThat(User.validate(null, password), is(false));
    }

    @Test
    public void validate_NullPassword_False(){
        assertThat(User.validate(nickname, null), is(false));
    }

    @Test
    public void validate_EmptyNickname_False() {
        assertThat(User.validate("", password), is(false));
    }

    @Test
    public void validate_EmptyPassword_False() {
        assertThat(User.validate(nickname, ""), is(false));
    }

    @Test
    public void equals_UsersWithSameProperties_True(){
        User user1 = new User(nickname, password);
        User user2 = new User(nickname, password);
        assertTrue(user1.equals(user2));
    }

    @Test
    public void equals_UsersWithSameNicknames_True(){
        User user1 = new User(nickname, "haslo");
        User user2 = new User(nickname, "trzaslo");
        assertTrue(user1.equals(user2));
    }

    @Test
    public void equals_UsersWithDifferentProperties_False(){
        User user1 = new User(nickname, password);
        User user2 = new User("Randomowe", "randomTotalus");
        assertFalse(user1.equals(user2));
    }

    @Test
    public void equals_UsersWithDifferentNicknames_False(){
        User user1 = new User(nickname, password);
        User user2 = new User("Random", password);
        assertFalse(user1.equals(user2));
    }

    @Test
    public void toString_UserObjectExists_ReturnsStringWithNickname(){
        user = new User(nickname, password);
        assertThat(user.toString(), containsString(nickname));
    }

    @After
    public void tearDown(){
        nickname = null;
        password = null;
    }
}
