package crud;

//Created by Jan Bienias 26.05.17

import javafx.beans.property.*;

public class Mapa {
	private final SimpleStringProperty id;
	private final SimpleStringProperty nazwa;
	private final SimpleStringProperty data;
	private final SimpleStringProperty rozmiar;
	private final SimpleStringProperty ocena;
	
	public Mapa(String id, String nazwa, String data, String rozmiar, String ocena) {
		this.id = new SimpleStringProperty(id);
		this.nazwa = new SimpleStringProperty(nazwa);
		this.data = new SimpleStringProperty(data);
		this.rozmiar = new SimpleStringProperty(rozmiar);
		this.ocena = new SimpleStringProperty(ocena);
	}
	
	public String getId() {
		return id.get();
	}
	
	public StringProperty IdProperty() {
		return id;
	}
	
	public void setId(String ID) {
		this.id.set(ID);
	}
	
	public String getNazwa() {
		return nazwa.get();
	}
	
	public StringProperty NazwaProperty() {
		return nazwa;
	}
	
	public void setNazwa(String nazwa) {
		this.nazwa.set(nazwa);
	}
	public String getData() {
		return data.get();
	}
	
	public StringProperty DataProperty() {
		return data;
	}
	
	public void setData(String rozmiar) {
		this.data.set(rozmiar);
	}
	
	public String getRozmiar() {
		return rozmiar.get();
	}
	
	public StringProperty RozmiarProperty() {
		return rozmiar;
	}
	
	public void setRozmiar(String rozmiar) {
		this.rozmiar.set(rozmiar);
	}
	
	public String getOcena() {
		return ocena.get();
	}
	
	public StringProperty OcenaProperty() {
		return ocena;
	}
	
	public void setOcena(String ocena) {
		this.ocena.set(ocena);
	}
}
