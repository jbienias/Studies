package application;

//Created by Jan Bienias 26.05.17

import javafx.collections.FXCollections;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.stage.Stage;
import crud.*;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class MainController implements Initializable{
	
	MainModel mainModel = new MainModel();
	
	@FXML
    private Label lbl_dbStatus, lbl_loginStatus;
	
	@FXML
	private Button btn_dbEnter;
	
    public void initialize(URL url, ResourceBundle rb){
        if (this.mainModel.isDatabaseConnected()){
            this.lbl_dbStatus.setText("Połączono z bazą danych");
            this.lbl_dbStatus.setTextFill(Color.web("#42f445"));
            this.lbl_dbStatus.setFont(new Font("Roboto", 12));
            this.lbl_dbStatus.setAlignment(Pos.CENTER);
        } else {
            this.lbl_dbStatus.setText("Brak połczenia z bazą danych");
            this.lbl_dbStatus.setTextFill(Color.web("#f44141"));
            this.lbl_dbStatus.setAlignment(Pos.CENTER);
        }
    }
    
    @FXML
    public void enter(ActionEvent event) {
    	try {
    		Stage stage = (Stage)this.btn_dbEnter.getScene().getWindow();
    		stage.close();
    		crudEnter();
    	} catch(Exception localException) {
    		localException.printStackTrace();
    	}
    }
    
    public void crudEnter() {
    	try {
    		Stage crudStage = new Stage();
    		FXMLLoader crudLoader = new FXMLLoader();
    		Pane crudroot = (Pane)crudLoader.load(getClass().getResource("/crud/CRUD.fxml").openStream());
    		CRUDController crudController = (CRUDController)crudLoader.getController();
    		
    		Scene scene = new Scene(crudroot);
    		crudStage.setScene(scene);
    		crudStage.setTitle("CSGO CRUD");
    		crudStage.setResizable(false);
    		crudStage.show();
    				
    	} catch(IOException e) {
    		e.printStackTrace();
    	}
    }
    
    @FXML
    public void exitProgram() {
    	System.exit(0);
    }

}
