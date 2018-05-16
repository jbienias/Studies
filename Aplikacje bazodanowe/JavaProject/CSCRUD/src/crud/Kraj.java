package crud;

//Created by Jan Bienias 26.05.17

import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;

public class Kraj {
	
	private final SimpleStringProperty id;
	private final SimpleStringProperty nazwa;
	private final SimpleStringProperty iso;
	private final SimpleStringProperty populacja;
	private final SimpleStringProperty data;
	
	public Kraj(String id, String nazwa, String iso, String populacja, String data_zalozenia){
		this.id = new SimpleStringProperty(id);
		this.nazwa = new SimpleStringProperty(nazwa);
		this.iso = new SimpleStringProperty(iso);
		this.populacja = new SimpleStringProperty(populacja);
		this.data = new SimpleStringProperty(data_zalozenia);
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
	
	public String getIso() {
		return iso.get();
	}
	
	public StringProperty IsoProperty() {
		return iso;
	}
	
	public void setIso(String iso) {
		this.iso.set(iso);
	}
	
	public String getPopulacja() {
		return populacja.get();
	}
	
	public StringProperty PopulacjaProperty() {
		return populacja;
	}
	
	public void setPopulacja(String pop) {
		this.populacja.set(pop);
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
}
