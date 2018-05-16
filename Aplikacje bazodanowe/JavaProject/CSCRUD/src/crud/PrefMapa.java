package crud;

//Created by Jan Bienias 26.05.17

import javafx.beans.property.*;

public class PrefMapa {
	private final SimpleStringProperty mapa;
	private final SimpleStringProperty zawodnik;
	
	public PrefMapa(String mapa, String zawodnik) {
		this.mapa = new SimpleStringProperty(mapa);
		this.zawodnik = new SimpleStringProperty(zawodnik);
	}
	
	public String getMapa() {
		return mapa.get();
	}
	
	public StringProperty MapaProperty() {
		return mapa;
	}
	
	public void setMapa(String mapa) {
		this.mapa.set(mapa);
	}
	
	public String getZawodnik() {
		return zawodnik.get();
	}
	
	public StringProperty ZawodnikProperty() {
		return zawodnik;
	}
	
	public void setZawodnik(String zawodnik) {
		this.zawodnik.set(zawodnik);
	}

}
