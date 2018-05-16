package dbConnection;

import java.beans.Statement;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Scanner;

public class dbConnection {
    private static final String SQLITECONN = "jdbc:sqlite:csgo.db";

    public static Connection getConnection() throws SQLException{

        try {
            Class.forName("org.sqlite.JDBC");
            return DriverManager.getConnection(SQLITECONN);
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
        return null;
    }
}
