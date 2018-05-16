package crud;

//Created by Jan Bienias 26.05.17

import javafx.beans.property.*;

public class Zawodnik {
	private final SimpleStringProperty id;
	private final SimpleStringProperty imie;
	private final SimpleStringProperty nazwisko;
	private final SimpleStringProperty pseudonim;
	private final SimpleStringProperty stawka;
	private final SimpleStringProperty druzyna_id;
	private final SimpleStringProperty kraj_id;
	
	public Zawodnik(String id, String imie, String nazwisko, String pseudonim, String stawka, String druzyna_id, String kraj_id){
		this.id = new SimpleStringProperty(id);
		this.imie = new SimpleStringProperty(imie);
		this.nazwisko = new SimpleStringProperty(nazwisko);
		this.pseudonim = new SimpleStringProperty(pseudonim);
		this.stawka = new SimpleStringProperty(stawka);
		this.druzyna_id = new SimpleStringProperty(druzyna_id);
		this.kraj_id = new SimpleStringProperty(kraj_id);
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
	
	public String getImie() {
		return imie.get();
	}
	
	public StringProperty imieProperty() {
		return imie;
	}
	
	public void setImie(String imie) {
		this.imie.set(imie);
	}
	
	public String getNazwisko() {
		return nazwisko.get();
	}
	
	public StringProperty nazwiskoProperty() {
		return nazwisko;
	}
	
	public void setNazwisko(String nazwisko) {
		this.nazwisko.set(nazwisko);
	}
	
	public String getPseudonim() {
		return pseudonim.get();
	}
	
	public StringProperty pseudonimProperty() {
		return pseudonim;
	}
	
	public void setPseudonim(String pseudonim) {
		this.pseudonim.set(pseudonim);
	}
	
	public String getStawka() {
		return stawka.get();
	}
	
	public StringProperty stawkaProperty() {
		return stawka;
	}
	
	public void setStawka(String stawka) {
		this.stawka.set(stawka);
	}
	
	public String getDruzyna_id() {
		return druzyna_id.get();
	}
	
	public StringProperty druzyna_idProperty() {
		return druzyna_id;
	}
	
	public void setDruzyna(String druzyna) {
		this.druzyna_id.set(druzyna);
	}
	
	
	public String getKraj_id() {
		return kraj_id.get();
	}
	
	public StringProperty krajProperty() {
		return kraj_id;
	}
	
	public void setKraj_id(String kraj) {
		this.kraj_id.set(kraj);
	}
}
