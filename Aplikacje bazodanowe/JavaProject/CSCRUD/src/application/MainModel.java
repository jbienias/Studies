package application;

//Created by Jan Bienias 26.05.17

import java.sql.Connection;
import java.sql.SQLException;
import dbConnection.dbConnection;

public class MainModel {
	Connection connection;
	
	public MainModel(){
		try {
			this.connection = dbConnection.getConnection();
		} catch(SQLException e){
			e.printStackTrace();
		}
		
		if(this.connection == null)
			System.exit(1);
	}
	
	public boolean isDatabaseConnected() {
		return this.connection != null;
	}

}
