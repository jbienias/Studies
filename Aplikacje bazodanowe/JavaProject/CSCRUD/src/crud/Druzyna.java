package crud;

//Created by Jan Bienias 26.05.17

import javafx.beans.property.*;

public class Druzyna {
	private final SimpleStringProperty id;
	private final SimpleStringProperty nazwa;
	private final SimpleStringProperty data;
	private final SimpleStringProperty liczbaCzlonkow;
	private final SimpleStringProperty sponsor;
	
	public Druzyna(String id, String nazwa, String data, String liczbaCzlonkow, String sponsor) {
		this.id = new SimpleStringProperty(id);
		this.nazwa = new SimpleStringProperty(nazwa);
		this.data = new SimpleStringProperty(data);
		this.liczbaCzlonkow = new SimpleStringProperty(liczbaCzlonkow);
		this.sponsor = new SimpleStringProperty(sponsor);
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
	
	public void setData(String data) {
		this.data.set(data);
	}
	
	public String getLiczbaCzlonkow() {
		return liczbaCzlonkow.get();
	}
	
	public StringProperty LiczbaCzlonkowProperty() {
		return liczbaCzlonkow;
	}
	
	public void setLiczbaCzlonkow(String liczba) {
		this.liczbaCzlonkow.set(liczba);
	}
	
	public String getSponsor() {
		return sponsor.get();
	}
	
	public StringProperty SponsorProperty() {
		return sponsor;
	}
	
	public void setSponsor(String sponsor) {
		this.sponsor.set(sponsor);
	}
}
