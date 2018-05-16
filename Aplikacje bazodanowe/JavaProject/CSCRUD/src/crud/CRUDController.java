package crud;

//Created by Jan Bienias 26.05.17

import dbConnection.*;
import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
import javafx.collections.ObservableList;
import javafx.collections.ListChangeListener;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.event.ActionEvent;
import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDate;
import java.time.format.*;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.Optional;
import java.util.ResourceBundle;

public class CRUDController implements Initializable {
	
	private dbConnection db;
	public DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
	
	//ZAWODNIK @FXML
	@FXML
	private TextField txt_imie, txt_nazwisko, txt_pseudonim, txt_stawka, txt_id;
	@FXML
	private Button btn_dodajZawodnik, btn_usunZawodnik, btn_edytujZawodnik, btn_zaladujZawodnik;
	@FXML
	private TableView <Zawodnik> tv_zawodnik;
	@FXML
	private TableColumn<Zawodnik, String> tc_imie, tc_nazwisko, tc_pseudonim, tc_stawka, tc_druzyna, tc_kraj;
	@FXML
	private ObservableList<Zawodnik> zawodnikList;
	@FXML
	private ComboBox combo_druzyna, combo_kraj;
	@FXML
	private ObservableList krajCombo = FXCollections.observableArrayList();
	@FXML
	private ObservableList druzynaCombo = FXCollections.observableArrayList();
	@FXML
	Tab tab_zawodnik;
	
	//MAPA @FXML
	@FXML
	private TextField txt_nazwaM, txt_rozmiarM, txt_ocenaM, txt_idM;
	@FXML
	private DatePicker dataM;
	@FXML
	private Button btn_dodajMapa, btn_usunMapa, btn_edytujMapa, btn_zaladujMapa;
	@FXML
	private TableView <Mapa> tv_mapa;
	@FXML
	private TableColumn<Mapa, String> tc_nazwaM, tc_dataM, tc_rozmiarM, tc_ocenaM;
	@FXML
	private ObservableList<Mapa> mapaList;
	@FXML
	Tab tab_mapa;	
	
	//KRAJ @FXML
	@FXML
	private TextField txt_nazwaK, txt_isoK, txt_populacjaK, txt_dataK, txt_idK;
	@FXML
	private DatePicker dataK;
	@FXML
	private Button btn_dodajKraj, btn_usunKraj, btn_edytujKraj, btn_zaladujKraj;
	@FXML
	private TableView <Kraj> tv_kraj;
	@FXML
	private TableColumn<Kraj, String> tc_nazwaK, tc_dataK, tc_isoK, tc_populacjaK;
	@FXML
	private ObservableList<Kraj> krajList;
	@FXML
	Tab tab_kraj;
	
	//PREFMAPA @FXML
	@FXML
	private Button btn_dodajPM, btn_usunPM, btn_edytujPM, btn_zaladujPM;
	@FXML
	private TableView <PrefMapa> tv_prefmapa;
	@FXML
	private TableColumn<PrefMapa, String> tc_mapa, tc_zawodnik;
	@FXML
	private ObservableList<PrefMapa> prefmapaList;
	@FXML
	private ComboBox combo_zawodnik, combo_mapa;
	@FXML
	private ObservableList zawodnikCombo = FXCollections.observableArrayList();
	@FXML
	private ObservableList mapaCombo = FXCollections.observableArrayList();
	@FXML
	Tab tab_prefmapa;

	//DRUZYNA @FXML
	@FXML
	private TextField txt_nazwaD, txt_idD, txt_sponsorD, txt_liczbaczlonkowD;
	@FXML
	private Button btn_dodajDruzyna, btn_usunDruzyna, btn_edytujDruzyna, btn_zaladujDruzyna;
	@FXML
	private DatePicker dataD;
	@FXML
	private TableView <Druzyna> tv_druzyna;
	@FXML
	private TableColumn<Druzyna, String> tc_nazwaD, tc_dataD, tc_liczbaczlonkowD, tc_sponsorD;
	@FXML
	private ObservableList<Druzyna> druzynaList;
	@FXML
	Tab tab_druzyna;
	
	@Override
	public void initialize(URL location, ResourceBundle resources) {
		this.db = new dbConnection();
	}
	
	@FXML
	private void selectedTab() {
		if(tab_zawodnik.isSelected()) {
			loadZawodnikCombo(); //inicjalizuj obydwa comboboxy
			ObservableList<Zawodnik> selectedRow = tv_zawodnik.getSelectionModel().getSelectedItems();
			selectedRow.addListener(new ListChangeListener() {
				@Override
				public void onChanged(Change c) throws NullPointerException {
					try {
						Zawodnik selectedR = tv_zawodnik.getSelectionModel().getSelectedItem();
						txt_id.setText(selectedR.getId());
						txt_imie.setText(selectedR.getImie());
						txt_nazwisko.setText(selectedR.getNazwisko());
						txt_pseudonim.setText(selectedR.getPseudonim());
						txt_stawka.setText(selectedR.getStawka());
						String kraj = selectedR.getKraj_id();
						String druzyna = selectedR.getDruzyna_id();
						combo_kraj.getSelectionModel().select(kraj);
						combo_druzyna.getSelectionModel().select(druzyna);
					} catch (NullPointerException e) {
						txt_id.setText(null);
						txt_imie.setText(null);
						txt_nazwisko.setText(null);
						txt_pseudonim.setText(null);
						txt_stawka.setText(null);
						combo_kraj.setValue(null);
						combo_druzyna.setValue(null);
					}
				}
			});
		}
		
		else if(tab_mapa.isSelected()) {
			ObservableList<Mapa> selectedRow = tv_mapa.getSelectionModel().getSelectedItems();
			selectedRow.addListener(new ListChangeListener() {
				@Override
				public void onChanged(Change c) throws NullPointerException {
					try {
						Mapa selectedR = tv_mapa.getSelectionModel().getSelectedItem();
						txt_idM.setText(selectedR.getId());
						txt_nazwaM.setText(selectedR.getNazwa());
						txt_rozmiarM.setText(selectedR.getRozmiar());
						txt_ocenaM.setText(selectedR.getOcena());
						try {
							dataM.setValue(LocalDate.parse(selectedR.getData(), formatter));
						} catch (DateTimeParseException | NullPointerException e) {
							e.printStackTrace();
						}
					} catch (NullPointerException e) {
						txt_idM.setText(null);
						txt_nazwaM.setText(null);
						dataM.setValue(null);
						txt_rozmiarM.setText(null);
						txt_ocenaM.setText(null);
					}
				}
			});
		}
		
		else if(tab_kraj.isSelected()) {
			ObservableList<Kraj> selectedRow = tv_kraj.getSelectionModel().getSelectedItems();
			selectedRow.addListener(new ListChangeListener() {
				@Override
				public void onChanged(Change c) throws NullPointerException {
					try {
						Kraj selectedR = tv_kraj.getSelectionModel().getSelectedItem();
						txt_idK.setText(selectedR.getId());
						txt_nazwaK.setText(selectedR.getNazwa());
						txt_populacjaK.setText(selectedR.getPopulacja());
						txt_isoK.setText(selectedR.getIso());
						try {
							dataK.setValue(LocalDate.parse(selectedR.getData(), formatter));
						} catch (DateTimeParseException | NullPointerException e) {
							e.printStackTrace();
						}
					} catch (NullPointerException e) {
						txt_idK.setText(null);
						txt_nazwaK.setText(null);
						dataK.setValue(null);
						txt_populacjaK.setText(null);
						txt_isoK.setText(null);
					}
				}
			});
		}
		
		else if(tab_prefmapa.isSelected()) {
			loadPrefmapaCombo();
			ObservableList<PrefMapa> selectedRow = tv_prefmapa.getSelectionModel().getSelectedItems();
			selectedRow.addListener(new ListChangeListener() {
				@Override
				public void onChanged(Change c) throws NullPointerException {
					try {
						//nic, bo chcemy modyfikowac n:m (2/2 (wszystkie) fieldy w tym tabie sa comboboxami)
					} catch (NullPointerException e) {
						
					}
				}
			});
		}
		
		else if(tab_druzyna.isSelected()) {
			ObservableList<Druzyna> selectedRow = tv_druzyna.getSelectionModel().getSelectedItems();
			selectedRow.addListener(new ListChangeListener() {
				@Override
				public void onChanged(Change c) throws NullPointerException {
					try {
						Druzyna selectedR = tv_druzyna.getSelectionModel().getSelectedItem();
						txt_idD.setText(selectedR.getId());
						txt_nazwaD.setText(selectedR.getNazwa());
						txt_liczbaczlonkowD.setText(selectedR.getLiczbaCzlonkow());
						txt_sponsorD.setText(selectedR.getSponsor());
						try {
							dataD.setValue(LocalDate.parse(selectedR.getData(), formatter));
						} catch (DateTimeParseException | NullPointerException e) {
							e.printStackTrace();;
						}
					} catch (NullPointerException e) {
						txt_idD.setText(null);
						txt_nazwaD.setText(null);
						dataD.setValue(null);
						txt_liczbaczlonkowD.setText(null);
						txt_sponsorD.setText(null);
					}
				}
			});
		}
	}

	//======================ZAWODNIK======================//
	@FXML
	public void loadZawodnikCombo() {
		String queryDruzyna = "SELECT * FROM druzyna;";
		String queryKraj = "SELECT * FROM kraj;";
		combo_druzyna.setItems(null);
		combo_kraj.setItems(null);
		krajCombo.clear();
		druzynaCombo.clear();
		try {
			Connection conn = dbConnection.getConnection();
			ResultSet rs = conn.prepareStatement(queryKraj).executeQuery();
			while(rs.next()){
				String krajName = rs.getString("nazwa");
				krajCombo.add(krajName);
			}
			combo_kraj.setItems(krajCombo);
			ResultSet ps = conn.prepareStatement(queryDruzyna).executeQuery();
			while(ps.next()) {
				String druzynaName = ps.getString("nazwa");
				druzynaCombo.add(druzynaName);
			}
			combo_druzyna.setItems(druzynaCombo);
		}catch(SQLException e) { }
	}
	
	@FXML
	public void loadZawodnik(ActionEvent actionEvent) {
		String query = "SELECT zawodnik.id, zawodnik.imie, zawodnik.nazwisko, zawodnik.pseudonim, zawodnik.stawka, druzyna.nazwa, kraj.nazwa FROM zawodnik JOIN druzyna ON druzyna.id = zawodnik.druzyna_id JOIN kraj ON kraj.id = zawodnik.kraj_id;";
		try {
			Connection conn = dbConnection.getConnection();
			this.zawodnikList = FXCollections.observableArrayList();
			ResultSet rs = conn.createStatement().executeQuery(query);
			while(rs.next()) {
				this.zawodnikList.add(new Zawodnik (
						rs.getString(1),
						rs.getString(2),
						rs.getString(3),
						rs.getString(4),
						rs.getString(5),
						rs.getString(6),
						rs.getString(7)));
			}
		} catch (SQLException er) { }
		this.tc_imie.setCellValueFactory(new PropertyValueFactory<Zawodnik, String>("imie"));
		this.tc_nazwisko.setCellValueFactory(new PropertyValueFactory<Zawodnik, String>("nazwisko"));
		this.tc_pseudonim.setCellValueFactory(new PropertyValueFactory<Zawodnik, String>("pseudonim"));
		this.tc_stawka.setCellValueFactory(new PropertyValueFactory<Zawodnik, String>("stawka"));
		this.tc_druzyna.setCellValueFactory(new PropertyValueFactory<Zawodnik, String>("druzyna_id"));
		this.tc_kraj.setCellValueFactory(new PropertyValueFactory<Zawodnik, String>("kraj_id"));
		this.tv_zawodnik.setItems(null);
		this.tv_zawodnik.setItems(this.zawodnikList);
	}
	
	@FXML
	public void clearZawodnik(ActionEvent actionEvent) {
		txt_id.setText(null);
		txt_imie.setText(null);
		txt_nazwisko.setText(null);
		txt_pseudonim.setText(null);
		txt_stawka.setText(null);
		combo_kraj.setValue(null);
		combo_druzyna.setValue(null);
	}

	@FXML
	public void addZawodnik(ActionEvent actionEvent) {
		String kraj = "", druzyna = "";
		String krajID = "", druzynaID = "";
		String krajQuery = "SELECT id FROM kraj WHERE nazwa = ? ; ";
		String checkUnique = "SELECT id FROM zawodnik WHERE pseudonim = ? ";
		String druzynaQuery = "SELECT id FROM druzyna WHERE nazwa = ? ; ";
		String insertQuery = "INSERT INTO zawodnik(imie, nazwisko, pseudonim, stawka, druzyna_id, kraj_id) " +
							"VALUES(?,?,?,?,?,?);";
		try {
			Connection conn = dbConnection.getConnection();
			PreparedStatement stm = conn.prepareStatement(insertQuery);
			if(!isName(this.txt_imie.getText()) || !isValidLen(this.txt_imie.getText())) {
				setAlertError("Niewlaściwe dane", "Imie nie jest poprawne (zawiera niedozwolone znaki badz ma zla dlugosc)!");
				txt_imie.setText(null);
				return;
			}
			else if(!isName(this.txt_nazwisko.getText()) || !isValidLen(this.txt_nazwisko.getText())) {
				setAlertError("Niewlaściwe dane", "Nazwisko nie jest poprawne(zawiera niedozwolone znaki badz ma zla dlugosc)!");
				txt_nazwisko.setText(null);
				return;
			}
			else if(!isUnique(this.txt_pseudonim.getText(), checkUnique)) {
				setAlertError("Niewlaściwe dane", "Pseudonim jest juz zajęty!");
				txt_pseudonim.setText(null);
				return;
			}
			else if(!isValidLen(this.txt_pseudonim.getText())) {
				setAlertError("Niewlaściwe dane", "Pseudonim nie jest poprawny(ma zla dlugosc)!");
				txt_pseudonim.setText(null);
				return;
			}
			else if(!isMoney(this.txt_stawka.getText())) {
				setAlertError("Niewlaściwe dane", "Stawka nie jest poprawna(ma zla wartosc pieniezna)!");
				txt_stawka.setText(null);
				return;
			}
			else if(combo_druzyna.getValue() == null || combo_kraj.getValue() == null) {
	            setAlertError("Niepoprawny wybór", "Wybierz wszystkie pola (kraj i druzyna)!");
			} else {
				kraj = combo_kraj.getValue().toString();
				druzyna = combo_druzyna.getValue().toString();
			}
			try{
				PreparedStatement h1 = dbConnection.getConnection().prepareStatement(krajQuery);
				h1.setString(1, kraj);
				ResultSet rs1 = h1.executeQuery();
				while(rs1.next()) {
					krajID = rs1.getString(1);
				}
				PreparedStatement h2 = dbConnection.getConnection().prepareStatement(druzynaQuery);
				h2.setString(1,druzyna);
				ResultSet rs2 = h2.executeQuery();
				while(rs2.next()) {
					druzynaID = rs2.getString(1);
				}	
			} catch(SQLException | NullPointerException e) { }
			stm.setString(1,  String.valueOf(CRUDController.this.txt_imie.getText()));
			stm.setString(2,  String.valueOf(CRUDController.this.txt_nazwisko.getText()));
			stm.setString(3,  String.valueOf(CRUDController.this.txt_pseudonim.getText()));
			stm.setString(4,  String.valueOf(CRUDController.this.txt_stawka.getText()));
			stm.setString(5,  druzynaID);
			stm.setString(6,  krajID);
			stm.execute();
			btn_zaladujZawodnik.fire();	
			stm.close();
			conn.close();
		} catch(SQLException | NullPointerException e) { }
	}
	
	@FXML
	public void deleteZawodnik(ActionEvent actionEvent) {
		String deleteQuery = "DELETE FROM ZAWODNIK WHERE id =";
		Zawodnik getSelectedRow = tv_zawodnik.getSelectionModel().getSelectedItem();
		try {
			Connection conn = dbConnection.getConnection();
			if(!getSelectedRow.toString().equals("")) {
				String id = getSelectedRow.getId();
				deleteQuery += id + "; ";
				Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
	            alert.setTitle("Potwierdzenie usunięcia");
	            alert.setHeaderText("Chcesz usunąć zawodnika?");
	            alert.setContentText("" + getSelectedRow.getPseudonim());
	            Optional<ButtonType> result = alert.showAndWait();
	            if (result.get() == ButtonType.OK) {
	            	PreparedStatement stm = conn.prepareStatement(deleteQuery);
	            	stm.execute();	 
	            	btn_zaladujZawodnik.fire();
	            	stm.close();
	            }      
			}
			conn.close();
		}catch(SQLException | NullPointerException e){ }
	}
	
	@FXML 
	public void editZawodnik(ActionEvent actionEvent) {
		String kraj = "", druzyna = "";
		String krajID = "", druzynaID = "";
		String krajQuery = "SELECT id FROM kraj WHERE nazwa = ? ; ";
		String druzynaQuery = "SELECT id FROM druzyna WHERE nazwa = ? ; ";
		String updateQuery = "UPDATE zawodnik SET imie = ?, nazwisko = ?, pseudonim = ?, stawka = ?, druzyna_id = ?, kraj_id = ? "
							+" WHERE id = ?; ";
		try { //Sprawdzenie, czy zawodnik o nowowprowadzonym pseudonimie(unique) istnieje + pozwolenie edycji (dlatego query walidujace ma id != swoje id)
			String s = this.txt_pseudonim.getText(); //unikalna wartosc tabeli zawodnik
			String id = this.txt_id.getText(); //primary key tabeli zawodnik
			String checkUnique = "SELECT * FROM zawodnik WHERE pseudonim = ? AND id != ?;";
			if (isUniqueOnEdit(s,id, checkUnique) >= 1) {
				setAlertError("Niewlaściwe dane", "Zawodnik z takim pseudonimem jest juz w bazie!");
				txt_pseudonim.setText(null);
				return;
			} 
		}catch(SQLException e) {}
		try {
			Connection conn = dbConnection.getConnection();
			PreparedStatement stm = conn.prepareStatement(updateQuery);
			if(!isName(this.txt_imie.getText()) || !isValidLen(this.txt_imie.getText())) {
				setAlertError("Niewlaściwe dane", "Imie nie jest poprawne (zawiera niedozwolone znaki badz ma zla dlugosc)!");
				txt_imie.setText(null);
				return;
			}
			else if(!isName(this.txt_nazwisko.getText()) || !isValidLen(this.txt_nazwisko.getText())) {
				setAlertError("Niewlaściwe dane", "Nazwisko nie jest poprawne(zawiera niedozwolone znaki badz ma zla dlugosc)!");
				txt_nazwisko.setText(null);
				return;
			}
			else if(!isValidLen(this.txt_pseudonim.getText())) {
				setAlertError("Niewlaściwe dane", "Pseudonim nie jest poprawny(ma zla dlugosc)!");
				txt_pseudonim.setText(null);
				return;
			}
			else if(!isMoney(this.txt_stawka.getText())) {
				setAlertError("Niewlaściwe dane", "Stawka nie jest poprawna(ma zla wartosc pieniezna)!");
				txt_stawka.setText(null);
				return;
			}
			else if(combo_druzyna.getValue() == null || combo_kraj.getValue() == null) {
	            setAlertError("Niepoprawny wybór", "Wybierz wszystkie pola (kraj i druzyna)!");
			} else {
				kraj = combo_kraj.getValue().toString();
				druzyna = combo_druzyna.getValue().toString();
			}
			try{
				PreparedStatement h1 = dbConnection.getConnection().prepareStatement(krajQuery);
				h1.setString(1, kraj);
				ResultSet rs1 = h1.executeQuery();
				while(rs1.next()) {
					krajID = rs1.getString(1);
				}
				PreparedStatement h2 = dbConnection.getConnection().prepareStatement(druzynaQuery);
				h2.setString(1,druzyna);
				ResultSet rs2 = h2.executeQuery();
				while(rs2.next()) {
					druzynaID = rs2.getString(1);
				}
			} catch(SQLException e) { }
			stm.setString(1,  String.valueOf(CRUDController.this.txt_imie.getText()));
			stm.setString(2,  String.valueOf(CRUDController.this.txt_nazwisko.getText()));
			stm.setString(3,  String.valueOf(CRUDController.this.txt_pseudonim.getText()));
			stm.setString(4,  String.valueOf(CRUDController.this.txt_stawka.getText()));
			stm.setString(5, druzynaID);
			stm.setString(6, krajID);
			stm.setString(7,  String.valueOf(CRUDController.this.txt_id.getText()));
			stm.execute();
			btn_zaladujZawodnik.fire();
			stm.close();
			
			conn.close();
		} catch(SQLException | NullPointerException e) { }
	}
	
	//======================MAPA======================//
	@FXML
	public void loadMapa(ActionEvent actionEvent) {
		String query = "SELECT id, nazwa, data_stworzenia, rozmiar, ocena FROM mapa;";
		try {
			Connection conn = dbConnection.getConnection();
			this.mapaList = FXCollections.observableArrayList();
			ResultSet rs = conn.createStatement().executeQuery(query);
			while(rs.next()) {
				this.mapaList.add(new Mapa (
						rs.getString(1),
						rs.getString(2),
						rs.getString(3),
						rs.getString(4),
						rs.getString(5)));
			}
			conn.close();
		} catch (SQLException | NullPointerException e) { }
		this.tc_nazwaM.setCellValueFactory(new PropertyValueFactory<Mapa, String>("nazwa"));
		this.tc_dataM.setCellValueFactory(new PropertyValueFactory<Mapa, String>("data"));
		this.tc_rozmiarM.setCellValueFactory(new PropertyValueFactory<Mapa, String>("rozmiar"));
		this.tc_ocenaM.setCellValueFactory(new PropertyValueFactory<Mapa, String>("ocena"));
		this.tv_mapa.setItems(null);			
		this.tv_mapa.setItems(this.mapaList);
	}
	
	@FXML
	public void clearMapa(ActionEvent actionEvent) {
		txt_idM.setText(null);
		txt_nazwaM.setText(null);
		dataM.setValue(null);
		txt_rozmiarM.setText(null);
		txt_ocenaM.setText(null);
	}
	
	@FXML
	public void addMapa(ActionEvent actionEvent) {
		String insertQuery = "INSERT INTO mapa(nazwa, data_stworzenia, rozmiar, ocena)" +
				" VALUES(?,?,?,?);";
		String checkUnique = "SELECT * FROM mapa WHERE nazwa = ? ";
        try {
            Connection conn = dbConnection.getConnection();
            PreparedStatement stm = conn.prepareStatement(insertQuery);
			if(!isValidLen(this.txt_nazwaM.getText())) {
				setAlertError("Niewlaściwe dane", "Nazwa nie jest poprawna(ma zla dlugosc)!");
				txt_nazwaM.setText(null);
				return;
			}
			else if(!isUnique(this.txt_nazwaM.getText(), checkUnique)) {
				setAlertError("Niewlaściwe dane", "Taka nazwa mapy juz istnieje!");
				txt_nazwaM.setText(null);
				return;
			}
			else if(this.dataM.getValue() == null) {
				setAlertError("Niepoprawny wybór!", "Data jest pusta!");
				return;
			}
			else if(!isPositiveInteger(this.txt_rozmiarM.getText())) {
				setAlertError("Niewlaściwe dane", "Rozmiar nie jest poprawny(rozmiar > 0 i rozmiar jest liczba naturalna)!");
				txt_rozmiarM.setText(null);
				return;
			}
			else if(!isValidRating(this.txt_ocenaM.getText())) {
				setAlertError("Niewlaściwe dane", "Ocena nie jest poprawna(10 >= ocena > 0 i ocena jest liczba naturalna)!");
				txt_ocenaM.setText(null);
				return;
			} else
			stm.setString(1, String.valueOf(CRUDController.this.txt_nazwaM.getText()));
			stm.setString(2, String.valueOf(CRUDController.this.dataM.getEditor().getText()));
			stm.setString(3, String.valueOf(CRUDController.this.txt_rozmiarM.getText()));
			stm.setString(4, String.valueOf(CRUDController.this.txt_ocenaM.getText()));
			stm.execute();
			btn_zaladujMapa.fire();
			stm.close();
			conn.close(); 
        } catch (SQLException  | NullPointerException e) { }
	}
	
	@FXML
	public void deleteMapa(ActionEvent actionEvent) {
		String deleteQuery = "DELETE FROM mapa WHERE id =";
		Mapa getSelectedRow = tv_mapa.getSelectionModel().getSelectedItem();
		try {
			Connection conn = dbConnection.getConnection();
			if(!getSelectedRow.toString().equals("")) {
				String id = getSelectedRow.getId();
				deleteQuery += id + "; ";
				Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
	            alert.setTitle("Potwierdzenie usunięcia");
	            alert.setHeaderText("Chcesz usunąć mapę?");
	            alert.setContentText("" + getSelectedRow.getNazwa());
	            Optional<ButtonType> result = alert.showAndWait();
	            if (result.get() == ButtonType.OK) {
	            	PreparedStatement stm = conn.prepareStatement(deleteQuery);
	            	stm.execute();	 
	            	btn_zaladujMapa.fire();
	            	stm.close();
	            }      
			}
			conn.close();
		}catch(SQLException | NullPointerException e){ }
	}
	
	@FXML 
	public void editMapa(ActionEvent actionEvent) {
		String updateQuery = "UPDATE mapa SET nazwa = ?, data_stworzenia = ?, rozmiar = ?, ocena = ? "
							+" WHERE id = ?; ";
		try { //Sprawdzenie, czy mapa o nowowprowadzonej nazwie(unique) istnieje + pozwolenie edycji (dlatego query walidujace ma id != swoje id)
			String s = this.txt_nazwaM.getText(); //unikalna wartosc tabeli mapa
			String id = this.txt_idM.getText(); //primary key tabeli mapa
			String checkUnique = "SELECT * FROM mapa WHERE nazwa = ? AND id != ?;";
			if (isUniqueOnEdit(s,id, checkUnique) >= 1) {
				setAlertError("Niewlaściwe dane", "Mapa o danej nazwie jest już w bazie!");
				txt_nazwaM.setText(null);
				return;
			} 
		}catch(SQLException e) {}
		try {
			Connection conn = dbConnection.getConnection();
			PreparedStatement stm = conn.prepareStatement(updateQuery);
			if(!isValidLen(this.txt_nazwaM.getText())) {
				setAlertError("Niewlaściwe dane", "Nazwa nie jest poprawna(ma zla dlugosc)!");
				txt_nazwaM.setText(null);
				return;
			}
			else if(this.dataM.getValue() == null) {
				setAlertError("Niepoprawny wybór!", "Data jest pusta!");
				return;
			}
			else if(!isPositiveInteger(this.txt_rozmiarM.getText())) {
				setAlertError("Niewlaściwe dane", "Rozmiar nie jest poprawny(rozmiar > 0 i rozmiar jest liczba naturalna)!");
				txt_rozmiarM.setText(null);
				return;
			}
			else if(!isValidRating(this.txt_ocenaM.getText())) {
				setAlertError("Niewlaściwe dane", "Ocena nie jest poprawna(10 >= ocena > 0 i ocena jest liczba naturalna)!");
				txt_ocenaM.setText(null);
				return;
			} else
			stm.setString(1, String.valueOf(CRUDController.this.txt_nazwaM.getText()));
			stm.setString(2, String.valueOf(CRUDController.this.dataM.getEditor().getText()));
			stm.setString(3, String.valueOf(CRUDController.this.txt_rozmiarM.getText()));
			stm.setString(4, String.valueOf(CRUDController.this.txt_ocenaM.getText()));
			stm.setString(5, String.valueOf(CRUDController.this.txt_idM.getText()));
			stm.execute();
			btn_zaladujMapa.fire();	
			stm.close();
			conn.close();
		} catch(SQLException | NullPointerException e) { }
	}
	
	//======================KRAJ======================//
	@FXML
	public void loadKraj(ActionEvent actionEvent) {
		String query = "SELECT * FROM kraj;";
		try {
			Connection conn = dbConnection.getConnection();
			this.krajList = FXCollections.observableArrayList();
			ResultSet rs = conn.createStatement().executeQuery(query);
			while(rs.next()) {
				this.krajList.add(new Kraj (
						rs.getString(1),
						rs.getString(2),
						rs.getString(3),
						rs.getString(4),
						rs.getString(5)));
			}
			conn.close();
		} catch (SQLException er) { }
		this.tc_nazwaK.setCellValueFactory(new PropertyValueFactory<Kraj, String>("nazwa"));
		this.tc_dataK.setCellValueFactory(new PropertyValueFactory<Kraj, String>("data"));
		this.tc_isoK.setCellValueFactory(new PropertyValueFactory<Kraj, String>("iso"));
		this.tc_populacjaK.setCellValueFactory(new PropertyValueFactory<Kraj, String>("populacja"));
		this.tv_kraj.setItems(null);
		this.tv_kraj.setItems(this.krajList);
	}
	
	@FXML
	public void clearKraj(ActionEvent actionEvent) {
		txt_idK.setText(null);
		txt_nazwaK.setText(null);
		dataK.setValue(null);
		txt_populacjaK.setText(null);
		txt_isoK.setText(null);
	}
	
	@FXML
	public void addKraj(ActionEvent actionEvent) {
		String insertQuery = "INSERT INTO kraj(nazwa, iso, populacja, data_zalozenia)" +
				" VALUES(?,?,?,?);";
		String checkUnique = "SELECT * FROM kraj WHERE nazwa = ?;";
        try {
            Connection conn = dbConnection.getConnection();
            PreparedStatement stm = conn.prepareStatement(insertQuery);
            if(!isUnique(this.txt_nazwaK.getText(), checkUnique)) {
            	setAlertError("Niewlaściwe dane", "Nazwa kraju jest juz zajeta!");
	              txt_nazwaK.setText(null);
	              return;
			}
            else if(!isName(this.txt_nazwaK.getText()) || !isValidLen(this.txt_nazwaK.getText())) {
            	setAlertError("Niewlaściwe dane", "Nazwa nie jest poprawna(ma zla dlugosc)!");
	              txt_nazwaK.setText(null);
	              return;
			}
            else if(this.dataK.getValue() == null) {
				setAlertError("Niepoprawny wybór!", "Data jest pusta!");
				return;
			}
            else if(!isPositiveInteger(this.txt_populacjaK.getText())) {
				setAlertError("Niewlaściwe dane", "Populacja nie jest poprawna(populacja > 0 i jest liczba naturalna)!");
				txt_populacjaK.setText(null);
				return;
			}
            else if(!isISO(this.txt_isoK.getText())) {
				setAlertError("Niewlaściwe dane", "ISO nie jest poprawne([A-Z][A-Z][A-Z]?)!");
				txt_ocenaM.setText(null);
				return;
			}
			stm.setString(1, String.valueOf(CRUDController.this.txt_nazwaK.getText()));
			stm.setString(2, String.valueOf(CRUDController.this.txt_isoK.getText()));
			stm.setString(3, String.valueOf(CRUDController.this.txt_populacjaK.getText()));
			stm.setString(4, String.valueOf(CRUDController.this.dataK.getEditor().getText()));
			stm.execute();
			btn_zaladujKraj.fire();
			stm.close();
			conn.close();
        } catch (SQLException  | NullPointerException e) { }
	}
	
	@FXML
	public void deleteKraj(ActionEvent actionEvent) {
		String deleteQuery = "DELETE FROM kraj WHERE id =";
		Kraj getSelectedRow = tv_kraj.getSelectionModel().getSelectedItem();
		try {
			Connection conn = dbConnection.getConnection();
			if(!getSelectedRow.toString().equals("")) {
				String id = getSelectedRow.getId();
				deleteQuery += id + "; ";
				Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
	            alert.setTitle("Potwierdzenie usunięcia");
	            alert.setHeaderText("Chcesz usunąć konto?");
	            alert.setContentText("" + getSelectedRow.getNazwa());
	            Optional<ButtonType> result = alert.showAndWait();
	            if (result.get() == ButtonType.OK) {
	            	PreparedStatement stm = conn.prepareStatement(deleteQuery);
	            	stm.execute();	 
	            	btn_zaladujKraj.fire();
	            	stm.close();
	            }      
			}
			else { throw new NullPointerException(); }
			conn.close();
		} catch(SQLException | NullPointerException e){ }
	}
	
	@FXML 
	public void editKraj(ActionEvent actionEvent) {
		String updateQuery = "UPDATE kraj SET nazwa = ?, iso = ?, populacja = ?, data_zalozenia = ? "
							+" WHERE id = ?; ";
		try { //Sprawdzenie, czy kraj o nowowprowadzonej nazwie(unique) istnieje + pozwolenie edycji (dlatego query walidujace ma id != swoje id)
			String s = this.txt_nazwaK.getText(); //unikalna wartosc tabeli zawodnik
			String id = this.txt_idK.getText(); //primary key tabeli zawodnik
			String checkUnique = "SELECT * FROM kraj WHERE nazwa = ? AND id != ?;";
			if (isUniqueOnEdit(s,id, checkUnique) >= 1) {
				setAlertError("Niewlaściwe dane", "Kraj o takiej nazwie jest juz w bazie!");
				txt_nazwaK.setText(null);
				return;
			} 
		}catch(SQLException e) {}
		try {
			Connection conn = dbConnection.getConnection();
			PreparedStatement stm = conn.prepareStatement(updateQuery);
            if(!isName(this.txt_nazwaK.getText()) || !isValidLen(this.txt_nazwaK.getText())) {
            	setAlertError("Niewlaściwe dane", "Nazwa kraju jest poprawna(ma zla dlugosc)!");
	              txt_nazwaK.setText(null);
	              return;
			}
            else if(this.dataK.getValue() == null) {
				setAlertError("Niepoprawny wybór!", "Data jest pusta!");
				return;
			}
            else if(!isPositiveInteger(this.txt_populacjaK.getText())) {
				setAlertError("Niewlaściwe dane", "Populacja nie jest poprawna(populacja > 0 i jest liczba naturalna)!");
				txt_populacjaK.setText(null);
				return;
			}
            else if(!isISO(this.txt_isoK.getText())) {
				setAlertError("Niewlaściwe dane", "ISO nie jest poprawne([A-Z][A-Z][A-Z]?)!");
				txt_ocenaM.setText(null);
				return;
			}
			stm.setString(1, String.valueOf(CRUDController.this.txt_nazwaK.getText()));
			stm.setString(2, String.valueOf(CRUDController.this.txt_isoK.getText()));
			stm.setString(3, String.valueOf(CRUDController.this.txt_populacjaK.getText()));
			stm.setString(4, String.valueOf(CRUDController.this.dataK.getEditor().getText()));
			stm.setString(5,  String.valueOf(CRUDController.this.txt_idK.getText()));
			stm.execute();
			btn_zaladujKraj.fire();	
			stm.close();
			conn.close();
		} catch(SQLException | NullPointerException e) { }
	}
	
	//======================PREFEROWANA MAPA======================//
	@FXML
	public void loadPrefmapaCombo() { //zaladowanie map(nazwa) i zawodnika(pseudonim) do comboboxow
		String queryZawodnik = "SELECT * FROM zawodnik;";
		String queryMapa = "SELECT * FROM mapa;";
		combo_zawodnik.setItems(null); // <- zerujemy comboboxy (inaczej by sie "stackowaly")
		combo_mapa.setItems(null);
		zawodnikCombo.clear();
		mapaCombo.clear();
		try {
			Connection conn = dbConnection.getConnection();
			ResultSet rs = conn.prepareStatement(queryZawodnik).executeQuery();
			while(rs.next()){
				String zawodnikName = rs.getString("pseudonim");
				zawodnikCombo.add(zawodnikName);
			}
			combo_zawodnik.setItems(zawodnikCombo);

			ResultSet ps = conn.prepareStatement(queryMapa).executeQuery();			
			while(ps.next()) {
				String mapaName = ps.getString("nazwa");
				mapaCombo.add(mapaName);
			}
			combo_mapa.setItems(mapaCombo);
			conn.close();	
		} catch(SQLException | NullPointerException e) { }
	}
	
	@FXML
	public void loadPrefmapa(ActionEvent actionEvent) {
		String query = "SELECT mapa.nazwa, zawodnik.pseudonim FROM preferowana_mapa JOIN zawodnik ON zawodnik.id = preferowana_mapa.zawodnik_id JOIN mapa ON mapa.id = preferowana_mapa.mapa_id;";
		try {
			Connection conn = dbConnection.getConnection();
			this.prefmapaList = FXCollections.observableArrayList();
			ResultSet rs = conn.createStatement().executeQuery(query);
			while(rs.next()) {
				this.prefmapaList.add(new PrefMapa (
						rs.getString(1),
						rs.getString(2)));
			}
			conn.close();
		} catch (SQLException | NullPointerException e) { }
		this.tc_zawodnik.setCellValueFactory(new PropertyValueFactory<PrefMapa, String>("zawodnik"));
		this.tc_mapa.setCellValueFactory(new PropertyValueFactory<PrefMapa, String>("mapa"));
		this.tv_prefmapa.setItems(null);
		this.tv_prefmapa.setItems(this.prefmapaList);
	}
	
	@FXML
	public void clearPrefMapa(ActionEvent actionEvent) {
		combo_zawodnik.setValue(null);
		combo_mapa.setValue(null);
	}
	
	@FXML
	public void addPrefmapa(ActionEvent actionEvent) {
		String zawodnik = "", mapa = ""; String zawodnikID = "", mapaID = "";
		String zawodnikQuery = "SELECT id FROM zawodnik WHERE pseudonim = ? ; ";
		String mapaQuery = "SELECT id FROM mapa WHERE nazwa = ? ; ";
		String insertQuery = "INSERT INTO preferowana_mapa(mapa_id, zawodnik_id) VALUES(?, ?);";
		//String uniqueMapaID = "SELECT mapa_id FROM preferowana_mapa WHERE mapa_id = ? ;";
		//String uniqueZawodnikID = "SELECT zawodnik_id FROM preferowana_mapa WHERE zawodnik_id = ? ;";
		try {
			Connection conn = dbConnection.getConnection();
			PreparedStatement stm = conn.prepareStatement(insertQuery);
			if(combo_zawodnik.getValue() == null || combo_mapa.getValue() == null) {
				setAlertError("Niewlaściwy wybór!", "Wybierz wszystkie dane (zawodnik i mapa)!");
	            return;
			} else {
				mapa = combo_mapa.getValue().toString();
				zawodnik = combo_zawodnik.getValue().toString();
				try { //Sprawdzenie, czy prefmapa o nowowprowadzonych mapa_id i zawodnik_id istnieje
					int helpM = findID(mapa, mapaQuery);
					int helpZ = findID(zawodnik, zawodnikQuery);
					String checkUnique1 = "SELECT mapa_id, zawodnik_id FROM preferowana_mapa WHERE mapa_id = ? AND zawodnik_id = ?;";
					String checkUnique2 = "SELECT DISTINCT mapa_id, zawodnik_id FROM preferowana_mapa WHERE mapa_id = ? AND zawodnik_id = ?;";
					if ((isUniqueOnEdit(helpM +"",helpZ+"", checkUnique1) == 1  && isUniqueOnEdit(helpM + "", helpZ + "", checkUnique2) == 1 )|| (isUniqueOnEdit(helpM +"",helpZ+"", checkUnique1) != isUniqueOnEdit(helpM + "", helpZ + "", checkUnique2))) {
						
						setAlertError("Niewlaściwe dane", "Taka preferencja mapy zawodnika już istnieje!");
						return;
					} 
				}catch(SQLException e) {}
			}
			try{
				PreparedStatement h1 = dbConnection.getConnection().prepareStatement(zawodnikQuery);
				h1.setString(1, zawodnik);
				ResultSet rs1 = h1.executeQuery();
				while(rs1.next()) {
					zawodnikID = rs1.getString(1);
				}
				PreparedStatement h2 = dbConnection.getConnection().prepareStatement(mapaQuery);
				h2.setString(1,mapa);
				ResultSet rs2 = h2.executeQuery();
				while(rs2.next()) {
					mapaID = rs2.getString(1);
				}
				
			} catch(SQLException | NullPointerException e) { }
			
			stm.setString(1,  mapaID);
			stm.setString(2,  zawodnikID);
			stm.execute();
			btn_zaladujPM.fire();	
			stm.close();
			conn.close();
			
		} catch(SQLException | NullPointerException e) { }
	}
	
	@FXML
	public void deletePrefmapa(ActionEvent actionEvent) {
		String deleteQuery = "DELETE FROM preferowana_mapa WHERE zawodnik_id = ? AND mapa_id = ?";
		String mapaID = "", zawodnikID = "";
		String zawodnikQuery = "SELECT id FROM zawodnik WHERE pseudonim = ? ; ";
		String mapaQuery = "SELECT id FROM mapa WHERE nazwa = ? ; "; 
		try {
			Connection conn = dbConnection.getConnection();
			PrefMapa selectedR = tv_prefmapa.getSelectionModel().getSelectedItem();
			String z = selectedR.getZawodnik();
			String m = selectedR.getMapa();
			try{
				PreparedStatement h1 = dbConnection.getConnection().prepareStatement(zawodnikQuery);
				h1.setString(1,z );
				ResultSet rs1 = h1.executeQuery();
				while(rs1.next()) {
					zawodnikID = rs1.getString(1);
				}
				PreparedStatement h2 = dbConnection.getConnection().prepareStatement(mapaQuery);
				h2.setString(1,m);
				ResultSet rs2 = h2.executeQuery();
				while(rs2.next()) {
					mapaID = rs2.getString(1);
				}	
			} catch(SQLException | NullPointerException e) { }	
			Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
			alert.setTitle("Potwierdzenie usunięcia");
			alert.setHeaderText("Chcesz usunąć preferowana mape zawodnika?");
			alert.setContentText("" + selectedR.getMapa() + " " + selectedR.getZawodnik());
			Optional<ButtonType> result = alert.showAndWait();
			if (result.get() == ButtonType.OK) {
				PreparedStatement stm = conn.prepareStatement(deleteQuery);
				stm.setString(1, zawodnikID);
				stm.setString(2, mapaID);
				stm.execute();	 
				btn_zaladujPM.fire();
				stm.close();
			}
			conn.close();
		} catch(SQLException | NullPointerException e){ }
	}
	
	@FXML
	public void editPrefmapa(ActionEvent actionEvent) {
		if(combo_zawodnik.getValue() == null || combo_mapa.getValue() == null) {
			setAlertError("Niewlaściwy wybór!", "Wybierz wszystkie dane (zawodnik i mapa)!");
            return;
		}
			
		else {
			String zawodnikQuery = "SELECT id FROM zawodnik WHERE pseudonim = ? ; ";
			String mapaQuery = "SELECT id FROM mapa WHERE nazwa = ? ; "; 
			String uniqueMapaID = "SELECT mapa_id FROM preferowana_mapa WHERE mapa_id = ? ;";
			String uniqueZawodnikID = "SELECT zawodnik_id FROM preferowana_mapa WHERE zawodnik_id = ? ;";
			try {
				String mapaH = combo_mapa.getValue().toString();
				String zawodnikH = combo_zawodnik.getValue().toString();
				try { //Sprawdzenie, czy prefmapa o nowowprowadzonych mapa_id i zawodnik_id istnieje
					int helpM = findID(mapaH, mapaQuery);
					int helpZ = findID(zawodnikH, zawodnikQuery);
					String checkUnique1 = "SELECT mapa_id, zawodnik_id FROM preferowana_mapa WHERE mapa_id = ? AND zawodnik_id = ?;";
					String checkUnique2 = "SELECT DISTINCT mapa_id, zawodnik_id FROM preferowana_mapa WHERE mapa_id = ? AND zawodnik_id = ?;";
					if ((isUniqueOnEdit(helpM +"",helpZ+"", checkUnique1) == 1  && isUniqueOnEdit(helpM + "", helpZ + "", checkUnique2) == 1 )|| (isUniqueOnEdit(helpM +"",helpZ+"", checkUnique1) != isUniqueOnEdit(helpM + "", helpZ + "", checkUnique2))) {
						
						setAlertError("Niewlaściwe dane", "Taka preferencja mapy zawodnika już istnieje!");
						return;
					} 
				}catch(SQLException e) {}
				Connection conn = dbConnection.getConnection();
				PrefMapa selectedR = tv_prefmapa.getSelectionModel().getSelectedItem();
				String selectedMapa = selectedR.getMapa();
				String selectedZawodnik = selectedR.getZawodnik();
				String mapa = String.valueOf(CRUDController.this.combo_mapa.getValue());
				String zawodnik = String.valueOf(CRUDController.this.combo_zawodnik.getValue());
				int selectedM = findID(selectedMapa, mapaQuery);
				int selectedZ = findID(selectedZawodnik, zawodnikQuery);
				int chosenM = findID(mapa, mapaQuery);
				int chosenZ = findID(zawodnik, zawodnikQuery);
				String queryUpdate = "UPDATE preferowana_mapa SET mapa_id = " + chosenM + ", zawodnik_id = " + chosenZ + " WHERE mapa_id = " + selectedM + " AND zawodnik_id = " + selectedZ + ";";
				PreparedStatement stm = dbConnection.getConnection().prepareStatement(queryUpdate);
				stm.execute();
				btn_zaladujPM.fire();	
				stm.close();
				conn.close();
			} catch(SQLException | NullPointerException e){ }
		}
	}
	
	//======================DRUZYNA======================//
	
	@FXML
	public void loadDruzyna(ActionEvent actionEvent) {
		String query = "SELECT * FROM druzyna;";
		try {
			Connection conn = dbConnection.getConnection();
			this.druzynaList = FXCollections.observableArrayList();
			ResultSet rs = conn.createStatement().executeQuery(query);
			while(rs.next()) { this.druzynaList.add(new Druzyna (
						rs.getString(1),
						rs.getString(2),
						rs.getString(3),
						rs.getString(4),
						rs.getString(5)));
			}
			conn.close();
		} catch (SQLException | NullPointerException e) { }
		this.tc_nazwaD.setCellValueFactory(new PropertyValueFactory<Druzyna, String>("nazwa"));
		this.tc_dataD.setCellValueFactory(new PropertyValueFactory<Druzyna, String>("data"));
		this.tc_liczbaczlonkowD.setCellValueFactory(new PropertyValueFactory<Druzyna, String>("liczbaCzlonkow"));
		this.tc_sponsorD.setCellValueFactory(new PropertyValueFactory<Druzyna, String>("sponsor"));
		this.tv_druzyna.setItems(null);
		this.tv_druzyna.setItems(this.druzynaList);
	}
	
	@FXML
	public void clearDruzyna(ActionEvent actionEvent) {
		txt_idD.setText(null);
		txt_nazwaD.setText(null);
		dataD.setValue(null);
		txt_liczbaczlonkowD.setText(null);
		txt_sponsorD.setText(null);
	}
	
	@FXML
	public void addDruzyna(ActionEvent actionEvent) {
		String insertQuery = "INSERT INTO druzyna(nazwa, data_utworzenia, liczba_czlonkow, sponsor)" +
				" VALUES(?,?,?,?);";
		String checkUnique = "SELECT * FROM druzyna WHERE nazwa = ? ;";
        try {
            Connection conn = dbConnection.getConnection();
            PreparedStatement stm = conn.prepareStatement(insertQuery);
			if(!isValidLen(this.txt_nazwaD.getText())) {
				setAlertError("Niewlaściwe dane", "Nazwa druzyny nie jest poprawna(ma zla dlugosc)!");
				txt_nazwaD.setText(null);
				return;
			}
			else if(!isUnique(this.txt_nazwaD.getText() ,checkUnique)) {
				setAlertError("Niewlaściwe dane", "Druzyna o takiej nazwie juz istnieje!");
            	txt_nazwaD.setText(null);
            	return;
            }
			else if(this.dataD.getValue() == null) {
				setAlertError("Niepoprawny wybór!", "Data jest pusta!");
				return;
			}
			else if(!isPositiveInteger(this.txt_liczbaczlonkowD.getText())) {
				setAlertError("Niewlaściwe dane", "Liczba czlonkow nie jest poprawna( > 0 i jest liczba naturalna)!");
				return;
			}
			else if(!isName(this.txt_sponsorD.getText()) || !isValidLen(this.tc_sponsorD.getText())) {
				setAlertError("Niewlaściwe dane", "Sponsor nie jest poprawny(ma zla dlugosc lub zawiera niepoprawne znaki)!");
				txt_sponsorD.setText(null);
				return;
			} else
			stm.setString(1, String.valueOf(CRUDController.this.txt_nazwaD.getText()));
			stm.setString(2, String.valueOf(CRUDController.this.dataD.getEditor().getText()));
			stm.setString(3, String.valueOf(CRUDController.this.txt_liczbaczlonkowD.getText()));
			stm.setString(4, String.valueOf(CRUDController.this.txt_sponsorD.getText()));
			stm.execute();
			btn_zaladujDruzyna.fire();
			stm.close();
			conn.close();
        } catch (SQLException  | NullPointerException e) { }
	}
	
	@FXML
	public void deleteDruzyna(ActionEvent actionEvent) {
		String deleteQuery = "DELETE FROM druzyna WHERE id =";
		Druzyna getSelectedRow = tv_druzyna.getSelectionModel().getSelectedItem();
		try {
			Connection conn = dbConnection.getConnection();
			if(!getSelectedRow.toString().equals("")) {
				String id = getSelectedRow.getId();
				deleteQuery += id + "; ";
				Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
				alert.setTitle("Potwierdzenie usunięcia");
	            alert.setHeaderText("Chcesz usunąć druzyne?");
	            alert.setContentText("" + getSelectedRow.getNazwa());
	            Optional<ButtonType> result = alert.showAndWait();
	            if (result.get() == ButtonType.OK) {
	            	PreparedStatement stm = conn.prepareStatement(deleteQuery);
	            	stm.execute();	 
	            	btn_zaladujDruzyna.fire();
	            	stm.close();
	            }      
			} else { throw new NullPointerException();}
			conn.close();
		} catch(SQLException | NullPointerException e){ }
	}
	
	@FXML 
	public void editDruzyna(ActionEvent actionEvent) {
		String updateQuery = "UPDATE druzyna SET nazwa = ?, data_utworzenia = ?, liczba_czlonkow = ?, sponsor = ? "
							+" WHERE id = ?; ";
		try { //Sprawdzenie, czy druzyna o nowowprowadzonej nazwie(unique) istnieje + pozwolenie edycji (dlatego query walidujace ma id != swoje id)
			String s = this.txt_nazwaD.getText(); //unikalna wartosc tabeli druzyna
			String id = this.txt_idD.getText(); //primary key tabeli druzyna
			String checkUnique = "SELECT * FROM druzyna WHERE nazwa = ? AND id != ?;";
			if (isUniqueOnEdit(s,id, checkUnique) >= 1) {
				setAlertError("Niewlaściwe dane", "Druzyna o takiej nazwie jest juz w bazie!");
				txt_nazwaD.setText(null);
				return;
			} 
		}catch(SQLException e) {}
		try {
			Connection conn = dbConnection.getConnection();
			PreparedStatement stm = conn.prepareStatement(updateQuery);
			if(!isValidLen(this.txt_nazwaD.getText())) {
				setAlertError("Niewlaściwe dane", "Nazwa druzyny nie jest poprawna(ma zla dlugosc)!");
				txt_nazwaD.setText(null);
				return;
			}
			else if(this.dataD.getValue() == null) {
				setAlertError("Niewlaściwy wybór!", "Data jest pusta!");
				return;
			}
			else if(!isPositiveInteger(this.txt_liczbaczlonkowD.getText())) {
				setAlertError("Niewlaściwe dane", "Liczba czlonkow nie jest poprawna( > 0 i jest liczba naturalna)!");
				return;
			}
			else if(!isName(this.txt_sponsorD.getText()) || !isValidLen(this.tc_sponsorD.getText())) {
				setAlertError("Niewlaściwe dane", "Sponsor nie jest poprawny(ma zla dlugosc lub zawiera niepoprawne znaki)!");
				txt_sponsorD.setText(null);
				return;
			} else
			stm.setString(1, String.valueOf(CRUDController.this.txt_nazwaD.getText()));
			stm.setString(2, String.valueOf(CRUDController.this.dataD.getEditor().getText()));
			stm.setString(3, String.valueOf(CRUDController.this.txt_liczbaczlonkowD.getText()));
			stm.setString(4, String.valueOf(CRUDController.this.txt_sponsorD.getText()));
			stm.setString(5, String.valueOf(CRUDController.this.txt_idD.getText()));
			stm.execute();
			btn_zaladujDruzyna.fire();	
			stm.close();
			conn.close();
		} catch(SQLException | NullPointerException e) { }
	}
	
	//Funkcje pomocnicze (stricte SQLowe)
	public void setAlertError(String type, String mssg) {
		Alert alert = new Alert(Alert.AlertType.ERROR);
		alert.setTitle(type);
		alert.setHeaderText(mssg);
		alert.showAndWait();
		return;
	}
	public int findID(String s, String query) throws SQLException { 
		PreparedStatement h = dbConnection.getConnection().prepareStatement(query);
		String id = "";
		h.setString(1,s);
		ResultSet rs = h.executeQuery();
		while(rs.next()) {
			id = rs.getString(1);
		}
		int ret = Integer.parseInt(id);
		return ret;
	}
	
	public boolean isUnique(String s, String query) throws SQLException {
		PreparedStatement h = dbConnection.getConnection().prepareStatement(query);
		int counter = 0;
		h.setString(1, s);
		ResultSet rs = h.executeQuery();
		while(rs.next()) {
			counter++;
		}
		if(counter == 0)
			return true;
		else
			return false;
	}
	
	public final int isUniqueOnEdit(String s, String d, String query) throws SQLException {
		PreparedStatement h = dbConnection.getConnection().prepareStatement(query);
		int counter = 0;
		h.setString(1, s);
		h.setString(2, d);
		ResultSet rs = h.executeQuery();
		while(rs.next()) {
			counter++;
		}
		return counter;
	}
	
	//Funkcje walidujące (stricte porównania stringow do patternu)
    public final boolean isName(String s) {
    	return s.matches("[A-ZĄĆĘŁŃÓŚŹĆŻ][a-ząćęłńóśźż]{1,}[\\s\\.-]{0,1}[A-Za-ząćęłńóśźżĄĆĘŁŃÓŚŹĆŻ\\s]{0,}$");
    	//[A-ZĄĆĘŁŃÓŚŹĆŻ][a-ząćęłńóśźż]{1,}[\\s\\.-]{0,1}[A-Za-ząćęłńóśźżĄĆĘŁŃÓŚŹĆŻ\\s]{0,}$
    	//^[A-Za-ząćęłńóśźżĄĆĘŁŃÓŚŹĆŻ.\\s]{1,}[\\.-]{0,1}[A-Za-ząćęłńóśźżĄĆĘŁŃÓŚŹĆŻ\\s]{0,}$"
    }
    
    public final boolean isValidLen(String s) {
    	if(s.length() >= 25 || s.length() <= 0) {
    		return false;
    	} return true;
    }
    
    public final boolean isISO(String s) {
    	return s.matches("[A-Z]{2,3}");
    }
    
    public final boolean isMoney(String s) {
    	return s.matches("[0-9]{1,8}(.[0-9]{1,2})?"); //lub zamiast {1,8} dac +
    }
    
    public final boolean isPositiveInteger(String s) {
    	return s.matches("[1-9][0-9]*");
    }
    
    public final boolean isValidRating(String s) {
    	if(s.matches("0[1-9]") || s.matches("10") || s.matches("[0-9]")) {
    		return true;
    	} return false;
    }
}
    
