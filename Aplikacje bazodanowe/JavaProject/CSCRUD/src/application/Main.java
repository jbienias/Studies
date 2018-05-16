package application;

//Created by Jan Bienias 26.05.17
	
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.stage.Stage;
import javafx.scene.Parent;
import javafx.scene.Scene;

import java.beans.Statement;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.sql.SQLException;
import java.util.Scanner;


import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.Optional;
import java.util.ResourceBundle;


import dbConnection.*;


public class Main extends Application {
	
	@Override
	public void start(Stage primaryStage) {
		try {
			Parent root = FXMLLoader.load(getClass().getResource("Main.fxml")); //Main.fxml
			primaryStage.setTitle("CSGO");
			primaryStage.setScene(new Scene(root));
			primaryStage.show();
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	public static void boot(String leer)
    {
    	int error = 0;
    	FileReader fr = null;
    	try {
            fr = new FileReader(leer);
    	} catch (FileNotFoundException e) {
            System.out.println("Błąd!");
            error = 1;
    	}
    	if (error == 0) { 
            String sql="";
            Scanner plik = new Scanner(fr);
            try {
            	int i = 0;
            	Connection conn = dbConnection.getConnection();
            	while(plik.hasNext()) {
                    sql=plik.nextLine();
                    PreparedStatement stm = conn.prepareStatement(sql);
                    stm.execute();
                    i++;
                    System.out.println("Wykonano instrukcj� z linii nr." + i + ".");
            	}
    		System.out.println("Wykonano " + i + " instrukcji z pliku : " + leer + ".");
            } catch (SQLException e) {
            	System.err.println("Blad przy wykonywaniu instrukcji.");
            }
            try {
            	fr.close();
            } catch (IOException e){
            	System.out.println("Blad przy zamykaniu pliku.");
                System.exit(3);
            }
        }
    }
	
	public static void main(String[] args) throws SQLException {
		File f = new File("csgo.db");
		if(!(f.exists() && !f.isDirectory())) {
			boot("csgoscript.sql");
		}
		launch();
	}
}
